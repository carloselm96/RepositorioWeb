using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models.Context
{
    public class ParticipantesContext
    {
        public ParticipantesContext(string cs)
        {
            ConnectionString = cs;
        }
        public string ConnectionString { get; set; }

        public List<Participante> getParticipantes()
        {
            List<Participante> result = new List<Participante>();
            string cmdText = "select * from consulta_participantes ";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            MySqlCommand command = new MySqlCommand(cmdText, my);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Participante participante = new Participante(); //Id_Participante, Nombres, Apellido_p, Apellido_m, Fecha_nac, FK_disciplina, disciplina, FK_equipo, equipo          
                participante.id = reader.GetInt16("Id_Participante");
                participante.nombres = reader.GetString("Nombres");
                participante.apellidop = reader.GetString("Apellido_p");
                participante.apellidom = reader.GetString("Apellido_m");
                participante.fecha_nacimiento = reader.GetDateTime("Fecha_nac");
                participante.disciplina = new Disciplina(reader.GetInt16("FK_disciplina"), reader.GetString("disciplina"));
                if (!reader.IsDBNull(8))
                {
                    participante.equipo = new Equipo();
                    participante.equipo.id = reader.GetInt16("FK_equipo");
                    participante.equipo.nombre = reader.GetString("equipo");
                }                
                result.Add(participante);
            }
            command.Dispose();
            my.Close();
            return result;
        }
    }
}
