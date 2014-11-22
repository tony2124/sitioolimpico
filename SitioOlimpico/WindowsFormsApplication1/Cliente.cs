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
        public String id_cliente;
        int FORMA;

        /*VARIABLES DE LA BASE DE DATOS*/
        MySqlDataReader Datos;
        ConexionBD Bdatos = new ConexionBD();

        /*VARIABLES PARA LA EDICION DE LOS CLIENTES*/
        Boolean se_encontro_cliente = false, editado = false;


        public cliente(string numero, int forma)
        {
            InitializeComponent();
            //autocompletar_unidades_disponibles();
            editar_btn_cliente.Visible = false;
            FORMA = forma;

            if (FORMA != 0)
            {
                
                num_tel_1_cliente.Text = numero;

                /**** VERIFICAR SI EXISTE EL CLIENTE **********/
                Bdatos.conexion();
                Datos = Bdatos.obtenerBasesDatosMySQL("select count(nombre) from clientes where numero_tel_1 = '" + numero + "'");
                int existe_cliente = 0;
                if (Datos.HasRows)
                    while (Datos.Read())
                        existe_cliente = Datos.GetInt32(0);
                Datos.Close();

                /**SI EXISTE***/
                if (existe_cliente != 0)
                {

                    Datos = Bdatos.obtenerBasesDatosMySQL("select id_cliente, nombre,colonia,calle,referencias from clientes where numero_tel_1 = '" + numero + "'");

                    if (Datos.HasRows)
                        while (Datos.Read())
                        {
                            id_cliente = Datos.GetString(0);
                            nombre_cliente.Text = Datos.GetString(1);
                            colonia_cliente.Text = Datos.GetString(2);
                            calle_cliente.Text = Datos.GetString(3);
                            ref_cliente.Text = Datos.GetString(4);
                        }
                    Datos.Close();

                    editar_btn_cliente.Visible = true;
                    deshabilitarCampos();
                    se_encontro_cliente = true;
                }
                else
                    num_tel_1_cliente.Enabled = false;

                Bdatos.Desconectar();
            }

           
            
        }

        void autocompletar_unidades_disponibles()
        {
            int contador = 0;
            String[] A;

            Bdatos.conexion();
            Datos = Bdatos.obtenerBasesDatosMySQL("select count(numero_unidad) from unidades where asignado = 1 and amonestado = 0");
            if (Datos.HasRows)
                while (Datos.Read())
                    contador = Datos.GetInt32(0);
            Datos.Close();

            if (contador > 0)
            {
                Datos = Bdatos.obtenerBasesDatosMySQL("select numero_unidad from unidades where asignado = 1 and amonestado = 0");
                int i = 0;
                A = new String[contador];
                while (Datos.Read())
                {
                    A[i] = Datos.GetInt32(0) + "";
                    i++;
                }

                Datos.Close();
                Bdatos.Desconectar();
                unidad_servicio.AutoCompleteCustomSource.AddRange(A);
            }
        }

        bool unidad_disponible(string unidad)
        {
            Bdatos.conexion();
            Datos = Bdatos.obtenerBasesDatosMySQL("SELECT count(numero_unidad) from unidades where numero_unidad='" + unidad + "' and amonestado = 0 and asignado = 1");
            int existe = 0;
            if (Datos.HasRows)
                while (Datos.Read())
                    existe = Datos.GetInt32(0);
            Datos.Close();
            Bdatos.Desconectar();
            if (existe == 0)
                return false;
            return true;
        }

        public void habilitarCampos()
        {
            //num_tel_1_cliente.Enabled = true;
            nombre_cliente.Enabled = true;
            colonia_cliente.Enabled = true;
            calle_cliente.Enabled = true;
            ref_cliente.Enabled = true;
        }

        public void deshabilitarCampos()
        {
            num_tel_1_cliente.Enabled = false;
            nombre_cliente.Enabled = false;
            colonia_cliente.Enabled = false;
            calle_cliente.Enabled = false;
            ref_cliente.Enabled = false;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        bool validacion()
        {
            if (nombre_cliente.Text.CompareTo("") == 0 ||
               num_tel_1_cliente.Text.CompareTo("") == 0)
            {
                MessageBox.Show("El campo nombre y teléfono son obligatorios",
                    "LLene los campos obligatorios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        void insertar_cliente()
        {
            DialogResult result;
            if (unidad_servicio.Text.CompareTo("") == 0)//VERIFICAMOS SI EL CAMPO DE UNIDAD NO TIENE DATOS
            {
                result = MessageBox.Show("No has llenado el campo de unidad para hacer el servicio. ¿Deseas guardar el cliente sin guardar el servicio?",
                       "¿Seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    //GUARDA EL CLIENTE SIN REALIZAR NINGUN SERVICIO
                    if (guardarClientes() > 0)
                    {
                        //BORRAMOS LOS DATOS PARA UN SIGUIENTE REGISTRO
                        borrarCampos();
                        MessageBox.Show("Datos ingresados correctamente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else//SI EL CAMPO DE UNIDAD TIENE DATOS: Guardamos el servicio junto con los datos del cliente
            {
                if (!unidad_disponible(unidad_servicio.Text))//Si no existe la unidad
                {
                    MessageBox.Show("No se puede utilizar la unidad seleccionada por alguna de las siguientes razones:\n* NO EXISTE\n*NO ESTÁ ASIGNADA\n* ESTÁ AMONESTADA\n\nResuelva el problema para continuar.", "Error: Unidad no válida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (guardarClientes() > 0)
                    {
                        guardarServicio(unidad_servicio.Text, descripcion_servicio.Text);

                        //BORRAMOS LOS DATOS PARA UN SIGUIENTE REGISTRO
                        borrarCampos();
                        MessageBox.Show("Datos ingresados correctamente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }

        void editar_cliente()
        {
            if (unidad_servicio.Text.CompareTo("") == 0)
            {
                if (editarClientes() > 0)
                {
                    deshabilitarCampos();
                    editar_btn_cliente.Enabled = true;
                    editado = false;
                    MessageBox.Show("Datos editados correctamente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (!unidad_disponible(unidad_servicio.Text))
                {
                    MessageBox.Show("No se puede utilizar la unidad seleccionada por alguna de las siguientes razones:\n* NO EXISTE\n*NO ESTÁ ASIGNADA\n* ESTÁ AMONESTADA\n\nResuelva el problema para continuar.", "Error: Unidad no válida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (editarClientes() > 0)
                    {
                        deshabilitarCampos();
                        editar_btn_cliente.Enabled = true;
                        editado = false;
                        guardarServicio(unidad_servicio.Text, descripcion_servicio.Text);
                        MessageBox.Show("Datos editados correctamente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void guardar_btn_personal_Click(object sender, EventArgs e)
        {

            if (editado)
            {
                if (validacion())
                {
                    editar_cliente();
                }
            }
            else
            {
                if (se_encontro_cliente)
                {
                    if (unidad_servicio.Text.CompareTo("") != 0)
                    {
                        if (unidad_disponible(unidad_servicio.Text))
                        {
                            guardarServicio(unidad_servicio.Text, descripcion_servicio.Text);
                            unidad_servicio.Text = "";
                            descripcion_servicio.Text = "";
                            MessageBox.Show("Se ha guardado el servicio al cliente ", " Acción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("No se puede utilizar la unidad seleccionada por alguna de las siguientes razones:\n* NO EXISTE\n*NO ESTÁ ASIGNADA\n* ESTÁ AMONESTADA\n\nResuelva el problema para continuar.", "Error: Unidad no válida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("No se ha seleccionado una unidad válida.", " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                }
                else
                {
                    //VERIFICA SI ESTAN LLENOS LOS CAMPOS DE NOMBRE Y TELEFONO.
                    if (validacion())
                    {
                        insertar_cliente();
                    }
                }
            }
            
            if(FORMA == 1 && se_encontro_cliente == false)
                Dispose();
            
        }

        public int editarClientes()
        {
            Bdatos.conexion();

            int resultado = Bdatos.peticion("update clientes set " +
                "nombre = '"+nombre_cliente.Text+
                "', colonia = '"+colonia_cliente.Text+
                "', calle = '"+calle_cliente.Text+
                "', referencias = '"+ref_cliente.Text+"' WHERE id_cliente = '"+id_cliente+"'");

            Bdatos.Desconectar();

            return resultado;
        }

        public void guardarServicio(string unidad, string descripcion)
        {
            int id_taxista_unidad = 0;
            Bdatos.conexion();
            Datos = Bdatos.obtenerBasesDatosMySQL("SELECT id_taxista_unidad from taxista_unidad where numero_unidad=" + unidad + " order by fecha asc");
            if (Datos.HasRows)
                while (Datos.Read())
                    id_taxista_unidad = Datos.GetInt32(0);
            Datos.Close();
          
            //INSERTA DATOS A SERVICIOS
            Bdatos.peticion("insert into servicios (id_cliente, id_taxista_unidad, fecha, hora, descripcion)" +
                           " values('" + id_cliente +
                           "','" + id_taxista_unidad +
                           "','" + DateTime.Now.ToString("yyyy-MM-dd") +
                           "','" + DateTime.Now.ToString("HH:mm:ss") +
                           "','" + descripcion + "')");
            Bdatos.Desconectar();
        }

        public int guardarClientes()
        {
            int consulta;
            Bdatos.conexion();

            //GENERO EL ID
            id_cliente = generarId();

            //INSERTA DATOS
            consulta = Bdatos.peticion("insert into clientes (id_cliente,nombre," +
                "numero_tel_1, colonia,calle, referencias, eliminado)" +
                "values('" + id_cliente + "','" + nombre_cliente.Text +
                "','" + num_tel_1_cliente.Text +
                "','" + colonia_cliente.Text +
                "','" + calle_cliente.Text +
                "','" + ref_cliente.Text +
                "',0)");

            Bdatos.Desconectar();
            return consulta;
            
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
            num_tel_1_cliente.Text = "";
            colonia_cliente.Text = "";
            calle_cliente.Text = "";
            ref_cliente.Text = "";
            unidad_servicio.Text = "";
            descripcion_servicio.Text = "";
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
            editar_btn_cliente.Enabled = false;
            habilitarCampos();
            editado = true;
            
        }
    }
}
