
using MovieStore.Web.Models;
using MovieStore.Web.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Web.Controllers
{
    /// <summary>
    /// Desarrollador: Bryan Silverio Nieves
    /// Fecha: 15/01/2018
    /// Controller que utiliza los metodos del cliente MovieServices para consumir los metodos de la capa MovieStoreService
    /// </summary>
    public class MovieStoreController : Controller
    {
        MovieService _movieService = new MovieService();

        // GET: MovieStore
        public ActionResult Index()
        {
            var respuesta = _movieService.GetMovies();
            if (respuesta != null)
            {
                return View(respuesta);
            }
            else
            {
                ViewBag.resultado = "No se encontraron resultado";
                return View(new List<Movie>());
            }
        }

        // GET: MovieStore/Details/5
        public ActionResult Details(string title)
        {
            if (title != string.Empty && title != null)
            {
                var respuesta = _movieService.GetMoviesByTitle(title);
                if (respuesta != null)
                {
                    return View(respuesta);
                }
                else
                {
                    ViewBag.resultado = "No se encontraron resultado";
                    return View("Details",null);
                }
            }
            return View("Details", null);
        }

        // GET: MovieStore/Create
        public ActionResult Create()
        {
            return View(new Movie());
        }

        // POST: MovieStore/Create
        [HttpPost]
        public ActionResult Create(Movie model)
        {
            if (ModelState.IsValid)
            {
                var _validateService = _movieService.ValidateMovies(model);
                if (_validateService != null)
                {
                    if (_validateService.existe == 0)
                    {
                        var _service = _movieService.PostMovie(model);
                        if (_service) return RedirectToAction("index");
                        else
                        {
                            @ViewBag.hidden = "hidden";
                            ViewBag.respuesta = "";
                            return View("Create", model);
                        }
                    }
                    else
                    {
                        ViewBag.respuesta = _validateService.mensaje;
                        @ViewBag.hidden = "";
                    }
                }
                
                return View("Create", model);               
            }
            return View("Create", model);
        }

        // GET: MovieStore/Edit/5
        public ActionResult Edit(int id, string title, DateTime releaseDate, string director)
        {
            var model = new Movie();
            model.Id = id;
            model.Title = title;
            model.ReleaseDate = releaseDate;
            model.Director = director;
            return View(model);
        }

        // POST: MovieStore/Edit/5
        [HttpPost]
        public ActionResult Edit(Movie model)
        {
            try
            {
                var respuesta = _movieService.PutMovies(model);
                if (respuesta) return RedirectToAction("Index");
                else return View("Edit", model);
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieStore/Delete/5
        public ActionResult Delete(int id)
        {
            var respuesta = _movieService.DeleteMovies(id);
            if (respuesta) return RedirectToAction("Index");
            else return RedirectToAction("Index");
        }

        // POST: MovieStore/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // Cambio de Idioma en Etiquetas de la capa presentacion
        public ActionResult ChangeLanguage(string idioma)
        {
            if (idioma != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(idioma);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(idioma);
            }
            HttpCookie cookie = new HttpCookie("idioma");
            cookie.Value = idioma;
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }

    }
}
