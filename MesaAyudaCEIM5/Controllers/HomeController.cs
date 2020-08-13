using Antlr.Runtime.Tree;
using MesaAyudaCEIM5.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace MesaAyudaCEIM5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UsuarioModel login)
        {
            if (ModelState.IsValid)
            {
                UsuarioModel modelo = new Usuarios().BuscaUsuario(login.Usuario, login.Password);
                if (modelo.MesajeError.IsEmpty())
                {
                    Session["UserName"] = modelo.Usuario;
                    Session["UserID"] = modelo.Usr;
                    Session["Nombres"] = modelo.NombreCompleto;
                    return Redirect("/Home/Principal");
                } else
                {
                    return View(modelo);
                }
            }
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Principal()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Principal(string id)
        {
            vista_participante miVistaParticipante = new vista_participante();

            PersonaModel myPersona = new Personas().BuscaPersonaPorRutdv(id);

            IEnumerable<ProyectosParticipantesModel> myProyectoParticipante = new Models.ProyectosParticipantes().BuscaProyectosParticipante(myPersona.per_id);
            IEnumerable<EtiquetaModel> myEtiquetas = new Etiquetas().BuscaTodasEtiquetas();

            

            ObservacionModel myObs = new ObservacionModel();
            myObs.observacion = "Busqueda del Participante " + myPersona.nombres + " " + myPersona.paterno;
            myObs.usr_id = (int)Session["UserID"];
            myObs.per_id = myPersona.per_id;
            Observaciones myObservacion = new Observaciones();
            if (myObservacion.Agregar(myObs))
            {
                ViewBag.Message = "Reistro de Observación";
            }
            IEnumerable<ObservacionModel> myListadoObservaciones = new Observaciones().ListadoObservaciones(myObs.per_id);

            miVistaParticipante.PersonaModel = myPersona;
            miVistaParticipante.EtiquetaModel = myEtiquetas;
            miVistaParticipante.ProyectosParticipantesModel = (IEnumerable<ProyectosParticipantesModel>)myProyectoParticipante;
            miVistaParticipante.ObservacionModel = myListadoObservaciones;

            return View(miVistaParticipante);
        }
        [HttpGet]
        public ActionResult Volver(string id)
        {
            vista_participante miVistaParticipante = new vista_participante();

            PersonaModel myPersona = new Personas().BuscaPersonaPorRutdv(id);

            IEnumerable<ProyectosParticipantesModel> myProyectoParticipante = new Models.ProyectosParticipantes().BuscaProyectosParticipante(myPersona.per_id);
            IEnumerable<EtiquetaModel> myEtiquetas = new Etiquetas().BuscaTodasEtiquetas();
            IEnumerable<ObservacionModel> myListadoObservaciones = new Observaciones().ListadoObservaciones(myPersona.per_id);

            miVistaParticipante.PersonaModel = myPersona;
            miVistaParticipante.EtiquetaModel = myEtiquetas;
            miVistaParticipante.ProyectosParticipantesModel = (IEnumerable<ProyectosParticipantesModel>)myProyectoParticipante;
            miVistaParticipante.ObservacionModel = myListadoObservaciones;

            return View("~/Views/Home/Principal.cshtml",miVistaParticipante);
        }
        public ActionResult Salir()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

    }
}