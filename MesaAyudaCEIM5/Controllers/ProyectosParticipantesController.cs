using MesaAyudaCEIM5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MesaAyudaCEIM5.Controllers
{
    public class ProyectosParticipantesController : Controller
    {
        // GET: ProyectosParticipantes
        public ActionResult Listado(int pyp)
        {
            vista_participante miVistaParticipante = new vista_participante();
            ProyectosParticipantesModel myModel = new Models.ProyectosParticipantes().BuscarPorPyp(pyp);
            PersonaModel myPersona = new Personas().BuscaPersonaPorRutdv(myModel.rutdv);
            EmpresaModel myEmpresa = new Empresas().BuscaEmpresaPorEmp(myModel.emp_id);
            ProyectoModel myProyecto = new Proyectos().BuscaProyectoPorPry(myModel.pry_id);
            ProyectosCursosModel myProyectoCurso = new Proyectos().BuscaProyectosCursosPorPry(myModel.pry_id);
            ProyectosCursosParticipanteModel myProyectoParticipante = new Proyectos().BuscaProyectosCursosParticipantePorPyp(pyp);
            IEnumerable<ProyectosCursosHorariosModel> myProyectosCursosHorario = new Proyectos().BuscaProyectosCursosHorariosPorPry(myModel.pry_id, pyp);
            IEnumerable<ProyectosCursosEvaluacionesModel> myProyectosCursosEvaluaciones = new Proyectos().BuscaProyectosCursosEvaluacionesPorPyp(pyp);
            CursoModel myCurso = new Cursos().BuscaCursoPorCur(myProyectoCurso.cur_id);
            

            miVistaParticipante.PersonaModel = myPersona;
            miVistaParticipante.EmpresaModel = myEmpresa;
            miVistaParticipante.ProyectoModel = myProyecto;
            miVistaParticipante.ProyectosCursosModel = myProyectoCurso;
            miVistaParticipante.ProyectosCursosParticipanteModel = myProyectoParticipante;
            miVistaParticipante.ProyectosCursosHorariosModel = myProyectosCursosHorario;
            miVistaParticipante.ProyectosCursosEvaluacionesModel = myProyectosCursosEvaluaciones;
            miVistaParticipante.CursoModel = myCurso;
            
            miVistaParticipante.proyectosParticipantes = myModel;

            return View("~/Views/ProyectosParticipantes/Listado.cshtml",miVistaParticipante);
        }
    }
}