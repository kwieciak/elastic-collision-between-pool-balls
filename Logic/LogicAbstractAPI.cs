using System;
using System.Collections.Generic;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateAPIInstance()
        {
            return new Board(1920,1080); //rozmiar jest randomowy, idk jaki powinien byc
        }


        public abstract void AddBalls(int number, int radius);

        public abstract void StartMovement();

        public abstract void ClearBoard();

        public abstract List<List<int>> GetAllBallsPosition();

        public abstract List<IBall> GetAllBalls();










        // W INSTRUKCJI JEST COS O OKRESOWYM WYSYLANIU POLOZENIA KUL
        // nie wiem jak to interpretowac, jest opcja ze po prostu wysyla tablice z polozeniami wszystkich kul (i chyba to to)
        // moze wysyla po prostu info na temat jakiejs konkretnej kuli

        // zagwozdka jest taka, czy powinno sie wysylac obiekty kul, czy tylko x,y,radius jako zwykle floaty
        // imho to drugie



    }
}
