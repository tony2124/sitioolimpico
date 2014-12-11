using MySql.Data.MySqlClient;
using WindowsFormsApplication1;
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
        
    public partial class BuscarUnidad : Form
    {
    
        public static MySqlDataAdapter Adaptador;
        public static DataTable ds;

        public BuscarUnidad()
        {
            InitializeComponent();

            Pintar_tabla("select numero_unidad, modelo, marca, color, placas, descripcion, asignado, eliminado  from unidades");

            tabla.Columns[0].HeaderText = "UNIDAD";
            tabla.Columns[0].Width = 120;
            tabla.Columns[1].HeaderText = "MODELO";
            tabla.Columns[1].Width = 100;
            tabla.Columns[2].HeaderText = "MARCA";
            tabla.Columns[2].Width = 180;
            tabla.Columns[3].HeaderText = "COLOR";
            tabla.Columns[4].HeaderText = "PLACAS";
            tabla.Columns[5].HeaderText = "DESCRIPCIÓN";
            tabla.Columns[6].HeaderText = "ASIGNADO";
            tabla.Columns[7].HeaderText = "ELIMINADO";
            total.Text = "" + tabla.Rows.Count;

        }

        public void Pintar_tabla(String filtro)
        {
            Adaptador = new MySqlDataAdapter(filtro, ConexionBD.conex);

            ds = new DataTable();
            Adaptador.Fill(ds);
            tabla.DataSource = ds;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose(); 
        }

        private void buscar_unidad_Click(object sender, EventArgs e)
        {
            Pintar_tabla("select numero_unidad, modelo, marca, color, placas, descripcion, asignado, eliminado  from unidades where numero_unidad like '%" + nombre.Text + "%'");
            total.Text = "" + tabla.Rows.Count;
            if (tabla.Rows.Count == 1)
                new Unidades(1, tabla.Rows[0].Cells[0].Value.ToString()).ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        private void tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            new Unidades(1, tabla.Rows[e.RowIndex].Cells[0].Value.ToString()).ShowDialog();
        }

        private void BuscarUnidad_Activated(object sender, EventArgs e)
        {
            buscar_unidad.PerformClick();
        }
    }
}
