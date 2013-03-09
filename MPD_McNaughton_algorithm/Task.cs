using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPD_McNaughton_algorithm
{
    class Task
    {
        public Task(String name, Int32 duration)
        {
            Name = name;
            Duration = duration;
        }

        public String Name { get; private set; }
        public Int32 Duration { get; private set; }
    }
}
