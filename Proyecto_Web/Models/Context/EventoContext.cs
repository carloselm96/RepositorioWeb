using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models.Context
{
    public class EventoContext
    {
        public EventoContext(string cs)
        {
            ConnectionString = cs;
        }
        public string ConnectionString { get; set; }

        public List<Evento> getEventos()
        {
            string cmdText = "select * from evento where Status='A' ";
            //Id_evento, Nombre_evento, Fecha_ini, Fecha_final, FK_imagen, Status
            List<Evento> results = new List<Evento>();
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            MySqlCommand command = new MySqlCommand(cmdText, my);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Evento evento = new Evento();
                evento.id = reader.GetInt16("Id_evento");
                evento.nombre = reader.GetString("Nombre_evento");
                evento.fecha_inicio = reader.GetDateTime("Fecha_ini");
                evento.fecha_final = reader.GetDateTime("Fecha_final");
                results.Add(evento);
            }
            command.Dispose();
            my.Close();
            return results;
        }

        public bool Add(String inputNombre, string inputInicio, string inputFinal, string imagePath)
        {

            string cmdText1 = "Insert into Imagenes(URL) value(@url)";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            bool result = false;
            using (MySqlCommand command = new MySqlCommand(cmdText1, my))
            {
                command.Parameters.Add(new MySqlParameter("url", imagePath));                
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            int img = this.lastInserted();
            string cmdText = "pro_in_evento(@nom,@ini,@fin,@img)";            
            my.Open();            
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("nom", inputNombre));
                command.Parameters.Add(new MySqlParameter("ini", inputInicio));
                command.Parameters.Add(new MySqlParameter("fin", inputFinal));
                command.Parameters.Add(new MySqlParameter("img", img));
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            return result;
        }

        public Evento detallesEvento(int id)
        {
            Evento evento = new Evento();

            string cmdText = "select * from evento where Id_evento=@id;";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();


            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("@id", id));

                var reader = command.ExecuteReader();
                reader.Read();

                if (reader.HasRows)
                {
                    evento.id = reader.GetInt16("Id_evento");
                    evento.nombre = reader.GetString("Nombre_evento");
                    evento.fecha_inicio = reader.GetDateTime("Fecha_ini");
                    evento.fecha_final = reader.GetDateTime("Fecha_final");
                }
            }                        
            my.Close();
            return evento;
        }

        public bool Edit(int id,string inputNombre, string inputInicio, string inputFinal)
        {
            string cmdText = "UPDATE evento SET Nombre_evento=@nom, Fecha_ini=@ini, Fecha_final=@fin WHERE Id_evento=@id";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            bool result = false;
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("nom", inputNombre));
                command.Parameters.Add(new MySqlParameter("ini", inputInicio));
                command.Parameters.Add(new MySqlParameter("fin", inputFinal));
                command.Parameters.Add(new MySqlParameter("id", id));
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            return result;
        }

        public int lastInserted()
        {
            string cmdText = "SELECT LAST_INSERT_ID() as id FROM imagenes;";
            int id = -1;
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            MySqlCommand command = new MySqlCommand(cmdText, my);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt16("id");
            }
            command.Dispose();
            my.Close();
            return id;
        }

    }
}
