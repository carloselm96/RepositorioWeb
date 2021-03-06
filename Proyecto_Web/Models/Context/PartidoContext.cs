﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models.Context
{
    public class PartidoContext
    {
        public PartidoContext(string cs)
        {
            ConnectionString = cs;
        }
        public string ConnectionString { get; set; }

        public List<Partido> getPartidos()
        {
            List<Partido> result = new List<Partido>();
            string cmdText = "select * from consulta_partidos ";            
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            MySqlCommand command = new MySqlCommand(cmdText, my);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Partido partido = new Partido(); // Id_partido, Fecha, Hora, FK_disciplina, Nombre, contrincante1, contrincante2, Equipo1, Equipo2, Ubicacion
                partido.id = reader.GetInt16("Id_partido");
                partido.fecha = reader.GetDateTime("Fecha").ToString("dd-mm-yyyy");
                partido.hora = reader.GetString("Hora");
                partido.disciplina = new Disciplina();
                partido.disciplina.id = reader.GetInt16("FK_disciplina");
                partido.disciplina.nombre = reader.GetString("Nombre");
                partido.equipo1 = new Equipo();
                partido.equipo2 = new Equipo();
                partido.equipo1.id = reader.GetInt16("Equipo1");
                partido.equipo2.id = reader.GetInt16("Equipo2");
                partido.equipo1.nombre = reader.GetString("contrincante1");
                partido.equipo2.nombre = reader.GetString("contrincante2");
                partido.ubicacion = new Ubicacion();
                partido.ubicacion.nombre = reader.GetString("ubicacion");
                partido.ubicacion.longitud = reader.GetString("longitud");
                partido.ubicacion.latitud = reader.GetString("Latitud");
                partido.evento = new Evento();
                partido.evento.id = reader.GetInt16("Id_evento");
                partido.evento.nombre = reader.GetString("Nombre_evento");
                result.Add(partido);
            }
            command.Dispose();
            my.Close();
            return result;
        }

        public Partido GetPartido(string id)
        {
            Partido partido = new Partido();
            string cmdText = "select * from consulta_partidos where Id_partido=@id;";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();


            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("@id", id));

                var reader = command.ExecuteReader();
                reader.Read();

                if (reader.HasRows)
                {
                    partido.id = reader.GetInt16("Id_partido");
                    partido.fecha = reader.GetDateTime("Fecha").ToString("yyyy-MM-dd");
                    partido.hora = reader.GetString("Hora");
                    partido.disciplina = new Disciplina();
                    partido.disciplina.id = reader.GetInt16("FK_disciplina");
                    partido.disciplina.nombre = reader.GetString("Nombre");
                    partido.equipo1 = new Equipo();
                    partido.equipo2 = new Equipo();
                    partido.equipo1.id = reader.GetInt16("Equipo1");
                    partido.equipo2.id = reader.GetInt16("Equipo2");
                    partido.equipo1.nombre = reader.GetString("contrincante1");
                    partido.equipo2.nombre = reader.GetString("contrincante2");
                    partido.ubicacion = new Ubicacion();
                    partido.ubicacion.nombre = reader.GetString("ubicacion");
                    partido.ubicacion.longitud = reader.GetString("longitud");
                    partido.ubicacion.latitud = reader.GetString("Latitud");
                }
            }

            my.Close();
            return partido;
        }
        /*pro_in_partido(IN n_fecha date,IN n_hora time,
         * IN n_fkdisc INT ,IN n_equipo1 INT,IN n_equipo2 INT,IN n_ubicacion INT) */
        public bool nuevoPartido(string fecha,string hora, int disciplina, int e1, int e2, int ubic, int eve)
        {            
            string cmdText = "pro_in_partido(@fecha,@hora,@disciplina,@e1,@e2,@ubi,@eve)";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            bool result = false;
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("fecha", fecha));
                command.Parameters.Add(new MySqlParameter("hora", hora));
                command.Parameters.Add(new MySqlParameter("disciplina", disciplina));
                command.Parameters.Add(new MySqlParameter("e1", e1));
                command.Parameters.Add(new MySqlParameter("e2", e2));
                command.Parameters.Add(new MySqlParameter("ubi", ubic));
                command.Parameters.Add(new MySqlParameter("eve", eve));
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            return result;
        }

        public bool editPartido(int id,string fecha, string hora, int disciplina, int e1, int e2, int ubic, int eve)
        { //(@fecha,@hora,@disciplina,@e1,@e2,@ubi,@eve)
            string cmdText = "UPDATE partido SET" +
                "Fecha=@fecha, FK_disciplina=@disciplina, Equipo1=@e1, Equipo2=@e2, Ubicacion=@ubi, " +
                "FK_evento=@eve, Hora=@hora where Id_partido=@id";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            bool result = false;
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("fecha", fecha));
                command.Parameters.Add(new MySqlParameter("hora", hora));
                command.Parameters.Add(new MySqlParameter("disciplina", disciplina));
                command.Parameters.Add(new MySqlParameter("e1", e1));
                command.Parameters.Add(new MySqlParameter("e2", e2));
                command.Parameters.Add(new MySqlParameter("ubi", ubic));
                command.Parameters.Add(new MySqlParameter("eve", eve));
                command.Parameters.Add(new MySqlParameter("id", id));
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            return result;
        }

        public bool eliminarPartido(int id)
        {
            string cmdText = "UPDATE partido SET Status='B' where Id_partido=@id";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            bool result = false;
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("id", id));
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            return result;
        }
        public bool IngresarResultados(int id, int r1, int r2)
        {
            string cmdText = "UPDATE partido SET R1='@r1', R2='@r2' where Id_partido=@id";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            bool result = false;
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("id", id));
                command.Parameters.Add(new MySqlParameter("r1", r1));
                command.Parameters.Add(new MySqlParameter("r2", r2));
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            return result;
        }
    }
}
