using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPD_McNaughton_algorithm
{
    class Processor
    {
        public Processor(String name)
        {
            Name = name;
        }

        public String Name { get; private set; }
        public Int16 TaskDuration = 0;
    }
}
