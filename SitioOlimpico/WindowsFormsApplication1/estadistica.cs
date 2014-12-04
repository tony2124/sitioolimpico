using MySql.Data.MySqlClient;
using SitioOlimpico;
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
    public partial class Estadistica : Form
    {
        MySqlDataReader Datos, dat;
        ConexionBD Bdatos = new ConexionBD();

        String[] unidades;
        int[] unidades_serv;

        public Estadistica()
        {
            InitializeComponent();

            String fecha_desde = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            String fecha_hasta = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            pintarEstadistica(fecha_desde, fecha_hasta);
            
        }

        public void pintarEstadistica(String fecha_desde, String fecha_hasta)
        {
            panel1.Controls.Clear();
            int x = 0, y = 0;

            Bdatos.conexion();
            Datos = Bdatos.obtenerBasesDatosMySQL("select count(numero_unidad) from unidades");
            if (Datos.HasRows)
                while (Datos.Read())
                    y = Datos.GetInt32(0);
            Bdatos.Desconectar();

            unidades = new String[y];
            unidades_serv = new int[y];

            Bdatos.conexion();

            Datos = Bdatos.obtenerBasesDatosMySQL("select count(id_servicio) from servicios where fecha_servicios >= '" + fecha_desde + "' and fecha_servicios <= '" + fecha_hasta + "'");

            if (Datos.HasRows)
                while (Datos.Read())
                    x = Datos.GetInt32(0);

            Datos.Close();


            total_servicios.Text = x + "";
            total_servicios.Enabled = false;

            Datos = Bdatos.obtenerBasesDatosMySQL("select numero_unidad from unidades");
            int xc = 0, i = 0;
            Bdatos.conexion();
            if (Datos.HasRows)
                while (Datos.Read())
                {

                    dat = Bdatos.obtenerBasesDatosMySQL("select count(id_servicio) from servicios natural join taxista_unidad  where numero_unidad=" + Datos.GetInt32(0) + " AND fecha_servicios >= '"+fecha_desde+"' and fecha_servicios <= '"+fecha_hasta+"'");
                    if (dat.HasRows)
                        while (dat.Read())
                            xc = dat.GetInt32(0);
                    dat.Close();
                    panel1.Controls.Add(new Label()
                    {
                        Name = "MiLabel" + Datos.GetInt32(0),
                        Text = "Unidad: " + Datos.GetInt32(0).ToString() + ".   Servicios: " + xc,
                        Width = 170,
                    });
                    unidades_serv[i] = xc;
                    unidades[i] = Datos.GetInt32(0) + "";
                    i++;
                }

            Bdatos.Desconectar();
            Datos.Close();

            Bdatos.Desconectar();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GraficaEstadisticas ge = new GraficaEstadisticas(unidades,unidades_serv);
            ge.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pintarEstadistica(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
        }
    }
}
