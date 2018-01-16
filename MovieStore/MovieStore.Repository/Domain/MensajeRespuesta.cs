using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Repository.Domain
{
    /// <summary>
    /// Desarrollador: Bryan Silverio Nieves
    /// Fecha: 15/01/2018
    /// Clase MensajeRespuesta
    /// </summary>
    public class MensajeRespuesta
    {
        public MensajeRespuesta()
        {
            this.existe = 0;
            this.mensaje = string.Empty;
        }
        /// <summary>
        /// almacena 
        /// 0 -> si no existe un registro con un title y director determinado 
        /// 1 -_ si ya existe un registro con un title y director determindo
        /// </summary>
        public int existe { get; set; }
        /// <summary>
        /// almacena una descripcion de mensaje para mostrar en el cliente dependiento del valor de la variable existe
        /// </summary>
        public string mensaje { get; set; }
    }
}
