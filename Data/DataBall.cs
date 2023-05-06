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

        public override event PropertyChangedEventHandler? PropertyChanged;

        public override double PosX
        {
            get => _PosX;
            set { _PosX = value; RaisePropertyChanged(); }
        }
        public override double PosY
        {
            get => _PosY;
            set { _PosY = value; RaisePropertyChanged(); }
        }
        public override int Weight { get; set; }
        public override double XSpeed { get; set; }
        public override double YSpeed { get; set;}
        public override int Radius { get; set;}
        public override double TempXSpeed { get; set; }
        public override double TempYSpeed { get; set; }

        public DataBall(int posX, int posY, int weight, int radius, int xSpeed, int ySpeed)
        {
            PosX = posX;
            PosY = posY;
            Weight = weight;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
            Radius = radius;
            Task.Run(StartMovement);
        }

        public void StartMovement()
        {
            while (true)
            {
                Move();
                Task.Delay(10).Wait();
            }
        }

        public override void Move()
        {
            PosX += XSpeed;
            PosY += YSpeed;
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
