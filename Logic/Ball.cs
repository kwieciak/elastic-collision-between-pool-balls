using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;


namespace Logic
{
    /* Ball implementuje interfejs INotifyPropertyChanged
     * tworzac event PropertyChanged
     * 
     * Metoda RaisePropertyChanged() informuje warstwe wyzsza, ze zmienil sie ktorys z atrybutow,
     * tzn. zostal uzyty setter dla tego atrybutu.
     * 
     * W ModelAPI (linijka 25) okresla co sie stanie, gdy nastapi jakas zmiana atrybutu (tzn. jaka funkcja zostanie wywolana).
     */
    internal class Ball : IBall, INotifyPropertyChanged
    {
        private int _PosX;
        private int _PosY;
        private int _Radius;
        private int _SpeedX;
        private int _SpeedY;

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
        public override int SpeedX
        {
            get => _SpeedX;
            set { _SpeedX = value;  }
        }
        public override int SpeedY
        {
            get => _SpeedY;
            set { _SpeedY = value; }
        }


        public int _TempSpeedX { get; set; }
        public int _TempSpeedY { get; set; }
        public override bool IsBouncedBack { get;set; }

        internal Ball(int posX, int posY, int radius)       
        {                                                   
            this.PosX = posX;
            this.PosY = posY;
            this.Radius = radius;
            IsBouncedBack = false;
        }


        public override void moveBall()
        {
            PosX += SpeedX;
            PosY += SpeedY;
        }

        public override void CheckCollision(int BoardWidth ,int BoardHeight)
        {
            if(this.PosX + this.SpeedX + this.Radius > BoardWidth || this.PosX + this.SpeedX - this.Radius < 0)
            {
                SpeedX *= -1;
            }
            if(this.PosY + this.SpeedY + this.Radius > BoardHeight || this.PosY + this.SpeedY - this.Radius < 0)
            {
                SpeedY *= -1;
            }

            /*
            if (this._PosX + this._SpeedX + this._Radius < BoardWidth && this._PosX + this._SpeedX - this._Radius > 0
                && this._PosY + this._SpeedY + this._Radius < BoardHeight && this._PosY + this._SpeedY - this._Radius > 0)
            {
                return true;
            }
            else
            {
                return false;
            }*/
        }


        /* Ta funkcja prawdopodobnie bedzie w Boardzie (jakkolwiek bedzie sie ten Board nazywal po refaktoringu, ale no, w Logice bedzie)
         * Bedzie trzeba przekazywac dwie kulki (zamist jednej) i ustawiać im setterami obliczone predkosci
         */
        public override void CollideWithBall(IBall collider)
        {
            //Te dwie zmienne sa tymczasowe, poki nei dodamy masy do Balla
            double ourMass = 1;
            double otherMass = 1;

            double ourSpeed = Math.Sqrt(this.SpeedX * this.SpeedX + this.SpeedY * this.SpeedY);
            double otherSpeed = Math.Sqrt(collider.SpeedX * collider.SpeedX + collider.SpeedY * collider.SpeedY);

            // Mozliwe ze zle uzywam arcus tangens, juz nie pamietam za bardzo trygonometrii XD
            double contactAngle = Math.Atan(Math.Abs(this.PosY - collider.PosY / this.PosX - collider.PosX));
            
            // same as before
            double ourMovementAngle = Math.Atan(this.SpeedY / this.SpeedX);
            double otherMovementAngle = Math.Atan(collider.SpeedY / collider.SpeedX);


            // numerator_SPEEDX = (ourSpeed*cos(ourMovementAngle-contactAngle)(ourMass - otheramss) + 2*otherMass*otherSpeed*cos(otherMovementAngle-contactAngle))*cos(contactAngle)
            double SpeedXNumerator = (ourSpeed * Math.Cos(ourMovementAngle - contactAngle) * (ourMass - otherMass) + 2 * otherMass * otherSpeed * Math.Cos(otherMovementAngle - contactAngle) * Math.Cos(contactAngle));
            double SpeedXDenominator = ourMass + otherMass;
            double addToSpeedX = ourSpeed * Math.Sin(ourMovementAngle - contactAngle) * Math.Cos(contactAngle + Math.PI / 2f);


            double SpeedYNumerator = (ourSpeed * Math.Cos(ourMovementAngle - contactAngle) * (ourMass - otherMass) + 2 * otherMass * otherSpeed * Math.Cos(otherMovementAngle - contactAngle) * Math.Sin(contactAngle));
            double SpeedYDenominator = SpeedXDenominator;
            double addToSpeedY = ourSpeed * Math.Sin(ourMovementAngle - contactAngle) * Math.Sin(contactAngle + Math.PI / 2f);

            _TempSpeedX = (int)(SpeedXNumerator / SpeedXDenominator + addToSpeedX); 
            _TempSpeedY = (int)(SpeedYNumerator / SpeedYDenominator + addToSpeedY);
            if (_TempSpeedY == 0)
            {
                _TempSpeedY = 1;
            }
            // numerator_SPEEDY = numerator_SPEEDX/cos(contactAngle)*sin(contactAngle) // to put it simply, the numerator is the same, except it's multiplied by sin instead of cos
            // denominator_SPEEDY = denomiator_SPEEDX;
            // addToSpeedY = ourSpeed*sin(ourMovementAngle - contactAngle)*sin(contactAngle + PI/2)
            //contactAngle is the angle of a line connecting the centers of the balls
        }
        public override void ApplyTempSpeed()
        {
            SpeedX = _TempSpeedX;
            SpeedY = _TempSpeedY;
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)   
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateBall(Object s, PropertyChangedEventArgs e)
        {
            IDataBall ball = (IDataBall)s;
            PosX = ball.PosX;
            PosY = ball.PosY;
        }
    }
}
