using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Forms.DataVisualization;

namespace MPD_McNaughton_algorithm
{
    /// <summary>
    /// Interaction logic for MyMainWindow.xaml
    /// </summary>
    public partial class MyMainWindow : Window
    {
        public MyMainWindow()
        {
            InitializeComponent();
            
            wfHost = new WindowsFormsHost();
//            if(myChart == null)
//                throw new Exception("Ulala");
            //myChart = new System.Windows.Forms.DataVisualization.Charting.Chart();

            myChart.Series.Add("My");

            for (int i = 0; i < 2000; i++)
            {
                // Add X and Y values for a point. 
                myChart.Series["My"].Points.AddXY(i, i);
            }
        }
    }
}
