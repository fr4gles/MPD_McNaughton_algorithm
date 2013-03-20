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

namespace MPD_McNaughton_algorithm
{
    public partial class Form1 : Form
    {
        private McNaughtonAlgorithm mcnaughton;
        public Form1()
        {
            InitializeComponent();

            dataGridView.Rows.Add(new object[] { "Zad1", "1" });
            dataGridView.Rows.Add(new object[] { "Zad2", "3" });
            dataGridView.Rows.Add(new object[] { "Zad3", "2" });
            dataGridView.Rows.Add(new object[] { "Zad4", "3" });
//            dataGridView.Rows.Add(new object[] { "Zad5", "4" });
//            dataGridView.Rows.Add(new object[] { "Zad6", "6" });
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            if(mcnaughton != null)
                mcnaughton.ClearObj();

            mcnaughton = null;

            mcnaughton = new McNaughtonAlgorithm((int)numericUpDown1.Value);

            for (int i = 0; i < dataGridView.Rows.Count-1; i++)
            {
                var tmp1 = dataGridView.Rows[i].Cells["Zadanie"].Value.ToString();
                var duration = Convert.ToInt32(dataGridView.Rows[i].Cells["CzasWykonania"].Value.ToString());

                var task = new Task(tmp1, duration);

                mcnaughton.TasksList.Add(task);
            }

            mcnaughton.Go();

            chart1 = new Chart();
        }
    }
}
