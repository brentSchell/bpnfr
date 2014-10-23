using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Gui
{
    
    class ProgressChart
    {
        int plotted_points = 0;
        List<double> graph_points_x, graph_points_y;
        Chart chart;
        ChartArea chartArea;
        Series series;
        public ProgressChart(Chart form_chart, int xmin, int xmax, int ymin, int ymax)
        {
            this.chart = form_chart;
            this.graph_points_x = new List<double>();
            this.graph_points_y = new List<double>();

            this.chartArea = new ChartArea();
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new Font("Consolas", 8);
            chartArea.AxisY.LabelStyle.Font = new Font("Consolas", 8);
            chartArea.AxisX.Minimum = xmin;
            chartArea.AxisX.Maximum = xmax;
            chartArea.AxisY.Minimum = ymin;
            chartArea.AxisY.Maximum = ymax;
            chartArea.AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            chartArea.AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;

            this.series = new Series();
            series.Name = "Recorded Points";
            series.ChartType = SeriesChartType.Point;
                       
            chart.ChartAreas.Add(chartArea);
            chart.Series.Add(series);
            
        }

        public void update() {
            for (int i = plotted_points; i < Globals.all_readings.Count; i++)
            {
                graph_points_x.Add(Globals.all_readings[i].pos[0]);
                graph_points_y.Add(Globals.all_readings[i].pos[1]);

            }
            plotted_points = Globals.all_readings.Count;

            // bind data points to the chart
            chart.Series["Recorded Points"].Points.DataBindXY(graph_points_x, graph_points_y);
        }
    }
}
