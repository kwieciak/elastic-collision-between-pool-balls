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

        //public event PropertyChangedEventHandler? PropertyChanged;


        public Board(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            Tasks = new List<Task>();
            Balls = new List<IBall>();
        }

        public override void AddBalls(int number, int radius)
        {
            Random rnd = new Random();
            for(int i = 0; i < number; i++)
            {
                int x = rnd.Next(-100, 100);
                int y = rnd.Next(-100, 100);
                Ball b = new Ball(x, y, radius);
                Balls.Add(b);
            }
        }

        public override void StartMovement()
        {
            foreach(Ball b in Balls)
            {
                b.RandomizeSpeed(-5, 5);
                b.moveBall();
            }
        }

        public override void ClearBoard()
        {
            Balls.Clear();           
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
