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
        public List<IBall> Balls { get; set; }
        public List<Task> Tasks { get; set; }

        private bool stopTasks;
        internal object Locker = new object();
        private readonly DataAbstractAPI _dataLayer;
        

        public Board(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            Tasks = new List<Task>();
            Balls = new List<IBall>();
            _dataLayer = DataAbstractAPI.CreateAPIInstance();
        }

        public override void AddBalls(int number, int radius)
        {
            for (int i = 0; i < number; i++)
            {
                Random random = new Random();
                int x = random.Next(radius, sizeX - radius);
                int y = random.Next(radius, sizeY - radius);
                IBall ball = IBall.CreateBall(x, y, radius);
                Balls.Add(ball);
                /* 
                 * Teraz tego taska tylko "przygotowujemy".
                 * Wywolywany on bedzie dopiero, gdy zostanie na nim wywolana metoda Start()
                 */

                Tasks.Add(new Task(() =>
                {
                    
                    ball.RandomizeSpeed(-5, 5);
                    while (!stopTasks)
                    {
                        ball.moveBall();
                        Monitor.Enter(Balls);
                        try
                        {
                            ball.CheckCollision(sizeX, sizeY);
                            CheckBallsCollision(ball);
                        }
                        finally { Monitor.Exit(Balls); }

                        Task.Delay(10).Wait();
                    }
                }));

            }
        }
        private void CheckBallsCollision(IBall me)
        {
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



        public override void StartMovement()            // Ta funkcja startuje taski, ktore zostaly utworzone w AddBalls(int,int)
        {
            stopTasks = false;

            foreach (Task task in Tasks)
            {
                task.Start();
            }
        }

        public override void ClearBoard()
        {
            stopTasks = true;
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



        public override List<List<int>> GetAllBallsPosition()
        {
            List<List<int>> positions = new List<List<int>>();
            foreach (Ball b in Balls)
            {
                List<int> BallPosition = new List<int>
                {
                    b._PosX,
                    b._PosY
                };
                positions.Add(BallPosition);
            }
            return positions;
        }

        public override List<IBall> GetAllBalls()
        {
            return Balls;
        }
    }
}
