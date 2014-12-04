using MySql.Data.MySqlClient;
using SitioOlimpico;
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
    public partial class InicioSesion : Form
    {
        public InicioSesion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConexionBD bd = new ConexionBD();
            bd.conexion();
            MySqlDataReader Datos = bd.obtenerBasesDatosMySQL("select nivel_autorizacion, usuario, nombre, horario_entrada, horario_salida from usuarios natural join personal where usuario = '"+user.Text+"' and contrasena = '"+pass.Text+"'");
            if(Datos.HasRows)
                while(Datos.Read()){
                        SitioOlimpico.AUTORIZACION = Datos.GetInt32(0);
                        SitioOlimpico.usuario = Datos.GetString(1);
                        SitioOlimpico.nombre_usuario = Datos.GetString(2);
                        SitioOlimpico.horario = Datos.GetString(3).ToString() + " - " + Datos.GetString(4).ToString();
                        SitioOlimpico.hora_inicio_sesion = DateTime.Now.ToString();
                }
            bd.Desconectar();
            Dispose();
        }
    }
}
