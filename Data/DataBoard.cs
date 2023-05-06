using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data
{
    internal class DataBoard:IDataBoard
    {
        public override int Width { get; set; }
        public override int Height { get; set; }

        private List<IDataBall> Balls = new List<IDataBall>();

        public DataBoard(int width, int height) {
            Width = width; 
            Height = height;
        }

        public override List<IDataBall> GetAllBalls()
        {
            return Balls;
        }
        public override void RemoveAllBalls()
        {
            Balls.Clear();
        }

        public override IDataBall AddDataBall(int xPosition, int yPosition, int radius, int weight, int xSpeed = 0, int ySpeed = 0)
        {
            IDataBall ballData = IDataBall.CreateDataBall(xPosition, yPosition, radius, weight, xSpeed, ySpeed);
            Balls.Add(ballData);
            return ballData;
        }

    }
}
