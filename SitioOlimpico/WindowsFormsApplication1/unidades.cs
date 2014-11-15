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
    public partial class unidades : Form
    {
        /*VARIABLES DE LA BASE DE DATOS*/
        MySqlDataReader Datos;
        ConexionBD Bdatos = new ConexionBD();

        public OpenFileDialog BuscarImagen;
        public Boolean foto = false;
        public String nombre_archivo = "sin_foto", 
            rutadestino = @"C:\\sitioOlimpicoPics\\unidades", 
            formato;

        public Boolean editar = false;
        String unidad_id, id_personal_unidad;

        public static TextBox txt;
        public static LinkLabel chofer_inf;
        public unidades(int forma, String unidad)
        {
            InitializeComponent();
            txt = chofer_unidad;
            chofer_inf = info_chofer_unidad;
            unidad_id = unidad;
            if (forma == 0)
            {
                editar_btn_unidades.Visible = false;
                label10.Visible = false;
                info_chofer_unidad.Visible = false;
                chofer_unidad.Visible = false;
                this.foto_unidad.Image = Properties.Resources.sin_foto_taxi;
                foto_unidad.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                
               /* this.ClientSize = new System.Drawing.Size(621, 536);
                this.editar_btn_unidades.Location = new System.Drawing.Point(168, 432);
                this.guardar_btn_unidades.Location = new System.Drawing.Point(293, 432);
                this.cancelar_btn_unidades.Location = new System.Drawing.Point(421, 432);
               // panel6.Size = new System.Drawing.Size(545, 187);*/
                editar_btn_unidades.Visible = true;
                guardar_btn_unidades.Enabled = false;
                int asignado = 0;
                Bdatos.conexion();
                Datos = Bdatos.obtenerBasesDatosMySQL("SELECT numero_unidad," +
                    "modelo, marca, color, descripcion, placas," +
                    "foto, fecha_ingreso,asignado from unidades where numero_unidad=" + unidad);
                String[] datosUnidades = new String[8];
                if (Datos.HasRows)
                    while (Datos.Read())
                    {
                        numero_unidad.Text = Datos.GetInt32(0) + "";
                        modelo_unidad.Text = Datos.GetString(1) + "";
                        marca_unidad.Text = Datos.GetString(2) + "";
                        color_unidad.Text = Datos.GetString(3) + "";
                        descripcion_unidad.Text = Datos.GetString(4) + "";
                        placas_unidad.Text = Datos.GetString(5) + "";

                        //FOTO
                          if (Datos.GetString(6).CompareTo("sin_foto.") == 0)
                              this.foto_unidad.Image = Properties.Resources.sin_foto_taxi;
                          else
                              foto_unidad.ImageLocation = rutadestino + "\\" + Datos.GetString(6);
                          foto_unidad.SizeMode = PictureBoxSizeMode.StretchImage;
                          

                        //foto_unidad.ImageLocation = rutadestino+"\\"+Datos.GetString(6) + "";
                        fecha_ingreso_unidades.Text = Datos.GetString(7) + "";
                        asignado = Datos.GetInt32(8);
                    }
                Datos.Close();
                Bdatos.Desconectar();
                deshabilitar();

                //CONSULTA PARA OBTENER EL CHOFER DE LA UNIDAD
                if (asignado == 0)
                {
                    chofer_unidad.Text = "Aún no se le asigna chofer";
                    info_chofer_unidad.Visible = false;
                }
                else
                {
                    Bdatos.conexion();
                    id_personal_unidad = "";
                    //MessageBox.Show(unidad_id);
                    Datos = Bdatos.obtenerBasesDatosMySQL("select id_personal from taxista_unidad where numero_unidad = " + unidad_id + " order by fecha asc");
                    if (Datos.HasRows)
                        while (Datos.Read())
                            id_personal_unidad = Datos.GetString(0);
                    Datos.Close();
                    // MessageBox.Show(id_personal_unidad);
                    if (id_personal_unidad.CompareTo("") == 0)
                    {
                        chofer_unidad.Text = "Aún no se le asigna chofer";
                        info_chofer_unidad.Visible = false;
                    }
                    else
                    {
                        Datos = Bdatos.obtenerBasesDatosMySQL("select nombre from personal where id_personal = " + id_personal_unidad);
                        if (Datos.HasRows)
                            while (Datos.Read())
                                chofer_unidad.Text = Datos.GetString(0);
                        Datos.Close();
                        Bdatos.Desconectar();
                    }
                    
                }
                guardar_btn_unidades.Enabled = false;


            }


        }

        public void deshabilitar()
        {
            numero_unidad.Enabled = false;
            modelo_unidad.Enabled = false;
            marca_unidad.Enabled = false;
            color_unidad.Enabled = false;
            descripcion_unidad.Enabled = false;
            placas_unidad.Enabled = false;
            fecha_ingreso_unidades.Enabled = false;
            foto_btn_unidad.Enabled = false;
        }

        public void habilitar()
        {
            numero_unidad.Enabled = true;
            modelo_unidad.Enabled = true;
            marca_unidad.Enabled = true;
            color_unidad.Enabled = true;
            descripcion_unidad.Enabled = true;
            placas_unidad.Enabled = true;
            fecha_ingreso_unidades.Enabled = true;
            foto_btn_unidad.Enabled = true;
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
            if (editar)
            {

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
                        Bdatos.conexion();
                        String foto_name = "" ;
                        Datos = Bdatos.obtenerBasesDatosMySQL("select foto from unidades where numero_unidad = '" + unidad_id + "'");
                        if (Datos.HasRows)
                            while (Datos.Read())
                                foto_name = Datos.GetString(0);
                        Datos.Close();
                        Bdatos.Desconectar();

                        String archivoDestino = System.IO.Path.Combine(rutadestino, foto_name);
                        MessageBox.Show(archivoDestino);
                        File.Delete(archivoDestino);
                        guardarfoto();
                        Bdatos.conexion();

                        Bdatos.peticion("update unidades set " +
                            "foto = '" + nombre_archivo + "." + formato + "' WHERE numero_unidad = '" + unidad_id + "'");

                        Bdatos.Desconectar();
                        foto = false;
                    }

                    if (editarUnidades() > 0)
                    {
                        //BORRAMOS LOS DATOS PARA UN SIGUIENTE REGISTRO
                        editar = false;
                        deshabilitar();
                        unidad_id = numero_unidad.Text;
                        guardar_btn_unidades.Enabled = false;
                        MessageBox.Show("Datos editados correctamente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                   
                }

            }
            else
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
                        foto = false;
                    }

                    if (guardarUnidades() > 0)
                    {
                        //BORRAMOS LOS DATOS PARA UN SIGUIENTE REGISTRO

                        borrarCampos();
                        MessageBox.Show("Datos ingresados correctamente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            
        }

        public int editarUnidades()
        {
            int resultado;

            Bdatos.conexion();
            resultado = Bdatos.peticion("update unidades set numero_unidad= "+numero_unidad.Text+
                ", modelo = '"+modelo_unidad.Text+"', marca= '"+marca_unidad.Text+
                "',color = '"+color_unidad.Text+"', descripcion = '"+descripcion_unidad.Text+
                "',placas = '"+placas_unidad.Text+"', fecha_ingreso='"+fecha_ingreso_unidades.Value.ToString("yyyy-MM-dd")+
                "'where numero_unidad = "+unidad_id);
            Bdatos.Desconectar();

            return resultado;
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
                "color, descripcion, placas, foto, fecha_ingreso, asignado, eliminado)" +
                "values('" + numero_unidad.Text + 
                "','" + modelo_unidad.Text +
                "','" + marca_unidad.Text +
                "','" + color_unidad.Text +
                "','" + descripcion_unidad.Text +
                "','" + placas_unidad.Text +
                "','" + nombre_archivo + "." + formato +
                "','" + fecha_ingreso_unidades.Value.ToString("yyyy-MM-dd") +
                "',0,0)");

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

        private void editar_btn_unidades_Click(object sender, EventArgs e)
        {
            habilitar();
            editar = true;
            guardar_btn_unidades.Text = "GUARDAR EDICIÓN";
            guardar_btn_unidades.Enabled = true;
        }

        private void info_chofer_unidad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            personal p = new personal(1, id_personal_unidad,true);
            p.ShowDialog();
        }

        private void desvincular_btn_unidad_Click(object sender, EventArgs e)
        {

        }
    }
}
