using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    internal class LogicAPI : LogicAbstractAPI
    {
        public LogicAPI() { }


        override public void MoveBall(){

        }
        public override void AddBall(int x, int y, int radius){
            Ball ball = new Ball(x,y,radius);
            // tutaj dodawanie kulki do Boarda
            // ALE: patrz LogicAbstractAPI (tam kwestionuje czy sie oplaca ta funkcje robic)
        }
        public override void CreateBoard(int x, int y, int ballAmount)
        {
            //throw new NotImplementedException();
        }

    }
}
