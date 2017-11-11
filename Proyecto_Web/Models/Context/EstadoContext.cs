using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models.Context
{
    public class EstadoContext
    {
        public EstadoContext(string conexion)
        {
            ConnectionString = conexion;
        }

        public string ConnectionString { get; set; }
        public List<Estado> getEstados()
        {
            List<Estado> results = new List<Estado>();
            string cmdText = "select * from estados; ";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            MySqlCommand command = new MySqlCommand(cmdText, my);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Estado estado = new Estado();
                estado.id = reader.GetInt16("Id_estados");
                estado.nombre = reader.GetString("nombre");
                results.Add(estado);
            }
            command.Dispose();
            my.Close();
            return results;
        }
    }
}
