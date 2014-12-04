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
    public partial class CambiarContrasena : Form
    {
        public CambiarContrasena()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!vacios())
            {
                if (contrasena_actual())
                {
                    if (coinciden_contrasenas())
                    {
                        cambiar_contrasena();
                    }
                }
            }
            
        }

        private bool coinciden_contrasenas()
        {
            if (nueva1.Text.CompareTo(nueva2.Text) == 0)
                return true;
            else
            {
                MessageBox.Show(null, "La contraseña nueva no coincide", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nueva1.Focus();
                nueva1.SelectAll();
                return false;
            }
        }

        private void cambiar_contrasena()
        {
            ConexionBD bd = new ConexionBD();
            bd.conexion();
            if (bd.peticion("UPDATE usuarios set contrasena = '" + nueva1.Text + "' where usuario = '" + SitioOlimpico.usuario + "'") > 0)
            {
                MessageBox.Show(null, "La contraseña ha sido cambiada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bd.Desconectar();
                Dispose();
            }
            bd.Desconectar();
        }

        private bool contrasena_actual()
        {
            ConexionBD bd = new ConexionBD();
            bd.conexion();
            MySqlDataReader datos = bd.obtenerBasesDatosMySQL("select contrasena from usuarios where usuario = '"+SitioOlimpico.usuario+"'");
            if (datos.HasRows)
            {
                while (datos.Read()) ;
                if (datos.GetString(0).CompareTo(actual.Text) == 0)
                {
                    bd.Desconectar();
                    return true;
                }
                else
                    MessageBox.Show(null, "La contraseña actual no coincide", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            bd.Desconectar();
            return false;
        }

        private bool vacios()
        {
            if (actual.Text.CompareTo("") == 0 || nueva1.Text.CompareTo("") == 0 || nueva2.Text.CompareTo("") == 0)
            {
                MessageBox.Show(null, "Debe llenar los campos", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }

        void enter(KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                button1.PerformClick();
        }

        private void actual_MouseClick(object sender, MouseEventArgs e)
        {
            actual.Focus();
            actual.SelectAll();
        }

        private void nueva1_MouseClick(object sender, MouseEventArgs e)
        {
            nueva1.Focus();
            nueva1.SelectAll();
        }

        private void nueva2_MouseClick(object sender, MouseEventArgs e)
        {
            nueva2.Focus();
            nueva2.SelectAll();
        }

        private void actual_KeyPress(object sender, KeyPressEventArgs e)
        {
            enter(e);
        }

        private void nueva1_KeyPress(object sender, KeyPressEventArgs e)
        {
            enter(e);
        }

        private void nueva2_KeyPress(object sender, KeyPressEventArgs e)
        {
            enter(e);
        }
    }
}
