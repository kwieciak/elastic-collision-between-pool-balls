using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class DataBall:IDataBall
    {
        private double _PosX;
        private double _PosY;

        public override event EventHandler<DataEventArgs>? ChangedPosition;

        public override double PosX
        {
            get => _PosX;
        }
        public override double PosY
        {
            get => _PosY;
        }
        public override double XSpeed { get; set; }
        public override double YSpeed { get; set;}
        public override bool HasCollided { get; set; }
        public DataBall(int posX, int posY,  int radius, int weight, int xSpeed, int ySpeed)
        {
            _PosX = posX;
            _PosY = posY;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
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
            _PosX += XSpeed;
            _PosY += YSpeed;
            DataEventArgs args = new DataEventArgs(this);
            ChangedPosition?.Invoke(this, args);
        }

    }
}
