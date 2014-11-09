using SitioOlimpico;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class personal : Form
    {
        public OpenFileDialog BuscarImagen;
        public Boolean foto = false;
        public String nombre_archivo = "sin_foto.jpg", rutadestino = @"C:\\sitioOlimpicoPics";
        ConexionBD Bdatos = new ConexionBD();

        public personal()
        {
            InitializeComponent();
            this.foto_personal.ImageLocation = rutadestino + "\\" + nombre_archivo;
            foto_personal.SizeMode = PictureBoxSizeMode.StretchImage;
            estado_civil_personal.SelectedIndex = 0;
            autorizacion_personal.SelectedIndex = 0;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void foto_btn_personal_Click(object sender, EventArgs e)
        {
            BuscarImagen = new OpenFileDialog();
            BuscarImagen.Filter = "Todos los archivos de Imagen (*.gif;*.bmp;*.jpg)|*.gif;*.bmp;*.jpg";
            //Aquí incluiremos los filtros que queramos.
            BuscarImagen.FileName = "";
            BuscarImagen.Title = "Titulo del Dialogo";
            BuscarImagen.InitialDirectory = "C:\\";
            if (BuscarImagen.ShowDialog() == DialogResult.OK)
            {
                /// Si esto se cumple, capturamos la propiedad File Name y la guardamos en el control
                String Direccion = BuscarImagen.FileName;

                this.foto_personal.ImageLocation = Direccion;
                //Pueden usar tambien esta forma para cargar la Imagen solo activenla y comenten la linea donde se cargaba anteriormente 
                foto_personal.SizeMode = PictureBoxSizeMode.StretchImage;
                foto = true;
            }         
        }

        private void guardar_btn_personal_Click(object sender, EventArgs e)
        {
            
           // MessageBox.Show(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            
            //VERIFICA SI ESTAN LLENOS LOS CAMPOS DE NOMBRE Y APELLIDO.
            if (nombre_personal.Text.CompareTo("") == 0 ||
                apellido_personal.Text.CompareTo("") == 0)
            {
                MessageBox.Show("Los campos de nombre y apellido son obligatorios",
                    "LLene los campos obligatorios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                if (foto)
                {
                    guardarfoto();
                }

                guardarPersonal();

            }
            
        }

        public void guardarfoto() 
        {
            String formato = BuscarImagen.FileName.Substring(BuscarImagen.FileName.LastIndexOf(".") + 1);
            String archivoorigen = BuscarImagen.FileName;
            
            nombre_archivo = DateTime.Now.Day + "-"
                + DateTime.Now.Month + "-" + DateTime.Now.Year + "-"
                + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-"
                + DateTime.Now.Second + "-" + nombre_personal.Text.Replace(" ","");
            String archivoDestino = System.IO.Path.Combine(rutadestino, nombre_archivo + "." + formato);

            if (!Directory.Exists(rutadestino))
            {
                Directory.CreateDirectory(rutadestino);
            }

            if (File.Exists(archivoDestino))
                File.Delete(archivoDestino);
            File.Copy(archivoorigen, archivoDestino, true);
            this.foto_personal.ImageLocation = archivoDestino;
            foto_personal.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void guardarPersonal()
        {
            Bdatos.conexion();

            //INSERTA DATOS
            if (Bdatos.peticion("insert into personal (nombre,apellido,foto,"+
                "fecha_ingreso,fecha_nacimiento,telefono,numero_cel,estado_civil,"+
                "colonia,calle,numero_int,numero_ext,codigo_postal,ciudad,estado,"+
                "referencias,nivel_autorizacion,horario,eliminado)"+
                "values('" + nombre_personal.Text + 
                "','" + apellido_personal.Text + 
                "','" + nombre_archivo + 
                "','" + fecha_ingreso_personal.Value.ToString("yyyy-MM-dd") + 
                "','" + fecha_nac_personal.Value.ToString("yyyy-MM-dd")+
                "','" + num_tel_personal.Text+
                "','" + num_cel_personal.Text+
                "','" + estado_civil_personal.Text+
                "','" + colonia_personal.Text+
                "','" + calle_personal.Text+
                "','" + num_int_personal.Text+
                "','" + num_ext_personal.Text+
                "','" + cp_personal.Text+
                "','" + ciudad_personal.Text+
                "','" + estado_personal.Text+
                "','" + ref_personal.Text+
                "','" + autorizacion_personal.SelectedIndex+
                "','" + horario_personal.Text+
                "',0)") > 0)
            {
                //BORRAMOS LOS DATOS PARA UN SIGUIENTE REGISTRO
                borrarCampos();
                MessageBox.Show("Datos ingresados correctamente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Bdatos.Desconectar();
        }

        public void borrarCampos()
        {
            nombre_personal.Text = "";
            apellido_personal.Text = "";
            nombre_archivo = "sin_foto.jpg";
            fecha_ingreso_personal.Value = DateTime.Now;
            fecha_nac_personal.Value = DateTime.Now;
            num_tel_personal.Text = "";
            num_cel_personal.Text = "";
            estado_civil_personal.SelectedIndex = 0;
            colonia_personal.Text = "";
            calle_personal.Text = "";
            num_int_personal.Text = "";
            num_ext_personal.Text = "";
            cp_personal.Text = "";
            ciudad_personal.Text = "";
            estado_personal.Text = "";
            ref_personal.Text = "";
            autorizacion_personal.SelectedIndex = 0;
            horario_personal.Text = "";
            foto = false;

            this.foto_personal.ImageLocation = rutadestino+"\\"+nombre_archivo;
            foto_personal.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void cancelar_btn_personal_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
