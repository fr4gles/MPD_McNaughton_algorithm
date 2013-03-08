using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPD_McNaughton_algorithm
{
    class McNaughtonAlgorithm
    {
        public McNaughtonAlgorithm(List<Processor> proc, List<Task> tasks)
        {
            Processors = proc;
            Tasks = tasks;
        }

        public Boolean Do()
        {
            return true;
        }

        public Boolean ResolveCmax()
        {
            if (Processors.Count < 1 || Tasks.Count < 1)
                return false;



            return true;
        }

        public List<Task> Tasks { get; private set; }
        public List<Processor> Processors { get; private set; }
    }
}
