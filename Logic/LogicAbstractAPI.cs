using System;
using System.Collections.Generic;

namespace Logic
{


    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateAPIInstance()
        {
            return new Board(580,400); 
        }


        public abstract void AddBalls(int number, int radius);

        public abstract void ClearBoard();

        public abstract List<IBall> GetAllBalls();




    }
}
