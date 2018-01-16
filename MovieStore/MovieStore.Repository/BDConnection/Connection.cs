using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Repository.BDConnection
{
    /// <summary>
    /// Desarrollador: Bryan Silverio Nieves
    /// Fecha: 15/01/2018
    /// Clase Connection
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// metodo para obtener la cadena de conexion a al base de datos SQL Server
        /// </summary>
        /// <returns></returns>
        public string getConnection()
        {
            return "Data Source=LAPTOP-7QBHMR1M;Initial Catalog=MovieStore;Persist Security Info=True;User ID=sa;Password=123";
        }
    }
}
