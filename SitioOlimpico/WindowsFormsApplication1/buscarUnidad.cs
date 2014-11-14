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
        
    public partial class buscarUnidad : Form
    {
    
        /*VARIABLES DE LA BASE DE DATOS*/
        MySqlDataReader Datos;
        ConexionBD Bdatos = new ConexionBD();

        public buscarUnidad()
        {
            InitializeComponent();


              //CONSULTA A LA BASE DE DATOS PARA EXTRAER INFORMACIÓN DE LAS UNIDADES
            int contador = 0;

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
                String[] B= new String[contador];

                while (Datos.Read())
                {
                    B[i] = Datos.GetInt32(0) + "";
                    i++;
                }
           

                Datos.Close();

                this.unidad_buscar_txt.AutoCompleteCustomSource.AddRange(B);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose(); 
        }

        private void buscar_unidad_Click(object sender, EventArgs e)
        {
            Bdatos.conexion();

            Datos = Bdatos.obtenerBasesDatosMySQL("SELECT count(numero_unidad) from unidades where numero_unidad=" + unidad_buscar_txt.Text);
            int existe = 0;
            if (Datos.HasRows)
                while (Datos.Read())
                    existe = Datos.GetInt32(0);
            Datos.Close();
            Bdatos.Desconectar();

            if (existe == 0)//Si no hay registros
            {
                MessageBox.Show("Esta unidad no esta registrada, para ver información de la unidad primero tiene que registrarla en el programa.", "Error: Unidad no registrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {           
                unidades u = new unidades(1, unidad_buscar_txt.Text);
                Dispose();
                u.ShowDialog();
                
            }

        }
    }
}
