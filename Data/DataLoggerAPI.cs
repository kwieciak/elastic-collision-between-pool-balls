using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public abstract class DataLoggerAPI
    {
        public abstract void AddBall(IDataBall ball);

        public abstract void AddBoard(IDataBoard board);

        public static DataLoggerAPI CreateLogger()
        { 
            return new DataLogger();
        }
    }
}
