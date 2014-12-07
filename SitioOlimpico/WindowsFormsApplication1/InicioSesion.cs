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
using System.Threading;
using System.Net.Mail;
using System.Net;

namespace WindowsFormsApplication1
{
    public partial class InicioSesion : Form
    {
        string texto = "";

        public InicioSesion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConexionBD bd = new ConexionBD();
            ConexionBD.host = ip.Text;
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
                
                Datos = bd.obtenerBasesDatosMySQL("select * from bitacoras order by id_bitacora desc");
                if (Datos.HasRows)
                    while (Datos.Read())
                    {
                        texto += Datos.GetString(0) + " - ";
                        texto += Datos.GetString(1) + " - ";
                        texto += Datos.GetDateTime(2).ToString("yyyy-MM-dd") + " - ";
                        texto += Datos.GetString(3) + "<br>";
                    }
                bd.Desconectar();
                Visible = false;
                Thread hilo = new Thread(enviar_correo);
                try
                {
                    hilo.Start();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(e.ToString());
                }
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

        private void enviar_correo()
        {
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress("alfonso.calderon.chavez@gmail.com"));
            email.From = new MailAddress("alfonso.calderon.chavez@gmail.com");
            email.Subject = "BITÁCORA ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
            email.Body = texto;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;
        
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("alfonso.calderon.chavez@gmail.com", "tonyteresasimpus");

            string output = null;

            try
            {
                smtp.Send(email);
                email.Dispose();
                output = "Corre electrónico fue enviado satisfactoriamente.";
                //MessageBox.Show(output);
            }
            catch (Exception ex)
            {
                output = "Error enviando correo electrónico: " + ex.Message;
                //MessageBox.Show(output);
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
