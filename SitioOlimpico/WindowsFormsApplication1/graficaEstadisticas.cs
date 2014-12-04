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
    public partial class GraficaEstadisticas : Form
    {
        public GraficaEstadisticas(String [] series, int[] puntos)
        {
            InitializeComponent();
            this.chart1.Palette = ChartColorPalette.Fire;
            // Se agrega un titulo al Grafico.
            this.chart1.Titles.Add("Servicios");
            // Agregar las Series al Grafico.
            for (int i = 0; i < series.Length; i++)
            {
                // Aqui se agregan las series o Categorias.
                Series serie = this.chart1.Series.Add(series[i]);
                // Aqui se agregan los Valores.
                serie.Points.Add(puntos[i]);
            } 
        }
    }
}
