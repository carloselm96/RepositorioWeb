using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models.Context
{
    public class UsuarioContext
    {
        public UsuarioContext(string cs)
        {
            ConnectionString = cs;
        }

        public string ConnectionString { get; set; }

        public Boolean VerificarUsuario(Usuario user)
        {
            Boolean valid = false;
            string cmdText = "select * from Usuario where Correo=@mail and Password=@cont";
            MySqlConnection my = new MySqlConnection(ConnectionString);
            my.Open();            
            using (MySqlCommand command = new MySqlCommand(cmdText, my))
            {
                command.Parameters.Add(new MySqlParameter("@mail", user.correo));
                command.Parameters.Add(new MySqlParameter("@cont", user.password));
                var reader = command.ExecuteReader();
                reader.Read();

                if (reader.HasRows)
                {
                    valid = true;
                }
            }

            my.Close();
            return valid;
        }
    }


}
