using System;
using System.Drawing;

namespace MPD_McNaughton_algorithm
{
    public class Task : ICloneable
    {
        public String Name { get; private set; }
        public Double Duration { get; set; }
        public Color Color;


        public Task(String name, Double duration, Color color)
        {
            Name = name;
            Duration = duration;
            Color = color;
        }

        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
