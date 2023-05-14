using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace Logic
{

    public abstract class IBall
    {
        public static IBall CreateBall(int xPosition, int yPosition)
        {
            return new Ball(xPosition, yPosition);
        }

        public abstract Vector2 Position {get; }
        public abstract event EventHandler<LogicEventArgs>? ChangedPosition;

        /*
        public abstract int Radius { get; set; }

        public abstract double SpeedX { get; set; }
        public abstract double SpeedY { get; set; }
        public abstract bool IsBouncedBack { get; set; }
        public abstract void moveBall();

        public abstract void CheckCollision(int BoardWidth, int BoardHeight);
        public abstract void CollideWithBall(IBall collider);
        public abstract void ApplyTempSpeed();*/

    }
}
