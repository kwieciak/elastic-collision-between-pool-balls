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
            
            for(int i = 0; i<number; i++)
            {
                Random random = new Random();
                int x = random.Next(radius, sizeX - radius);
                int y = random.Next(radius, sizeY - radius);
                int weight = random.Next(1, 5);
                int SpeedX = random.Next(-5, 5);
                int SpeedY = random.Next(-5, 5);
                IDataBall dataBall = dataAPI.AddDataBall(x, y, _BallRadius, weight, SpeedX, SpeedY);
                Ball ball = new Ball(dataBall.PosX, dataBall.PosY,radius);
                dataBall.PropertyChanged += ball.UpdateBall;
                dataBall.PropertyChanged += CheckCollisionWithWall;
                //dataBall.PropertyChanged += CheckCollisionWithWall;
                Balls.Add(ball);
            }
        }
        private void CheckCollisionWithWall(Object s, PropertyChangedEventArgs e)
        {
            IDataBall ball = (IDataBall)s;

            if (ball.PosX + ball.XSpeed+ ball.Radius > dataAPI.Width || ball.PosX + ball.XSpeed - ball.Radius < 0)
            {
                ball.XSpeed *= -1;
            }
            if (ball.PosY + ball.YSpeed + ball.Radius > dataAPI.Height || ball.PosY + ball.YSpeed - ball.Radius < 0)
            {
                ball.YSpeed *= -1;
            }
        }

        /*
        private void CheckBallsCollision(Object s, PropertyChangedEventArgs e)
        {
            IDataBall me = (IDataBall)s;
            foreach(IBall ball in Balls)
            {
                if (!ball.Equals(me))
                {
                    //zmienic na odleglosc euklidesowa bo to po kwadracie dziala teraz
                    if(Math.Abs(ball.PosX - me.PosX) < me.Radius + ball.Radius && Math.Abs(ball.PosY - me.PosY) < me.Radius + ball.Radius)
                    {
                        Monitor.Enter(ball);
                        Monitor.Enter(me);
                        try
                        {
                            ball.CollideWithBall(me);
                            me.CollideWithBall(ball);
                            ball.ApplyTempSpeed();
                            me.ApplyTempSpeed();
                            ball.moveBall();
                            me.moveBall();
                        }
                        finally { Monitor.Exit(ball); Monitor.Exit(me); }
                    }
                    return;
                }
            }
        }
        */



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
                Tasks.Clear();                              // Dispose chyba nie usuwa obiektu, wiec trzeba wyczyscic liste                                     
        }



        public override List<IBall> GetAllBalls()
        {
            return Balls;
        }
    }
}
