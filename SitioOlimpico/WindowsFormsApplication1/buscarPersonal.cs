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

            Adaptador = new MySqlDataAdapter("select * from personal", ConexionBD.conex);

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
            //MessageBox.Show(""+ tabla["id_personal", e.RowIndex].Value);
        }
    }
}
