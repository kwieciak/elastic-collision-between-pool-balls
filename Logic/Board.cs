using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    internal class Board : LogicAbstractAPI
    {
        public int sizeX { get; set; }
        public int sizeY { get; set; }
        public List<Ball> Balls { get; set; }
        public List<Task> Tasks { get; set; }

        public Board(int sizeX, int sizeY) {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            Tasks = new List<Task>();
            Balls = new List<Ball>();
        }

        public override void AddBalls(int number, int radius)
        {
            for(int i =0; i < number; i++)
            {
                Tasks.Add(new Task(() =>
                {
                    Random random = new Random();
                    int x = random.Next(radius, sizeX - radius);
                    int y = random.Next(radius, sizeY - radius);
                    Ball ball = new Ball(x, y, radius);
                    Balls.Add(ball);

                    while (true)
                    {
                        ball.RandomizeSpeed(-5,5);
                        ball.moveBall();
                        Thread.Sleep(500);
                    }
                }));
                
            }
        }

        public override void StartMovement()
        {
            foreach(Task task in Tasks)
            {
                task.Start();
            }
        }

        public override void ClearBoard()
        {
            foreach (Task task in Tasks)
            {
                task.Dispose();
            }
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
    }
}
