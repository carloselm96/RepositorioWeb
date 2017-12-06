using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models.Context
{
    public class NoticiaContext
    {
        public NoticiaContext(string cs)
        {
            ConnectionString = cs;
        }

        public string ConnectionString { get; set; }

        public List<Noticia> getNoticias()
        {
            List<Noticia> results = new List<Noticia>();
            string cmdText = "select distinct * from consulta_noticias";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            MySqlCommand command = new MySqlCommand(cmdText, my);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Noticia noticia = new Noticia();
                noticia.id = reader.GetInt16("Id_noticias");
                noticia.titulo = reader.GetString("Titulo");
                noticia.fecha = reader.GetString("Fecha");
                noticia.noticia = reader.GetString("Noticia");
                noticia.evento = new Evento();
                noticia.evento.id = reader.GetInt16("FK_evento");
                noticia.evento.nombre = reader.GetString("Nombre_evento");
                noticia.imagen = reader.GetString("imagen");
                results.Add(noticia);
            }
            command.Dispose();
            my.Close();
            return results;
        }

        public bool nuevaNoticia(string titulo,string fecha, string noticia, int evento, string imagePath)
        {
            string cmdText = "pro_in_noticias(@titulo,@fecha,@noticia,@evento)";                        
            MySqlConnection my = new MySqlConnection(ConnectionString);
            bool result=false;
            my.Open();            
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("titulo", titulo));
                command.Parameters.Add(new MySqlParameter("fecha", fecha));
                command.Parameters.Add(new MySqlParameter("noticia", noticia));
                command.Parameters.Add(new MySqlParameter("evento", evento));                
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            int last = lastInserted();
            string cmdText1 = "Insert into Imagenes(URL,FK_noticia) value(@url,@last)";            
            my.Open();            
            using (MySqlCommand command = new MySqlCommand(cmdText1, my))
            {
                command.Parameters.Add(new MySqlParameter("url", imagePath));
                command.Parameters.Add(new MySqlParameter("last", last));
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            return result;
        }

        public bool editarNoticia(string titulo, string fecha, string noticia, int evento, int id)
        {
            string cmdText = "pro_upd_noticias(@titulo,@fecha,@noticia,@evento,@id)";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            bool result = false;
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("titulo", titulo));
                command.Parameters.Add(new MySqlParameter("fecha", fecha));
                command.Parameters.Add(new MySqlParameter("noticia", noticia));
                command.Parameters.Add(new MySqlParameter("evento", evento));
                command.Parameters.Add(new MySqlParameter("id", id));
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            return result;
        }

        public Noticia getNoticia(int id)
        {
            Noticia noticia = null;
            string cmdText = "select distinct * from consulta_noticias where Id_noticias=@id";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("@id", id));
                var reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    noticia = new Noticia();
                    noticia.id = reader.GetInt16("Id_noticias");
                    noticia.titulo = reader.GetString("Titulo");
                    noticia.fecha = reader.GetDateTime("Fecha").ToString("yyyy-MM-dd");
                    noticia.noticia = reader.GetString("Noticia");
                    noticia.evento = new Evento();
                    noticia.evento.id = reader.GetInt16("FK_evento");
                    noticia.evento.nombre = reader.GetString("Nombre_evento");
                }
            }
            return noticia;
        }
        public int lastInserted()
        {
            string cmdText = "SELECT MAX(Id_noticias) as id FROM noticias;";
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
