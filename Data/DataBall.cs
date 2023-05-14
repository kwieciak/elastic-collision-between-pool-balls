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
            set { _PosX = value; }
        }
        public override double PosY
        {
            get => _PosY;
            set { _PosY = value;}
        }
        public override int Weight { get; set; }
        public override double XSpeed { get; set; }
        public override double YSpeed { get; set;}
        public override int Radius { get; set;}
        public override double TempXSpeed { get; set; }
        public override double TempYSpeed { get; set; }
        public override bool HasCollided { get; set; }
        public DataBall(int posX, int posY,  int radius, int weight, int xSpeed, int ySpeed)
        {
            PosX = posX;
            PosY = posY;
            Weight = weight;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
            Radius = radius;
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
            PosX += XSpeed;
            PosY += YSpeed;
            DataEventArgs args = new DataEventArgs(this);
            ChangedPosition?.Invoke(this, args);
        }

        /*
        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/
    }
}
