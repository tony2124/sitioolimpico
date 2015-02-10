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
    public partial class Filtro : Form
    {
        public Filtro()
        {
            InitializeComponent();
            if (SitioOlimpico.busqueda.CompareTo( "nombre_clientes" ) == 0)
                radioButton1.Checked = true; 
            else if(SitioOlimpico.busqueda.CompareTo( "numero_tel_clientes" ) == 0)
                radioButton2.Checked = true;
            else if(SitioOlimpico.busqueda.CompareTo( "numero_unidad" ) == 0)
                radioButton3.Checked = true;
            else if(SitioOlimpico.busqueda.CompareTo( "nombre" ) == 0)
                radioButton4.Checked = true; 
            else if(SitioOlimpico.busqueda.CompareTo( "fecha_servicios" ) == 0)
                radioButton5.Checked = true;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                SitioOlimpico.busqueda = "nombre_clientes";
            else if (radioButton2.Checked)
                SitioOlimpico.busqueda = "numero_tel_clientes";
            else if (radioButton3.Checked)
                SitioOlimpico.busqueda = "numero_unidad";
            else if (radioButton4.Checked)
                SitioOlimpico.busqueda = "nombre";
            else if (radioButton5.Checked)
                SitioOlimpico.busqueda = "fecha_servicios";

            Dispose();
        }
    }
}
