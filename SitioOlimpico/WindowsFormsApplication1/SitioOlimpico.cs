using SitioOlimpico;
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
    }
}
