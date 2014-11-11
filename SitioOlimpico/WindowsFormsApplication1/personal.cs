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
    public partial class personal : Form
    {
        public OpenFileDialog BuscarImagen;
        public Boolean foto = false;
        public String nombre_archivo = "sin_foto", 
            rutadestino = @"C:\\sitioOlimpicoPics\\personal",
            formato, id_personal;

        ConexionBD Bdatos = new ConexionBD();
        MySqlDataReader Datos;

        public personal(int forma)
        {
            InitializeComponent();

            if (forma == 0)
                editar_btn_personal.Visible = false;
            else
                editar_btn_personal.Visible = true;

            this.foto_personal.Image = Properties.Resources.sin_foto;
            foto_personal.SizeMode = PictureBoxSizeMode.StretchImage;
            estado_civil_personal.SelectedIndex = 0;
            autorizacion_personal.SelectedIndex = 0;
            encabezado_personal.Image = Properties.Resources.PERSONAL_ENCABEZADO;
            encabezado_personal.SizeMode = PictureBoxSizeMode.StretchImage;


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
                while (Datos.Read())
                {
                    A[i] = Datos.GetInt32(0) + "";
                    i++;
                }

                Datos.Close();

                this.unidad_personal.AutoCompleteCustomSource.AddRange(A);

            }


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
               
                if (guardarPersonal() > 0)
                {
                    if (foto)//Si busco foto se guarda la foto
                    {
                        guardarfoto();
                    }

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

        public int guardarPersonal()
        {
            int consulta;
            id_personal = generarId();
            Bdatos.conexion();

            if (autorizacion_personal.SelectedIndex == 0)
            {
                consulta = Bdatos.peticion("insert into personal (id_personal,nombre,apellido,foto," +
                    "fecha_ingreso,fecha_nacimiento,telefono,numero_cel,estado_civil," +
                    "colonia,calle,numero_int,numero_ext,codigo_postal,ciudad,estado," +
                    "referencias,nivel_autorizacion,eliminado)" +
                    "values('"+ id_personal + 
                    "','" + nombre_personal.Text +
                    "','" + apellido_personal.Text +
                    "','" + nombre_archivo + "." + formato +
                    "','" + fecha_ingreso_personal.Value.ToString("yyyy-MM-dd") +
                    "','" + fecha_nac_personal.Value.ToString("yyyy-MM-dd") +
                    "','" + num_tel_personal.Text +
                    "','" + num_cel_personal.Text +
                    "','" + estado_civil_personal.Text +
                    "','" + colonia_personal.Text +
                    "','" + calle_personal.Text +
                    "','" + num_int_personal.Text +
                    "','" + num_ext_personal.Text +
                    "','" + cp_personal.Text +
                    "','" + ciudad_personal.Text +
                    "','" + estado_personal.Text +
                    "','" + ref_personal.Text +
                    "','" + autorizacion_personal.SelectedIndex +
                    "',0)");

                if (consulta > 0)
                {
                    if (Bdatos.peticion("insert into taxista_unidad (id_personal, numero_unidad, fecha)"+
                        "values('" + id_personal + "'," + unidad_personal.Text + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "')") > 0)
                    {
                        //MessageBox.Show("Unidad guardada exitosamente", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Bdatos.Desconectar();
                    return consulta;

                }
                else
                {
                    Bdatos.Desconectar();
                    return consulta;
                }
            }
            else
            {
                consulta = Bdatos.peticion("insert into personal (id_personal, nombre,apellido,foto," +
                   "fecha_ingreso,fecha_nacimiento,telefono,numero_cel,estado_civil," +
                   "colonia,calle,numero_int,numero_ext,codigo_postal,ciudad,estado," +
                   "referencias,nivel_autorizacion,horario_entrada,horario_salida,eliminado)" +
                   "values('" + id_personal +
                   "','" + nombre_personal.Text +
                   "','" + apellido_personal.Text +
                   "','" + nombre_archivo + "." + formato +
                   "','" + fecha_ingreso_personal.Value.ToString("yyyy-MM-dd") +
                   "','" + fecha_nac_personal.Value.ToString("yyyy-MM-dd") +
                   "','" + num_tel_personal.Text +
                   "','" + num_cel_personal.Text +
                   "','" + estado_civil_personal.Text +
                   "','" + colonia_personal.Text +
                   "','" + calle_personal.Text +
                   "','" + num_int_personal.Text +
                   "','" + num_ext_personal.Text +
                   "','" + cp_personal.Text +
                   "','" + ciudad_personal.Text +
                   "','" + estado_personal.Text +
                   "','" + ref_personal.Text +
                   "','" + autorizacion_personal.SelectedIndex +
                   "','" + horario_entrada_personal.Value.ToString("HH:mm:ss") +
                   "','" + horario_salida_personal.Value.ToString("HH:mm:ss") +
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


        }

        public void borrarCampos()
        {
            nombre_personal.Text = "";
            apellido_personal.Text = "";
            nombre_archivo = "sin_foto";
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
            horario_entrada_personal.Value = DateTime.Now;
            horario_salida_personal.Value = DateTime.Now;
            unidad_personal.Text = "";
            foto = false;

            this.foto_personal.ImageLocation = null;
            this.foto_personal.Image = Properties.Resources.sin_foto;
            foto_personal.SizeMode = PictureBoxSizeMode.StretchImage;
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


        private void cancelar_btn_personal_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void num_tel_personal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void num_cel_personal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void autorizacion_personal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (autorizacion_personal.SelectedIndex == 0)
            {
                label_personal.Text = "Unidad: ";
                unidad_personal.Visible = true;
                this.panel6.Size = new System.Drawing.Size(217, 100);

                horario_entrada_personal.Visible = false;
                horario_salida_personal.Visible = false;
                label30.Visible = false;
                label29.Visible = false;
            }
            else
            {
                label_personal.Text = "Horario: ";
                this.panel6.Size = new System.Drawing.Size(217, 126);
                horario_entrada_personal.Visible = true;
                horario_salida_personal.Visible = true;
                label30.Visible = true;
                label29.Visible = true;

                
                unidad_personal.Visible = false;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void unidad_personal_KeyPress(object sender, KeyPressEventArgs e)
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
