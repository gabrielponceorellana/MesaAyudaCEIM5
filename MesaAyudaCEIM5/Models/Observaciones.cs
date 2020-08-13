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
    public class Observaciones
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["DBMesaAyuda"].ConnectionString;
        public IEnumerable<ObservacionModel> ListadoObservaciones(int per)
        {
            List<ObservacionModel> myModel = new List<ObservacionModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC OBSERVACIONES_SEL @ACCION, @OBS, @PER";
                command.Parameters.AddWithValue("@ACCION", 1);
                command.Parameters.AddWithValue("@OBS", -1);
                command.Parameters.AddWithValue("@PER", per);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ObservacionModel model = new ObservacionModel();
                    model.per_id = (int)reader["per_id_silicio"];
                    model.obs_id = (int)reader["obs_id"];
                    model.observacion = (string)reader["observacion"];
                    model.fecha = (DateTime)reader["fecha"];
                    model.usr_id = (int)reader["usr_id"];
                    model.nombre_usuario = (string)reader["nombre_usuario"];
                    myModel.Add(model);
                }
                reader.Close();
                connection.Close();
            }
            return myModel;
        }
        public bool Agregar(ObservacionModel Obs)
        {
            SqlConnection connection = null;
            

            connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("OBSERVACIONES_ACT", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ACCION", 1);
            command.Parameters.AddWithValue("@USR", Obs.usr_id);
            //command.Parameters.AddWithValue("@OBS", -1);
            command.Parameters.AddWithValue("@PER", Obs.per_id);
            command.Parameters.AddWithValue("@OBSERVACION", Obs.observacion);

            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();

            if (i >= 1)
                return true;
            else
                return false;

        }
        
    }
    public class ObservacionModel
    {

        [Required(ErrorMessage = "Favor ingrese una Observación")]
        [Display(Name = "Observacion")]
        [StringLength(100)]
        public string observacion { get; set; }
        public int obs_id { get; set; }
        public int per_id { get; set; }
        public int usr_id { get; set; }
        public DateTime fecha { get; set; }
        public string nombre_usuario { get; set; }
        
    }
}