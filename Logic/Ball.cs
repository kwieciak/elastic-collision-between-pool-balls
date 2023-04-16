using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;


namespace Logic
{
    internal class Ball : IBall, INotifyPropertyChanged
    {
        /* Te 3 overridy dobrze ilustruja to co napisalem w IBall.cs
         * Trzeba by sie serio zastanowic czy nie nazwac tego BallAPI (w sensie IBalla)
         */

        public override event PropertyChangedEventHandler? PropertyChanged;             // To wykrywa (I suppose) wszystkie wywolania RaisePropertyChanged()
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

        public int _SpeedX { get; set; }
        public int _SpeedY { get; set; }


        internal Ball(int posX, int posY, int radius)       
        {                                                   
            this._PosX = posX;
            this._PosY = posY;
            this._Radius = radius;
        }


        public override void moveBall()
        {
            PosX += _SpeedX;
            PosY += _SpeedY;
        }

        internal bool CheckCollision(int BoardWidth ,int BoardHeight)
        {
            if (this._PosX + this._SpeedX + this._Radius < BoardWidth && this._PosX + this._SpeedX - this._Radius > 0
                && this._PosY + this._SpeedY + this._Radius < BoardHeight && this._PosY + this._SpeedY - this._Radius > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void RandomizeSpeed(int min, int max)
        {
            Random rnd = new Random();
            this._SpeedY = rnd.Next(min, max);
            this._SpeedX = rnd.Next(min, max);
        }

        /* To jest powiazane z tym PropertyChangedHandlerem
         * Tzn. ten handler wychwytuje wywolanie tej funkcji (?)
         */

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)   
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
