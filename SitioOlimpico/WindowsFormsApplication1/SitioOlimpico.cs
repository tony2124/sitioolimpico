using WindowsFormsApplication1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class SitioOlimpico : Form
    {
        CallerID obj;

        public SitioOlimpico()
        {
            InitializeComponent();
            
            obj = new CallerID();
           /* Thread hilo = new Thread( obj.EscuchaTelefono );
            hilo.Start();*/

            
        }

        private void SitioOlimpico_FormClosing(object sender, FormClosingEventArgs e)
        {
            obj.terminar = true;
        }


        private void registrarPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            personal p = new personal(0);
            p.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            new configuracion().ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "¿Está seguro que desea salir del sistema? Una vez que salga del sistema no será posible identificar las llamadas.", "Confirme operación",  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Close();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new acercade().ShowDialog();

        }

        private void registroDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cliente c = new cliente(0);
            c.ShowDialog();
        }
    }
}
