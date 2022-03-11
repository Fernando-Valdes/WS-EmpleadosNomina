using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS_EmpleadosNomina
{
    public class Conexion
    {
        public static MySqlConnection ConexionBase()
        {
            string CadenaConexion = "Database=siga_administrativo; Server=192.168.1.224; Port=3306; User Id=siga; Password=siga&%$admvo01";


            try
            {
                MySqlConnection conexionBD = new MySqlConnection(CadenaConexion);
                return conexionBD;
            }

            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }



        }
        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection();
        }
    }
}