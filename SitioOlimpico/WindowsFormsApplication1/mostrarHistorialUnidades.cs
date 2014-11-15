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
    public partial class mostrarHistorialUnidades : Form
    {
        public static MySqlDataAdapter Adaptador;
        public static DataTable ds;

        public mostrarHistorialUnidades(String id_personal)
        {  
            InitializeComponent();
            unidad_actual.Enabled = false;
            Pintar_tabla("select id_personal, nombre, numero_unidad, fecha, asignado from personal natural join taxista_unidad where id_personal ='" + id_personal + "' ");
            tabla.Columns[0].Visible = false;
            tabla.Columns[1].Width = 400;
            tabla.Columns[1].HeaderText = "NOMBRE";
            tabla.Columns[2].HeaderText = "UNIDAD";
            tabla.Columns[3].HeaderText = "FECHA";
            tabla.Columns[4].Visible = false;

            if (int.Parse(tabla.Rows[tabla.RowCount - 1].Cells[4].Value.ToString()) == 1)
            {
                unidad_actual.Text = tabla.Rows[tabla.RowCount - 1].Cells[2].Value.ToString();
            }
            else
            {
                unidad_actual.Text = "Aún no se le asigna unidad";
            }
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
            Dispose();
        }

        private void ver_unidad_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            unidades u = new unidades(1,unidad_actual.Text);
            u.ShowDialog();
        }
    }
}
