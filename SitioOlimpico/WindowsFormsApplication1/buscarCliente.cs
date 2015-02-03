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
    public partial class BuscarCliente : Form
    {

        public static MySqlDataAdapter Adaptador;
        public static DataTable ds;

        public BuscarCliente()
        {
            InitializeComponent();

            Pintar_tabla("select id_cliente, nombre_clientes, numero_tel_clientes, colonia_clientes, calle_clientes, referencias_clientes  from clientes");

            tabla.Columns[0].HeaderText = "ID PERSONAL";
            tabla.Columns[0].Width = 130;
            tabla.Columns[1].HeaderText = "NOMBRE";
            tabla.Columns[1].Width = 400;
            tabla.Columns[2].HeaderText = "TELÉFONO";
            tabla.Columns[3].HeaderText = "COLONIA";
            tabla.Columns[4].HeaderText = "CALLE";
            tabla.Columns[5].HeaderText = "REFERENCIAS";

            total.Text = "" + tabla.Rows.Count;
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
            Pintar_tabla("select id_cliente, nombre_clientes, numero_tel_clientes, colonia_clientes, calle_clientes, referencias_clientes  from clientes where numero_tel_clientes like '%"+num_tel.Text+"%' or nombre_clientes like '%"+num_tel.Text+"%'");
            total.Text = "" + tabla.Rows.Count;
          /*  if (tabla.Rows.Count == 1)
                new cliente(tabla.Rows[0].Cells[2].Value.ToString(), 1).ShowDialog();*/
        }

        private void num_tel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                button1.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            new cliente(tabla.Rows[e.RowIndex].Cells[2].Value.ToString(), 1).ShowDialog();
        }

        private void BuscarCliente_Activated(object sender, EventArgs e)
        {
            button1.PerformClick();
        }
    }
}
