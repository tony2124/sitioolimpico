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
    public partial class cliente : Form
    {
        public OpenFileDialog BuscarImagen;
        public Boolean foto = false;
        public String nombre_archivo = "sin_foto", rutadestino = @"C:\\sitioOlimpicoPics\\clientes", formato;
        public string num_tel = "";
        ConexionBD Bdatos = new ConexionBD();

        public cliente(string forma)
        {
            InitializeComponent();

            if (forma.Length > 0)
                editar_btn_personal.Visible = false;
            else
                editar_btn_personal.Visible = true;

            num_tel = forma;
            num_tel_1_cliente.Text = forma;

            this.foto_cliente.Image = Properties.Resources.sin_foto;
            foto_cliente.SizeMode = PictureBoxSizeMode.StretchImage;
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

                this.foto_cliente.ImageLocation = Direccion;
                //Pueden usar tambien esta forma para cargar la Imagen solo activenla y comenten la linea donde se cargaba anteriormente 
                foto_cliente.SizeMode = PictureBoxSizeMode.StretchImage;
                foto = true;
            }         
        }

        private void guardar_btn_personal_Click(object sender, EventArgs e)
        {
            
           // MessageBox.Show(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            
            //VERIFICA SI ESTAN LLENOS LOS CAMPOS DE NOMBRE Y APELLIDO.
            if (nombre_cliente.Text.CompareTo("") == 0 ||
                apellido_cliente.Text.CompareTo("") == 0)
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

                 DialogResult result;
                 if (unidad_servicio.Text.CompareTo("") == 0)//VERIFICAMOS SI EL CAMPO DE UNIDAD NO TIENE DATOS
                 {
                     result = MessageBox.Show("No has llenado el campo de unidad para hacer el servicio. ¿Deseas guardar el cliente sin guardar el servicio?",
                            "¿Seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                     if (result == DialogResult.Yes)
                     {//GUARDA EL CLIENTE SIN REALIZAR NINGUN SERVICIO
                         guardarPersonal();
                     }
                 }
                 else//SI EL CAMPO DE UNIDAD TIENE DATOS: Guardamos el servicio junto con los datos del cliente
                 {

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
                + DateTime.Now.Second + "-" + nombre_cliente.Text.Replace(" ","");
            String archivoDestino = System.IO.Path.Combine(rutadestino, nombre_archivo + "." + formato);

            if (!Directory.Exists(rutadestino))
            {
                Directory.CreateDirectory(rutadestino);
            }

            if (File.Exists(archivoDestino))
                File.Delete(archivoDestino);
            File.Copy(archivoorigen, archivoDestino, true);
            this.foto_cliente.ImageLocation = archivoDestino;
            foto_cliente.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void guardarPersonal()
        {

                    Bdatos.conexion();

                    //GENERO EL ID
                    String id = generarId();
                    //INSERTA DATOS
                    if (Bdatos.peticion("insert into clientes (id_cliente,nombre,apellido,fecha_nacimiento, foto," +
                        "numero_tel_1,numero_tel_2,numero_tel_3,numero_cel,correo_electronico," +
                        "colonia,calle,numero_int,numero_ext,codigo_postal,ciudad,estado," +
                        "referencias,eliminado)" +
                        "values('" + id + "','" + nombre_cliente.Text +
                        "','" + apellido_cliente.Text +
                        "','" + fecha_nac_cliente.Value.ToString("yyyy-MM-dd") +
                        "','" + nombre_archivo + "." + formato +
                        "','" + num_tel_1_cliente.Text +
                        "','" + num_tel_2_cliente.Text +
                        "','" + num_tel_3_cliente.Text +
                        "','" + num_cel_cliente.Text +
                        "','" + email_cliente.Text +
                        "','" + colonia_cliente.Text +
                        "','" + calle_cliente.Text +
                        "','" + num_int_cliente.Text +
                        "','" + num_ext_cliente.Text +
                        "','" + cp_cliente.Text +
                        "','" + ciudad_cliente.Text +
                        "','" + estado_cliente.Text +
                        "','" + ref_cliente.Text +
                        "',0)") > 0)
                    {
                        //BORRAMOS LOS DATOS PARA UN SIGUIENTE REGISTRO
                        borrarCampos();
                        MessageBox.Show("Datos ingresados correctamente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Bdatos.Desconectar();
                
            
        }

        public String generarId()
        {
            return DateTime.Now.Year
                + "" + DateTime.Now.Month 
                + "" + DateTime.Now.Day
                + "" + DateTime.Now.Hour
                + "" + DateTime.Now.Minute
                + "" + DateTime.Now.Second
                + "" + DateTime.Now.Millisecond + "";
        }

        public void borrarCampos()
        {
            nombre_cliente.Text = "";
            apellido_cliente.Text = "";
            nombre_archivo = "sin_foto";
            fecha_nac_cliente.Value = DateTime.Now;
            num_tel_1_cliente.Text = "";
            num_tel_2_cliente.Text = "";
            num_tel_3_cliente.Text = "";
            num_cel_cliente.Text = "";
            email_cliente.Text = "";
            colonia_cliente.Text = "";
            calle_cliente.Text = "";
            num_int_cliente.Text = "";
            num_ext_cliente.Text = "";
            cp_cliente.Text = "";
            ciudad_cliente.Text = "";
            estado_cliente.Text = "";
            ref_cliente.Text = "";
            foto = false;

            this.foto_cliente.ImageLocation = null;
            this.foto_cliente.Image = Properties.Resources.sin_foto;
            foto_cliente.SizeMode = PictureBoxSizeMode.StretchImage;
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
    }
}
