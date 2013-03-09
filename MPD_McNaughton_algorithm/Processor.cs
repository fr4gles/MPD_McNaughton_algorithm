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
            Tasks = null;
        }

        public void addTask(Task t)
        {
            Tasks.Add(t);
            TaskDuration += t.Duration;
        }
        public String Name { get; private set; }
        public Int32 TaskDuration = 0;
        public List<Task> Tasks;
    }
}
