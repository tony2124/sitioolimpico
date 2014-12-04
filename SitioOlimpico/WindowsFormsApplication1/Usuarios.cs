using MySql.Data.MySqlClient;
using SitioOlimpico;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Usuarios : Form
    {
        public static MySqlDataAdapter Adaptador;
        public static DataTable ds;
        string consulta_tabla = "select usuario, contrasena, nivel_autorizacion as nivel, nombre from usuarios NATURAL JOIN personal";
        List<string> id_personal;

        public Usuarios()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            id_personal = new List<string>();

            /********** MOSTRAR EL PERSONAL PARA ASIGNARLE UN USUARIO Y CONTRASEÑA **********/
            ConexionBD bd = new ConexionBD();
            bd.conexion();
            MySqlDataReader datos = bd.obtenerBasesDatosMySQL("select id_personal, nombre from personal");
            
            if(datos.HasRows)
                while(datos.Read()){
                    comboBox1.Items.Add(datos.GetString(1));
                    id_personal.Add(datos.GetString(0));
                }
            bd.Desconectar();

            /********* PINTAR LA TABLA CON LOS USUARIOS **********/
            Pintar_tabla(consulta_tabla);
            tabla.Columns[0].Width = 150;
            tabla.Columns[1].Width = 150;
            tabla.Columns[2].Width = 40;
            tabla.Columns[3].Width = 250;
        }

        public void Pintar_tabla(String filtro)
        {
            Adaptador = new MySqlDataAdapter(filtro, ConexionBD.conex);

            ds = new DataTable();
            Adaptador.Fill(ds);
            tabla.DataSource = ds;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (vacios())
                return;

            if (generar_usuario())
            {
                Pintar_tabla(consulta_tabla);
            }
        }

        private bool vacios()
        {
            if (usuario_box.Text.CompareTo("") == 0 || contrasena_box.Text.CompareTo("") == 0)
            {
                MessageBox.Show(null, "Los campos de usuario y contraseña deben contener caracteres.", "Campos nulos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            return false;
        }

        private bool generar_usuario()
        {
            if (comboBox1.SelectedIndex < 1)
                return false ;
            ConexionBD bd = new ConexionBD();
            bd.conexion();
            MySqlDataReader datos = bd.obtenerBasesDatosMySQL("select usuario from usuarios where id_personal = '" + id_personal.ElementAt(comboBox1.SelectedIndex - 1) + "'");
            if (datos.HasRows)
            {
                datos.Close();
                MessageBox.Show(null, "Este usuario ya ha sido asignado, si desea editarlo haga doble clic sobre él en la tabla.","Usuario ya asignado",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            else
            {
                datos.Close();
                bd.peticion("INSERT INTO usuarios VALUES ('" + usuario_box.Text + "','" + contrasena_box.Text + "','" + id_personal.ElementAt(comboBox1.SelectedIndex - 1) + "')");
                contrasena_box.Text = "";
                usuario_box.Text = "";
                return true;
            }        
        }
    }
}
