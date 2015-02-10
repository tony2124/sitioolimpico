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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class BuscarPersonal : Form
    {
        public static MySqlDataAdapter Adaptador;
        public static DataTable ds;

        public BuscarPersonal()
        {
            InitializeComponent();

            Pintar_tabla("select id_personal, nombre, fecha_ingreso, telefono, nivel_autorizacion, asignado, eliminado  from personal");

            tabla.Columns[0].HeaderText = "ID PERSONAL";
            tabla.Columns[0].Width = 120;
            tabla.Columns[1].HeaderText = "NOMBRE";
            tabla.Columns[1].Width = 460;
            tabla.Columns[2].HeaderText = "FECHA INGRESO";
            tabla.Columns[3].HeaderText = "TELÉFONO";
            tabla.Columns[4].HeaderText = "AUTORIZACIÓN";
            tabla.Columns[5].HeaderText = "ASIGNADO";
            tabla.Columns[6].HeaderText = "BAJA";
            total.Text = "" + tabla.Rows.Count;

            /************** ESTADÍSTICA **********************/
            ConexionBD bd = new ConexionBD();
            bd.conexion();
            MySqlDataReader Datos = bd.obtenerBasesDatosMySQL("SELECT count(id_personal) from personal where nivel_autorizacion = 2");
            if (Datos.HasRows)
                while (Datos.Read())
                {
                    reg_admin.Text = Datos.GetString(0);
                }
            Datos.Close();
            Datos = bd.obtenerBasesDatosMySQL("SELECT count(id_personal) from personal where nivel_autorizacion = 1");
            if (Datos.HasRows)
                while (Datos.Read())
                {
                    reg_monitor.Text = Datos.GetString(0);
                }
            Datos.Close();
            Datos = bd.obtenerBasesDatosMySQL("SELECT count(id_personal) from personal where nivel_autorizacion = 0");
            if (Datos.HasRows)
                while (Datos.Read())
                {
                    reg_taxista.Text = Datos.GetString(0);
                }
            Datos.Close();
            Datos = bd.obtenerBasesDatosMySQL("SELECT count(id_personal) from personal");
            if (Datos.HasRows)
                while (Datos.Read())
                {
                    tot.Text = Datos.GetString(0);
                }
            Datos.Close();
            Datos = bd.obtenerBasesDatosMySQL("SELECT count(id_personal) from personal where nivel_autorizacion = 0 and asignado = 1");
            if (Datos.HasRows)
                while (Datos.Read())
                {
                    asignados.Text = Datos.GetString(0);
                }
            Datos.Close();
            bd.Desconectar();
            /************************************************/

        }

        public void Pintar_tabla(String filtro)
        {
            Adaptador = new MySqlDataAdapter(filtro, ConexionBD.conex);

            ds = new DataTable();
            Adaptador.Fill(ds);
            tabla.DataSource = ds;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SitioOlimpico.AUTORIZACION < 2 && (tabla["nivel_autorizacion", e.RowIndex].Value.ToString().CompareTo("2") == 0 || tabla["nivel_autorizacion", e.RowIndex].Value.ToString().CompareTo("1") == 0))
            {
                MessageBox.Show(this, "No tiene permiso de editar este usuario", "No hay privilegios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new Personal(1, tabla["id_personal", e.RowIndex].Value.ToString(), false ).ShowDialog();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string busq = "";
            if (radioButton1.Checked)
                busq = "nombre";
            else if (radioButton2.Checked)
                busq = "telefono";
            else if (radioButton3.Checked)
                busq = "nivel_autorizacion";
            else if (radioButton4.Checked)
                busq = "asignado";
            if(radioButton4.Checked)
                Pintar_tabla("select id_personal, nombre, fecha_ingreso, telefono, nivel_autorizacion, eliminado  from personal where " + busq + " like '%" + nombre.Text + "%' and nivel_autorizacion = 0");
            else
                Pintar_tabla("select id_personal, nombre, fecha_ingreso, telefono, nivel_autorizacion, eliminado  from personal where "+busq+" like '%"+nombre.Text+"%'");
            total.Text = "" + tabla.Rows.Count;
           /* if (tabla.Rows.Count == 1)
                new Personal(1, tabla["id_personal", 0].Value.ToString(), false).ShowDialog();*/
        }

        private void nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                buscar.PerformClick();
        }

        private void BuscarPersonal_Activated(object sender, EventArgs e)
        {
            buscar.PerformClick();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // MessageBox.Show("Aquí se generará un archivo PDF.");
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento

            saveFileDialog1.FileName = "LISTA_PERSONAL_" + DateTime.Now.Day + "_" + DateTime.Now.ToString("MMMM") + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".pdf";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Title = "GENERANDO REPORTE DE PERSONAL";
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

            PdfPCell cell_izq = new PdfPCell(new Phrase("Sitio " + SitioOlimpico.n_sitio + "\nLista del personal\nFecha: " + DateTime.Now.ToString() + "\nFiltro aplicado: " + nombre.Text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));

            //Logotipo del sitio de taxis
            iTextSharp.text.Image tif = iTextSharp.text.Image.GetInstance(System.IO.Path.Combine(SitioOlimpico.rutadestino, SitioOlimpico.nombre_imagen));
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
            widths = new float[] { 50f, 100f, 50f, 50f, 50f, 50f, };
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
    }
}
