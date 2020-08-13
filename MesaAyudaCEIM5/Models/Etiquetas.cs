using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MesaAyudaCEIM5.Models
{
    public class Etiquetas
    {
        //public string connectionString = "Data Source=200.200.200.114; Initial Catalog=MESAAYUDA; Integrated Security=False; User ID=sa;Password=Ceim7419/; MultipleActiveResultSets=True";
        public string connectionString = ConfigurationManager.ConnectionStrings["DBMesaAyuda"].ConnectionString;
        public EtiquetaModel BuscaEtiquetaPorEti(int eti)
        {
            var model = new EtiquetaModel();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC ETIQUETAS_SEL @ACCION, @ETI";
                command.Parameters.AddWithValue("@ACCION", 1);
                command.Parameters.AddWithValue("@ETI", eti);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model.eti_id = (int)reader["eti_id"];
                    model.nombre_etiqueta = (string)reader["etiqueta"];
                    model.descripcion = (string)reader["descripcion"];
                    model.causa = (string)reader["causa"];
                    model.solucion = (string)reader["solucion"];
                }
                reader.Close();
                connection.Close();
            }
            return model;
        }
        public IEnumerable<AreasOrganiacionalModel> BuscaAreasOrganizacionales()
        {
            List<AreasOrganiacionalModel> myModel = new List<AreasOrganiacionalModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC ETIQUETAS_SEL @ACCION";
                command.Parameters.AddWithValue("@ACCION", 1);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AreasOrganiacionalModel model = new AreasOrganiacionalModel();

                    model.aor_id = (int)reader["pch_id"];
                    model.nombre_area = (string)reader["area"];
                    model.numero_registros = (int)reader["registros"];
                    myModel.Add(model);
                }
                reader.Close();
                connection.Close();
            }
            return myModel;
        }
        public IEnumerable<EtiquetaModel> BuscaTodasEtiquetas()
        {
            List<EtiquetaModel> myModel = new List<EtiquetaModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC ETIQUETAS_SEL @ACCION";
                command.Parameters.AddWithValue("@ACCION", 4);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EtiquetaModel model = new EtiquetaModel();

                    model.aor_id = (int)reader["aor_id"];
                    model.nombre_area = (string)reader["area"];
                    model.eti_id = (int)reader["eti_id"];
                    model.nombre_etiqueta = (string)reader["etiqueta"];
                    model.descripcion = (string)reader["descripcion"];
                    model.causa = (string)reader["causa"];
                    model.solucion = (string)reader["solucion"];
                    myModel.Add(model);
                }
                reader.Close();
                connection.Close();
            }
            return myModel;
        }
    }
    public class EtiquetaModel
    {
        public int eti_id { get; set; }
        public string nombre_etiqueta { get; set; }
        public string descripcion { get; set; }
        public string causa { get; set; }
        public string solucion { get; set; }
        public int aor_id { get; set; }
        public string nombre_area { get; set; }

    }
    public class AreasOrganiacionalModel
    {
        public int aor_id { get; set; }
        public string nombre_area { get; set; }
        public int numero_registros { get; set; }
    }
}