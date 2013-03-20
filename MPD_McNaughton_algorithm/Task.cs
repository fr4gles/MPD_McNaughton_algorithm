using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPD_McNaughton_algorithm
{
    public class Task: ICloneable
    {
        public String Name { get; private set; }
        public Int32 Duration { get; set; }


        public Task(String name, Int32 duration)
        {
            Name = name;
            Duration = duration;
        }

        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
