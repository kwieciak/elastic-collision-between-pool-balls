using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public abstract class IDataBoard
    {
        public abstract int Width {get;}
        public abstract int Height { get;}

        public abstract List<IDataBall> GetAllBalls();
        public abstract void RemoveAllBalls();
        public abstract IDataBall AddDataBall(int xPosition, int yPosition, int radius, int weight, int xSpeed = 0, int ySpeed = 0);

        public static IDataBoard CreateApi(int boardWidth, int boardHeight)
        {
            return new DataBoard(boardWidth, boardHeight);
        }

    }
}
