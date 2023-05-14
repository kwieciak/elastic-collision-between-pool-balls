using Data;
using System;
using System.Collections.Generic;

namespace Logic
{


    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateAPIInstance(IDataBoard dataApi = null)
        {
            return new Board( dataApi==null ? IDataBoard.CreateApi() : dataApi); 
        }


        public abstract void AddBalls(int number, int radius);

        public abstract void ClearBoard();

        public abstract List<IBall> GetAllBalls();




    }
}
