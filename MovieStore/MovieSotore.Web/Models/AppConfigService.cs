using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MovieStore.Web.Models
{
    /// <summary>
    /// Desarrollador: Bryan Silverio Nieves
    /// Fecha: 15/01/2018
    /// Clase utilizada para recuperar la url del Web Servies del archivo de configuracion
    /// </summary>
    public class AppConfigService
    {
        public string strUrlService
        {
            get
            {
                return ConfigurationManager.AppSettings["URLService"].ToString();
            }
        }
    }
}