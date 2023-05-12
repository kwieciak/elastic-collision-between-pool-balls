using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    internal class Board : LogicAbstractAPI
    {
        public int sizeX { get; set; }
        public int sizeY { get; set; }

        private int _BallRadius { get; set; }
        public List<IBall> Balls { get; set; }
        public List<Task> Tasks { get; set; }

        private Object _locker = new Object();

        public IDataBoard dataAPI;



        public Board(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            Tasks = new List<Task>();
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
                Ball ball = new Ball(dataBall.PosX, dataBall.PosY);

                //dodajemy do eventu funkcje, ktore beda sie wywolywaly po wykonaniu Move(), bo wtedy jest PropertyChanged wywolywane
                dataBall.PropertyChanged += ball.UpdateBall;    //ball to nasz ball w logice, nie w data
                dataBall.PropertyChanged += CheckCollisionWithWall;
                dataBall.PropertyChanged += CheckBallsCollision;

                Balls.Add(ball);
            }
        }

        private void CheckCollisionWithWall(Object s, PropertyChangedEventArgs e)
        {
            
            IDataBall ball = (IDataBall)s;
            if (!ball.HasCollided)
            {
                if (ball.PosX + ball.XSpeed + ball.Radius > dataAPI.Width || ball.PosX + ball.XSpeed - ball.Radius < 0)
                {
                    ball.XSpeed *= -1;
                }
                if (ball.PosY + ball.YSpeed + ball.Radius > dataAPI.Height || ball.PosY + ball.YSpeed - ball.Radius < 0)
                {
                    ball.YSpeed *= -1;
                }
            }
        }

        private void CheckBallsCollision(Object s, PropertyChangedEventArgs e)
        {
            IDataBall me = (IDataBall)s;
            if(!me.HasCollided)
            {
                lock(_locker)
                {
                    foreach (IDataBall ball in dataAPI.GetAllBalls())
                    {
                        if (ball!=me)
                        {
                            if (Math.Sqrt(Math.Pow(ball.PosX - me.PosX , 2) + Math.Pow(ball.PosY - me.PosY, 2)) <= 2*_BallRadius) 
                            {
                                ballCollision(me, ball);
                            }
                        }
                    }
                }
            }
        }
        /*public void ApplyTempSpeed(IDataBall ball)
        {
            ball.YSpeed = ball.TempYSpeed;
            ball.XSpeed = ball.TempXSpeed;
        }*/

        private void ballCollision(IDataBall ball, IDataBall otherBall)
        {
            if (Math.Sqrt(Math.Pow(ball.PosX+ball.XSpeed - otherBall.PosX - otherBall.XSpeed, 2) + Math.Pow(ball.PosY + ball.YSpeed - otherBall.PosY - otherBall.YSpeed, 2)) <= otherBall.Radius + ball.Radius)
            {
                double weight = 1d;

                double newXMovement = (2d * weight * ball.XSpeed) / (2d * weight);
                ball.XSpeed = (2d * weight * otherBall.XSpeed) / (2d * weight);
                otherBall.XSpeed = newXMovement;

                double newYMovement = (2 * weight * ball.YSpeed) / (2d * weight);
                ball.YSpeed = (2d * weight * otherBall.YSpeed) / (2d * weight);
                otherBall.YSpeed = newYMovement;

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
            bool IsEveryTaskCompleted = false;
       
                while (!IsEveryTaskCompleted)               // Ta petla upewnia sie, ze wszystkie Taski sa w stanie "Completed"
                {                                           // Gdy wszystkie beda Completed to skonczy sie ona i funkcja Task.Dispose()                                        
                    IsEveryTaskCompleted = true;            // uwolni wszystkie uzywane przez taski zasoby
                    foreach (Task task in Tasks)
                    {
                        if (!task.IsCompleted)
                        {
                            IsEveryTaskCompleted = false;
                            break;
                        }
                    }
                }

                foreach (Task task in Tasks)
                {
                    task.Dispose();                         // Uwalnianie zasobow uzywanych przez dany task
                }
                Balls.Clear();
        }



        public override List<IBall> GetAllBalls()
        {
            return Balls;
        }
    }
}
