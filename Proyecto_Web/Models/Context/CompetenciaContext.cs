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
    }
}

