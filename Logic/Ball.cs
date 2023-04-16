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
            get => _PosX;
            set { _PosX = value; RaisePropertyChanged(); }
        }
        public override int PosY
        {
            get => _PosY;
            set { _PosY = value; RaisePropertyChanged(); }
        }

        public override int Radius
        {
            get => _Radius;
            set {  _Radius = value; RaisePropertyChanged();}
        }



        public int _PosX { get; set; }
        public int _PosY { get; set; }

        public int _Radius { get; set; }

        public int SpeedX { get; set; }
        public int SpeedY { get; set; }

        internal Ball(int posX, int posY, int radius)   // nie wiem czy potrzebujemy tutaj od razu podawac predkosc
        {                                                   // czy jednak powinniśmy dopiero potem to robic
            this._PosX = posX;
            this._PosY = posY;
            this._Radius = radius;
        }

        public override event PropertyChangedEventHandler? PropertyChanged;

        internal void moveBall()
        {
            this._PosX += SpeedX;
            this._PosY += SpeedY;
        }

        internal bool CheckCollision(int BoardWidth ,int BoardHeight)
        {
            if (this._PosX + this.SpeedX + this._Radius < BoardWidth && this._PosX + this.SpeedX - this._Radius > 0
                && this._PosY + this.SpeedY + this._Radius < BoardHeight && this._PosY + this.SpeedY - this._Radius > 0)
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
