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

        public static String port = "COM24";
        public static int baud = 115200;
        public static int paridad = 3;
        public String ComandoATID = "AT+VCID=1";

        public static void OnSerialDataReceived(object sender, SerialDataReceivedEventArgs args)
        {

            data = ComPort.ReadExisting();

            //OBTENER FECHA PARA ESCRIBIR EL LA BITACORA
            DateTime date = DateTime.Now;
            string dateformat = date.ToString("yyyyMMdd"); //OBTENER EL AÑO MES Y DÍA


            if (data.Length >= 30)
            {
                Thread m = new Thread( metodo );
                m.Start();
            }

        }

        public static void metodo()
        {
            data = data.Substring(data.LastIndexOf("= ") + 1);
            cliente obj = new cliente(data,1);
            obj.ShowDialog();

        }

        public void EscuchaTelefono() {

            InitializeComPort(port, baud, paridad);

            //ACTIVAR IDENTIFICADOR DE LLAMADAS
            try
            {
                ComPort.Write(ComandoATID + '\r');
            }
            catch (Exception e) { }

            while (!terminar);
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
