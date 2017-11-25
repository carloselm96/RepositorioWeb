using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models.Context
{
    public class EquipoContext
    {
        public EquipoContext(string cs)
        {
            ConnectionString = cs;
        }

        public string ConnectionString { get; set; }

        public List<Equipo> GetAllEquipos()
        {
            string cmdText = "select * from consulta_equipos; ";
            List<Equipo> results = new List<Equipo>();
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            MySqlCommand command = new MySqlCommand(cmdText, my);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Equipo equipo = new Equipo();
                equipo.id = reader.GetInt16("Id_equipo");
                equipo.nombre = reader.GetString("nombre_equipo");
                equipo.gremio = new Gremio();
                equipo.gremio.id = reader.GetString("num_gremio");
                equipo.gremio.localidad = reader.GetString("nombre_loc");
                equipo.gremio.estado = new Estado();                
                equipo.gremio.estado.nombre = reader.GetString("nombre_estado");
                equipo.disciplina = new Disciplina();
                equipo.disciplina.id = reader.GetInt16("Id_Disciplina");
                equipo.disciplina.nombre = reader.GetString("disciplina");                  
                //Id_Equipo, nombre_equipo,num_gremio,nombre_loc,nombre_estado,Id_Disciplina,disciplina
                results.Add(equipo);
            }
            command.Dispose();
            my.Close();
            return results;
        }

        public List<Equipo> BuscarEquipo(string bus)
        {            
            string cmdText = "select * from consulta_equipos where nombre_equipo like '%@bus%'";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            List<Equipo> results = new List<Equipo>();            
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("@bus", bus));
                var reader = command.ExecuteReader();
                reader.Read();

                if (reader.HasRows)
                {
                    Equipo equipo = new Equipo();
                    equipo.id = reader.GetInt16("Id_equipo");
                    equipo.nombre = reader.GetString("nombre_equipo");
                    equipo.gremio = new Gremio();
                    equipo.gremio.id = reader.GetString("num_gremio");
                    equipo.gremio.localidad = reader.GetString("nombre_loc");
                    equipo.gremio.estado = new Estado();
                    equipo.gremio.estado.nombre = reader.GetString("nombre_estado");
                    equipo.disciplina = new Disciplina();
                    equipo.disciplina.id = reader.GetInt16("Id_Disciplina");
                    equipo.disciplina.nombre = reader.GetString("disciplina");
                    //Id_Equipo, nombre_equipo,num_gremio,nombre_loc,nombre_estado,Id_Disciplina,disciplina
                    results.Add(equipo);
                }
            }

            my.Close();
            return results;
        }

        public Equipo detallesEquipo(string id)
        {
            Equipo equipo = null;

            string cmdText = "select * from consulta_equipos where Id_equipo=@id;";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();


            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("@id", id));

                var reader = command.ExecuteReader();
                reader.Read();

                if (reader.HasRows)
                {
                    equipo = new Equipo();
                    equipo.id = reader.GetInt16("Id_equipo");
                    equipo.nombre = reader.GetString("nombre_equipo");
                    equipo.gremio = new Gremio();
                    equipo.gremio.id = reader.GetString("num_gremio");
                    equipo.gremio.localidad = reader.GetString("nombre_loc");
                    equipo.gremio.estado = new Estado();
                    equipo.gremio.estado.nombre = reader.GetString("nombre_estado");
                    equipo.disciplina = new Disciplina();
                    equipo.disciplina.id = reader.GetInt16("Id_Disciplina");
                    equipo.disciplina.nombre = reader.GetString("disciplina");
                }
            }

            string cmdText2 = "select * from Participante where FK_equipo=@id;";                       

            equipo.participantes = new List<Participante>();            
            MySqlCommand command2 = new MySqlCommand(cmdText2, my);
            command2.Parameters.Add(new MySqlParameter("@id", id));
            MySqlDataReader reader2 = command2.ExecuteReader();

            while (reader2.Read())
            {
                Participante participante = new Participante();
                participante.nombres = reader2.GetString("Nombres");
                participante.apellidop = reader2.GetString("Apellido_p");
                participante.apellidom = reader2.GetString("Apellido_m");
                equipo.participantes.Add(participante);
            }
            command2.Dispose();            
            my.Close();

            return equipo;
        }

        public bool Add(String inputNombre, int selectGremio, int selectDisciplina)
        {
            string cmdText = "INSERT INTO Equipo(Nombre,FK_gremio,FK_disciplina) VALUES(@nombre,@gremio,@disciplina)";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            bool result = false;
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("nombre", inputNombre));
                command.Parameters.Add(new MySqlParameter("gremio", selectGremio));
                command.Parameters.Add(new MySqlParameter("disciplina", selectDisciplina));
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            return result;
        }
    }
}
