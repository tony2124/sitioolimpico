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
using System.Diagnostics;
using System.IO;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class SitioOlimpico : Form
    {
        public static int AUTORIZACION = -1;
        CallerID obj;
        public static MySqlDataAdapter Adaptador;
        public static DataTable ds;
        /************* NIVELES DE AUROTIZACION ***************
         * 0 - TAXISTA
         * 1 - MONITOR
         * 2 - ADMINISTRADOR
         * ***************************************************/
        public string consulta_tabla = "select numero_tel_clientes as TELÉFONO, nombre_clientes as CLIENTE, numero_unidad as UNIDAD, nombre as NOMBRE_PERSONAL, fecha_servicios as FECHA, hora_servicios as HORA, descripcion_servicios as DESCRIPCION from SERVICIOS natural join PERSONAL natural join TAXISTA_UNIDAD natural join CLIENTES order by fecha_servicios desc, hora_servicios desc";
        public static string usuario = "tony2124", nombre_usuario = "ALFONSO CALDERÓN CHÁVEZ", hora_inicio_sesion = "A las 2:20 pm", horario = "8am - 2pm";

        public SitioOlimpico()
        {
            InitializeComponent();

            if (AUTORIZACION == 2)
            {
                usuarios.Visible = true;
                toolStripSeparator3.Visible = true;
            }

            usuario_label.Text = usuario;
            nombre_usuario_label.Text = nombre_usuario;
            hora_inicio_sesion_label.Text = hora_inicio_sesion;
            horario_label.Text = horario;
            hora.Text = DateTime.Now.ToString();
            /*************** CONSULTA INFORMACIÓN DEL MODEM ************/
           
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

            Pintar_tabla(consulta_tabla);
            //tabla.Columns[0].Visible = false;
            tabla.Columns[1].Width = 250;
            tabla.Columns[2].Width = 70;
            tabla.Columns[3].Width = 250;
            tabla.Columns[4].Width = 90;
            tabla.Columns[5].Width = 80;
            tabla.Columns[6].Width = 250;

            obj = new CallerID();
            
            Thread hilo = new Thread( delegate(){
                obj.EscuchaTelefono(this);
            });
            try
            {
                hilo.Start();
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
            }

        }

        public void Pintar_tabla(String filtro)
        {
            Adaptador = new MySqlDataAdapter(filtro, ConexionBD.conex);
            ds = new DataTable();
            Adaptador.Fill(ds);
            tabla.DataSource = ds;
        
        }

        public void metodoRespaldo()
        {
            Cursor.Current = Cursors.WaitCursor;
            saveFileDialog1.FileName = "RESPALDO_BD_" + DateTime.Now.Day + "_" + DateTime.Now.ToString("MMMM") + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".sql";
            saveFileDialog1.AddExtension = true;
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
            DialogResult a = MessageBox.Show(this, "¿Está seguro que desea salir del sistema? Una vez que salga del sistema no será posible identificar las llamadas.", "Confirme operación",  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (a == DialogResult.Yes)
            {
                Close();
            }

            
        }

        private void SitioOlimpico_FormClosing(object sender, FormClosingEventArgs e)
        {
            //cerrar();
            obj.terminar = true;
        }


        private void registrarPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Personal p = new Personal(0,"",false);
            p.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Personal p = new Personal(0, "", false);
            p.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cliente c = new cliente("",0);
            c.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            BuscarUnidad bU = new BuscarUnidad();
            bU.ShowDialog();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Estadistica est = new Estadistica();
            est.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            metodoRespaldo();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new Configuracion().ShowDialog();
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
            Unidades u = new Unidades(0,"");
            u.ShowDialog();
        }

        private void buscarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new BuscarCliente().ShowDialog();
        }

        private void buscarUnidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuscarUnidad bU = new BuscarUnidad();
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
            new BuscarPersonal().ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Unidades u = new Unidades(0, "");
            u.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new BuscarPersonal().ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            new BuscarCliente().ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Pintar_tabla("select numero_tel_clientes as TELÉFONO, nombre_clientes as CLIENTE, numero_unidad as UNIDAD, nombre as NOMBRE_PERSONAL, fecha_servicios as FECHA, hora_servicios as HORA, descripcion_servicios as DESCRIPCION from SERVICIOS natural join PERSONAL natural join TAXISTA_UNIDAD natural join CLIENTES where nombre_clientes LIKE '%" + campo.Text + "%' or numero_unidad = '" + campo.Text + "' order by fecha_servicios desc, hora_servicios desc");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new CambiarContrasena().ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void nuevoServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new BuscarCliente().ShowDialog();
        }

        private void estadísticasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Estadistica est = new Estadistica();
            est.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Bitacora().ShowDialog();
        }

        private void usuarios_Click(object sender, EventArgs e)
        {
            new Usuarios().ShowDialog();
        }

        private void campo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                buscar.PerformClick();
        }

        private void tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            new cliente(tabla.Rows[e.RowIndex].Cells[0].Value+"",1).ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            hora.Text = DateTime.Now.ToString();
            linea.Text = CallerID.data;
        }

        private void SitioOlimpico_Activated(object sender, EventArgs e)
        {
            buscar.PerformClick();
        }
    }
}
