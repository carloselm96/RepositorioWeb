using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models.Context
{
    public class ActividadContext
    {
        public ActividadContext(string cs)
        {
            ConnectionString = cs;
        }
        public string ConnectionString { get; set; }

        public List<Actividad> getActividades()
        {
            List<Actividad> result = new List<Actividad>();
            string cmdText = "select actividad.Id_actividad, actividad.Nombre, actividad.Fecha, " +
                "actividad.Hora, actividad.Descripcion, tipo_actividad.Nombre as 'act_tip' " +
                " from actividad, tipo_actividad  where actividad.FK_tipo_actividad = tipo_actividad.Id_tipo_actividad ";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            MySqlCommand command = new MySqlCommand(cmdText, my);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Actividad actividad = new Actividad(); //Id_actividad, Nombre, Fecha, Hora, Descripcion, Nombre
                actividad.id = reader.GetInt16("Id_actividad");
                actividad.nombre = reader.GetString("Nombre");
                actividad.fecha_hora = reader.GetString("Fecha");                
                actividad.descripcion = reader.GetString("Descripcion");
                actividad.tipo = reader.GetString("act_tip");
                //department.dept_no = reader.GetString("dept_no");                
                result.Add(actividad);
            }
            command.Dispose();
            my.Close();
            return result;
        }
    }
}
