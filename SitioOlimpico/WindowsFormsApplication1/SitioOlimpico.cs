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
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace WindowsFormsApplication1
{
    public partial class SitioOlimpico : Form
    {
        public static int AUTORIZACION = -1;
        public static string rutadestino = @"C:\\sitioOlimpicoPics\\logo", nombre_imagen = "", n_sitio = "", busqueda = "nombre_clientes";
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
           
            /*******************************/
            Screen screen= Screen.PrimaryScreen; 
            int ancho = screen.Bounds.Width; 
            int alto = screen.Bounds.Height;
            /* if (ancho < 1200)
            {
                contenedor_izquierdo.Visible = false;
                MinimumSize = new Size(800, 726);
            }
            //  MessageBox.Show(Height + " -- " + Width);
            /******************************/

            if (AUTORIZACION == 2)
            {
                usuarios.Visible = true;
                toolStripSeparator3.Visible = true;
            }

            usuario_label.Text = usuario;
            nombre_usuario_label.Text = nombre_usuario;
            hora_inicio_sesion_label.Text = hora_inicio_sesion;
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
            Datos = bd.obtenerBasesDatosMySQL("select nombre, foto  from informacion");
            if (Datos.HasRows)
                while (Datos.Read())
                {
                    n_sitio = Datos.GetString(0);
                    nombre_imagen = Datos.GetString(1);
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
            total_registros.Text = tabla.RowCount + "";
        
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
            ConexionBD bd = new ConexionBD();
            bd.conexion();
            bd.bitacora("El usuario " + SitioOlimpico.usuario + " ha cerrado el programa");
            bd.Desconectar();
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
            Pintar_tabla("select numero_tel_clientes as TELÉFONO, nombre_clientes as CLIENTE, numero_unidad as UNIDAD, nombre as NOMBRE_PERSONAL, fecha_servicios as FECHA, hora_servicios as HORA, descripcion_servicios as DESCRIPCION from SERVICIOS natural join PERSONAL natural join TAXISTA_UNIDAD natural join CLIENTES where "+busqueda+" LIKE '%" + campo.Text + "%' order by fecha_servicios desc, hora_servicios desc");
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
        }

        private void SitioOlimpico_Activated(object sender, EventArgs e)
        {
            buscar.PerformClick();
            actualizarLogo();
            campo_busqueda.Text = busqueda;
        }

        private void actualizarLogo()
        {
            Text = "Sistema para el Control del Sitio " + n_sitio;
            nombre_sitio.Text = n_sitio;
            logo.ImageLocation = System.IO.Path.Combine(rutadestino, nombre_imagen);
        }

        private void informaciónDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CambiarContrasena().ShowDialog();
        }

        private void verBitácoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Bitacora().ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            new Bitacora().ShowDialog();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            new CambiarContrasena().ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new Informacion().ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("Aquí se generará un archivo PDF.");
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento

            saveFileDialog1.FileName = "SERVICIOS_" + DateTime.Now.Day + "_" + DateTime.Now.ToString("MMMM") + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".pdf";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Title = "GENERANDO REPORTE DE SERVICIOS";
            saveFileDialog1.Filter = "Archivos PDF(*.pdf)|*.pdf|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName != String.Empty)
                {

                }

            }
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(saveFileDialog1.FileName, FileMode.Create));


            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("Resporte de servicios");
            doc.AddCreator("Simpus Soluciones Tecnológicas");

            // Abrimos el archivo
            doc.Open();

            // Escribimos el encabezamiento en el documento

            //tabla para el encabezado
            PdfPTable encabezado = new PdfPTable(2);
            encabezado.TotalWidth = 540f;
            encabezado.LockedWidth = true;
            float[] widths = new float[] { 470f, 70f };
            encabezado.SetWidths(widths);
            encabezado.HorizontalAlignment = Element.ALIGN_LEFT;
            encabezado.DefaultCell.BorderWidth = 0;

            PdfPCell cell_izq = new PdfPCell(new Phrase("Sitio " + n_sitio + "\nReporte de servicios\nFecha: " + DateTime.Now.ToString() + "\nFiltro aplicado: " + campo.Text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));

            //Logotipo del sitio de taxis
            iTextSharp.text.Image tif = iTextSharp.text.Image.GetInstance(System.IO.Path.Combine(rutadestino, nombre_imagen));
            tif.ScaleToFit(50f, 50f);
            tif.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;

            //celda derecha
            PdfPCell cell_der = new PdfPCell(new Phrase(""));
            cell_der.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell_der.AddElement(tif);

            cell_der.Border = 0;
            cell_der.BorderWidthBottom = 1f;
            cell_der.BorderColorBottom = BaseColor.BLACK;

            cell_izq.Border = 0;
            cell_izq.BorderWidthBottom = 1f;
            cell_izq.BorderColorBottom = BaseColor.BLACK;

            encabezado.AddCell(cell_izq);
            encabezado.AddCell(cell_der);

            //agregar al documento el encabezado
            doc.Add(encabezado);
            doc.Add(new Paragraph(""));
            doc.Add(Chunk.NEWLINE);

            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(tabla.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;

            pdfTable.TotalWidth = 540f;
            pdfTable.LockedWidth = true;
            widths = new float[] { 50f, 100f, 50f, 100f, 50f, 50f, 90f };
            pdfTable.SetWidths(widths);
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 0;

            //Adding Header row
            foreach (DataGridViewColumn column in tabla.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.PaddingTop = 10f;
                cell.PaddingBottom = 10f;
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            foreach (DataGridViewRow row in tabla.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(cell.Value.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                    pdfTable.AddCell(celda);
                }
            }

            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            doc.Add(pdfTable);

            doc.Close();
            writer.Close();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Filtro().ShowDialog();
        }

        private void modemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Configuracion().ShowDialog();
        }

        private void cambiarDatosDelSitioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Informacion().ShowDialog();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            new Filtro().ShowDialog();
        }

    }
}
