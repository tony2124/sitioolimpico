using MySql.Data.MySqlClient;
using SitioOlimpico;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Configuracion : Form
    {
        static SerialPort ComPort;
        static string tipodemodem = "";

        public Configuracion()
        {
            InitializeComponent();

            /*************** CONSULTA INFORMACIÓN DEL MODEM ************/
            CallerID obj = new CallerID();
            ConexionBD bd = new ConexionBD();
            bd.conexion();
            MySqlDataReader Datos = bd.obtenerBasesDatosMySQL("select puerto, baud, paridad, norma, dir_fotos, ip_base_datos from configuracion");
            if (Datos.HasRows)
                while (Datos.Read())
                {
                    puerto.Text = Datos.GetString(0);
                    bits.Text = Datos.GetString(1);
                    paridad.SelectedIndex = Datos.GetInt32(2);
                    norma.Text = Datos.GetString(3);
                    dir_foto.Text = Datos.GetString(4);
                    ip_base_datos.Text = Datos.GetString(5);
                }
            Datos.Close();
            bd.Desconectar();
           /* MessageBox.Show(obj.port);
            Thread hilo = new Thread(obj.EscuchaTelefono);
            try
            {
                hilo.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            */
            /************* SISTEMA OPERATIVO ******************/
            OperatingSystem os = Environment.OSVersion;

            plataforma.Text = os.VersionString.ToString() + " " + os.Platform.ToString();
            service.Text = os.ServicePack.ToString();
            ip.Text = LocalIPAddress();

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

        public void guardar()
        {
            ConexionBD bd = new ConexionBD();
            bd.conexion();
            if (bd.peticion("update configuracion set puerto = '" + puerto.Text + "'," +
                "baud = '" + bits.Text + "', paridad = '" + paridad.SelectedIndex + "'," +
                "norma = '" + norma.Text + "', dir_fotos = '" + dir_foto.Text + "'," +
                "ip_base_datos = '" + ip_base_datos.Text + "'") > 0)
                MessageBox.Show(this,"Los datos se han guardado correctamente.","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
                MessageBox.Show(this,"Los datos NO se han guardado correctamente.", "Aviso",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            bd.Desconectar();
        }

        public static void OnSerialDataReceived(object sender, SerialDataReceivedEventArgs args)
        {
            //SE ALMACENA EL EVENTO ACTUAL DEL PUERTO EN LA VARIABLE DATA
            String data = ComPort.ReadExisting();
            tipodemodem = data;
            
        }

        void detectarModem()
        {
            try
            {
                InitializeComPort(puerto.Text, Int32.Parse(bits.Text), paridad.SelectedIndex, "ATI3");
                while (tipodemodem.CompareTo("") == 0 && ComPort.IsOpen) ;
                //ACTIVAR IDENTIFICADOR DE LLAMADAS
                modem.Text = tipodemodem;
                tipodemodem = "";


                ComPort.Close();
            }
            catch (Exception e) { MessageBox.Show("error" + e); }
        }

        private static void InitializeComPort(string port, int baud, int paridad, string norma)
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
                ComPort.Write(norma + '\r');
            }
            catch (System.IO.IOException e)
            {
                MessageBox.Show(null, "El puerto seleccionado no está disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(null, ""+e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                CallerID.ComPort.Close();

                detectarModem();

                CallerID.InitializeComPort(CallerID.port, CallerID.baud, CallerID.paridad);

            }
            catch (Exception exc) { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string tempPath = "";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tempPath = folderBrowserDialog1.SelectedPath; // prints path
            }

            tempPath = tempPath.Replace("\\","/");
            dir_foto.Text = tempPath;
        }
    }
}
