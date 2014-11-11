using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsApplication1;

namespace WindowsFormsApplication1
{
    class CallerID
    {

        static SerialPort ComPort;
        static ASCIIEncoding ASCIIEncoder = new ASCIIEncoding();
        static string data;
        public bool terminar = false;

        public String port = "COM24";
        public int baud = 115200;
        public int paridad = 3;
        public String ComandoATID = "AT+VCID=1";

        public static void OnSerialDataReceived(object sender, SerialDataReceivedEventArgs args)
        {

            //SE ALMACENA EL EVENTO ACTUAL DEL PUERTO EN LA VARIABLE DATA
            data = ComPort.ReadExisting();

            /*string ring_date = "";
            string ring_time = "";
            string ring_nmbr = "";*/


            //OBTENER FECHA PARA ESCRIBIR EL LA BITACORA
            DateTime date = DateTime.Now;
            string dateformat = date.ToString("yyyyMMdd"); //OBTENER EL AÑO MES Y DÍA


            //OBTENEMOS LA LONGITUD DE LAS CADENAS RECIBIDAS POR EL MÓDEM, CUANDO SEA MAYOR QUE 30 CARACTERES
            //SACAMOS LOS DATOS DE FECHA Y NÚMERO DE TELÉFONO
            if (data.Length >= 30)
            {
               /* ring_date = data.Substring(7, 4);
                ring_time = data.Substring(20, 4);
                ring_nmbr = data.Substring(33, 10);

                */
                //SEGEMENTO PARA ESCRIBIR EN BITACORA LOS EVENTOS CAPTURADOS
               // string fileName = "C:\\appserv\\Tracer_Log_" + dateformat + ".txt";
                //esto inserta texto en un archivo existente, si el archivo no existe lo crea
               // StreamWriter writer = File.AppendText(fileName);
               // writer.WriteLine(ring_nmbr + "|" + ring_date + "|" + ring_time);
                //writer.Close();
                Thread m = new Thread( metodo );
                m.Start();
               // System.Console.Write(data + "\n");
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
            ComPort.Write(ComandoATID + '\r');

            do
            {
                //CAPTURAMOS EL TEXTO ESCRITO EN LA CONSOLA
                //text = System.Console.ReadLine();

                //ESCRIBIMOS EN LA CONSOLA EL TEXTO CAPTURADO
                //ComPort.Write(text + '\r');
            }
            while (!terminar);//text != "q");
        }

        private static void InitializeComPort(string port, int baud, int paridad)
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

    }
}
