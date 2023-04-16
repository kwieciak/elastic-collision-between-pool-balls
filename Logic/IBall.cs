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
        public abstract void RandomizeSpeed(int min, int max);
        public abstract void moveBall();

        public abstract bool CheckCollision(int BoardWidth, int BoardHeight);


        public abstract event PropertyChangedEventHandler? PropertyChanged;

    }
}
