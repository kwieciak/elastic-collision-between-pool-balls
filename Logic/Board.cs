using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    internal class Board : LogicAbstractAPI
    {
        private int sizeX;
        private int sizeY;

        private int _BallRadius { get; set; }
        public List<IBall> Balls { get; set; }

        private Object _locker = new Object();

        public IDataBoard dataAPI;



        public Board(int SizeX, int SizeY)
        {
            sizeX = SizeX;
            sizeY = SizeY;
            Balls = new List<IBall>();
            dataAPI = IDataBoard.CreateApi(sizeY, sizeX);
        }

        public override void AddBalls(int number, int radius)
        {
            _BallRadius = radius;
            for (int i = 0; i < number; i++)
            {
                Random random = new Random();
                int x = random.Next(radius, sizeX - radius);
                int y = random.Next(radius, sizeY - radius);
                int weight = random.Next(3, 3);

                int SpeedX;
                do
                {
                    SpeedX = random.Next(-3, 3);
                } while (SpeedX == 0);

                int SpeedY;
                do
                {
                    SpeedY = random.Next(-3, 3);
                } while (SpeedY == 0);

                IDataBall dataBall = dataAPI.AddDataBall(x, y, _BallRadius, weight, SpeedX, SpeedY);
                Ball ball = new Ball(dataBall.Position.X, dataBall.Position.Y);

                //dodajemy do eventu funkcje, ktore beda sie wywolywaly po wykonaniu Move(), bo wtedy jest PropertyChanged wywolywane
                dataBall.ChangedPosition += ball.UpdateBall;        //ball to nasz ball w logice, nie w data
                dataBall.ChangedPosition += CheckCollisionWithWall;
                dataBall.ChangedPosition += CheckBallsCollision;

                Balls.Add(ball);
            }
        }

        private void CheckCollisionWithWall(Object s, DataEventArgs e)
        {
            
            IDataBall ball = (IDataBall)s;
            if (!ball.HasCollided)
            {
                if (ball.Position.X + ball.Speed.X + _BallRadius > dataAPI.Width || ball.Position.X + ball.Speed.X - _BallRadius < 0)
                {
                    ball.Speed = new Vector2(-ball.Speed.X, ball.Speed.Y);
                }
                if (ball.Position.Y + ball.Speed.Y + _BallRadius > dataAPI.Height || ball.Position.Y + ball.Speed.Y - _BallRadius < 0)
                {
                    ball.Speed = new Vector2(ball.Speed.X, -ball.Speed.Y);
                }
            }
        }

        private void CheckBallsCollision(Object s, DataEventArgs e)
        {
            IDataBall me = (IDataBall)s;
            lock (_locker)
            {
                if (!me.HasCollided)
                {  
                    foreach (IDataBall ball in dataAPI.GetAllBalls().ToArray())
                    {
                        if (ball!=me)
                        {
                            if (Math.Sqrt(Math.Pow(ball.Position.X - me.Position.X , 2) + Math.Pow(ball.Position.Y - me.Position.Y, 2)) <= 2*_BallRadius) 
                            {
                                ballCollision(me, ball);
                            }
                        }
                    }
                }
            }
        }


        private void ballCollision(IDataBall ball, IDataBall otherBall)
        {
            if (Math.Sqrt(Math.Pow(ball.Position.X+ball.Speed.X - otherBall.Position.X - otherBall.Speed.X, 2) + Math.Pow(ball.Position.Y + ball.Speed.Y - otherBall.Position.Y - otherBall.Speed.Y, 2)) <= _BallRadius + _BallRadius)
            {
                float weight = 1f;

                float otherBallXMovement = (2f * weight * ball.Speed.X) / (2f * weight);
                float ballXMovement = (2f * weight * otherBall.Speed.X) / (2f * weight);

                float otherBallYMovement = (2f * weight * ball.Speed.Y) / (2f * weight);
                float ballYMovement = (2f * weight * otherBall.Speed.Y) / (2f * weight);


                ball.Speed = new Vector2(ballXMovement, ballYMovement);
                otherBall.Speed = new Vector2(otherBallXMovement, otherBallYMovement);

                ball.HasCollided = true;
                otherBall.HasCollided = true;
            }
        }


        
        /*
        public void CollideWithBall(IDataBall me, IDataBall collider)
        {
            //Te dwie zmienne sa tymczasowe, poki nei dodamy masy do Balla
            double ourMass = 1;
            double otherMass = 1;

            double ourSpeed = Math.Sqrt(me.XSpeed * me.XSpeed + me.YSpeed * me.YSpeed);
            double otherSpeed = Math.Sqrt(collider.XSpeed * collider.XSpeed + collider.YSpeed * collider.YSpeed);

            // Mozliwe ze zle uzywam arcus tangens, juz nie pamietam za bardzo trygonometrii XD
            //TUTAJ MOZLIWE ZE JEST BLAD W TYM WZORZE
            double contactAngle = Math.Atan(Math.Abs((me.PosY - collider.PosY )/ (me.PosX - collider.PosX)));

            // same as before
            double ourMovementAngle = Math.Atan(me.YSpeed / me.XSpeed);
            double otherMovementAngle = Math.Atan(collider.YSpeed / collider.XSpeed);


            // numerator_SPEEDX = (ourSpeed*cos(ourMovementAngle-contactAngle)(ourMass - otheramss) + 2*otherMass*otherSpeed*cos(otherMovementAngle-contactAngle))*cos(contactAngle)
            double SpeedXNumerator = (ourSpeed * Math.Cos(ourMovementAngle - contactAngle) * (ourMass - otherMass) + 2 * otherMass * otherSpeed * Math.Cos(otherMovementAngle - contactAngle) * Math.Cos(contactAngle));
            double SpeedXDenominator = ourMass + otherMass;
            double addToSpeedX = ourSpeed * Math.Sin(ourMovementAngle - contactAngle) * Math.Cos(contactAngle + Math.PI / 2f);


            double SpeedYNumerator = (ourSpeed * Math.Cos(ourMovementAngle - contactAngle) * (ourMass - otherMass) + 2 * otherMass * otherSpeed * Math.Cos(otherMovementAngle - contactAngle) * Math.Sin(contactAngle));
            double SpeedYDenominator = SpeedXDenominator;
            double addToSpeedY = ourSpeed * Math.Sin(ourMovementAngle - contactAngle) * Math.Sin(contactAngle + Math.PI / 2f);

            me.TempXSpeed = (SpeedXNumerator / SpeedXDenominator + addToSpeedX);
            me.TempYSpeed = (SpeedYNumerator / SpeedYDenominator + addToSpeedY);
            // numerator_SPEEDY = numerator_SPEEDX/cos(contactAngle)*sin(contactAngle) // to put it simply, the numerator is the same, except it's multiplied by sin instead of cos
            // denominator_SPEEDY = denomiator_SPEEDX;
            // addToSpeedY = ourSpeed*sin(ourMovementAngle - contactAngle)*sin(contactAngle + PI/2)
            //contactAngle is the angle of a line connecting the centers of the balls
        }
        */


        // tutaj jest problem, bo nie usuwamy instancji kulek (tzn. taski dalej sie wykonuja w tle, ale nie ma ich narysowanych na planszy)
        public override void ClearBoard()
        {
            Balls.Clear();
        }

        public override List<IBall> GetAllBalls()
        {
            return Balls;
        }
    }
}
