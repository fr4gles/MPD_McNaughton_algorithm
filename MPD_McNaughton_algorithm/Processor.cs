using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPD_McNaughton_algorithm
{
    public class Processor
    {
        public int          ProcessorNumber { get; private set; }
        public List<Task>   ProcessorTasksList { get; private set; } 

        public Processor(int processorNumber)
        {
            ProcessorNumber = processorNumber;
            ProcessorTasksList = new List<Task>();
        }
    }
}
