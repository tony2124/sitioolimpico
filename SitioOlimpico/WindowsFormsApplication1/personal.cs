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
        public Boolean foto = false, editar = false;
        public String nombre_archivo = "sin_foto", 
            rutadestino = @"C:\\sitioOlimpicoPics\\personal",
            formato, id_personal;

        ConexionBD Bdatos = new ConexionBD();
        MySqlDataReader Datos;

        Boolean ventanaUnidades;

        public personal(int forma, string id_perso, Boolean ventanaUnidades)
        {
            InitializeComponent();
            this.ventanaUnidades = ventanaUnidades;
            id_personal = id_perso;
            if (forma == 0)
            {
                editar_btn_personal.Visible = false;
                this.foto_personal.Image = Properties.Resources.sin_foto;
                foto_personal.SizeMode = PictureBoxSizeMode.StretchImage;
                desvincular_unidad_personal.Visible = false;
                historial_personal.Visible = false;

                estado_civil_personal.SelectedIndex = 0;
                autorizacion_personal.SelectedIndex = 0;
            }
            else
            {
                desvincular_unidad_personal.Visible = true;
                label_personal.Text = "Unidad: ";
                unidad_personal.Visible = true;
                historial_personal.Visible = false;
               // this.panel6.Size = new System.Drawing.Size(217, 100);

                horario_entrada_personal.Visible = false;
                horario_salida_personal.Visible = false;
                label30.Visible = false;
                label29.Visible = false;
                guardar_btn_personal.Enabled = false;
                Bdatos.conexion();

                Datos = Bdatos.obtenerBasesDatosMySQL("select nombre, foto, fecha_ingreso,"
                    +"fecha_nacimiento, telefono, numero_cel, estado_civil, colonia, calle, numero_int, numero_ext,"+
                    "codigo_postal, ciudad, estado, referencias, nivel_autorizacion, asignado, horario_entrada, horario_salida from personal where id_personal ='"+id_perso+"'");
                int asignado = 0;
                if(Datos.HasRows)
                    while (Datos.Read())
                    {
                        nombre_personal.Text = Datos.GetString(0);

                        //FOTO
                        if (Datos.GetString(1).CompareTo("sin_foto.") == 0)
                            this.foto_personal.Image = Properties.Resources.sin_foto;
                        else
                            foto_personal.ImageLocation = rutadestino + "\\" + Datos.GetString(1);
                        foto_personal.SizeMode = PictureBoxSizeMode.StretchImage;

                        fecha_ingreso_personal.Text = Datos.GetString(2);
                        fecha_nac_personal.Text = Datos.GetString(3);
                        num_tel_personal.Text = Datos.GetString(4);
                        num_cel_personal.Text = Datos.GetString(5);
                        estado_civil_personal.SelectedIndex = int.Parse(Datos.GetString(6));
                        colonia_personal.Text = Datos.GetString(7);
                        calle_personal.Text = Datos.GetString(8);
                        num_int_personal.Text = Datos.GetString(9);
                        num_ext_personal.Text = Datos.GetString(10);
                        cp_personal.Text = Datos.GetString(11);
                        ciudad_personal.Text = Datos.GetString(12);
                        estado_personal.Text = Datos.GetString(13);
                        ref_personal.Text = Datos.GetString(14);
                        autorizacion_personal.SelectedIndex = int.Parse(Datos.GetString(15));
                        if (int.Parse(Datos.GetString(15)) == 0)
                            asignado = Datos.GetInt32(16);
                        else
                        {
                            horario_entrada_personal.Text = Datos.GetString(17);
                            horario_salida_personal.Text = Datos.GetString(18);
                        }

                    }

                Datos.Close();

                if (autorizacion_personal.SelectedIndex == 0)
                {//SI ES TAXISTA
                    if (asignado == 1)
                    {
                        Datos = Bdatos.obtenerBasesDatosMySQL("select numero_unidad from taxista_unidad where id_personal = " + id_perso + " order by fecha asc");
                        if (Datos.HasRows)
                            while (Datos.Read())
                                unidad_personal.Text = Datos.GetString(0);

                        Bdatos.Desconectar();
                        desvincular_unidad_personal.Enabled = true;
                    }
                    else
                    {

                        desvincular_unidad_personal.Enabled = false;
                    }
                    historial_personal.Visible = true;
                }
                
                    
                deshabilitar();
                editar_btn_personal.Visible = true;
            }

            encabezado_personal.Image = Properties.Resources.PERSONAL_ENCABEZADO;
            encabezado_personal.SizeMode = PictureBoxSizeMode.StretchImage;


            //CONSULTA A LA BASE DE DATOS PARA EXTRAER INFORMACIÓN DE LAS UNIDADES
            int contador = 0;
            String[] A;

            Bdatos.conexion();
            Datos = Bdatos.obtenerBasesDatosMySQL("select count(numero_unidad) from unidades where asignado = 0");
            if (Datos.HasRows)
                while (Datos.Read())
                    contador = Datos.GetInt32(0);
            Datos.Close();
            if (contador > 0)
            {
                Datos = Bdatos.obtenerBasesDatosMySQL("select numero_unidad from unidades where asignado = 0");
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

        public void deshabilitar()
        {
            nombre_personal.Enabled = false;
            fecha_ingreso_personal.Enabled = false;
            fecha_nac_personal.Enabled = false;
            num_tel_personal.Enabled = false;
            num_cel_personal.Enabled = false;
            estado_civil_personal.Enabled = false;
            colonia_personal.Enabled = false;
            calle_personal.Enabled = false;
            num_int_personal.Enabled = false;
            num_ext_personal.Enabled = false;
            cp_personal.Enabled = false;
            ciudad_personal.Enabled = false;
            estado_personal.Enabled = false;
            ref_personal.Enabled = false;
            autorizacion_personal.Enabled = false;
            unidad_personal.Enabled = false;
            foto_btn_personal.Enabled = false;
            horario_entrada_personal.Enabled = false;
            horario_salida_personal.Enabled = false;

        }

        public void habilitar()
        {
            nombre_personal.Enabled = true;
            fecha_ingreso_personal.Enabled = true;
            fecha_nac_personal.Enabled = true;
            num_tel_personal.Enabled = true;
            num_cel_personal.Enabled = true;
            estado_civil_personal.Enabled = true;
            colonia_personal.Enabled = true;
            calle_personal.Enabled = true;
            num_int_personal.Enabled = true;
            num_ext_personal.Enabled = true;
            cp_personal.Enabled = true;
            ciudad_personal.Enabled = true;
            estado_personal.Enabled = true;
            ref_personal.Enabled = true;
            autorizacion_personal.Enabled = true;
            foto_btn_personal.Enabled = true;
            horario_entrada_personal.Enabled = true;
            horario_salida_personal.Enabled = true;
           // unidad_personal.Enabled = true;
            //autorizacion_personal.Enabled = true;
           // unidad_personal.Enabled = true;

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

        public bool existe_unidad(string unidad)
        {
            int existe = 0;
            Bdatos.conexion();
            Datos = Bdatos.obtenerBasesDatosMySQL("SELECT count(numero_unidad) from unidades where numero_unidad=" + unidad + " and asignado = 0");
            if (Datos != null)
            {
                 if (Datos.HasRows)
                    while (Datos.Read())
                        existe = Datos.GetInt32(0);
             } 
            Datos.Close();
            Bdatos.Desconectar();
            if (existe == 0)
                return false;
            return true;

        }

        public void relacionar_taxista_unidad(string id_taxista, string unidad)
        {
            Bdatos.conexion();
            Bdatos.peticion("insert into taxista_unidad(id_personal, numero_unidad, fecha) VALUES ('"+id_taxista+"','"+unidad+"','"+DateTime.Now.ToString("yyyy-MM-dd")+"')");
            Bdatos.peticion("UPDATE personal set asignado = 1 where id_personal = '"+id_taxista+"'");
            Bdatos.peticion("UPDATE unidades set asignado = 1 where numero_unidad = '" + unidad + "'");

            Bdatos.Desconectar();
        }

        private void guardar_btn_personal_Click(object sender, EventArgs e)
        {
            if (editar)
            {
                if (nombre_personal.Text.CompareTo("") == 0 )
                {
                    MessageBox.Show("El campo de nombre es obligatorio",
                        "LLene los campos obligatorios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                else//SI SI TIENEN DATOS NOMBRE Y APELLIDO
                {
                   if (foto)//SI SE SELECCIONO FOTO SE GUARDA LA FOTO
                    {
                        Bdatos.conexion();
                        String foto_name = "";
                        Datos = Bdatos.obtenerBasesDatosMySQL("select foto from personal where id_personal = '" + id_personal + "'");
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

                        Bdatos.peticion("update personal set " +
                            "foto = '" + nombre_archivo + "." + formato + "' WHERE id_personal = '" + id_personal+ "'");

                        Bdatos.Desconectar();
                        foto = false;
                    }


                   if (unidad_personal.Text.CompareTo("") != 0)
                   {

                       if (existe_unidad(unidad_personal.Text) || unidad_personal.Enabled == false)
                       {
                           if (unidad_personal.Enabled == true)
                               relacionar_taxista_unidad(id_personal, unidad_personal.Text);

                           desvincular_unidad_personal.Enabled = true;
                           if (editarPersonal() > 0)
                           {
                               //BORRAMOS LOS DATOS PARA UN SIGUIENTE REGISTRO
                               editar = false;

                               deshabilitar();
                               guardar_btn_personal.Enabled = false;
                               editar_btn_personal.Enabled = true;
                               MessageBox.Show("Datos editados correctamente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                           }

                       }
                       else
                           MessageBox.Show("NO EXISTE LA UNIDAD SELECCIONADA O ESTÁ ASIGNADA A OTRO CHOFER");
                   }
                   else
                   {
                       if (editarPersonal() > 0)
                       {
                           //BORRAMOS LOS DATOS PARA UN SIGUIENTE REGISTRO
                           editar = false;

                           deshabilitar();
                           guardar_btn_personal.Enabled = false;
                           editar_btn_personal.Enabled = true;
                           MessageBox.Show("Datos editados correctamente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       }
                   }
                        

                        


                                //MessageBox.Show("Edicion");
                }//FIN DE SI SI TIENEN DATOS NOMBRE Y APELLIDO
            }
            else
            {

                // MessageBox.Show(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

                //VERIFICA SI ESTAN LLENOS LOS CAMPOS DE NOMBRE Y APELLIDO.
                if (nombre_personal.Text.CompareTo("") == 0)
                {
                    MessageBox.Show("El campo de nombre es obligatorios",
                        "LLene los campos obligatorios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (autorizacion_personal.SelectedIndex == 0)//SI ES TAXISTA
                    {
                        if (unidad_personal.Text.CompareTo("") == 0)//SI EL CAMPO UNIDAD ESTA VACIO
                        {
                            DialogResult result = MessageBox.Show("No has llenado el campo de unidad. ¿Deseas guardar el taxista sin asignarle una unidad?",
                                   "¿Seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (result == DialogResult.Yes)
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
                        else
                        {
                            Bdatos.conexion();

                            Datos = Bdatos.obtenerBasesDatosMySQL("SELECT count(numero_unidad) from unidades where numero_unidad=" + unidad_personal.Text + " and asignado = 0");
                            if (Datos != null)
                            {
                                int existe = 0;

                                if (Datos.HasRows)
                                    while (Datos.Read())
                                        existe = Datos.GetInt32(0);
                                Datos.Close();
                                Bdatos.Desconectar();

                                if (existe == 0)//Si no existe la unidad
                                {
                                    MessageBox.Show("No se puede guardar un taxista con una unidad que no este registrada o ya este asignada a otro taxista, por favor registre o desvincule primero la unidad en el programa.", "Error: Unidad no registrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    if (guardarPersonal() > 0)
                                    {


                                        Bdatos.peticion("insert into taxista_unidad (id_personal, numero_unidad, fecha)" +
                                                "values('" + id_personal + "'," + unidad_personal.Text + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "')");

                                        if (foto)//Si busco foto se guarda la foto
                                        {
                                            guardarfoto();
                                        }

                                        //MODIFICAMOS LA UNIDAD PARA PONERLA ASIGNADA
                                        Bdatos.conexion();
                                        Bdatos.peticion("update unidades set asignado = 1 where numero_unidad = " + unidad_personal.Text);
                                        //MODIFICAMOS EL PERSONAL PARA PONERLO ASIGNADO
                                        Bdatos.peticion("update personal set asignado = 1 where id_personal = " + id_personal);
                                        Bdatos.Desconectar();

                                        //BORRAMOS LOS DATOS PARA UN SIGUIENTE REGISTRO
                                        borrarCampos();
                                        MessageBox.Show("Datos ingresados correctamente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                    }//FIN DEL SI ES TAXISTA O MONITOR
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
                }//FIN DE LA VERIFICACION SI LOS CAMPOS NOMBRE ESTAN VACIOS
            }
        }

        public int editarPersonal()
        {
            int resultado;
            
            Bdatos.conexion();
            if (autorizacion_personal.SelectedIndex == 0)
            {
                resultado = Bdatos.peticion("update personal set nombre= '" + nombre_personal.Text +
                    "', fecha_ingreso= '" + fecha_ingreso_personal.Value.ToString("yyyy-MM-dd") +
                    "', fecha_nacimiento = '" + fecha_nac_personal.Value.ToString("yyyy-MM-dd") +
                    "', telefono = '" + num_tel_personal.Text +
                    "', numero_cel = '" + num_cel_personal.Text +
                    "', estado_civil='" + estado_civil_personal.SelectedIndex +
                    "', colonia='" + colonia_personal.Text +
                    "', calle='" + calle_personal.Text +
                    "', numero_int='" + num_int_personal.Text +
                    "', numero_ext='" + num_ext_personal.Text +
                    "', codigo_postal='" + cp_personal.Text +
                    "', ciudad='" + ciudad_personal.Text +
                    "', estado='" + estado_personal.Text +
                    "', referencias='" + ref_personal.Text +
                    "', nivel_autorizacion=0" +
                    " where id_personal = " + id_personal);
            }
            else
            {
                resultado = Bdatos.peticion("update personal set nombre= '" + nombre_personal.Text +
                   "', fecha_ingreso= '" + fecha_ingreso_personal.Value.ToString("yyyy-MM-dd") +
                   "', fecha_nacimiento = '" + fecha_nac_personal.Value.ToString("yyyy-MM-dd") +
                   "', telefono = '" + num_tel_personal.Text +
                   "', numero_cel = '" + num_cel_personal.Text +
                   "', estado_civil='" + estado_civil_personal.SelectedIndex +
                   "', colonia='" + colonia_personal.Text +
                   "', calle='" + calle_personal.Text +
                   "', numero_int='" + num_int_personal.Text +
                   "', numero_ext='" + num_ext_personal.Text +
                   "', codigo_postal='" + cp_personal.Text +
                   "', ciudad='" + ciudad_personal.Text +
                   "', estado='" + estado_personal.Text +
                   "', referencias='" + ref_personal.Text +
                   "', nivel_autorizacion=1" +
                   ", horario_entrada='" + horario_entrada_personal.Value.ToString("HH:mm:ss") +
                   "', horario_salida='" + horario_salida_personal.Value.ToString("HH:mm:ss") +
                   "' where id_personal = " + id_personal);
            }


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
                consulta = Bdatos.peticion("insert into personal (id_personal,nombre,foto," +
                    "fecha_ingreso,fecha_nacimiento,telefono,numero_cel,estado_civil," +
                    "colonia,calle,numero_int,numero_ext,codigo_postal,ciudad,estado," +
                    "referencias,nivel_autorizacion,asignado,eliminado)" +
                    "values('"+ id_personal + 
                    "','" + nombre_personal.Text +
                    "','" + nombre_archivo + "." + formato +
                    "','" + fecha_ingreso_personal.Value.ToString("yyyy-MM-dd") +
                    "','" + fecha_nac_personal.Value.ToString("yyyy-MM-dd") +
                    "','" + num_tel_personal.Text +
                    "','" + num_cel_personal.Text +
                    "','" + estado_civil_personal.SelectedIndex +
                    "','" + colonia_personal.Text +
                    "','" + calle_personal.Text +
                    "','" + num_int_personal.Text +
                    "','" + num_ext_personal.Text +
                    "','" + cp_personal.Text +
                    "','" + ciudad_personal.Text +
                    "','" + estado_personal.Text +
                    "','" + ref_personal.Text +
                    "','" + autorizacion_personal.SelectedIndex +
                    "',0,0)");

                
                    return consulta;
                
            }
            else
            {
                consulta = Bdatos.peticion("insert into personal (id_personal, nombre,foto," +
                   "fecha_ingreso,fecha_nacimiento,telefono,numero_cel,estado_civil," +
                   "colonia,calle,numero_int,numero_ext,codigo_postal,ciudad,estado," +
                   "referencias,nivel_autorizacion,horario_entrada,horario_salida,eliminado)" +
                   "values('" + id_personal +
                   "','" + nombre_personal.Text +
                   "','" + nombre_archivo + "." + formato +
                   "','" + fecha_ingreso_personal.Value.ToString("yyyy-MM-dd") +
                   "','" + fecha_nac_personal.Value.ToString("yyyy-MM-dd") +
                   "','" + num_tel_personal.Text +
                   "','" + num_cel_personal.Text +
                   "','" + estado_civil_personal.SelectedIndex +
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
                + "" + DateTime.Now.Second;
        }


        private void cancelar_btn_personal_Click(object sender, EventArgs e)
        {
            if (ventanaUnidades)
            {
                Bdatos.conexion();

                Datos = Bdatos.obtenerBasesDatosMySQL("SELECT nombre from personal where id_personal=" + id_personal + " and asignado= 1");
                if (Datos != null)
                {
                    if (Datos.HasRows)
                        while (Datos.Read())
                            unidades.txt.Text = Datos.GetString(0);
                    Datos.Close();
                    Bdatos.Desconectar();
                }
                else
                {
                    unidades.txt.Text = "Aún no se le asigna chofer";
                }
            }
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
                //this.panel6.Size = new System.Drawing.Size(217, 100);

                horario_entrada_personal.Visible = false;
                horario_salida_personal.Visible = false;
                label30.Visible = false;
                label29.Visible = false;
                
            }
            else
            {
                label_personal.Text = "Horario: ";
                //this.panel6.Size = new System.Drawing.Size(217, 126);
                horario_entrada_personal.Visible = true;
                horario_salida_personal.Visible = true;
                label30.Visible = true;
                label29.Visible = true;
                desvincular_unidad_personal.Visible = false;
                
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

        private void editar_btn_personal_Click(object sender, EventArgs e)
        {
            guardar_btn_personal.Enabled = true;
            editar_btn_personal.Enabled = false;
            desvincular_unidad_personal.Enabled = false;
            if (unidad_personal.Text.CompareTo("") == 0)
                unidad_personal.Enabled = true;
            habilitar();
            editar = true;

        }

        private void agregar_unidad_personal_Click(object sender, EventArgs e)
        {
            unidades u = new unidades(0,"");
            u.ShowDialog();
        }

        private void desvincular_unidad_personal_Click(object sender, EventArgs e)
        {
            //DESVINCULAMOS LA UNIDAD
            Bdatos.conexion();
            Bdatos.peticion("update unidades set asignado = 0 where numero_unidad = " + unidad_personal.Text);
            Bdatos.peticion("update personal set asignado = 0 where id_personal = " + id_personal);
            Bdatos.Desconectar();

            unidad_personal.Enabled = false;
            unidad_personal.Text = "";
            desvincular_unidad_personal.Enabled = false;
            MessageBox.Show("El taxista ha sido desvinculado de la unidad correctamente");

            if (ventanaUnidades)
            {
                unidades.txt.Text = "Aun no se le asigna chofer";
                unidades.chofer_inf.Visible = false;
                Dispose();
            }
        }

        private void personal_Load(object sender, EventArgs e)
        {

        }

        private void personal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ventanaUnidades)
            {
                Bdatos.conexion();

                Datos = Bdatos.obtenerBasesDatosMySQL("SELECT nombre from personal where id_personal=" + id_personal+" and asignado= 1");
                if (Datos != null)
                {
                    if (Datos.HasRows)
                        while (Datos.Read())
                            unidades.txt.Text = Datos.GetString(0);
                    Datos.Close();
                    Bdatos.Desconectar();
                }
                else
                {
                    unidades.txt.Text = "Aún no se le asigna chofer";
                }
            }
        }//FIN DEL METODO DE FORM CLOSING

        private void historial_personal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           /* String cadena="", asignado="0", unidad_actual="";
            Bdatos.conexion();
            Datos = Bdatos.obtenerBasesDatosMySQL("select id_personal, nombre, numero_unidad, fecha, asignado from personal natural join taxista_unidad where id_personal ='" + id_personal + "' ");
            if(Datos.HasRows)
                while (Datos.Read())
                {
                    cadena += Datos.GetString(0) +" - "+Datos.GetString(1)+" - "+Datos.GetString(2)+" - "+Datos.GetDateTime(3).ToString("yyyy-MM-dd");
                    cadena += "\n";
                    asignado = Datos.GetString(4);
                    unidad_actual = Datos.GetString(2);
                }
            Bdatos.Desconectar();

            if (int.Parse(asignado) == 1)
                cadena += "\n\n\nUnidad actual: "+unidad_actual+" \n";
            else
                cadena += "\n\n\nAún no se le asigna unidad\n";

            MessageBox.Show(cadena);*/
            mostrarHistorialUnidades mhu = new mostrarHistorialUnidades(id_personal);
            mhu.ShowDialog();
        }
    }
}
