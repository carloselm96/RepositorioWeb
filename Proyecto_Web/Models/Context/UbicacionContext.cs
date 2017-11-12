using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models.Context
{
    public class UbicacionContext
    {
        public UbicacionContext(string cs)
        {
            ConnectionString = cs;
        }
        public string ConnectionString { get; set; }

        public List<Ubicacion> getUbicaciones()
        {
            List<Ubicacion> result = new List<Ubicacion>();
            string cmdText = "select * from ubicacion ";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            MySqlCommand command = new MySqlCommand(cmdText, my);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                Ubicacion ubicacion = new Ubicacion();
                ubicacion.id = reader.GetInt16("Id_ubicacion");
                ubicacion.nombre = reader.GetString("Nombre");
                ubicacion.longitud = reader.GetString("Longitud");
                ubicacion.latitud = reader.GetString("Latitud");
                result.Add(ubicacion);
            }
            command.Dispose();
            my.Close();
            return result;
        }
    }
}
