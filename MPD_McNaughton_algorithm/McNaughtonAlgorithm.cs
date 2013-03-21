using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MPD_McNaughton_algorithm
{
    public class McNaughtonAlgorithm
    {
        public List<Processor> ProcessorsList;
        public List<Task> TasksList;
        public static Double Cmax;

        public McNaughtonAlgorithm(int numberOfProcessors)
        {
            ProcessorsList = new List<Processor>();
            TasksList = new List<Task>();

            Cmax = -1;

            AddProcessors(numberOfProcessors);
        }

        public void Go()
        {
            CalculateCmax();

            StartAlgo();
        }



        public void ClearObj()
        {
            if (ProcessorsList != null)
                ProcessorsList.Clear();

            if (TasksList != null)
                TasksList.Clear();

            Cmax = -1.0;
        }

        public void AddProcessors(int numberOfProcessors)
        {
            for (var i = 0; i < numberOfProcessors; ++i)
                ProcessorsList.Add(new Processor(i));
        }

        public void CalculateCmax()
        {
            var tmpList = new List<Task>(TasksList);
            tmpList = tmpList.OrderBy(x => x.Duration).ToList();

            var max = tmpList[tmpList.Count - 1].Duration;

            var sum = tmpList.Sum(task => task.Duration);

            Cmax = Math.Max(max, sum / ProcessorsList.Count);
        }

        private void StartAlgo()
        {
            try
            {
                TasksList = TasksList.OrderBy(a => Guid.NewGuid()).ToList();
                var j = 0;
                var jcopy = 0;
                for (var i = 0; i < ProcessorsList.Count; ++i)
                {
                    var duration = 0.0;
                    j = jcopy;

                    for (; j < TasksList.Count; j++)
                    {
                        var tmpDuration = duration + TasksList[j].Duration;

                        if (tmpDuration > Cmax)
                        {
                            var tmpTask = (Task)TasksList[j].Clone();
                            TasksList[j].Duration = TasksList[j].Duration - (tmpDuration - Cmax);

                            tmpTask.Duration = (tmpDuration - Cmax);

                            TasksList.Insert(j + 1, tmpTask);

                            ProcessorsList[i].ProcessorTasksList.Add(TasksList[j]);
//                            n++;

                            jcopy = j + 1;
                            j = TasksList.Count; // wyjscie z petli taskows
                        }
                        else
                        {
                            duration += TasksList[j].Duration;

                            ProcessorsList[i].ProcessorTasksList.Add(TasksList[j]);
//                            n++;

                            if (Math.Abs(duration - Cmax) < 0.001)
                            {
                                jcopy = j + 1;
                                j = TasksList.Count; // wyjscie z petli taskow
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
