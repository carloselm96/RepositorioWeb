using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models.Context
{
    public class GremioContext
    {
        public GremioContext(string conexion)
        {
            ConnectionString = conexion;
        }

        public string ConnectionString { get; set; }
        public List<Gremio> getGremios()
        {
            List<Gremio> results = new List<Gremio>();
            string cmdText = "select * from view_gremios";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            MySqlCommand command = new MySqlCommand(cmdText, my);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Gremio gremio = new Gremio();
                gremio.id = reader.GetString("no_gremio");
                gremio.localidad = reader.GetString("nombre_loc");
                gremio.estado = new Estado();
                gremio.estado.id = reader.GetInt16("FK_estados");
                gremio.estado.nombre = reader.GetString("estado");
                results.Add(gremio);
            }
            command.Dispose();
            my.Close();
            return results;
        }
        public bool nuevoGremio(int nGremio, String localidad, int estado)
        {            
            string cmdText = "pro_in_gremios(@nGremio,@local,@estado)";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            bool result = false;
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("nGremio", nGremio));
                command.Parameters.Add(new MySqlParameter("local", localidad));
                command.Parameters.Add(new MySqlParameter("estado", estado));
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            return result;
        }

        public bool eliminar(int nGremio)
        {
            string cmdText = "UPDATE gremios set Status='B' where no_gremio=@id";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            bool result = false;
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("id", nGremio));                
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            return result;
        }
    }
}
