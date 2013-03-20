using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MPD_McNaughton_algorithm
{
    class GanttChart
    {
        public List<Processor> ProcessorsList;
        public Chart MyChart;

        public GanttChart(ref Chart myChart, List<Processor> processorsList)
        {
            ProcessorsList = new List<Processor>(processorsList);
            MyChart = myChart;

            MyChart.Visible = true;
        }

        public void MakeChart()
        {
            int j = 0;
            foreach (var p in ProcessorsList)
            {
                var start = 0;
                foreach (var t in p.ProcessorTasksList)
                {
                    int end = start + t.Duration;
                    MyChart.Series[j].Points.AddXY(p.ProcessorNumber, new object[] {start, end});
                    MyChart.Series[j].Points[MyChart.Series[j].Points.Count - 1].Label = t.Name;
                    start = end;
                    j++;
                }

            }
        }
    }
}

