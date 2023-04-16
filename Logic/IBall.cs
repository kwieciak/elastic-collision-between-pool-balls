using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Logic
{
    public abstract class IBall
    {
        public static IBall CreateBall(int xPosition, int yPosition, int radius)
        {
            return new Ball(xPosition, yPosition, radius);
        }

        public abstract int PosX { get; set; }
        public abstract int PosY { get; set; }
        public abstract int Radius { get; set; }

        public abstract int SpeedX { get; set; }
        public abstract int SpeedY { get; set; }

        public abstract event PropertyChangedEventHandler? PropertyChanged;

    }
}
