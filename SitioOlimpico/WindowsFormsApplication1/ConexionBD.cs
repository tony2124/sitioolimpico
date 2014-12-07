using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    class ConexionBD
    {
        public static string usuario, contrasena, basedatos, host, puerto;
        public static bool exit = false;
        public static string conex;
        public static MySqlConnection conexionBD;

        public ConexionBD()
        {
           // host = "localhost";
            usuario = "root";
            contrasena = "simpus2124";
            puerto = "3306";
            basedatos = "sitioolimpico";
        }

        public void conexion()
        {
            try
            {
                conex = "server=" + host + "; port=" + puerto + "; user id=" + usuario + "; password= " + contrasena + "; database=" + basedatos + ";";
                conexionBD = new MySqlConnection(conex);
                conexionBD.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al conectar a la base de datos de MySQL: \nDETALLES DEL ERROR: " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            try
            {
                byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
                result = System.Text.Encoding.Unicode.GetString(decryted);
            }
            catch (Exception e) { }
            return result;
        }

        public MySqlDataReader obtenerBasesDatosMySQL(String consulta)
        {
            MySqlDataReader registrosObtenidosMySQL = null;

            MySqlCommand cmd = new MySqlCommand(consulta, conexionBD);
            try
            {
                registrosObtenidosMySQL = cmd.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al obtener bases de datos de MySQL: \nDETALLES DEL ERROR: " + ex.Message, "Error al obtener catálogos",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return registrosObtenidosMySQL;
        }

        public int peticion(String consulta)
        {
            int resultado=0;
            try
            {
                MySqlCommand cmd = new MySqlCommand(consulta, conexionBD);
                resultado = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al alterar datos en MySQL: \nDETALLES DEL ERROR: " + ex.Message, " Error al ingresar ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return resultado;
        }

        public int bitacora(String registro)
        {
            int resultado = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO BITACORAS (hora, fecha, descripcion) values('" + DateTime.Now.ToString("HH:mm:ss") + "','" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "','" +SitioOlimpico.usuario+"  -  "+registro + "')", conexionBD);
                resultado = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al ingresar datos en MySQL: \nDETALLES DEL ERROR: " + ex.Message, " Error al ingresar ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return resultado;
        }
        

        public void Desconectar()
        {
            conexionBD.Close();
        }
    }
}