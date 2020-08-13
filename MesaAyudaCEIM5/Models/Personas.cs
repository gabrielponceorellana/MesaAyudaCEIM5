using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MesaAyudaCEIM5.Models
{
    public class Personas
    {
        
        public string connectionString = ConfigurationManager.ConnectionStrings["DBMesaAyuda"].ConnectionString;
        public PersonaModel BuscaPersonaPorRutdv(string Rutdv)
        {
            var model = new PersonaModel();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC PERSONAS_SEL @ACCION, @PER, @RUTDV";
                command.Parameters.AddWithValue("@ACCION", 1);
                command.Parameters.AddWithValue("@PER", -1);
                command.Parameters.AddWithValue("@RUTDV", Rutdv);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model.per_id = (int)reader["per_id"];
                    model.rut = (int)reader["rut"];
                    model.dv = (string)reader["dv"];
                    model.rutdv = (string)reader["rutdv"];
                    model.nombres = (string)reader["nombres"];
                    model.paterno = (string)reader["paterno"];
                    model.materno = (string)reader["materno"];
                    model.genero = (string)reader["genero"];
                    model.fecha_nacimiento = (DateTime)reader["fecha_nacimiento"];
                    model.celular = (string)reader["celular"];
                    model.correo = (string)reader["correo"];
                    model.usuario = (string)reader["usuario"];
                    model.password = (string)reader["password"];
                }
                reader.Close();
                connection.Close();
            }
            return model;
        }
        public PersonaModel BuscaPersonaPorPer(int per)
        {
            var model = new PersonaModel();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC PERSONAS_SEL @ACCION, @PER";
                command.Parameters.AddWithValue("@ACCION", 2);
                command.Parameters.AddWithValue("@PER", per);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model.per_id = (int)reader["per_id"];
                    model.rut = (int)reader["rut"];
                    model.dv = (string)reader["dv"];
                    model.rutdv = (string)reader["rutdv"];
                    model.nombres = (string)reader["nombres"];
                    model.paterno = (string)reader["paterno"];
                    model.materno = (string)reader["materno"];
                    model.genero = (string)reader["genero"];
                    model.fecha_nacimiento = (DateTime)reader["fecha_nacimiento"];
                    model.celular = (string)reader["celular"];
                    model.correo = (string)reader["correo"];
                }
                reader.Close();
                connection.Close();
            }
            return model;
        }
        public bool ActualizaCorreo(PersonaModel Per)
        {
            SqlConnection connection = null;


            connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("PERSONAS_ACT", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ACCION", 1);
            command.Parameters.AddWithValue("@USR", -1);
            command.Parameters.AddWithValue("@PER_SILICIO", Per.per_id);
            command.Parameters.AddWithValue("@TCO", 3);
            command.Parameters.AddWithValue("@VALOR", Per.correo);
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();

            if (i >= 1)
                return true;
            else
                return false;

        }
    }
    public class PersonaModel
    {
        [Required(ErrorMessage = "Favor ingrese un RUT")]
        [Display(Name = "RUT")]
        [StringLength(10)]
        public string rutdv { get; set; }
        public int per_id { get; set; }
        public int rut { get; set; }
        public string dv { get; set; }
        public string nombres { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string genero { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime fecha_nacimiento { get; set; }
        public string celular { get; set; }
        public string correo { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
    }

}