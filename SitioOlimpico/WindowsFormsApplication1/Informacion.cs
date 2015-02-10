using MySql.Data.MySqlClient;
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
    public partial class Informacion : Form
    {
        public bool cambiofoto = false;
        OpenFileDialog BuscarImagen;

        public Informacion()
        {
            InitializeComponent();

            /*************** CONSULTA INFORMACIÓN ************/
            CallerID obj = new CallerID();
            ConexionBD bd = new ConexionBD();
            bd.conexion();
            MySqlDataReader Datos = bd.obtenerBasesDatosMySQL("select nombre, direccion, rfc, foto  from informacion");
            if (Datos.HasRows)
                while (Datos.Read())
                {
                    nombre.Text = Datos.GetString(0);
                    direccion.Text = Datos.GetString(1);
                    rfc.Text = Datos.GetString(2);
                    logo.ImageLocation = System.IO.Path.Combine(SitioOlimpico.rutadestino, Datos.GetString(3));

                }
            Datos.Close();
            bd.Desconectar();
        }


        public void guardar()
        {
            ConexionBD bd = new ConexionBD();
            bd.conexion();

            if (cambiofoto)
            {
                string formato = BuscarImagen.FileName.Substring(BuscarImagen.FileName.LastIndexOf(".") + 1);
                string archivoorigen = BuscarImagen.FileName;
                string nombre_imagen = DateTime.Now.ToString("yyyyMMddHHmmss")+"."+formato;

                String archivoDestino = System.IO.Path.Combine(SitioOlimpico.rutadestino, nombre_imagen);

                if (!Directory.Exists(SitioOlimpico.rutadestino))
                {
                    Directory.CreateDirectory(SitioOlimpico.rutadestino);
                }

                if (File.Exists(archivoDestino))
                    File.Delete(archivoDestino);
                File.Copy(archivoorigen, archivoDestino, true);
                bd.peticion("update informacion set foto = '" + nombre_imagen+"'");
                SitioOlimpico.nombre_imagen = nombre_imagen;
                
                cambiofoto = false;

            }

            if (bd.peticion("update informacion set nombre = '" + nombre.Text + "'," +
                "direccion = '" + direccion.Text + "', rfc = '" + rfc.Text + "'") > 0)
            {
                SitioOlimpico.n_sitio = nombre.Text;
                bd.bitacora("Se ha editado la información del sitio");
                MessageBox.Show(this, "Los datos se han guardado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show(this, "Los datos NO se han guardado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            bd.Desconectar();
        }

        void cambiarImagen()
        {
            BuscarImagen = new OpenFileDialog();
            BuscarImagen.Filter = "Todos los archivos de Imagen (*.gif;*.bmp;*.jpg;*.png)|*.gif;*.bmp;*.jpg;*.png";
            BuscarImagen.FileName = "";
            BuscarImagen.Title = "Buscar imagen logo del sitio";
            BuscarImagen.InitialDirectory =  Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (BuscarImagen.ShowDialog() == DialogResult.OK)
            {
                /// Si esto se cumple, capturamos la propiedad File Name y la guardamos en el control
                String Direccion = BuscarImagen.FileName;

                logo.ImageLocation = Direccion;
                //Pueden usar tambien esta forma para cargar la Imagen solo activenla y comenten la linea donde se cargaba anteriormente 
                logo.SizeMode = PictureBoxSizeMode.Zoom;
                cambiofoto = true;
            }         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cambiarImagen();
        }
    }
}
