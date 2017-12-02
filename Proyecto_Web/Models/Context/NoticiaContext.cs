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
            string cmdText = "select noticias.Id_noticias, noticias.Titulo, noticias.Fecha, " +
                "noticias.Noticia, noticias.FK_evento, evento.Nombre_evento from noticias," +
                "evento where noticias.FK_evento = evento.Id_evento;";
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
                results.Add(noticia);
            }
            command.Dispose();
            my.Close();
            return results;
        }

        public bool nuevaNoticia(string titulo,string fecha, string noticia, int evento)
        {
            string cmdText = "pro_in_noticias(@titulo,@fecha,@noticia,@evento)";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            bool result = false;
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("titulo", titulo));
                command.Parameters.Add(new MySqlParameter("fecha", fecha));
                command.Parameters.Add(new MySqlParameter("noticia", noticia));
                command.Parameters.Add(new MySqlParameter("evento", evento));
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
            string cmdText = "select noticias.Id_noticias, noticias.Titulo, noticias.Fecha, " +
                "noticias.Noticia, noticias.FK_evento, evento.Nombre_evento from noticias," +
                "evento where noticias.FK_evento = evento.Id_evento;";
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
    }
}
