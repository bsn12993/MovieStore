using MovieStore.Data.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MovieStore.Repository.BDConnection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Repository.Domain;
using System.Configuration;

namespace MovieStore.Repository.Repository
{
    /// <summary>
    /// Desarrollador: Bryan Silverio Nieves
    /// Fecha: 15/01/2018
    /// Clase utilizada para la capa de negocio o DAO donde se llevan a cabo las operaciones a la base de datos por medio de Store Procedures
    /// </summary>
    public class MovieRepository
    {
        /// <summary>
        /// Variable definida para almacenar la cadena de conexion a SQL Server
        /// </summary>
        private string strConnection
        {
            get
            {
                return new Connection().getConnection();
            }
        }
        /// <summary>
        /// Metodo para obtener un listado de peliculas consultando un store procedure
        /// </summary>
        /// <returns>Retorna un listado de tipo Movie donde almacena los datos recuperados de la consulta</returns>
        public List<Movie> getAllMovies()
        {
            List<Movie> lstMovie = null;
            Movie movie = null;
            try
            {
                using (var sql = new SqlConnection(this.strConnection))
                {
                    using (var cmd = new SqlCommand("SP_GetMovieList", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sql.Open();
                        var rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            lstMovie = new List<Movie>();
                            while (rd.Read())
                            {
                                movie = new Movie();
                                movie.Id = (int)rd["Id"];
                                movie.Title = (string)rd["Title"];
                                movie.ReleaseDate = (DateTime)rd["ReleaseDate"];
                                movie.Director = (string)rd["Director"];
                                lstMovie.Add(movie);
                            }
                        }
                        else
                        {
                            lstMovie = new List<Movie>();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                lstMovie = null;
            }
                return lstMovie;
        }
        /// <summary>
        /// Metodo para obtener un listado de peliculas consultando un store procedure filtrando por titulo
        /// </summary>
        /// <param name="title">nombre del titulo a filtrar la busqueda</param>
        /// <returns>Retorna un modelo de tipo Movie donde se almacena los datos de la consulta</returns>
        public Movie getMovieByTitle(string title)
        {
            Movie movie = null;
            try
            {
                if(title!=string.Empty && title != null)
                {
                    using (var sql = new SqlConnection(this.strConnection))
                    {
                        using (var cmd = new SqlCommand("SP_GetMovieByTitle", sql))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@title", title));
                            sql.Open();
                            var rd = cmd.ExecuteReader();
                            if (rd.HasRows)
                            {
                                while (rd.Read())
                                {
                                    movie = new Movie();
                                    movie.Id = (int)rd["Id"];
                                    movie.Title = (string)rd["Title"];
                                    movie.ReleaseDate = (DateTime)rd["ReleaseDate"];
                                    movie.Director = (string)rd["Director"];
                                }
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return movie;
        }
        /// <summary>
        /// Metodo que realiza el update de un registro
        /// </summary>
        /// <param name="modelo">parametros a editar almacenados en una clase</param>
        /// <returns>true en caso de ser exitosa la operacion o false en caso contrario</returns>
        public bool putMovie(Movie modelo)
        {
            bool resultado = false;
            try
            {
                using(var sql=new SqlConnection(this.strConnection))
                {
                    using(var cmd=new SqlCommand("SP_PutMovie", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@id", modelo.Id));
                        cmd.Parameters.Add(new SqlParameter("@title", modelo.Title));
                        cmd.Parameters.Add(new SqlParameter("@releaseDate", modelo.ReleaseDate));
                        cmd.Parameters.Add(new SqlParameter("@director", modelo.Director));
                        sql.Open();
                         
                        var rd = cmd.ExecuteNonQuery();
                        var transaccion = sql.BeginTransaction();
                        if (rd != 0)
                        {
                            transaccion.Commit();
                            resultado = true;
                        }
                        else
                        {
                            transaccion.Rollback();
                            throw new Exception();                      
                        }
                    }
                }
            }
            catch (Exception e)
            {
                resultado = false;
            }
            return resultado;
        }
        /// <summary>
        /// Metodo que realiza el Delete de un registro en base a su id
        /// </summary>
        /// <param name="id">identificador para eliminar registro</param>
        /// <returns>true en caso de ser exitosa la operacion o false en caso contrario</returns>
        public bool deleteMovie(int id)
        {
            bool resultado = false;
            try
            {
                using (var sql = new SqlConnection(this.strConnection))
                {
                    using (var cmd = new SqlCommand("SP_DeleteMovie", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        sql.Open();
                        var rd = cmd.ExecuteNonQuery();
                        var transaccion = sql.BeginTransaction();
                        if (rd != 0)
                        {                           
                            transaccion.Commit();
                            resultado = true;
                        }
                        else
                        {
                            transaccion.Rollback();
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                resultado = false;
            }
            return resultado;
        }
        /// <summary>
        /// Metodo que realiza el Insert de un nuevo registro
        /// </summary>
        /// <param name="modelo">parametros con los datos del insert almacenados en un modelo</param>
        /// <returns>true en caso de ser exitosa la operacion o false en caso contrario</returns>
        public bool PostMovie(Movie modelo)
        {
            bool resultado = false;
            try
            {
                using (var sql = new SqlConnection(this.strConnection))
                {
                    using (var cmd = new SqlCommand("SP_PostMovie", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@title", modelo.Title));
                        cmd.Parameters.Add(new SqlParameter("@releaseDate", modelo.ReleaseDate));
                        cmd.Parameters.Add(new SqlParameter("@director", modelo.Director));
                        SqlParameter existeRegistro = new SqlParameter("", System.Data.SqlDbType.Int);
                        existeRegistro.Direction = System.Data.ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(existeRegistro);
                        sql.Open();
                        var rd = cmd.ExecuteNonQuery();
                        var transaccion = sql.BeginTransaction();      
                        if (rd != 0)
                        { 
                            transaccion.Commit();
                            resultado = true;
                        }
                        else
                        {
                            transaccion.Rollback();
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                resultado = false;
            }
            return resultado;
        }
        /// <summary>
        /// Metodo que realiza una validacion si ya existe un registro con un title y director registrado
        /// </summary>
        /// <param name="title">titulo de la pelicula</param>
        /// <param name="director">director de la pelicula</param>
        /// <returns>
        /// retorna un objeto de tipo MensajeRespuesta que almacena existe y mensaje, 
        /// existe=0 -> significa que no existe un registro con el mismo title y director
        /// existe=1 -> siginifica que ya existe un registro con el mismo title y director
        /// </returns>
        public MensajeRespuesta ValidateMovie(string title,string director)
        {
            MensajeRespuesta msg = new MensajeRespuesta();
            try
            {
                using(var sql=new SqlConnection(this.strConnection))
                {
                    using(var cmd=new SqlCommand("SP_ValidateMovie", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@title", title));
                        cmd.Parameters.Add(new SqlParameter("@director", director));
                        sql.Open();
                        var rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            
                            while (rd.Read())
                            {
                                //false =  no existe
                                //true = ya existe
                                int existe = (int)rd["existe"];
                                //Si el resultado es > 0 -> ya existe el registro - true
                                //Si el resultado es = 0 -> no existe el registro - false
                                if (existe == 0)
                                {
                                    msg.existe = 0;
                                    msg.mensaje = "No existe algun registro con el mismo titulo y director";
                                }
                                else
                                {
                                    throw new Exception();
                                }
                            }
                        }
                    }
                }
            }catch(Exception e)
            {
                msg.existe = 1;
                msg.mensaje = "Ya existe registro con el mismo titulo y director";
            }
            return msg;
        }
    }
}
