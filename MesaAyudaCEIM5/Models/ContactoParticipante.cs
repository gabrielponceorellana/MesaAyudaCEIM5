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
    public class ContactoParticipante
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["DBMesaAyuda"].ConnectionString;
        public ContactoParticipanteModel BuscaPorPyp(int pyp)
        {
            var model = new ContactoParticipanteModel();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("CONTACTO_PARTICIPANTE_SEL", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ACCION", 1);
                command.Parameters.AddWithValue("@PYP", pyp);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model.ctc_id = (int)reader["ctc_id"];
                    model.pyp_id = (int)reader["pyp_id"];
                    model.invitacion = (int)reader["invitacion"];
                    model.nivel_tecnologico = (int)reader["nivel_tecnologico"];
                    model.requiere_asistencia = (int)reader["requiere_asistencia"];
                    model.tipo_dispositivo = (int)reader["tipo_dispositivo"];
                    model.tipo_conexion = (int)reader["tipo_conexion"];
                    model.cmn_id_conexion = (int)reader["cmn_id_conexion"];
                    model.dispositivos_externos = (int)reader["dispositivos_externos"];
                    model.observacion = (string)reader["observacion"];
                } else
                {
                    model.pyp_id = pyp;
                }
                reader.Close();
                connection.Close();
            }
            return model;
        }
        public bool Agregar(ContactoParticipanteModel Ctc)
        {
            SqlConnection connection = null;


            connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("CONTACTO_PARTICIPANTE_ACT", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ACCION", 1);
            command.Parameters.AddWithValue("@USR", Ctc.usr_id);
            command.Parameters.AddWithValue("@PYP", Ctc.pyp_id);
            command.Parameters.AddWithValue("@INVITACION", Ctc.invitacion);
            command.Parameters.AddWithValue("@NIVEL_TECNOLOGICO", Ctc.nivel_tecnologico);
            command.Parameters.AddWithValue("@REQUIERE_ASISTENCIA", Ctc.requiere_asistencia);
            command.Parameters.AddWithValue("@TIPO_DISPOSITIVO", Ctc.tipo_dispositivo);
            command.Parameters.AddWithValue("@TIPO_CONEXION", Ctc.tipo_conexion);
            command.Parameters.AddWithValue("@CMN_ID_CONEXION", Ctc.cmn_id_conexion);
            command.Parameters.AddWithValue("@DISPOSITIVOS_EXTERNOS", Ctc.dispositivos_externos);
            command.Parameters.AddWithValue("@OBSERVACION", Ctc.observacion);
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();

            if (i >= 1)
                return true;
            else
                return false;

        }
    }
    public class ContactoParticipanteModel
    {
        public int ctc_id { get; set; }
        public int pyp_id { get; set; }

        [Required(ErrorMessage = "Favor seleccione una opción")]
        public int invitacion { get; set; }
        [Required(ErrorMessage = "Favor seleccione una opción")]
        public int nivel_tecnologico { get; set; }
        [Required(ErrorMessage = "Favor seleccione una opción")]
        public int requiere_asistencia { get; set; }
        [Required(ErrorMessage = "Favor seleccione una opción")]
        public int tipo_dispositivo { get; set; }
        [Required(ErrorMessage = "Favor seleccione una opción")]
        public int tipo_conexion { get; set; }
        [Required(ErrorMessage = "Favor seleccione una opción")]
        public int cmn_id_conexion { get; set; }
        [Required(ErrorMessage = "Favor seleccione una opción")]
        public int dispositivos_externos { get; set; }
        public string observacion { get; set; }
        public int usr_id { get; set; }
        public string MesajeError { get; set; }
    }
}