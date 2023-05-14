using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace Data
{
    public abstract class IDataBall
    {
        public abstract Vector2 Position { get;}
        public abstract Vector2 Speed { get; set; }
        public abstract bool HasCollided { get; set; }
        public abstract bool ContinueMoving { get; set; }
        public abstract void Move();

        public abstract event EventHandler<DataEventArgs> ChangedPosition;

        public static IDataBall CreateDataBall(int posX, int posY, int radius, int weight, int xSpeed, int ySpeed)
        {
            return new DataBall(posX, posY, radius, weight, xSpeed, ySpeed);
        }
    }
}
