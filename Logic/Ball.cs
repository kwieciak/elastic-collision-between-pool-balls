using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;


namespace Logic
{
    internal class Ball : IBall
    {
        private Vector2 _position;

        public override Vector2 Position { get =>_position; }


        public override event EventHandler<LogicEventArgs>? ChangedPosition;

        internal Ball(float posX, float posY)       
        {                                                   
            _position.X = posX;
            _position.Y = posY;
        }

        

        public void UpdateBall(Object s, DataEventArgs e)
        {
            IDataBall ball = (IDataBall)s;
            _position.X = ball.Position.X;
            _position.Y = ball.Position.Y;
            LogicEventArgs args = new LogicEventArgs(this);
            ChangedPosition?.Invoke(this, args);
        }
     

    }
}
