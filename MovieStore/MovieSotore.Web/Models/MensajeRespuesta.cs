using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Web.Models
{
    /// <summary>
    /// Desarrollador: Bryan Silverio Nieves
    /// Fecha: 15/01/2018
    /// Clase para recuperar las respuestas de webservices y almacenarlas en esta clase como mapeo
    /// </summary>
    public class MensajeRespuesta
    {
        public MensajeRespuesta()
        {
            this.existe = 0;
            this.mensaje = string.Empty;
        }
        /// <summary>
        /// existe registro
        /// 0 -> no existe
        /// 1 -> ya existe
        /// </summary>
        public int existe { get; set; }
        /// <summary>
        /// mensaje dependiendo del valor de existe
        /// </summary>
        public string mensaje { get; set; }
    }
}
