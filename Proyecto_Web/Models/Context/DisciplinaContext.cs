using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models.Context
{
    public class DisciplinaContext
    {
        public DisciplinaContext(string cs)
        {
            ConnectionString = cs;
        }

        public string ConnectionString { get; set; }

        public List<Disciplina> getDisciplinas()
        {
            List<Disciplina> results= new List<Disciplina>();
            string cmdText = "select * from disciplina ";            
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            MySqlCommand command = new MySqlCommand(cmdText, my);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Disciplina disciplina = new Disciplina();
                disciplina.id = reader.GetInt16("Id_disciplina");
                disciplina.nombre = reader.GetString("Nombre");
                results.Add(disciplina);
            }
            command.Dispose();
            my.Close();
            return results;            
        }

    }
}
