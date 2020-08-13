using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MesaAyudaCEIM5.Models
{
    public class Comunas
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["DBMesaAyuda"].ConnectionString;
        public IEnumerable<ComunaModel> BuscarTodas()
        {
            List<ComunaModel> myModelo = new List<ComunaModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("COMUNAS_SEL", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ACCION", 1);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ComunaModel model = new ComunaModel();

                    model.cmn_id = (int)reader["cmn_id"];
                    model.comuna = (string)reader["comuna"];
                    myModelo.Add(model);
                }
                reader.Close();
                connection.Close();
            }
            return myModelo;
        }
    }
    public class ComunaModel
    {
        public int cmn_id { get; set; }
        public string comuna { get; set; }
    }
}