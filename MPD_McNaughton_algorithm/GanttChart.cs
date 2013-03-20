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

        public GanttChart(Chart myChart, List<Processor> processorsList)
        {
            ProcessorsList = new List<Processor>(processorsList);
            MyChart = myChart;
        }





    }
}
