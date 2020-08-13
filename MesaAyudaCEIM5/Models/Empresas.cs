using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MesaAyudaCEIM5.Models
{
    public class Empresas
    {
       
        public string connectionString = ConfigurationManager.ConnectionStrings["DBMesaAyuda"].ConnectionString;
        public EmpresaModel BuscaEmpresaPorEmp(int emp)
        {
            var model = new EmpresaModel();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC EMPRESAS_SEL @ACCION, @EMP, @RUTDV";
                command.Parameters.AddWithValue("@ACCION", 2);
                command.Parameters.AddWithValue("@EMP", emp);
                command.Parameters.AddWithValue("@RUTDV", "");
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model.emp_id = (int)reader["emp_id"];
                    model.rutdv = (string)reader["rutdv"];
                    model.rut = (int)reader["rut"];
                    model.dv = (string)reader["dv"];
                    model.razon = (string)reader["razon"];
                }
                reader.Close();
                connection.Close();
            }
            return model;
        }

    }
    public class EmpresaModel
    {
        public int emp_id { get; set; }
        public string rutdv { get; set; }
        public int rut { get; set; }
        public string dv { get; set; }
        public string razon { get; set; }
    }
}