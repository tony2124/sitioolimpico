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
    public partial class unidades : Form
    {
        public OpenFileDialog BuscarImagen;
        public Boolean foto = false;
        public String nombre_archivo = "sin_foto", 
            rutadestino = @"C:\\sitioOlimpicoPics\\unidades", 
            formato;
        ConexionBD Bdatos = new ConexionBD();

        public unidades(int forma)
        {
            InitializeComponent();

            if (forma > 0)
                editar_btn_unidades.Visible = false;
            else
                editar_btn_unidades.Visible = true;

            this.foto_unidad.Image = Properties.Resources.sin_foto_taxi;
            foto_unidad.SizeMode = PictureBoxSizeMode.StretchImage;
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

                this.foto_unidad.ImageLocation = Direccion;
                //Pueden usar tambien esta forma para cargar la Imagen solo activenla y comenten la linea donde se cargaba anteriormente 
                foto_unidad.SizeMode = PictureBoxSizeMode.StretchImage;
                foto = true;
            }         
        }

        private void guardar_btn_personal_Click(object sender, EventArgs e)
        {
            //VERIFICA SI ESTAN LLENOS LOS CAMPOS DE NUMERO DE UNIDAD.
            if (numero_unidad.Text.CompareTo("") == 0)
            {
                MessageBox.Show("El campo de número de unidad es obligatorio",
                    "LLene los campos obligatorios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                if (foto)//SI SE SELECCIONO FOTO DE GUARDA LA FOTO
                {
                    guardarfoto();
                }

                if (guardarUnidades() > 0)
                {
                    //BORRAMOS LOS DATOS PARA UN SIGUIENTE REGISTRO
                    borrarCampos();
                    MessageBox.Show("Datos ingresados correctamente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
           
            
        }

        public void guardarfoto() 
        {
           formato = BuscarImagen.FileName.Substring(BuscarImagen.FileName.LastIndexOf(".") + 1);
            String archivoorigen = BuscarImagen.FileName;
            
            nombre_archivo = DateTime.Now.Day + "-"
                + DateTime.Now.Month + "-" + DateTime.Now.Year + "-"
                + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-"
                + DateTime.Now.Second + "-" + numero_unidad.Text.Replace(" ","");
            String archivoDestino = System.IO.Path.Combine(rutadestino, nombre_archivo + "." + formato);

            if (!Directory.Exists(rutadestino))
            {
                Directory.CreateDirectory(rutadestino);
            }

            if (File.Exists(archivoDestino))
                File.Delete(archivoDestino);
            File.Copy(archivoorigen, archivoDestino, true);
            this.foto_unidad.ImageLocation = archivoDestino;
            foto_unidad.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public int guardarUnidades()
        {
            int consulta;
            Bdatos.conexion();

            //INSERTA DATOS
            consulta = Bdatos.peticion("insert into unidades (numero_unidad, modelo, marca," +
                "color, descripcion, placas, foto, fecha_ingreso,eliminado)" +
                "values('" + numero_unidad.Text + 
                "','" + modelo_unidad.Text +
                "','" + marca_unidad.Text +
                "','" + color_unidad.Text +
                "','" + descripcion_unidad.Text +
                "','" + placas_unidad.Text +
                "','" + nombre_archivo + "." + formato +
                "','" + fecha_ingreso_unidades.Value.ToString("yyyy-MM-dd") +
                "',0)");

            if (consulta > 0)
            {
                Bdatos.Desconectar();
                return consulta;
            }
            else
            {
                Bdatos.Desconectar();
                return consulta;
            }

        }

        public void borrarCampos()
        {
            numero_unidad.Text = "";
            nombre_archivo = "sin_foto";
            modelo_unidad.Text = "";
            color_unidad.Text = "";
            marca_unidad.Text = "";
            descripcion_unidad.Text = "";
            placas_unidad.Text = "";
            fecha_ingreso_unidades.Value = DateTime.Now;
            foto = false;

            this.foto_unidad.ImageLocation = null;
            this.foto_unidad.Image = Properties.Resources.sin_foto_taxi;
            foto_unidad.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void cancelar_btn_personal_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void foto_personal_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void numero_unidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
