using MySql.Data.MySqlClient;
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
        public String nombre_archivo = "sin_foto", 
            rutadestino = @"C:\\sitioOlimpicoPics\\clientes", 
            formato, id_cliente;
        int FORMA;
        MySqlDataReader Datos;
        ConexionBD Bdatos = new ConexionBD();

        public cliente(string numero, int forma)
        {
            InitializeComponent();
           
           // this.foto_cliente.Image = Properties.Resources.sin_foto;
           // foto_cliente.SizeMode = PictureBoxSizeMode.StretchImage;

            FORMA = forma;
            if (forma == 0)
                editar_btn_cliente.Visible = false;
            else
                editar_btn_cliente.Visible = true;

            if (FORMA != 0)
            {
                num_tel_1_cliente.Text = numero;

                Bdatos.conexion();

                Datos = Bdatos.obtenerBasesDatosMySQL("select count(nombre) from clientes where numero_tel_1 = '" + numero + "'");
                int x = 0;
                if (Datos.HasRows)
                    while (Datos.Read())
                        x = Datos.GetInt32(0);
                Datos.Close();

                if (x != 0)
                {
                    Datos = Bdatos.obtenerBasesDatosMySQL("select nombre,foto,colonia,calle,referencias from clientes where numero_tel_1 = '" + numero + "'");
                    if (Datos.HasRows)
                        while (Datos.Read())
                        {
                            nombre_cliente.Text = Datos.GetString(0);

                            if (Datos.GetString(1).CompareTo("sin_foto.") == 0)
                                this.foto_cliente.Image = Properties.Resources.sin_foto;
                            else
                                foto_cliente.ImageLocation = rutadestino + Datos.GetString(1);
                            foto_cliente.SizeMode = PictureBoxSizeMode.StretchImage;

                            colonia_cliente.Text = Datos.GetString(2);
                            calle_cliente.Text = Datos.GetString(3);
                            ref_cliente.Text = Datos.GetString(4);
                        }
                    Datos.Close();
                    Bdatos.Desconectar();

                    deshabilitarCampos();
                    guardar_btn_cliente.Image = null;
                }
            }
            

            //CONSULTA A LA BASE DE DATOS PARA EXTRAER INFORMACIÓN DE LAS UNIDADES
            int contador = 0;
            String[] A;

            Bdatos.conexion();
            Datos = Bdatos.obtenerBasesDatosMySQL("select count(numero_unidad) from unidades");
            if (Datos.HasRows)
                while (Datos.Read())
                    contador = Datos.GetInt32(0);
            Datos.Close();

            if (contador > 0)
            {
                Datos = Bdatos.obtenerBasesDatosMySQL("select numero_unidad from unidades");
                int i = 0;
                A = new String[contador];
                while (Datos.Read()){
                    A[i] = Datos.GetInt32(0)+"";
                    i++;
                }

                Datos.Close();

                this.unidad_servicio.AutoCompleteCustomSource.AddRange(A);

            }
        }

        public void habilitarCampos()
        {

        }

        public void deshabilitarCampos()
        {
            num_tel_1_cliente.Enabled = false;
            nombre_cliente.Enabled = false;
            foto_btn_cliente.Enabled = false;
            colonia_cliente.Enabled = false;
            calle_cliente.Enabled = false;
            ref_cliente.Enabled = false;
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
            
            //VERIFICA SI ESTAN LLENOS LOS CAMPOS DE NOMBRE Y TELEFONO.
            if (nombre_cliente.Text.CompareTo("") == 0 ||
                num_tel_1_cliente.Text.CompareTo("") == 0)
            {
                MessageBox.Show("El campo nombre y telefono son obligatorios",
                    "LLene los campos obligatorios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                

                 DialogResult result;
                 if (unidad_servicio.Text.CompareTo("") == 0)//VERIFICAMOS SI EL CAMPO DE UNIDAD NO TIENE DATOS
                 {
                     result = MessageBox.Show("No has llenado el campo de unidad para hacer el servicio. ¿Deseas guardar el cliente sin guardar el servicio?",
                            "¿Seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                     if (result == DialogResult.Yes)
                     {//GUARDA EL CLIENTE SIN REALIZAR NINGUN SERVICIO

                         if (guardarClientes() > 0)
                         {
                            
                             if (foto)//SI SE SELECCIONO FOTO DE GUARDA LA FOTO
                             {
                                 guardarfoto();
                             }

                             //BORRAMOS LOS DATOS PARA UN SIGUIENTE REGISTRO
                             borrarCampos();
                             MessageBox.Show("Datos ingresados correctamente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                     }
                 }
                 else//SI EL CAMPO DE UNIDAD TIENE DATOS: Guardamos el servicio junto con los datos del cliente
                 {
                     Bdatos.conexion();
                     
                     Datos = Bdatos.obtenerBasesDatosMySQL("SELECT count(numero_unidad) from unidades where numero_unidad=" + unidad_servicio.Text);
                     int existe = 0;
                     if (Datos.HasRows)
                         while (Datos.Read())
                             existe = Datos.GetInt32(0);
                     Datos.Close();
                     Bdatos.Desconectar();
                     if (existe != 1)//Si no existe la unidad
                     {
                         MessageBox.Show("No se puede guardar un servicio a una unidad que no este registrada, por favor registre primero la unidad en el programa.", "Error: Unidad no registrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                     else//Si si existe la unidad
                     {
                         
                         if (guardarClientes() > 0)
                         {

                             if (foto)//SI SE SELECCIONO FOTO DE GUARDA LA FOTO
                             {
                                 guardarfoto();
                             }

                             guardarServicio();

                             //BORRAMOS LOS DATOS PARA UN SIGUIENTE REGISTRO
                             borrarCampos();
                             MessageBox.Show("Datos ingresados correctamente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                      //   MessageBox.Show("Datos ingresados correctamente y Servicio guardado", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     }

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

        public void guardarServicio()
        {

            int id_taxista_unidad = 0;
            Bdatos.conexion();
            Datos = Bdatos.obtenerBasesDatosMySQL("SELECT id_taxista_unidad from taxista_unidad where numero_unidad=" + unidad_servicio.Text);
            if (Datos.HasRows)
                while (Datos.Read())
                    id_taxista_unidad = Datos.GetInt32(0);
            Datos.Close();
            Bdatos.Desconectar();
          
            //INSERTA DATOS A SERVICIOS
            Bdatos.conexion();
            Bdatos.peticion("insert into servicios (id_cliente, id_taxista_unidad, fecha, hora, descripcion)" +
                           " values('" + id_cliente +
                           "','" + id_taxista_unidad +
                           "','" + DateTime.Now.ToString("yyyy-MM-dd") +
                           "','" + DateTime.Now.ToString("HH:mm:ss") +
                           "','" + descripcion_servicio.Text + "')");
            Bdatos.Desconectar();
        }

        public int guardarClientes()
        {
                    int consulta;
                    Bdatos.conexion();

                    //GENERO EL ID
                    id_cliente = generarId();

                    //INSERTA DATOS
                    consulta = Bdatos.peticion("insert into clientes (id_cliente,nombre, foto," +
                        "numero_tel_1, colonia,calle, referencias, eliminado)" +
                        "values('" + id_cliente + "','" + nombre_cliente.Text +
                        "','" + nombre_archivo + "." + formato +
                        "','" + num_tel_1_cliente.Text +
                        "','" + colonia_cliente.Text +
                        "','" + calle_cliente.Text +
                        "','" + ref_cliente.Text +
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
            nombre_archivo = "sin_foto";
            num_tel_1_cliente.Text = "";
            colonia_cliente.Text = "";
            calle_cliente.Text = "";
            ref_cliente.Text = "";
            unidad_servicio.Text = "";
            descripcion_servicio.Text = "";
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

        private void unidad_servicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void num_tel_1_cliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void editar_btn_cliente_Click(object sender, EventArgs e)
        {

        }
    }
}
