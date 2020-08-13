using MesaAyudaCEIM5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;

namespace MesaAyudaCEIM5.Controllers
{
    public class ProyectosController : Controller
    {
        // GET: Proyectos
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProximosProyectos()
        {
            vistaProximosProyectos myVista = new vistaProximosProyectos();

            IEnumerable<ProyectoModel> myProximosProyectos = new Proyectos().BuscaProximosProyectos();


            myVista.ProyectoModels = myProximosProyectos;

            return View(myVista);
        }
        public ActionResult Proyectos(int pry)
        {
            vistaProximosProyectos myVista = new vistaProximosProyectos();
            ProyectoModel myProyecto = new Proyectos().BuscaProyectoPorPry(pry);
            ProyectosCursosModel myProyectoCurso = new Proyectos().BuscaProyectosCursosPorPry(pry);
            CursoModel myCurso = new Cursos().BuscaCursoPorCur(myProyectoCurso.cur_id);
            IEnumerable<ProyectoParticipantes> myProyectoParticipantes = new Proyectos().BuscarProyectoParticipantes(pry);


            myVista.ProyectoModel = myProyecto;
            myVista.ProyectosCursosModel = myProyectoCurso;
            myVista.CursoModel = myCurso;
            myVista.ProyectoParticipantes = myProyectoParticipantes;

            return View(myVista);
        }
        public ActionResult EnvioInformeProactivo(int pry)
        {

            Proyectos myProyecto = new Proyectos();
            if (myProyecto.EnviarInformeProactivo(pry))
            {
                ViewBag.Message = "Infome Proactivo Enviado";
            }
            return Redirect("/Proyectos/Proyectos/?pry=" + pry.ToString()); 
        }
        public ActionResult ProyectoInformeProactivo(int pry)
        {
            vistaProximosProyectos myVista = new vistaProximosProyectos();
            ProyectoModel myProyecto = new Proyectos().BuscaProyectoPorPry(pry);
            ProyectosCursosModel myProyectoCurso = new Proyectos().BuscaProyectosCursosPorPry(pry);
            CursoModel myCurso = new Cursos().BuscaCursoPorCur(myProyectoCurso.cur_id);
            IEnumerable<ProyectoParticipantesContacto> myProyectoParticipantesContacto = new Proyectos().BuscarProyectoParticipantesContacto(pry);


            myVista.ProyectoModel = myProyecto;
            myVista.ProyectosCursosModel = myProyectoCurso;
            myVista.CursoModel = myCurso;
            myVista.ProyectoParticipantesContacto = myProyectoParticipantesContacto;

            return View(myVista);
        }
        public ActionResult InformeProactivo(int pry)
        {
            vistaProximosProyectos myVista = new vistaProximosProyectos();
            ProyectoModel myProyecto = new Proyectos().BuscaProyectoPorPry(pry);
            ProyectosCursosModel myProyectoCurso = new Proyectos().BuscaProyectosCursosPorPry(pry);
            CursoModel myCurso = new Cursos().BuscaCursoPorCur(myProyectoCurso.cur_id);
            IEnumerable<ProyectoParticipantesContacto> myProyectoParticipantesContacto = new Proyectos().BuscarProyectoParticipantesContacto(pry);


            myVista.ProyectoModel = myProyecto;
            myVista.ProyectosCursosModel = myProyectoCurso;
            myVista.CursoModel = myCurso;
            myVista.ProyectoParticipantesContacto = myProyectoParticipantesContacto;

            return View(myVista);
        }
        [HttpGet]
        public ActionResult ContactoParticipante(int pyp)
        {
            vistaContactoParticipante myVista = new vistaContactoParticipante();
            ProyectosParticipantesModel myProyectoParticipante = new ProyectosParticipantes().BuscarPorPyp(pyp);
            ProyectoModel myProyecto = new Proyectos().BuscaProyectoPorPry(myProyectoParticipante.pry_id);
            PersonaModel myPersona = new Personas().BuscaPersonaPorPer(myProyectoParticipante.per_id);
            ContactoParticipanteModel myContacto = new ContactoParticipante().BuscaPorPyp(pyp);
            IEnumerable<ComunaModel> myComunas = new Comunas().BuscarTodas();

            myVista.PersonaModel = myPersona;
            myVista.ProyectoModel = myProyecto;
            myVista.ContactoParticipanteModel = myContacto;
            myVista.ComunaModels = myComunas;

            return View(myVista);
        }
        [HttpPost]
        [ActionName("ContactoParticipante")]
        public ActionResult ContactoParticipanteForm(vistaContactoParticipante myVis)
        {
            if (ModelState.IsValid)
            {
                int pyp = myVis.ContactoParticipanteModel.pyp_id;
                myVis.ContactoParticipanteModel.usr_id = (int)Session["UserID"];
                ContactoParticipante myCon = new ContactoParticipante();
                if (myCon.Agregar(myVis.ContactoParticipanteModel))
                {
                    ViewBag.Message = "Reistro de Observación";
                }

                ContactoParticipanteModel myContacto = new ContactoParticipante().BuscaPorPyp(pyp);
                ProyectosParticipantesModel myProyectoParticipante = new ProyectosParticipantes().BuscarPorPyp(pyp);
                ProyectoModel myProyecto = new Proyectos().BuscaProyectoPorPry(myProyectoParticipante.pry_id);
                PersonaModel myPersona = new Personas().BuscaPersonaPorPer(myProyectoParticipante.per_id);
                vistaContactoParticipante myVista = new vistaContactoParticipante();

                myVista.PersonaModel = myPersona;
                myVista.ProyectoModel = myProyecto;
                myVista.ContactoParticipanteModel = myContacto;

                return View(myVista);
            }
            return View();
     
        }
        [HttpPost]
        //[ActionName("DatosContactoParticipante")]
        public ActionResult DatosContactoParticipante(vistaContactoParticipante myVis)
        {
            if (ModelState.IsValid)
            {
                int pyp = myVis.ContactoParticipanteModel.pyp_id;
                myVis.ContactoParticipanteModel.usr_id = (int)Session["UserID"];
                Personas myPers = new Personas();
                if (myPers.ActualizaCorreo(myVis.PersonaModel))
                {
                    ViewBag.Message = "Reistro actualizado";
                }

                ContactoParticipanteModel myContacto = new ContactoParticipante().BuscaPorPyp(pyp);
                ProyectosParticipantesModel myProyectoParticipante = new ProyectosParticipantes().BuscarPorPyp(pyp);
                ProyectoModel myProyecto = new Proyectos().BuscaProyectoPorPry(myProyectoParticipante.pry_id);
                PersonaModel myPersona = new Personas().BuscaPersonaPorPer(myProyectoParticipante.per_id);
                vistaContactoParticipante myVista = new vistaContactoParticipante();

                myVista.PersonaModel = myPersona;
                myVista.ProyectoModel = myProyecto;
                myVista.ContactoParticipanteModel = myContacto;

                //return View(myVista);
                return Redirect("/Proyectos/ContactoParticipante/?pyp=" + pyp.ToString());
                //return View("~/Views/Proyectos/ContactoParticipante.cshtml", myVis);

            }
            return View();
            //return View("~/Views/Proyectos/ContactoParticipante.cshtml", myVis);

        }
    }
}