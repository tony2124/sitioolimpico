using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        public static string data, texto;
        public bool terminar = false;
        SitioOlimpico sitio;

        public static String port = "COM24";
        public static int baud = 115200;
        public static int paridad = 3;
        public static String ComandoATID = "AT+VCID=1";

        static int fase = -1;

        public static void OnSerialDataReceived(object sender, SerialDataReceivedEventArgs args)
        {
            try
            {
                data = ComPort.ReadExisting();

                /*************************************************
                 * SI EL MODEM HA DEJADO DE FUNCIONAR
                 * **********************************************/
                if (data.Length > 100)
                {
                    MessageBox.Show("El modem ha dejado de funcionar por interferencia en la línea, por favor cierre el programa y desconecte el modem, luego vuelva a conectar el modem e inicie el programa.");
                    ConexionBD bd = new ConexionBD();
                    bd.conexion();
                    bd.bitacora("Error en el modem, hay cadenas muy grandes entrando por la línea...");
                    bd.Desconectar();
                    return;
                }
                /************************************************/
                texto += DateTime.Now + data;
                fase++;
                if (data.Length >= 30)
                    fase = 2;
                
                if (data.Length >= 30)
                {
                    Thread m = new Thread(metodo);
                    m.Start();
                }

                if (fase == 2)
                {
                    Thread n = new Thread(enviar_correo);
                    n.Start();
                }


               
            }
            catch (Exception e) { MessageBox.Show("Se detectó una desconexión de MODEM"+e); };
        }


        static void enviar_correo()
        {
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress("javier.c.chavez@gmail.com"));
            email.To.Add(new MailAddress("alfonso.calderon.chavez@gmail.com"));
            email.From = new MailAddress("alfonso.calderon.chavez@gmail.com");
            if(fase > 0)
                email.Subject = "LLAMADA ENTRANTE ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
            else
                email.Subject = "ESTADÍSTICA ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
            email.Body = texto;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("alfonso.calderon.chavez@gmail.com", "tonyteresasimpus");

            string output = null;

            try
            {
                smtp.Send(email);
                email.Dispose();
                output = "Correo electrónico fue enviado satisfactoriamente.";
                
            }
            catch (Exception ex)
            {
                output = "Error enviando correo electrónico: " + ex.Message;
            }
            fase = 0;
            Thread.Sleep(5000);
            data = "";
        }

        public static void metodo()
        {
            ConexionBD bd = new ConexionBD();
            bd.conexion();
            bd.bitacora("Se ha detectado una llamada: " + data);
            bd.Desconectar();
            data = data.Substring(data.LastIndexOf("= ") + 1);
            cliente obj = new cliente(data.Trim(),1);
            obj.ShowDialog();
            data = "";
        }

        void contador()
        {
            int contador = 0;
            do{
                contador++;
                Thread.Sleep(1000);
                if (contador == 600)
                {
                    enviar_correo();
                    contador = 0;
                }

            } while (!terminar);

        }

        public void EscuchaTelefono(SitioOlimpico sitio)
        {
            this.sitio = sitio;
            InitializeComPort(port, baud, paridad);
            
            Thread n = new Thread(contador);
            n.Start();

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
                ConexionBD bd = new ConexionBD();
                bd.conexion();
                bd.bitacora("El puerto seleccionado no está disponible");
                bd.Desconectar();
                MessageBox.Show(null, "El puerto seleccionado no está disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception e)
            {
                MessageBox.Show(null, "" + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
