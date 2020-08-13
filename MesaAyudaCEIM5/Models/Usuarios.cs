using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MesaAyudaCEIM5.Models
{
    public class Usuarios
    {

        public string connectionString = ConfigurationManager.ConnectionStrings["DBMesaAyuda"].ConnectionString;
        public UsuarioModel BuscaUsuario(string Usuario, string Password)
        {
            var model = new UsuarioModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
                
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC USUARIOS_SEL @ACCION, @USR, @USUARIO, @PASSWORD";
                command.Parameters.AddWithValue("@ACCION", 1);
                command.Parameters.AddWithValue("@USR", -1);
                command.Parameters.AddWithValue("@USUARIO", Usuario);
                command.Parameters.AddWithValue("@PASSWORD", Password);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model.Usr = (int)reader["usr_id"]; 
                    model.NombreCompleto = reader["nombres"].ToString();
                    model.Usuario = Usuario;
                    model.MesajeError = "";
                } else
                {
                    model.MesajeError = "El Usuario y/o Contraseña no son válidos";
                }
                reader.Close();
                connection.Close();
            }
            return model;
        }
    }
    public class UsuarioModel
    {
        [Required(ErrorMessage = "Favor ingrese un Usuario")]
        [Display(Name = "Usuario")]
        [StringLength(10)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Favor ingrese una Contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(50)]
        public string Password { get; set; }
        public int Usr { get; set; }
        public string NombreCompleto { get; set; }
        public string MesajeError { get; set; }
    }
}