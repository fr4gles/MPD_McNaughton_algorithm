using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private Int32 UID = 0;
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
//            dataGridView.Rows.Add(new object[] { "Zad6", "6" });
//            dataGridView.Rows.Add(new object[] { "Zad7", "12" });
//            dataGridView.Rows.Add(new object[] { "Zad8", "22" });
//            dataGridView.Rows.Add(new object[] { "Zad9", "25" });
//            dataGridView.Rows.Add(new object[] { "Zad10", "20" });
//            dataGridView.Rows.Add(new object[] { "Zad11", "11" });
//            dataGridView.Rows.Add(new object[] { "Zad12", "23" });
//            dataGridView.Rows.Add(new object[] { "Zad13", "16" });
//            dataGridView.Rows.Add(new object[] { "Zad14", "11" });
//            dataGridView.Rows.Add(new object[] { "Zad15", "18" });
//            dataGridView.Rows.Add(new object[] { "Zad16", "33" });
//            dataGridView.Rows.Add(new object[] { "Zad17", "12" });
//            dataGridView.Rows.Add(new object[] { "Zad18", "22" });
//            dataGridView.Rows.Add(new object[] { "Zad19", "25" });
//            dataGridView.Rows.Add(new object[] { "Zad20", "20" });
//            dataGridView.Rows.Add(new object[] { "Zad21", "11" });
//            dataGridView.Rows.Add(new object[] { "Zad22", "23" });
//            dataGridView.Rows.Add(new object[] { "Zad23", "16" });
//            dataGridView.Rows.Add(new object[] { "Zad24", "11" });
//            dataGridView.Rows.Add(new object[] { "Zad25", "18" });
//            dataGridView.Rows.Add(new object[] { "Zad26", "33" });
//            dataGridView.Rows.Add(new object[] { "Zad27", "12" });
//            dataGridView.Rows.Add(new object[] { "Zad28", "22" });
//            dataGridView.Rows.Add(new object[] { "Zad29", "25" });
//            dataGridView.Rows.Add(new object[] { "Zad30", "20" });

            UID = dataGridView.Rows.Count;

            chart1.Titles.Add("Wykres Gantt'a");
            chart1.ChartAreas[0].AxisY.Title = "Czas wykonania";
            chart1.ChartAreas[0].AxisX.Title = "Procesor / Maszyna";
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
            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();
            //ClearChart();

            if (mcnaughton != null)
                mcnaughton.ClearObj();

            mcnaughton = null;

            mcnaughton = new McNaughtonAlgorithm((int)numericIloscProc.Value);

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

            chart1.ChartAreas[0].AxisY.Maximum = McNaughtonAlgorithm.Cmax + 1;
//            chart1.ChartAreas[0].AxisX.Maximum = mcnaughton.ProcessorsList.Count;
            chart1.ChartAreas[0].AxisX.Minimum = -1;

            stopwatch.Stop();
            
            PrepareInfoBox(ref mcnaughton, stopwatch.Elapsed);
            PrepareTitle();
            

            btnZapisz.Enabled = true;
        }

        public void PrepareInfoBox(ref McNaughtonAlgorithm mc, TimeSpan time)
        {
            richTextBox1.Text = "Cmax = " + McNaughtonAlgorithm.Cmax;
            mc.TasksList = mc.TasksList.OrderBy(x => x.Duration).ToList();
            richTextBox1.AppendText("\n"
                + "Zadanie o najmniejszej wadze czasowej:\t\t" + mc.TasksList[0].Name + "\tczas : " + mc.TasksList[0].Duration + "\n"
                + "Zadanie o największej wadze czasowej:\t\t" + mc.TasksList[mc.TasksList.Count - 1].Name + "\tczas : " + mc.TasksList[mc.TasksList.Count - 1].Duration + "\n"
                + "Czas działania algorymtu + czas generowania wykresu:\t"+time.Milliseconds+" [ms]");
        }

        public void PrepareTitle()
        {
            chart1.Titles[0].Text = "Wykres Gantt'a  /  Cmax = "+McNaughtonAlgorithm.Cmax;
        }

        public Color GetFreeColor()
        {
            if (colors.Count < 1)
            {
                GenerateColors();
            }

            Random random = new Random();
            var index = random.Next(colors.Count - 1);
            var tmpcolor = colors[index];
            colors.RemoveAt(index);
            return tmpcolor;
        }

        public void AddSeries()
        {
            chart1.Series.Clear();
            var i = 0;
            for (; i < mcnaughton.TasksList.Count; ++i)
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
            chart1.Series.Add(new Series
                {
                    Name = (++i).ToString(CultureInfo.InvariantCulture),
                    ChartType = SeriesChartType.RangeBar,
                    Color = GetFreeColor(),
                    BorderColor = Color.Black,
                    BorderWidth = 2,
                    MarkerStep = 1,
                    CustomProperties = "DrawSideBySide=False"
                });
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGridView != null)
                if (dataGridView.CurrentRow != null)
                    dataGridView.CurrentRow.Cells[0].Value = string.Format("Zad{0}", (UID++).ToString());
            //
            //            var c = dataGridView.RowCount;
            //            DataGridViewCell cell = dataGridView.Rows[c - 1].Cells[0];
            //            cell.Value
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            btnStart.PerformClick();
        }

        private void btnGeneruj_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();

            var rnd = new Random();
            for (var i = 0; i < numericIloscZadan.Value; ++i)
            {
                dataGridView.Rows.Add(new object[] { "Zad"+(UID++).ToString(), rnd.Next((int)numericUpTime.Value, (int)numericDownTime.Value) });
            }

            btnStart.PerformClick();
        }

        private void btnZapisz_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PNG Image|*.png";
            saveFileDialog1.Title = "Zapisz wykres Gantt'a jako plikPNG";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                chart1.SaveImage(saveFileDialog1.FileName, ChartImageFormat.Png);
            }
            
        }
    }
}
