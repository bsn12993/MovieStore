using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Domain
{
    /// <summary>
    /// Desarrollador: Bryan Silverio Nieves
    /// Fecha: 15/01/2018
    /// Clase Movie con los atributos definidos
    /// </summary>
    public class Movie
    {
        public Movie()
        {
            this.Id = 0;
            this.Title = string.Empty;
            this.ReleaseDate = DateTime.Now;
            this.Director = string.Empty;
        }
        /// <summary>
        /// almacena el identificador
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// almacena el titulo de la pelicula
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// almacena la fecha de lanzamiento
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// almacaena el director de la pelicula
        /// </summary>
        public string Director { get; set; }
    }
}
