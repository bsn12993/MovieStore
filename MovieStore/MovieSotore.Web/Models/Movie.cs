using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieStore.Web.Models
{
    /// <summary>
    /// Desarrollador: Bryan Silverio Nieves
    /// Fecha: 15/01/2018
    /// Modelo Movie 
    /// </summary>
    public class Movie
    {
        public Movie()
        {
            this.Id = 0;
            this.Title = string.Empty;
            this.ReleaseDate = DateTime.Today;
            this.Director = string.Empty;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = " Title is required")]
        [Display(Name ="Title")]
        [StringLength(maximumLength:50,MinimumLength =5,ErrorMessage ="Maximum 50 minimum 5 ")]
        public string Title { get; set; }

        [Required(ErrorMessage = " Title is ReleaseDate")]
        [Display(Name = "ReleaseDate")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = " Title is Director")]
        [Display(Name = "Director")]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Maximum 30 minimum 5 ")]
        public string Director { get; set; }
    }
}