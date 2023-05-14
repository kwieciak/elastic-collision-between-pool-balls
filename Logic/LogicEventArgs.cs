using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class LogicEventArgs
    {
        public IBall Ball;
        public LogicEventArgs(IBall ball)
        {
            Ball = ball;
        }
    }
}
