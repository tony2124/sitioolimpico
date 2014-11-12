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
    public partial class buscarCliente : Form
    {
        public buscarCliente()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new cliente(num_tel.Text, 1).Show() ;
        }

        private void num_tel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                button1.PerformClick();
        }
    }
}
