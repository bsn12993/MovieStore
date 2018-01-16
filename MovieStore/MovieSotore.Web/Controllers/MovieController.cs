
using MovieStore.Web.Models;
using MovieStore.Web.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Web.Controllers
{
    public class MovieController : Controller
    {
        MovieService _movieService = new MovieService();

        // GET: Movie
        public ActionResult Index()
        {
            var respuesta = _movieService.GetMovies();
            if (respuesta != null)
            {
                return View(respuesta);
            }
            else
            {
                return View(new List<Movie>());
            }
        }

        public PartialViewResult SearchMovie(string title)
        {
            if (title != string.Empty && title != null)
            {
                var respuesta = _movieService.GetMoviesByTitle(title);
                if (respuesta != null)
                {
                    return PartialView(respuesta);
                }
            }
            return PartialView(null);
        }

        public ActionResult Edit(int id, string title, DateTime releaseDate, string director)
        {
            var model = new Movie();
            model.Id = id;
            model.Title = title;
            model.ReleaseDate = releaseDate;
            model.Director = director;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditMovie(Movie model)
        {
            var respuesta = _movieService.PutMovies(model);
            if (respuesta) return RedirectToAction("Index");
            else return View("Edit", model);
        }
        public ActionResult DeleteMovie(int id)
        {
            var respuesta = _movieService.DeleteMovies(id);
            if (respuesta) return RedirectToAction("Index");
            else return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new Movie());
        }

        [HttpPost]
        public ActionResult CreateMovie(Movie modelo)
        {
            if (ModelState.IsValid)
            {
                var _service = _movieService.PostMovie(modelo);
                if (_service) return RedirectToAction("index");
                else return View("Create", modelo);
            }
            return View("Create", modelo);
        }


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
            return View("Index");
        }

    }
}