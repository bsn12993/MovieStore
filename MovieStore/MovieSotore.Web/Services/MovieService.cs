
using MovieStore.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace MovieStore.Web.Services
{
    /// <summary>
    /// Desarrollador: Bryan Silverio Nieves
    /// Fecha: 15/01/2018
    /// Clase utilizada como Cliente para el consumo del Web Services con los metodos GET, POST, PUT, DELETE
    /// </summary>
    public class MovieService
    {
        AppConfigService appService = new AppConfigService();

        /// <summary>
        /// Motodo para consumir metodo GET - Listado de peliculas
        /// </summary>
        /// <returns>Lista tipo Movies</returns>
        public List<Movie> GetMovies()
        {
            string resultado = string.Empty;
            List<Movie> lstMovies = null;
            HttpWebRequest request = null;  
            request = (HttpWebRequest)WebRequest.Create(appService.strUrlService);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.ContentLength = 0;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception("");
                else
                {
                    using(StreamReader reader=new StreamReader(response.GetResponseStream()))
                    {
                        resultado = reader.ReadToEnd();
                        lstMovies = JsonConvert.DeserializeObject<List<Movie>>(resultado);
                    }
                }
            }
            catch (Exception e)
            {
                lstMovies = null;
            }
            return lstMovies;
        }
        /// <summary>
        /// Motodo para consumir metodo GET - filtrar pelicula por title
        /// </summary>
        /// <param name="title">parametroa filtrar - titulo</param>
        /// <returns>objeto tipo Movie</returns>
        public Movie GetMoviesByTitle(string title)
        {
            string resultado = string.Empty;
            Movie Movie = null;
            HttpWebRequest request = null;
            if(title != string.Empty && title != null)
            {
                request = (HttpWebRequest)WebRequest.Create(appService.strUrlService + "?title=" + title);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.ContentLength = 0;
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception("");
                    else
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            resultado = reader.ReadToEnd();
                            Movie = JsonConvert.DeserializeObject<Movie>(resultado);
                        }
                    }
                }
                catch (Exception e)
                {
                    Movie = null;
                }
            }             
            return Movie;
        }
        /// <summary>
        /// Motodo para consumir metodo PUT - update peliculas
        /// </summary>
        /// <param name="modelo">datos almacenados en un modelo</param>
        /// <returns>true si fue existoso la operacion o false si ocurrio un error</returns>
        public bool PutMovies(Movie modelo)
        {
            bool resultado = false;
            string respuesta = string.Empty;
            HttpWebRequest request = null;
            if (modelo != null)
            {
                try
                {
                    var dataPost = JsonConvert.SerializeObject(modelo);
                    var data = UTF8Encoding.UTF8.GetBytes(dataPost);
                    request = (HttpWebRequest)WebRequest.Create(appService.strUrlService);
                    request.Method = "PUT";
                    request.ContentType = "application/json";
                    request.ContentLength = data.Length;

                    Stream stream = request.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.StatusCode == HttpStatusCode.OK) resultado = true;
                        else resultado = false;
                    }

                }
                catch (Exception e)
                {
                    resultado = false;
                }
            }
            return resultado;
        }
        /// <summary>
        /// Motodo para consumir metodo DELETE - delete peliculas
        /// </summary>
        /// <param name="id">id de la pelicula a eliminar</param>
        /// <returns>true si fue existoso la operacion o false si ocurrio un error</returns>
        public bool DeleteMovies(int id)
        {
            bool resultado = false;
            string respuesta = string.Empty;
            HttpWebRequest request = null;
            if (id != 0)
            {
                try
                {               
                    request = (HttpWebRequest)WebRequest.Create(appService.strUrlService+"/" + id);
                    request.Method = "DELETE";
                    request.ContentType = "application/json";
                    request.ContentLength = 0;

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.StatusCode == HttpStatusCode.OK) resultado = true;
                        else resultado = false;
                    }
                }
                catch (Exception e)
                {
                    resultado = false;
                }
            }
            return resultado;
        }
        /// <summary>
        /// Motodo para consumir metodo POST - insert peliculas
        /// </summary>
        /// <param name="modelo">datos almacenados en un modelo</param>
        /// <returns>true si fue existoso la operacion o false si ocurrio un error</returns>
        public bool PostMovie(Movie modelo)
        {
            bool resultado = false;
            string respuesta = string.Empty;
            HttpWebRequest request = null;
            if (modelo != null)
            {
                try
                {
                    var dataPost = JsonConvert.SerializeObject(modelo);
                    var data = UTF8Encoding.UTF8.GetBytes(dataPost);
                    request = (HttpWebRequest)WebRequest.Create(appService.strUrlService);
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = data.Length;

                    Stream stream = request.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.StatusCode == HttpStatusCode.OK) resultado = true;
                        else resultado = false;
                    }

                }
                catch (Exception e)
                {
                    resultado = false;
                }
            }
            return resultado;
        }
        /// <summary>
        /// Motodo para consumir metodo GET - validar registros existentes
        /// </summary>
        /// <param name="modelo">datos almacenados en un modelo</param>
        /// <returns>
        /// objeto tipo MensajeRespuesta con atributos:
        /// existe = 0 -> no existe 
        /// existe = 1 -> ya existe
        /// mensaje -> descripcion del mensaje respuesta 
        /// </returns>
        public MensajeRespuesta ValidateMovies(Movie modelo)
        {

            string resultado = string.Empty;
            MensajeRespuesta msg = new MensajeRespuesta();
            HttpWebRequest request = null;
            request = (HttpWebRequest)WebRequest.Create(appService.strUrlService + "?title=" + modelo.Title + "&director=" + modelo.Director + "");
            request.Method = "GET";
            request.ContentType = "application/json";
            request.ContentLength = 0;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception("");
                else
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        resultado = reader.ReadToEnd();
                        var json = JsonConvert.DeserializeObject(resultado);
                        var jobject = JObject.Parse(json.ToString());
                        msg.existe = Convert.ToInt32(jobject["Data"]["existe"].ToString());
                        msg.mensaje = jobject["Data"]["msg"].ToString();
                        
                    }
                }
            }
            catch (Exception e)
            {
                msg = null;
            }
            return msg;
        }
    }
}