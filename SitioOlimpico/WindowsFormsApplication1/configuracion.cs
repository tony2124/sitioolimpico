using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class configuracion : Form
    {
        public configuracion()
        {
            InitializeComponent();
            puerto.SelectedIndex = 0;
            bits.SelectedIndex = 0;
            paridad.SelectedIndex = 0;
            norma.SelectedIndex = 0;

            /************* SISTEMA OPERATIVO ******************/
            OperatingSystem os = Environment.OSVersion;

            plataforma.Text = os.VersionString.ToString() + " " + os.Platform.ToString();
            service.Text = os.ServicePack.ToString();
            ip.Text = LocalIPAddress();
            /**
            Console.WriteLine("VersionString: {0}", os.VersionString);
            Console.WriteLine("Platform: {0}", os.Platform);
            Console.WriteLine("Versión: {0}", os.Version);
            Console.WriteLine("ServicePack: {0}", os.ServicePack);
            */

        }

        public string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
