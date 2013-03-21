using System.Collections.Generic;

namespace MPD_McNaughton_algorithm
{
    public class Processor
    {
        public int ProcessorNumber { get; private set; }
        public List<Task> ProcessorTasksList { get; private set; }

        public Processor(int processorNumber)
        {
            ProcessorNumber = processorNumber;
            ProcessorTasksList = new List<Task>();
        }
    }
}
