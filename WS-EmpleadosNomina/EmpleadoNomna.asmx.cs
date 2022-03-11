using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Services;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NJsonSchema;

namespace WS_EmpleadosNomina
{
    /// <summary>
    /// Descripción breve de EmpleadoNomna
    /// </summary>

    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]

    public class EmpleadoNomna : System.Web.Services.WebService
    {
        MySqlConnection conexionBD = Conexion.ConexionBase();

        [WebMethod]
        public string DameListaDeEmpleados()
        {
            string sql;
            DataTable DatosTabla = new DataTable();
            conexionBD.Open();

            sql = "SELECT id_emp_empleado, " +
                    "CONVERT(CONCAT(emp_nombres, ' ', emp_paterno, ' ', emp_materno) USING utf8) AS Empleado, " +
                    "emp_curp AS Curp, " +
                    "emp_enlace AS Enlace, " +
                    "pla_numero AS Plaza, " +
                    "cat_nombre AS Categoria, " +
                    "ads_descripcion AS 'Area de Adscripcion' , " +
                    "id_pla_plantilla " +
            "FROM pri_empleado " +
            "INNER JOIN pri_plantilla p ON p.fk_emp_empleado = id_emp_empleado " +
            "INNER JOIN pri_plaza ON fk_pla_plaza = id_pla_plaza " +
            "INNER JOIN pri_adscripcion ON fk_ads_adscripcion = id_ads_adscripcion " +
            "INNER JOIN pri_categoria ON fk_cat_categoria = id_cat_categoria " +
            "WHERE p.pla_estado = 1";
    
            MySqlCommand commando = new MySqlCommand(sql, conexionBD);
            MySqlDataAdapter data = new MySqlDataAdapter(commando);
            data.Fill(DatosTabla);
            conexionBD.Close();
           return JsonConvert.SerializeObject(DatosTabla);



            /*
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in DatosTabla.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in DatosTabla.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            //return jsSerializer.Serialize(parentRow);
            */
        }
    }
}
