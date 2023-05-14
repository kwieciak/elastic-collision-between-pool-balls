using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class DataBall:IDataBall
    {

        public override event EventHandler<DataEventArgs>? ChangedPosition;

        private Vector2 _position;
        public override Vector2 Position
        {
            get => _position;
        }

        public override Vector2 Speed { get; set; }
        
        public override bool HasCollided { get; set; }
        public DataBall(int posX, int posY,  int radius, int weight, int xSpeed, int ySpeed)
        {
            _position = new Vector2(posX, posY);
            Speed = new Vector2(xSpeed, ySpeed);
            Task.Run(StartMovement);
            HasCollided = false;
        }

        public async void StartMovement()
        {
            while (true)
            {
                Move();
                HasCollided = false;
                await Task.Delay(10);
            }
        }

        public override void Move()
        {
            _position.X += Speed.X;
            _position.Y += Speed.Y;
            DataEventArgs args = new DataEventArgs(this);
            ChangedPosition?.Invoke(this, args);
        }

    }
}
