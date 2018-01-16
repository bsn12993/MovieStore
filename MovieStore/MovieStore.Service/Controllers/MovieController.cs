using MovieStore.Data.Domain;
using MovieStore.Repository.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace MovieStore.Service.Controllers
{
    /// <summary>
    /// Desarrollador: Bryan Silverio Nieves
    /// Fecha: 15/01/2018
    /// ApiController que se comunicara a MovieStore.Repository para usar los metodos de las operaciones de las base de datos
    /// </summary>
    public class MovieController : ApiController
    {
        // GET
        // api/Movie
        /// <summary>
        /// Metodo Get que recupera un listado de peliculas ordenadas por la fecha de lanzamiento
        /// </summary>
        /// <returns>devuelve una respuesta de tipo Http con la respuesta</returns>
        public IHttpActionResult Get()
        {
            var _service = new MovieRepository().getAllMovies().OrderBy(x => x.ReleaseDate);
            if (_service != null)
                return Ok(_service);
            else
                return BadRequest("No se encontraron Resultadoas");
        }
        // GET
        // api/Movie?title={title}
        /// <summary>
        /// Metodo Get con parametro title para filtrar pelicual en base a un titulo determinado
        /// </summary>
        /// <param name="title">titulo de una pelicula a buscar</param>
        /// <returns>>devuelve una respuesta de tipo Http con la respuesta</returns>
        public IHttpActionResult Get(string title)
        {
            var _service = new MovieRepository().getMovieByTitle(title);
            if (_service != null)
                return Ok(_service);
            else
                return BadRequest("No se encontraron Resultados");
        }
        // GET
        // api/Movie?title={title}&director={director}
        /// <summary>
        /// Metodo Get con parametros title y director para validar la existencia de un registro con estos datos
        /// </summary>
        /// <param name="title">titulo de pelicula</param>
        /// <param name="director">director de la pelicula</param>
        /// <returns>devuelve una respuesta de tipo Http con la respuesta</returns>
        public JsonResult Get(string title,string director)
        {
            var _service = new MovieRepository().ValidateMovie(title, director);
            var json = new JsonResult();
            if (_service.existe == 0)
            {
                json.Data = new { existe = _service.existe, msg = _service.mensaje };
                return json;
            }
            else
            {
                json.Data = new { existe = _service.existe, msg = _service.mensaje };
                return json;
            }          
        }
        // PUT
        // api/Movie
        /// <summary>
        /// metodo para actulizar un registro
        /// </summary>
        /// <param name="modelo">datos a actualizar  en un modelo</param>
        /// <returns>devuelve una respuesta de tipo Http con la respuesta</returns>
        public IHttpActionResult Put(Movie modelo)
        {
            var _service = new MovieRepository().putMovie(modelo);
            if (_service)
                return Ok(_service);
            else
                return BadRequest("Ocurrio un poblema al ejecutar la operacion");
        }
        // DELETE
        // api/Movie?id={id}
        /// <summary>
        /// Metodo para eliminar un registro en base a su id
        /// </summary>
        /// <param name="id">id del registro a eliminar</param>
        /// <returns>devuelve una respuesta de tipo Http con la respuesta</returns>
        public IHttpActionResult Delete(int id)
        {
            var _service = new MovieRepository().deleteMovie(id);
            if (_service)
                return Ok();
            else
                return BadRequest("Ocurrio un poblema al ejecutar la operacion");
        }
        // POST
        // api/Movie
        /// <summary>
        /// Metodo para registrar una nueva pelicula
        /// </summary>
        /// <param name="modelo">datos a insertar en almacenados en un modelo</param>
        /// <returns>devuelve una respuesta de tipo Http con la respuesta</returns>
        public IHttpActionResult Post(Movie modelo)
        {
            var _service = new MovieRepository().PostMovie(modelo);
            if(_service)
                return Ok();
            else
                return BadRequest("Ocurrio un poblema al ejecutar la operacion");
        }
    }
}
