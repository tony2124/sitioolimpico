﻿using MySql.Data.MySqlClient;
using WindowsFormsApplication1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1
{
    public partial class Estadistica : Form
    {
        MySqlDataReader Datos;
        ConexionBD Bdatos = new ConexionBD();

        public Estadistica()
        {
            InitializeComponent();

            String fecha_desde = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            String fecha_hasta = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            pintarEstadistica(fecha_desde, fecha_hasta);
            combo_numero.SelectedIndex = 3;
            combo_periodo.SelectedIndex = 0;
            //dibujar_grafico();
        }

        public void dibujar_grafico()
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Palette = ChartColorPalette.Fire;
            chart1.Titles.Add("Servicios realizados");
            
            /*** variables **/
            int numero = Int32.Parse(combo_numero.Text);

            /*** HACER CALCULOS PARA LOS DÍAS **/
            string fecha_actual = DateTime.Now.ToString("yyyy-MM-dd");
            string fecha_anterior = DateTime.Now.AddDays( - numero  - 1 ).ToString("yyyy-MM-dd");
            string consulta = "select fecha_servicios, count(id_servicio) from servicios where fecha_servicios >= '" + fecha_anterior + "' and fecha_servicios <= '" + fecha_actual + "' group by fecha_servicios";
            Bdatos.conexion();
            Datos = Bdatos.obtenerBasesDatosMySQL(consulta);
            if (Datos.HasRows)
                while (Datos.Read())
                {
                    Series serie = chart1.Series.Add(Datos.GetString(0));
                    serie.Points.Add(Datos.GetInt32(1));
                }
            Datos.Close();
            Bdatos.Desconectar();

           // MessageBox.Show(DateTime.Now. + "");

            /*** HACER CALCULOS POR SEMANAS **/
            for (int i = 0; i < numero; i++)
            {
                
            }
            
            /***********************************/
            /*
            for (int i = 0; i < Int32.Parse(combo_numero.Text); i++)
            {
                Series serie = chart1.Series.Add(combo_periodo.Text + " " +(i + 1));
                serie.Points.Add(100);
            } */
        }

        public void pintarEstadistica(String fecha_desde, String fecha_hasta)
        {
            int _total_servicios = 0;

            panel1.Controls.Clear();
            Bdatos.conexion();
            Datos = Bdatos.obtenerBasesDatosMySQL("select numero_unidad, count(id_servicio) from servicios natural join taxista_unidad where fecha_servicios >= '" + fecha_desde + "' and fecha_servicios <= '" + fecha_hasta + "' group by numero_unidad");
            if (Datos.HasRows)
                while (Datos.Read())
                {
                    panel1.Controls.Add(new Label()
                    {
                        Name = "MiLabel" + Datos.GetString(0),
                        Text = "Unidad: " + Datos.GetString(0) + ".   Servicios: " + Datos.GetString(1),
                        Width = 170,
                    });
                    _total_servicios += Datos.GetInt32(1);
                }
            total_servicios.Text = _total_servicios.ToString();
            Datos.Close();
            Bdatos.Desconectar();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           // GraficaEstadisticas ge = new GraficaEstadisticas(unidades,unidades_serv);
            //ge.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pintarEstadistica(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
        }

        private void combo_numero_SelectedIndexChanged(object sender, EventArgs e)
        {
            dibujar_grafico();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void combo_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            dibujar_grafico();
        }
    }
}
