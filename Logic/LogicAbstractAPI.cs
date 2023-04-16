using System;
using System.Collections.Generic;

namespace Logic
{
    /* Powinnismy tutaj jeszcze wstrzyknac jakos DataAbstractAPI */


    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateAPIInstance()
        {
            return new Board(1920,1080); //rozmiar jest randomowy, idk jaki powinien byc, raczej mniejszy i to wyjdzie pewnie w ViewModel
        }


        public abstract void AddBalls(int number, int radius);

        public abstract void StartMovement();

        public abstract void ClearBoard();

        // W sumie to chyba ta metoda jest jednak useless
        public abstract List<List<int>> GetAllBallsPosition();

        public abstract List<IBall> GetAllBalls();




    }
}
