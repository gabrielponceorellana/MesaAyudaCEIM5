using Microsoft.Ajax.Utilities;
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
    public class Proyectos
    {
        
        public string connectionString = ConfigurationManager.ConnectionStrings["DBMesaAyuda"].ConnectionString;
        public ProyectoModel BuscaProyectoPorPry(int pry)
        {
            var model = new ProyectoModel();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC PROYECTOS_SEL @ACCION, @PRY";
                command.Parameters.AddWithValue("@ACCION", 1);
                command.Parameters.AddWithValue("@PRY", pry);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model.pry_id = (int)reader["pry_id"];
                    model.nombre_proyecto = (string)reader["nombre_proyecto"];
                    model.nombre_codigo_producto = (string)reader["codigo_sap_producto"];
                    model.ins_id = (int)reader["ins_id"];
                    model.nombre_institucion = (string)reader["nombre_institucion"];
                    model.sed_id = (int)reader["sed_id"];
                    model.lin_id = (int)reader["lin_id"];
                    model.are_id = (int)reader["are_id"];
                    model.nombre_sede = (string)reader["nombre_sede"];
                    model.nombre_linea = (string)reader["nombre_linea"];
                    model.nombre_area = (string)reader["nombre_area"];
                    model.nombre_sede_sigla = (string)reader["nombre_sigla_sede"];
                    model.nombre_linea_sigla = (string)reader["nombre_sigla_linea"];
                    model.nombre_area_sigla = (string)reader["nombre_sigla_area"];
                    model.mod_id = (int)reader["mod_proyecto"];
                    model.nombre_modalidad_proyecto = (string)reader["modalidad_proyecto"];
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
                    model.epr_id = (int)reader["epr_id"];
                    model.nombre_estado_proyecto = (string)reader["nombre_estado_proyecto"];
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
                    model.inicio_ejecucion = (DateTime)reader["inicio_ejecucion"];
                    model.termino_ejecucion = (DateTime)reader["termino_ejecucion"];
                    model.nombre_persona_usuario = (string)reader["nombre_persona_usuario"];
                    model.hora_inicio = (string)reader["hora_inicio"];
                }
                reader.Close();
                connection.Close();
            }
            return model;
        }
        public ProyectosCursosModel BuscaProyectosCursosPorPry(int pry)
        {
            var model = new ProyectosCursosModel();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC PROYECTOS_SEL @ACCION, @PRY";
                command.Parameters.AddWithValue("@ACCION", 2);
                command.Parameters.AddWithValue("@PRY", pry);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model.pyc_id = (int)reader["pyc_id"];
                    model.pry_id = (int)reader["pry_id"];
                    model.cur_id = (int)reader["cur_id"];
                    model.lej_id = (int)reader["lej_id"];
                    model.nombre_lujar_ejecucion = (string)reader["nombre_lugar_ejecucion"];
                    model.nombre_direccion_ejecucion = (string)reader["direccion_ejecucion"];
                    model.cmn_id_ejecucion = (int)reader["cmn_id_ejecucion"];
                    model.nombre_comuna_ejecucion = (string)reader["nombre_comuna_ejecucion"];
                    model.asistencia_minima = (int)reader["asistencia_minima"];
                    model.nota_minima = (int)reader["nota_minima"];
                }
                reader.Close();
                connection.Close();
            }
            return model;
        }
        public ProyectosCursosParticipanteModel BuscaProyectosCursosParticipantePorPyp(int pyp)
        {
            var model = new ProyectosCursosParticipanteModel();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC PROYECTOS_SEL @ACCION, @PRY, @PYP";
                command.Parameters.AddWithValue("@ACCION", 3);
                command.Parameters.AddWithValue("@PRY", -1);
                command.Parameters.AddWithValue("@PYP", pyp);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model.pyp_id = (int)reader["pyp_id"];
                    model.id_accion_sence = (int)reader["id_accion_sence"];
                    model.asistencia_final = (int)reader["asistencia_final"];
                    model.evaluacion_final = (int)reader["evaluacion_final"];
                    model.sit_id = (int)reader["sit_id"];
                    if (model.sit_id == 1)
                    {
                        model.situcion_final = "APROBADO";
                    } else
                    {
                        model.situcion_final = "REPROBADO";
                    }
                    model.usuario_classroom = (string)reader["usuario_classroom"];
                }
                reader.Close();
                connection.Close();
            }
            return model;
        }
        public IEnumerable<ProyectosCursosHorariosModel> BuscaProyectosCursosHorariosPorPry(int pry, int pyp)
        {
            List<ProyectosCursosHorariosModel> myModel = new List<ProyectosCursosHorariosModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC PROYECTOS_SEL @ACCION, @PRY, @PYP";
                command.Parameters.AddWithValue("@ACCION", 4);
                command.Parameters.AddWithValue("@PRY", pry);
                command.Parameters.AddWithValue("@PYP", pyp);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProyectosCursosHorariosModel model = new ProyectosCursosHorariosModel();

                    model.pch_id = (int)reader["pch_id"];
                    model.pyc_id = (int)reader["pyc_id"];
                    model.dia = (DateTime)reader["dia"];
                    model.desde = (string)reader["desde"];
                    model.hasta = (string)reader["hasta"];
                    model.ins_id = (int)reader["ins_id"];
                    model.nombre_instructor = (string)reader["nombre_instructor"];
                    model.asistencia = (int)reader["asistencia"];
                    if (model.asistencia == 1)
                    {
                        model.asistencia_icono = "glyphicon glyphicon-ok-circle";
                    } else
                    {
                        model.asistencia_icono = "glyphicon glyphicon-remove-circle";
                    }
                    myModel.Add(model);
                }
                reader.Close();
                connection.Close();
            }
            return myModel;
        }
        public IEnumerable<ProyectosCursosEvaluacionesModel> BuscaProyectosCursosEvaluacionesPorPyp(int pyp)
        {
            List<ProyectosCursosEvaluacionesModel> myModelo = new List<ProyectosCursosEvaluacionesModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC PROYECTOS_SEL @ACCION, @PRY, @PYP";
                command.Parameters.AddWithValue("@ACCION", 5);
                command.Parameters.AddWithValue("@PRY", -1);
                command.Parameters.AddWithValue("@PYP", pyp);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProyectosCursosEvaluacionesModel model = new ProyectosCursosEvaluacionesModel();

                    model.pce_id = (int)reader["pce_id"];
                    model.nota = (int)reader["nota"];
                    model.nombre_plan_evaluacion = (string)reader["nombre_plan_evaluacion"];
                    //model.porcentaje_plan_evalacion = (int)reader["porcentaje_plan_evalacion"];
                    myModelo.Add(model);
                }
                reader.Close();
                connection.Close();
            }
            return myModelo;
        }
        public IEnumerable<ProyectoModel> BuscaProximosProyectos()
        {
            List<ProyectoModel> myModelo = new List<ProyectoModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC PROYECTOS_SEL @ACCION, @PRY, @PYP";
                command.Parameters.AddWithValue("@ACCION", 6);
                command.Parameters.AddWithValue("@PRY", -1);
                command.Parameters.AddWithValue("@PYP", -1);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProyectoModel model = new ProyectoModel();

                    model.pry_id = (int)reader["pry_id"];
                    model.nombre_proyecto = (string)reader["nombre_proyecto"];
                    model.nombre_codigo_producto = (string)reader["codigo_sap_producto"];
                    model.ins_id = (int)reader["ins_id"];
                    model.nombre_institucion = (string)reader["nombre_institucion"];
                    model.sed_id = (int)reader["sed_id"];
                    model.lin_id = (int)reader["lin_id"];
                    model.are_id = (int)reader["are_id"];
                    model.nombre_sede = (string)reader["nombre_sede"];
                    model.nombre_linea = (string)reader["nombre_linea"];
                    model.nombre_area = (string)reader["nombre_area"];
                    model.nombre_sede_sigla = (string)reader["nombre_sigla_sede"];
                    model.nombre_linea_sigla = (string)reader["nombre_sigla_linea"];
                    model.nombre_area_sigla = (string)reader["nombre_sigla_area"];
                    model.mod_id = (int)reader["mod_proyecto"];
                    model.nombre_modalidad_proyecto = (string)reader["modalidad_proyecto"];
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
                    model.epr_id = (int)reader["epr_id"];
                    model.nombre_estado_proyecto = (string)reader["nombre_estado_proyecto"];
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
                    model.inicio_ejecucion = (DateTime)reader["inicio_ejecucion"];
                    model.termino_ejecucion = (DateTime)reader["termino_ejecucion"];
                    model.nombre_persona_usuario = (string)reader["nombre_persona_usuario"];
                    model.numero_participantes=(int)reader["numero_participantes"];
                    model.numero_participantes_contactado = (int)reader["numero_participantes_contactado"];
                    model.numero_participantes_bienvenida = (int)reader["numero_participantes_bienvenida"];
                    model.hora_inicio = (string)reader["hora_inicio"];
                    model.clase_tr = "";
                    if (model.numero_participantes_contactado > 0)
                    {
                        model.clase_tr = "warning";
                    } 
                    if (model.numero_participantes==model.numero_participantes_contactado)
                    {
                        model.clase_tr = "success";
                    }

                    myModelo.Add(model);
                }
                reader.Close();
                connection.Close();
            }
            return myModelo;
        }
        public IEnumerable<ProyectoParticipantes> BuscarProyectoParticipantes(int pry)
        {
            List<ProyectoParticipantes> myModelo = new List<ProyectoParticipantes>();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC PROYECTOS_SEL @ACCION, @PRY, @PYP";
                command.Parameters.AddWithValue("@ACCION", 7);
                command.Parameters.AddWithValue("@PRY", pry);
                command.Parameters.AddWithValue("@PYP", -1);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProyectoParticipantes model = new ProyectoParticipantes();

                    model.pyp_id = (int)reader["pyp_id"];
                    model.per_id = (int)reader["per_id"];
                    model.rut = (int)reader["rut"];
                    model.dv = (string)reader["dv"];
                    model.nombres = (string)reader["nombres"];
                    model.paterno = (string)reader["paterno"];
                    model.materno = (string)reader["materno"];
                    model.celular = (string)reader["celular"];
                    model.correo = (string)reader["correo"];
                    model.usuario = (string)reader["usuario"];
                    model.password = (string)reader["password"];
                    model.usuario_classroom = (string)reader["usuario_classroom"];
                    model.password_classroom = (string)reader["password_classroom"];
                    model.emp_id = (int)reader["emp_id"];
                    model.rutdv_empresa = (string)reader["rutdv_empresa"];
                    model.razon = (string)reader["razon"];
                    model.indicador_contacto = (int)reader["indicador_contacto"];
                    model.nombre_usuario_contacto = (string)reader["nombre_usuario_contacto"];
                    switch (model.indicador_contacto)
                    {
                        case 0:
                            model.clase_tr = "";
                            break;
                        case 1:
                            model.clase_tr = "success";
                            break;
                    }
                    myModelo.Add(model);
                }
                reader.Close();
                connection.Close();
            }
            return myModelo;
        }
        public IEnumerable<ProyectoParticipantesContacto> BuscarProyectoParticipantesContacto(int pry)
        {
            List<ProyectoParticipantesContacto> myModelo = new List<ProyectoParticipantesContacto>();
            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "EXEC PROYECTOS_SEL @ACCION, @PRY, @PYP";
                command.Parameters.AddWithValue("@ACCION", 8);
                command.Parameters.AddWithValue("@PRY", pry);
                command.Parameters.AddWithValue("@PYP", -1);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProyectoParticipantesContacto model = new ProyectoParticipantesContacto();

                    model.pyp_id = (int)reader["pyp_id"];
                    model.per_id = (int)reader["per_id"];
                    model.rut = (int)reader["rut"];
                    model.dv = (string)reader["dv"];
                    model.nombres = (string)reader["nombres"];
                    model.paterno = (string)reader["paterno"];
                    model.materno = (string)reader["materno"];
                    model.celular = (string)reader["celular"];
                    model.correo = (string)reader["correo"];
                    if (reader["nombre_usuario_contacto"] != DBNull.Value) {
                        model.nombre_usuario_contacto = (string)reader["nombre_usuario_contacto"];
                        
                    } else
                    {
                        model.nombre_usuario_contacto = "";
                    }
                        
                    model.fecha_contacto = (DateTime)reader["fecha_contacto"];
                    model.invitacion = (int)reader["invitacion"];
                    switch (model.invitacion)
                    {
                        case 1:
                            model.invitacion_respuesta = "SI";
                            model.invitacion_clase_span = "success";
                            break;
                        case 2:
                            model.invitacion_respuesta = "NO";
                            model.invitacion_clase_span = "danger";
                            break;
                    }
                    model.nivel_tecnologico = (int)reader["nivel_tecnologico"];
                    switch (model.nivel_tecnologico)
                    {
                        case 1:
                            model.nivel_tecnologico_respuesta = "Bajo";
                            model.nivel_tecnologico_clase_span = "danger";
                            break;
                        case 2:
                            model.nivel_tecnologico_respuesta = "Medio";
                            model.nivel_tecnologico_clase_span = "warning";
                            break;
                        case 3:
                            model.nivel_tecnologico_respuesta = "Alto";
                            model.nivel_tecnologico_clase_span = "success";
                            break;
                    }
                    model.requiere_asistencia = (int)reader["requiere_asistencia"];
                    switch (model.requiere_asistencia)
                    {
                        case 1:
                            model.requiere_asistencia_respuesta = "SI";
                            model.requiere_asistencia_clase_span = "danger";
                            break;
                        case 2:
                            model.requiere_asistencia_respuesta = "NO";
                            model.requiere_asistencia_clase_span = "success";
                            break;
                    }
                    model.tipo_dispositivo = (int)reader["tipo_dispositivo"];
                    switch (model.tipo_dispositivo)
                    {
                        case 1:
                            model.tipo_dispositivo_respuesta = "Computador";
                            model.tipo_dispositivo_clase_span = "success";
                            break;
                        case 2:
                            model.tipo_dispositivo_respuesta = "Tablet";
                            model.tipo_dispositivo_clase_span = "warning";
                            break;
                        case 3:
                            model.tipo_dispositivo_respuesta = "Celular";
                            model.tipo_dispositivo_clase_span = "danger";
                            break;
                    }
                    model.tipo_conexion = (int)reader["tipo_conexion"];
                    switch (model.tipo_conexion)
                    {
                        case 1:
                            model.tipo_conexion_respuesta = "Casa Red Fija";
                            model.tipo_conexion_clase_span = "success";
                            break;
                        case 2:
                            model.tipo_conexion_respuesta = "Red Movil";
                            model.tipo_conexion_clase_span = "danger";
                            break;
                        case 3:
                            model.tipo_conexion_respuesta = "Trabajo";
                            model.tipo_conexion_clase_span = "warning";
                            break;
                        case 4:
                            model.tipo_conexion_respuesta = "Ciber";
                            model.tipo_conexion_clase_span = "warning";
                            break;
                    }
                    model.dispositivos_externos = (int)reader["dispositivos_externos"];
                    switch (model.dispositivos_externos)
                    {
                        case 1:
                            model.dispositivos_externos_respuesta = "SI";
                            model.dispositivos_externos_clase_span = "success";
                            break;
                        case 2:
                            model.dispositivos_externos_respuesta = "NO";
                            model.dispositivos_externos_clase_span = "danger";
                            break;
                    }
                    model.cmn_id_conexion = (int)reader["cmn_id_conexion"];
                    model.observacion = (string)reader["observacion_contacto"];
                    myModelo.Add(model);
                }
                reader.Close();
                connection.Close();
            }
            return myModelo;
        }
        public bool EnviarInformeProactivo(int pry)
        {
            SqlConnection connection = null;

            connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("CONTACTO_PARTICIPANTE_ACT", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ACCION", 2);
            command.Parameters.AddWithValue("@PRY", pry);
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();

            if (i >= 1)
                return true;
            else
                return false;

        }
    }
    public class ProyectoModel
    {
        public int pry_id { get; set; }
        public string nombre_proyecto { get; set; }
        public string nombre_codigo_producto { get; set; }
        public int ins_id { get; set; }
        public string nombre_institucion { get; set; }
        public int sed_id { get; set; }
        public string nombre_sede { get; set; }
        public string nombre_sede_sigla { get; set; }
        public int lin_id { get; set; }
        public string nombre_linea { get; set; }
        public string nombre_linea_sigla { get; set; }
        public int are_id { get; set; }
        public string nombre_area { get; set; }
        public string nombre_area_sigla { get; set; }
        public int epr_id { get; set; }
        public string nombre_estado_proyecto { get; set; }
        public string clase_label_estado_proyecto { get; set; }
        public int mod_id { get; set; }
        public string nombre_modalidad_proyecto { get; set; }
        public string clase_label_modalidad { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime inicio_ejecucion { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime termino_ejecucion { get; set; }
        public string nombre_persona_usuario { get; set; }
        public int numero_participantes { get; set; }
        public int numero_participantes_bienvenida { get; set; }
        public int numero_participantes_contactado { get; set; }
        public string clase_tr { get; set; }
        public string hora_inicio { get; set; }
    }
    public class ProyectosCursosModel
    {
        public int pyc_id { get; set; }
        public int pry_id { get; set; }
        public int cur_id { get; set; }
        public int lej_id { get; set; }
        public string nombre_lujar_ejecucion { get; set; }
        public string nombre_direccion_ejecucion { get; set; }
        public int cmn_id_ejecucion { get; set; }
        public string nombre_comuna_ejecucion { get; set; }
        public int nota_minima { get; set; }
        public int asistencia_minima { get; set; }
    }
    public class ProyectosCursosParticipanteModel
    {
        public int pyp_id { get; set; }
        public int id_accion_sence { get; set; }
        public int asistencia_final { get; set; }
        public int evaluacion_final { get; set; }
        public int sit_id { get; set; }
        public string situcion_final { get; set; }
        public string usuario_classroom { get; set; }
        public string password_classroom { get; set; }
    }
    public class ProyectosCursosHorariosModel
    {
        public int pch_id { get; set; }
        public int pyc_id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime dia { get; set; }
        public string desde { get; set; }
        public string hasta { get; set; }
        public int ins_id { get; set; }
        public string nombre_instructor { get; set; }
        public int asistencia { get; set; }
        public string asistencia_icono { get; set; }

    }
    public class ProyectosCursosEvaluacionesModel
    {
        public int pce_id { get; set; }
        public int nota { get; set; }
        public string nombre_plan_evaluacion { get; set; }
        public int porcentaje_plan_evalacion { get; set; }
    }
    public class ProyectoParticipantes
    {
        public int pyp_id { get; set; }
        public int per_id { get; set; }
        public int rut { get; set; }
        public string dv { get; set; }
        public string nombres { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string celular { get; set; }
        public string correo { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string usuario_classroom { get; set; }
        public string password_classroom { get; set; }
        public int emp_id { get; set; }
        public string rutdv_empresa { get; set; }
        public string razon { get; set; }
        public int indicador_contacto { get; set; }
        public string nombre_usuario_contacto { get; set; }
        public string clase_tr { get; set; }
    }
    public class ProyectoParticipantesContacto
    {
        public int pyp_id { get; set; }
        public int per_id { get; set; }
        public int rut { get; set; }
        public string dv { get; set; }
        public string nombres { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string celular { get; set; }
        public string correo { get; set; }
        public string nombre_usuario_contacto { get; set; }
        public DateTime fecha_contacto { get; set; }
        public int invitacion { get; set; }
        public string invitacion_respuesta { get; set; }
        public string invitacion_clase_span { get; set; }
        public int nivel_tecnologico { get; set; }
        public string nivel_tecnologico_respuesta { get; set; }
        public string nivel_tecnologico_clase_span { get; set; }
        public int requiere_asistencia { get; set; }
        public string requiere_asistencia_respuesta { get; set; }
        public string requiere_asistencia_clase_span { get; set; }
        public int tipo_dispositivo { get; set; }
        public string tipo_dispositivo_respuesta { get; set; }
        public string tipo_dispositivo_clase_span { get; set; }
        public int tipo_conexion { get; set; }
        public string tipo_conexion_respuesta { get; set; }
        public string tipo_conexion_clase_span { get; set; }
        public int cmn_id_conexion { get; set; }
        public int dispositivos_externos { get; set; }
        public string dispositivos_externos_respuesta { get; set; }
        public string dispositivos_externos_clase_span { get; set; }
        public string observacion { get; set; }
    }
    public class vistaProximosProyectos
    {
        public IEnumerable<ProyectoModel> ProyectoModels { get; set; }
        public ProyectoModel ProyectoModel { get; set; }
        public ProyectosCursosModel ProyectosCursosModel { get; set; }
        public CursoModel CursoModel { get; set; }
        public IEnumerable<ProyectoParticipantes> ProyectoParticipantes { get; set; }
        public IEnumerable<ProyectoParticipantesContacto> ProyectoParticipantesContacto { get; set; }
    }
    public class vistaContactoParticipante
    {
        public ProyectoModel ProyectoModel { get; set; }
        public PersonaModel PersonaModel { get; set; }
        public ContactoParticipanteModel ContactoParticipanteModel { get; set; }
        public IEnumerable<ComunaModel> ComunaModels { get; set; }
    }
}