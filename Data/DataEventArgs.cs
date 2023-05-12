using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class DataEventArgs
    {
        public IDataBall Ball;
        public DataEventArgs(IDataBall ball)
        {
            Ball = ball;
        }
    }
}
