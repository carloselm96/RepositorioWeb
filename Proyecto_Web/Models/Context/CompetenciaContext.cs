using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models.Context
{
    public class CompetenciaContext
    {
        public CompetenciaContext(string cs)
        {
            ConnectionString = cs;
        }
        public string ConnectionString { get; set; }

        public List<Competencia> getCompetencias()
        {
            List<Competencia> result = new List<Competencia>();
            string cmdText = "select * from consulta_competencia";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            MySqlCommand command = new MySqlCommand(cmdText, my);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Competencia competencia = new Competencia(); //Id_competencia, Fecha, hora, FK_disciplina, disciplina, FK_ubicacion, ubicacion
                competencia.id = reader.GetInt16("Id_competencia");
                competencia.nombre = reader.GetString("Nombre");
                competencia.hora = reader.GetString("hora");
                competencia.fecha = reader.GetString("Fecha");
                competencia.disciplina = new Disciplina();
                /*competencia.disciplina.id=reader.GetInt16("FK_disciplina");
                competencia.disciplina.nombre = reader.GetString("disciplina");*/
                competencia.ubicacion = new Ubicacion();
                competencia.ubicacion.id = reader.GetInt16("Fk_ubicacion");
                competencia.ubicacion.nombre = reader.GetString("ubicacion");
                result.Add(competencia);
            }
            command.Dispose();
            my.Close();
            return result;
        }

        public bool Add1(int selectEvento, string inputNombre, string desc, int ubi, string fecha, string hora)
        {
            string cmdText = "pro_in_competencia(@evento,@nombre,@desc,@ubi,@fecha,@hora)";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            bool result = false;
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("nombre", inputNombre));
                command.Parameters.Add(new MySqlParameter("evento", selectEvento));
                command.Parameters.Add(new MySqlParameter("desc", desc));
                command.Parameters.Add(new MySqlParameter("ubi", ubi));
                command.Parameters.Add(new MySqlParameter("fecha", fecha));
                command.Parameters.Add(new MySqlParameter("hora", hora));
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            return result;
        }

        public  int lastInserted()
        {            
            string cmdText = "SELECT LAST_INSERT_ID() as id FROM competencia;";
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

        public bool Add2(int id, List<int> idSelected)
        {
            string cmdText = "pro_in_compcomp(@idcomp,@idcompe)";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            bool result = false;
            for (int i = 0; i < idSelected.Count; i++)
            {
                using (MySqlCommand command = new MySqlCommand(cmdText, my))
                {
                    command.Parameters.Add(new MySqlParameter("idcomp", id));
                    command.Parameters.Add(new MySqlParameter("idcompe", idSelected[i]));
                    result = command.ExecuteNonQuery() > 0 ? true : false;
                }
            }                
            my.Close();
            return result;
        }
    }
}

