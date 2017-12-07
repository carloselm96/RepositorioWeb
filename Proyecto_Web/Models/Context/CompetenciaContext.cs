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
                competencia.fecha = reader.GetDateTime("Fecha").ToString("dd-mm-yyyy");
                competencia.disciplina = new Disciplina();                
                competencia.ubicacion = new Ubicacion();
                competencia.ubicacion.id = reader.GetInt16("Fk_ubicacion");
                competencia.ubicacion.nombre = reader.GetString("ubicacion");
                competencia.evento = new Evento();
                competencia.evento.id = reader.GetInt16("Evento");
                result.Add(competencia);
            }
            command.Dispose();
            foreach (Competencia competencia in result)
            {
            competencia.participantes = new List<Participante>();
                string cmdText2 = "select distinct Participante.Nombres," +
                " Participante.Apellido_P,Participante.Apellido_m,equipo.Nombre" +
                " as equipo,competencia_competidores.puntaje  from Participante, competencia_competidores,competencia," +
                " equipo where FK_competencia=@id" +
                " and participante.Id_Participante=competencia_competidores.FK_competidor" +
                " and participante.FK_equipo=equipo.Id_equipo;";
                MySqlCommand command2 = new MySqlCommand(cmdText2, my);
                command2.Parameters.Add(new MySqlParameter("@id", competencia.id));
                MySqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    Participante participante = new Participante();
                    participante.nombres = reader2.GetString("Nombres");
                    participante.apellidop = reader2.GetString("Apellido_p");
                    participante.apellidom = reader2.GetString("Apellido_m");
                    participante.equipo = new Equipo();
                    participante.equipo.nombre = reader2.GetString("equipo");
                    participante.puntaje = reader2.GetInt16("puntaje");
                    competencia.participantes.Add(participante);                    
                }
                command2.Dispose();
            }
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

        public Competencia GetCompetencia(int id)
        {
            Competencia competencia = null;

            string cmdText = "select * from consulta_competencia where Id_competencia=@id;";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();


            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("@id", id));

                var reader = command.ExecuteReader();
                reader.Read();

                if (reader.HasRows)
                {
                    competencia = new Competencia();
                    competencia.id = reader.GetInt16("Id_competencia");
                    competencia.nombre = reader.GetString("Nombre");
                    competencia.hora = reader.GetString("hora");
                    competencia.fecha = reader.GetDateTime("Fecha").ToString("dd-mm-yyyy");
                    competencia.disciplina = new Disciplina();
                    competencia.ubicacion = new Ubicacion();
                    competencia.ubicacion.id = reader.GetInt16("Fk_ubicacion");
                    competencia.ubicacion.nombre = reader.GetString("ubicacion");
                    competencia.evento = new Evento();
                    competencia.evento.id = reader.GetInt16("Evento");
                }
            }

            if (competencia!=null)
            {
                string cmdText2 = "select distinct FK_competidor, Participante.Nombres,Participante.Apellido_P,Participante.Apellido_m,equipo.Nombre, competencia_competidores.puntaje, equipo.Nombre as equipo from Participante, competencia_competidores,competencia,equipo where FK_competencia=@id and participante.Id_Participante=competencia_competidores.FK_competidor and participante.FK_equipo=equipo.Id_equipo;";
                competencia.participantes = new List<Participante>();
                MySqlCommand command2 = new MySqlCommand(cmdText2, my);
                command2.Parameters.Add(new MySqlParameter("@id", id));
                MySqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    Participante participante = new Participante();
                    participante.id = reader2.GetInt16("FK_competidor");
                    participante.nombres = reader2.GetString("Nombres");
                    participante.apellidop = reader2.GetString("Apellido_p");
                    participante.apellidom = reader2.GetString("Apellido_m");
                    participante.equipo = new Equipo();
                    participante.equipo.nombre = reader2.GetString("equipo");
                    participante.puntaje = reader2.GetFloat("puntaje");
                    competencia.participantes.Add(participante);
                }
                command2.Dispose();
            }
            my.Close();
            return competencia;
        }


        public bool RegistrarResultados(int idc, int idp, float puntaje)
        {
            string cmdText = "UPDATE competencia_competidores SET puntaje=@p where FK_competencia=@id and FK_competidor=@idp";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();
            bool result = false;
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("p", puntaje));
                command.Parameters.Add(new MySqlParameter("id", idc));
                command.Parameters.Add(new MySqlParameter("idp", idp));
                result = command.ExecuteNonQuery() > 0 ? true : false;
            }
            my.Close();
            return result;            
        }
        public bool Eliminar(int id)
        {
            {
                bool result = false;
                string cmdText = "UPDATE competencia SET status='B' where Id_competencia=@id";
                MySqlConnection my = new MySqlConnection(ConnectionString);
                my.Open();
                using (MySqlCommand command = new MySqlCommand(cmdText, my))
                {
                    command.Parameters.Add(new MySqlParameter("id", id));
                    result = command.ExecuteNonQuery() > 0 ? true : false;
                }
                my.Close();
                return result;
            }
        }
    }
}

