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
    public partial class BuscarPersonal : Form
    {
        public static MySqlDataAdapter Adaptador;
        public static DataTable ds;

        public BuscarPersonal()
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
            if (SitioOlimpico.AUTORIZACION < 2 && (tabla["nivel_autorizacion", e.RowIndex].Value.ToString().CompareTo("2") == 0 || tabla["nivel_autorizacion", e.RowIndex].Value.ToString().CompareTo("1") == 0))
            {
                MessageBox.Show(this, "No tiene permiso de editar este usuario", "No hay privilegios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new Personal(1, tabla["id_personal", e.RowIndex].Value.ToString(), false ).ShowDialog();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pintar_tabla("select id_personal, nombre, fecha_ingreso, telefono, nivel_autorizacion, eliminado  from personal where nombre like '%"+nombre.Text+"%' or telefono like '%"+nombre.Text+"%'");
            total.Text = "" + tabla.Rows.Count;
           /* if (tabla.Rows.Count == 1)
                new Personal(1, tabla["id_personal", 0].Value.ToString(), false).ShowDialog();*/
        }

        private void nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                buscar.PerformClick();
        }

        private void BuscarPersonal_Activated(object sender, EventArgs e)
        {
            buscar.PerformClick();
        }
    }
}
