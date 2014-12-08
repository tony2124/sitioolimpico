using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace WindowsFormsApplication1
{
    class CallerID
    {

        public static SerialPort ComPort;
        static ASCIIEncoding ASCIIEncoder = new ASCIIEncoding();
        static string data;
        public bool terminar = false;
        SitioOlimpico sitio;

        public static String port = "COM24";
        public static int baud = 115200;
        public static int paridad = 3;
        public static String ComandoATID = "AT+VCID=1";

        public static void OnSerialDataReceived(object sender, SerialDataReceivedEventArgs args)
        {
            try
            {

                data = ComPort.ReadExisting();

                //MessageBox.Show("LLAMADA ENTRANTE: " + data);

                //if (data.Length >= 30)
                {
                    Thread m = new Thread(metodo);
                    m.Start();
                }
               
               
            }
            catch (Exception e) { MessageBox.Show("Se detectó una desconexión de MODEM"+e); };
        }

        public static void metodo()
        {
            data = data.Substring(data.LastIndexOf("= ") + 1);
            cliente obj = new cliente(data.Trim(),1);
            obj.ShowDialog();
        }

        public void EscuchaTelefono(SitioOlimpico sitio)
        {
            this.sitio = sitio;
            InitializeComPort(port, baud, paridad);

            while (!terminar) ;
        }

        public static void InitializeComPort(string port, int baud, int paridad)
        {
            try
            {
                ComPort = new SerialPort(port, baud);
                if (paridad == 0) ComPort.Parity = Parity.Even;
                else if (paridad == 1) ComPort.Parity = Parity.Mark;
                else if (paridad == 2) ComPort.Parity = Parity.None;
                else if (paridad == 3) ComPort.Parity = Parity.Odd;
                else if (paridad == 4) ComPort.Parity = Parity.Space;

                ComPort.StopBits = StopBits.One;
                ComPort.DataBits = 8;
                ComPort.Handshake = Handshake.None;
                ComPort.DataReceived += OnSerialDataReceived;
                ComPort.Open();
                //ACTIVAR IDENTIFICADOR DE LLAMADAS
                try
                {
                    ComPort.Write(ComandoATID + '\r');
                }
                catch (Exception e) { MessageBox.Show(null, "No se puede inicializar el Identificador de llamadas" + e, "sd", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            }
            catch (System.IO.IOException e)
            {
                MessageBox.Show(null, "El puerto seleccionado no está disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(null, "" + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
