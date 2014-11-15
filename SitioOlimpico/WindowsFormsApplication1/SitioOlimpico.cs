using WindowsFormsApplication1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SitioOlimpico;
using System.Diagnostics;
using System.IO;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class SitioOlimpico : Form
    {
        public static int AUTORIZACION = 0;
        CallerID obj;
        public SitioOlimpico()
        {
            InitializeComponent();
            

            /*************** CONSULTA INFORMACIÓN DEL MODEM ************/
            obj = new CallerID();
            ConexionBD bd = new ConexionBD();
            bd.conexion();
            MySqlDataReader Datos = bd.obtenerBasesDatosMySQL("select puerto, baud, paridad, norma from configuracion");
            if (Datos.HasRows)
                while (Datos.Read())
                {
                    CallerID.port = Datos.GetString(0);
                    CallerID.baud = Datos.GetInt32(1);
                    CallerID.paridad = Datos.GetInt32(2);
                    CallerID.ComandoATID = Datos.GetString(3);
                }
            Datos.Close();
            bd.Desconectar();

            if (AUTORIZACION == 0)
                usuarios.Visible = true;

          //  MessageBox.Show(CallerID.port);
            Thread hilo = new Thread( obj.EscuchaTelefono );
            try
            {
                hilo.Start();

            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
            }

        }

        public void metodoRespaldo()
        {
            Cursor.Current = Cursors.WaitCursor;
            saveFileDialog1.FileName = "RESPALDO_BD_" + DateTime.Now.Day + "_" + DateTime.Now.ToString("MMMM") + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".sql";
            saveFileDialog1.AddExtension = true;
            // saveFileDialog1.CheckFileExists = true;
            saveFileDialog1.Title = "RESPALDO DE LA BASE DE DATOS";
            saveFileDialog1.Filter = "Archivos SQL(*.sql)|*.sql|Archivos de Texto (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName != String.Empty)
                {
                    ConexionBD bd = new ConexionBD();
                    bd.conexion();
                    String linea;
                    String fichero = saveFileDialog1.FileName;
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.FileName = "mysqldump";
                    proc.StartInfo.Arguments = ConexionBD.basedatos + " --single-transaction --host=" + ConexionBD.host + " --user=" + ConexionBD.usuario + " --password=" + ConexionBD.contrasena;
                    Process miProceso;
                    miProceso = Process.Start(proc.StartInfo);
                    try
                    {
                        StreamReader sr = miProceso.StandardOutput;
                        TextWriter tw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.Default);
                        while ((linea = sr.ReadLine()) != null)
                        {
                            tw.WriteLine(linea);
                        }
                        tw.Close();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        return;
                    }
                    bd.Desconectar();
                    MessageBox.Show("Copia de seguridad realizada con éxito");
                }
            }
            Cursor.Current = Cursors.Default;
        }

        public void cerrar ()
        {
            MessageBox.Show(this, "¿Está seguro que desea salir del sistema? Una vez que salga del sistema no será posible identificar las llamadas.", "Confirme operación",  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Close();
        }

        private void SitioOlimpico_FormClosing(object sender, FormClosingEventArgs e)
        {
            obj.terminar = true;
        }


        private void registrarPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            personal p = new personal(0,"",false);
            p.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cliente c = new cliente("",0);
            c.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            buscarUnidad bU = new buscarUnidad();
            bU.ShowDialog();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            metodoRespaldo();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new configuracion().ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cerrar();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new acercade().ShowDialog();

        }


        private void registroDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cliente c = new cliente("",0);
            c.ShowDialog();
        }

        private void respaldoDeBDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            metodoRespaldo();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cerrar();
        }

        private void registrarUnidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unidades u = new unidades(0,"");
            u.ShowDialog();
        }

        private void buscarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new buscarCliente().ShowDialog();
        }

        private void buscarUnidadToolStripMenuItem_Click(object sender, EventArgs e)
        {


            buscarUnidad bU = new buscarUnidad();
            bU.ShowDialog();
        }
            

        private void contactarASimpusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("www.simpus.com.mx/es/software/contacto");

        }

        private void informaciónDelSitioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Informacion().ShowDialog();
        }

        private void buscarPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new buscarPersonal().ShowDialog();
        }
    }
}
