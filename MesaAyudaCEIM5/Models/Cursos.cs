using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MesaAyudaCEIM5.Models
{
    public class Cursos
    {
       
        public string connectionString = ConfigurationManager.ConnectionStrings["DBMesaAyuda"].ConnectionString;
        public CursoModel BuscaCursoPorCur(int cur)
        {
            var model = new CursoModel();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC CURSOS_SEL @ACCION, @CUR";
                command.Parameters.AddWithValue("@ACCION", 1);
                command.Parameters.AddWithValue("@CUR", cur);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model.cur_id = (int)reader["cur_id"];
                    model.nombre_curso = (string)reader["nombre"];
                    model.duracion = (int)reader["duracion"];
                    model.are_id = (int)reader["are_id"];
                    model.nombre_area = (string)reader["nombre_area"];
                    model.nombre_area_sigla = (string)reader["nombre_area_sigla"];
                    model.codigo_sence = (string)reader["codigo_sence"];
                }
                reader.Close();
                connection.Close();
            }
            return model;
        }
    }
    public class CursoModel
    {
        public int cur_id { get; set; }
        public string nombre_curso { get; set; }
        public int duracion { get; set; }
        public int are_id { get; set; }
        public string nombre_area { get; set; }
        public string nombre_area_sigla { get; set; }
        public string codigo_sence { get; set; }
    }
}