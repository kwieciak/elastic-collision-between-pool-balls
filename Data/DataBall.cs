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
        private int _PosX;
        private int _PosY;

        public override event PropertyChangedEventHandler? PropertyChanged;

        public override int PosX
        {
            get => _PosX;
            set { _PosX = value; RaisePropertyChanged(); }
        }
        public override int PosY
        {
            get => _PosY;
            set { _PosY = value; RaisePropertyChanged(); }
        }
        public override int Weight { get; set; }
        public override int XSpeed { get; set; }
        public override int YSpeed { get; set;}
        public override int Radius { get; set;}

        public DataBall(int posX, int posY, int weight, int radius, int xSpeed, int ySpeed)
        {
            PosX = posX;
            PosY = posY;
            Weight = weight;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
            Radius = radius;
            Task ballTask = Task.Run(StartMovement);
            //ballTask.Start();
        }

        public void StartMovement()
        {
            while (true)
            {
                Move();
                Task.Delay(10).Wait();
            }
        }

        public void Move()
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
