using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data
{
    public abstract class IDataBall
    {
        public abstract double PosX { get;  }
        public abstract double PosY { get;  }
        public abstract double XSpeed { get; set; }
        public abstract double YSpeed { get; set; }
        public abstract bool HasCollided { get; set; }
        public abstract void Move();

        public abstract event EventHandler<DataEventArgs> ChangedPosition;

        public static IDataBall CreateDataBall(int posX, int posY, int radius, int weight, int xSpeed, int ySpeed)
        {
            return new DataBall(posX, posY, radius, weight, xSpeed, ySpeed);
        }
    }
}
