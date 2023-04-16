using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;


namespace Logic
{
    internal class Ball : IBall
    {
        public override int PosX 
        {
            get => PosX; 
            set { RaisePropertyChanged(); }
        }
        public override int PosY
        {
            get => PosY;
            set { RaisePropertyChanged(); }
        }
        public override int Radius { get; set; }

        public override int SpeedX { get; set; }
        public override int SpeedY { get; set; }



        internal Ball(int posX, int posY, int radius)   // nie wiem czy potrzebujemy tutaj od razu podawac predkosc
        {                                                   // czy jednak powinniśmy dopiero potem to robic
            this.PosX = posX;
            this.PosY = posY;
            this.Radius = radius;
        }

        public override event PropertyChangedEventHandler? PropertyChanged;

        internal void moveBall()
        {
            this.PosX += SpeedX;
            this.PosY += SpeedY;
        }

        internal bool CheckCollision(int BoardWidth ,int BoardHeight)
        {
            if (this.PosX + this.SpeedX + this.Radius < BoardWidth && this.PosX + this.SpeedX - this.Radius > 0
                && this.PosY + this.SpeedY + this.Radius < BoardHeight && this.PosY + this.SpeedY - this.Radius > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void RandomizeSpeed(int min, int max)
        {
            Random rnd = new Random();
            this.SpeedY = rnd.Next(min, max);
            this.SpeedX = rnd.Next(min, max);
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)   //MOZE NAZWA NOTIFY PROPERTY CHANGED
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
