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
    public partial class buscarPersonal : Form
    {
        public static MySqlDataAdapter Adaptador;
        public static DataTable ds;

        public buscarPersonal()
        {
            InitializeComponent();

            Pintar_tabla("select id_personal, nombre, fecha_ingreso, telefono, nivel_autorizacion, asignado, eliminado  from personal");

            tabla.Columns[0].HeaderText = "ID PERSONAL";
            tabla.Columns[0].Width = 120;
            tabla.Columns[1].HeaderText = "NOMBRE";
            tabla.Columns[1].Width = 460;
            tabla.Columns[2].HeaderText = "FECHA INGRESO";
            tabla.Columns[3].HeaderText = "TELÉFONO";
            tabla.Columns[4].HeaderText = "AUTORIZACIÓN";
            tabla.Columns[5].HeaderText = "ASIGNADO";
            tabla.Columns[6].HeaderText = "BAJA";
            //MessageBox.Show("PIÑA");
           /* for (int i = 0; i < tabla.Rows.Count; i++)
            {
                if (tabla.Rows[i].Cells[6].Value.ToString().CompareTo("0") == 0)
                    tabla.Rows[i].DefaultCellStyle.BackColor = Color.Red;
            }
            //tabla.Rows[0].DefaultCellStyle.ForeColor = Color.Yellow;*/
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

        private void tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            new personal(1, tabla["id_personal", e.RowIndex].Value.ToString(), false ).ShowDialog();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(nombre.Text);
            Pintar_tabla("select id_personal, nombre, fecha_ingreso, telefono, nivel_autorizacion, eliminado  from personal where nombre like '%"+nombre.Text+"%' or telefono like '%"+nombre.Text+"%'");
            total.Text = "" + tabla.Rows.Count;
            if (tabla.Rows.Count == 1)
                new personal(1, tabla["id_personal", 0].Value.ToString(), false).ShowDialog();
        }

        private void nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                buscar.PerformClick();
        }
    }
}
