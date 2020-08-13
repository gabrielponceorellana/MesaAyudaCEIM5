using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MesaAyudaCEIM5.Models
{
    public class ProyectosParticipantes
    {
        
        public string connectionString = ConfigurationManager.ConnectionStrings["DBMesaAyuda"].ConnectionString;
        public IEnumerable<ProyectosParticipantesModel> BuscaProyectosParticipante(int per)
        {
            List<ProyectosParticipantesModel> myModel = new List<ProyectosParticipantesModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC PARTICIPANTES_SEL @ACCION, @PER";
                command.Parameters.AddWithValue("@ACCION", 1);
                command.Parameters.AddWithValue("@PER", per);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProyectosParticipantesModel model = new ProyectosParticipantesModel();
                    model.pry_id = (int)reader["pry_id"];
                    model.nombre_proyecto = (string)reader["nombre_proyecto"];
                    model.inicio_ejecucion = (DateTime)reader["inicio_ejecucion"];
                    model.termino_ejecucion = (DateTime)reader["termino_ejecucion"];
                    model.nombre_sede = (string)reader["nombre_sede"];
                    model.nombre_linea = (string)reader["nombre_linea"];
                    model.nombre_area = (string)reader["nombre_area"];
                    model.modalidad_proyecto = (string)reader["modalidad_proyecto"];
                    model.nombre_estado_proyecto = (string)reader["nombre_estado_proyecto"];
                    model.pyp_id = (int)reader["pyp_id"];
                    model.rutdv = (string)reader["rutdv"];
                    model.nombre_sede_sigla = (string)reader["nombre_sigla_sede"];
                    model.nombre_linea_sigla = (string)reader["nombre_sigla_linea"];
                    model.nombre_area_sigla = (string)reader["nombre_sigla_area"];
                    switch ((int)reader["mod_proyecto"])
                    {
                        case 1:
                            model.clase_label_modalidad = "primary";
                            break;
                        case 2:
                        case 5:
                            model.clase_label_modalidad = "success";
                            break;
                        case 3:
                            model.clase_label_modalidad = "warning";
                            break;
                        case 4:
                            model.clase_label_modalidad = "info";
                            break;
                        default:
                            model.clase_label_modalidad = "default";
                            break;
                    }
                    switch ((int)reader["epr_id"])
                    {
                        case 1:
                            model.clase_label_estado_proyecto = "default";
                            break;
                        case 2:
                        case 3:
                        case 4:
                        case 7:
                            model.clase_label_estado_proyecto = "warning";
                            break;
                        case 5:
                        case 8:
                            model.clase_label_estado_proyecto = "success";
                            break;
                        case 6:
                            model.clase_label_estado_proyecto = "info";
                            break;
                        case 9:
                            model.clase_label_estado_proyecto = "danger";
                            break;
                        default:
                            model.clase_label_estado_proyecto = "default";
                            break;
                    }
                    model.pov_id = (int)reader["pov_id"];
                    model.emp_id = (int)reader["emp_id"];
                    model.per_id = (int)reader["per_id"];
                    myModel.Add(model);
                }
                reader.Close();
                connection.Close();
            }
            return myModel;
        }
        public ProyectosParticipantesModel BuscarPorPyp(int pyp)
        {
            var model = new ProyectosParticipantesModel();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC PARTICIPANTES_SEL @ACCION, @PER, @PYP";
                command.Parameters.AddWithValue("@ACCION", 2);
                command.Parameters.AddWithValue("@PER", -1);
                command.Parameters.AddWithValue("@PYP", pyp);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model.pry_id = (int)reader["pry_id"];
                    model.nombre_proyecto = (string)reader["nombre_proyecto"];
                    model.inicio_ejecucion = (DateTime)reader["inicio_ejecucion"];
                    model.termino_ejecucion = (DateTime)reader["termino_ejecucion"];
                    model.nombre_sede = (string)reader["nombre_sede"];
                    model.nombre_linea = (string)reader["nombre_linea"];
                    model.nombre_area = (string)reader["nombre_area"];
                    model.modalidad_proyecto = (string)reader["modalidad_proyecto"];
                    model.nombre_estado_proyecto = (string)reader["nombre_estado_proyecto"];
                    model.pyp_id = (int)reader["pyp_id"];
                    model.rutdv = (string)reader["rutdv"];
                    model.nombre_sede_sigla = (string)reader["nombre_sigla_sede"];
                    model.nombre_linea_sigla = (string)reader["nombre_sigla_linea"];
                    model.nombre_area_sigla = (string)reader["nombre_sigla_area"];
                    switch ((int)reader["mod_proyecto"])
                    {
                        case 1:
                            model.clase_label_modalidad = "primary";
                            break;
                        case 2:
                        case 5:
                            model.clase_label_modalidad = "success";
                            break;
                        case 3:
                            model.clase_label_modalidad = "warning";
                            break;
                        case 4:
                            model.clase_label_modalidad = "info";
                            break;
                        default:
                            model.clase_label_modalidad = "default";
                            break;
                    }
                    switch ((int)reader["epr_id"])
                    {
                        case 1:
                            model.clase_label_estado_proyecto = "default";
                            break;
                        case 2:
                        case 3:
                        case 4:
                        case 7:
                            model.clase_label_estado_proyecto = "warning";
                            break;
                        case 5:
                        case 8:
                            model.clase_label_estado_proyecto = "success";
                            break;
                        case 6:
                            model.clase_label_estado_proyecto = "info";
                            break;
                        case 9:
                            model.clase_label_estado_proyecto = "danger";
                            break;
                        default:
                            model.clase_label_estado_proyecto = "default";
                            break;
                    }
                    model.pov_id = (int)reader["pov_id"];
                    model.emp_id = (int)reader["emp_id"];
                    model.per_id = (int)reader["per_id"];
                }
                reader.Close();
            }
            return model;
        }
    }
    public class ProyectosParticipantesModel
    {
        public int pry_id { get; set; }
        public string nombre_proyecto { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime inicio_ejecucion { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime termino_ejecucion { get; set; }
        public string nombre_sede { get; set; }
        public string nombre_sede_sigla { get; set; }
        public string nombre_linea { get; set; }
        public string nombre_linea_sigla { get; set; }
        public string nombre_area { get; set; }
        public string nombre_area_sigla { get; set; }
        public string modalidad_proyecto { get; set; }
        public string nombre_estado_proyecto { get; set; }
        public string clase_label_estado_proyecto { get; set; }
        public string clase_label_modalidad { get; set; }
        public int pyp_id { get; set; }
        public string rutdv { get; set; }
        public int pov_id { get; set; }
        public int emp_id { get; set; }
        public int per_id { get; set; }
    }
    public class vista_participante
    {
        public int per_id { get; set; }
        public IEnumerable<ProyectosParticipantesModel> ProyectosParticipantesModel { get; set; }
        public PersonaModel PersonaModel { get; set; }
        public EmpresaModel EmpresaModel { get; set; }
        public ProyectoModel ProyectoModel { get; set; }
        public ProyectosCursosModel ProyectosCursosModel { get; set; }
        public ProyectosCursosParticipanteModel ProyectosCursosParticipanteModel { get; set; }
        public IEnumerable<ProyectosCursosHorariosModel> ProyectosCursosHorariosModel { get; set; }
        public IEnumerable<ProyectosCursosEvaluacionesModel> ProyectosCursosEvaluacionesModel { get; set; }
        public CursoModel CursoModel { get; set; }
        public ProyectosParticipantesModel proyectosParticipantes { get; set; }
        public IEnumerable<EtiquetaModel> EtiquetaModel { get; set; }
        public IEnumerable<ObservacionModel> ObservacionModel { get; set; }

    }


}