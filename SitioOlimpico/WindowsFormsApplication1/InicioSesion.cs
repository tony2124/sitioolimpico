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
            if (Datos.HasRows)
            {
                while (Datos.Read())
                {
                    SitioOlimpico.AUTORIZACION = Datos.GetInt32(0);
                    SitioOlimpico.usuario = Datos.GetString(1);
                    SitioOlimpico.nombre_usuario = Datos.GetString(2);
                    SitioOlimpico.horario = Datos.GetString(3).ToString() + " - " + Datos.GetString(4).ToString();
                    SitioOlimpico.hora_inicio_sesion = DateTime.Now.ToString();
                }
                Datos.Close();
                bd.bitacora("Inició sesión el usuario: " + SitioOlimpico.nombre_usuario);
                bd.Desconectar();
                Visible = false;
                new SitioOlimpico().ShowDialog();
                Dispose();
            }
            else
            {
                MessageBox.Show(null, "Usuario o contraseña incorrectos", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                user.ResetText();
                pass.ResetText();
                user.Focus();
            }
           
        }

        private void pass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                iniciar.PerformClick();
        }

        private void InicioSesion_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void user_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                iniciar.PerformClick();
        }

        private void user_Click(object sender, EventArgs e)
        {
            user.Focus();
            user.SelectAll();
        }

        private void pass_Click(object sender, EventArgs e)
        {
            pass.Focus();
            pass.SelectAll();
        }
    }
}
