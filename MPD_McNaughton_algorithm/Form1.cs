using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MPD_McNaughton_algorithm
{
    public partial class Form1 : Form
    {
        private McNaughtonAlgorithm mcnaughton;
        private List<Color> colors;
        public Form1()
        {
            InitializeComponent();

            colors = new List<Color>();

            chart1.Visible = false;

            dataGridView.Rows.Add(new object[] { "Zad1", "1" });
            dataGridView.Rows.Add(new object[] { "Zad2", "3" });
            dataGridView.Rows.Add(new object[] { "Zad3", "2" });
            dataGridView.Rows.Add(new object[] { "Zad4", "3" });
            dataGridView.Rows.Add(new object[] { "Zad5", "4" });
            dataGridView.Rows.Add(new object[] { "Zad6", "6" });
        }

        private void ClearChart()
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Legends.Clear();
        }

        private void GenerateColors()
        {
            var names = (KnownColor[])Enum.GetValues(typeof(KnownColor));

            foreach (var knownColor in names)
            {
                colors.Add(Color.FromKnownColor(knownColor));
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //ClearChart();

            if (mcnaughton != null)
                mcnaughton.ClearObj();

            mcnaughton = null;

            mcnaughton = new McNaughtonAlgorithm((int)numericUpDown1.Value);

            for (var i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                var tmp1 = dataGridView.Rows[i].Cells["Zadanie"].Value.ToString();
                var duration = Convert.ToInt32(dataGridView.Rows[i].Cells["CzasWykonania"].Value.ToString());

                var task = new Task(tmp1, duration, GetFreeColor());

                mcnaughton.TasksList.Add(task);
            }

            mcnaughton.Go();

            AddSeries();

            var ganttChart = new GanttChart(ref chart1, mcnaughton.ProcessorsList);
            ganttChart.MakeChart();
            chart1.ChartAreas[0].AxisY.Maximum = McNaughtonAlgorithm.Cmax+1;
            chart1.ChartAreas[0].AxisX.Maximum = mcnaughton.ProcessorsList.Count;
            chart1.ChartAreas[0].AxisX.Minimum = -1;
        }

        public Color GetFreeColor()
        {
            if (colors.Count < 1)
            {
                GenerateColors();
            }

            Random random = new Random();
            var index = random.Next(colors.Count-1);
            var tmpcolor = colors[index];
            colors.RemoveAt(index);
            return tmpcolor;
        }

        public void AddSeries()
        {
            chart1.Series.Clear();
            for (var i = 0; i < mcnaughton.TasksList.Count; ++i)
            {
                var s = new Series
                    {
                        Name = i.ToString(CultureInfo.InvariantCulture),
                        ChartType = SeriesChartType.RangeBar,
                        Color = mcnaughton.TasksList[i].Color,
                        BorderColor = Color.Black,
                        BorderWidth = 2,
                        MarkerStep = 1,
                        CustomProperties = "DrawSideBySide=False"
                    };
                chart1.Series.Add(s);
            }
        }
    }
}
