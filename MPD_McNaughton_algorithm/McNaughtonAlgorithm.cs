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
            CMax = 0;
        }

        public Boolean Do()
        {
            if (CMax.Equals(0) && ( Tasks.Count < 1 || Processors.Count < 1 ))
                return false;

            Int32 tmp = 0;
            for (int i = 0; i < Processors.Count; i++)
            {
                tmp = 0;
                foreach (var task in Tasks)
                {
                    if (Processors[i].TaskDuration + task.Duration <= CMax)
                    {
                        Processors[i].addTask(task);
//                        Processors[i].Tasks.Add(task);
//                        Processors[i].TaskDuration += task.Duration;
                    }
                    else
                    {
                        tmp = CMax - Processors[i].TaskDuration;

                        Processors[i++].addTask(new Task(task.Name, tmp));
//                        Processors[i+1].TaskDuration += (tmp);
//                        Processors[i+1].Tasks.Add(new Task(task.Name, tmp));
                    }
                }
            }


            return true;
        }

        public Boolean ResolveCmax()
        {
            if (Processors.Count < 1 || Tasks.Count < 1)
                return false;

            // licz sumę
            var sum = Tasks.Aggregate(0, (current, task) => current + task.Duration);

            // znajdz max liczbę w taskach
            var maxFromTasks = Tasks.Max(t => t.Duration);

            // znajdz max pomiędzy dwoma powyższymi
            CMax = Math.Max( sum / Processors.Count, maxFromTasks);

            return true;
        }

        public List<Task>       Tasks { get; private set; }
        public List<Processor>  Processors { get; private set; }
        public Int32            CMax { get; private set; }
    }
}
