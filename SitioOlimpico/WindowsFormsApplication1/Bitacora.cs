using MySql.Data.MySqlClient;
using WindowsFormsApplication1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Bitacora : Form
    {
        public Bitacora()
        {
            InitializeComponent();
            string texto="";
            ConexionBD bd = new ConexionBD();
            bd.conexion();
            MySqlDataReader Datos = bd.obtenerBasesDatosMySQL("select * from bitacoras order by id_bitacora desc");
            if (Datos.HasRows)
                while (Datos.Read())
                {
                    texto += Datos.GetString(0) + " - ";
                    texto += Datos.GetString(1) + " - ";
                    texto += Datos.GetDateTime(2).ToString("yyyy-MM-dd") + " - ";
                    texto += Datos.GetString(3) + "\r\n";
                }
            Datos.Close();
            bd.Desconectar();
            mov.Text = texto;
        }
    }
}
