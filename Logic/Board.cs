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

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool stopTasks;


        public Board(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            Tasks = new List<Task>();
            Balls = new List<IBall>();
        }

        public override void AddBalls(int number, int radius)
        {
            for (int i = 0; i < number; i++)
            {
                Random random = new Random();
                int x = random.Next(radius, sizeX - radius);
                int y = random.Next(radius, sizeY - radius);
                Ball ball = new Ball(x, y, radius);
                Balls.Add(ball);
                Tasks.Add(new Task(() =>
                {
                    while (!stopTasks)
                    {
                        ball.RandomizeSpeed(-5, 5);
                        ball.moveBall();
                        Thread.Sleep(1);
                    }
                }));

            }
        }

        public override void StartMovement()
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
                    b.PosX,
                    b.PosY
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
