using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;


namespace Logic
{
    internal class Ball : IBall
    {
        private double _PosX;
        private double _PosY;
        /*private int _Radius;
        private double _SpeedX;
        private double _SpeedY;*/


        public override event EventHandler<LogicEventArgs>? ChangedPosition;
        // To wykrywa (I suppose) wszystkie wywolania RaisePropertyChanged()
        public override double PosX
        {
            get => _PosX;
        }
        public override double PosY
        {
            get => _PosY;
        }
        /*
        public override int Radius
        {
            get => _Radius;
            set {  _Radius = value;}
        }*/
        /*
        public override double SpeedX
        {
            get => _SpeedX;
            set { _SpeedX = value;  }
        }
        public override double SpeedY
        {
            get => _SpeedY;
            set { _SpeedY = value; }
        }*/


        /*public int _TempSpeedX { get; set; }
        public int _TempSpeedY { get; set; }
        public override bool IsBouncedBack { get;set; }*/

        internal Ball(double posX, double posY)       
        {                                                   
            _PosX = posX;
            _PosY = posY;
        }

        

        public void UpdateBall(Object s, DataEventArgs e)
        {
            IDataBall ball = (IDataBall)s;
            _PosX = ball.PosX;
            _PosY = ball.PosY;
            LogicEventArgs args = new LogicEventArgs(this);
            ChangedPosition?.Invoke(this, args);
        }
        /*
        public override void moveBall()
        {
            PosX += SpeedX;
            PosY += SpeedY;
        }*/
        /*public override void CheckCollision(int BoardWidth ,int BoardHeight)
        {
            if(this.PosX + this.SpeedX + this.Radius > BoardWidth || this.PosX + this.SpeedX - this.Radius < 0)
            {
                SpeedX *= -1;
            }
            if(this.PosY + this.SpeedY + this.Radius > BoardHeight || this.PosY + this.SpeedY - this.Radius < 0)
            {
                SpeedY *= -1;
            }
        }*/


        /* Ta funkcja prawdopodobnie bedzie w Boardzie (jakkolwiek bedzie sie ten Board nazywal po refaktoringu, ale no, w Logice bedzie)
         * Bedzie trzeba przekazywac dwie kulki (zamist jednej) i ustawiać im setterami obliczone predkosci
         */
        /*
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
        }*/


    }
}
