using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Logic
{

    /*
     * Dlaczego ta klasa w ogole istnieje?
     * Mianowicie pojawia sie problem taki, ze potrzebujemy informowac warstwe wyzsza o zmianie X lub Y
     * za pomoca RaisePropertyChanged() w Ball.cs.
     * Bez interfejsu/tej abstrakcji nie jestesmy w stanie tego zrobic, bo kula jest prywatna.
     * 
     * To jest w sumie API w tym momencie i tak chyba najlatwiej jest o tym myslec.
     */
    public abstract class IBall
    {
        public static IBall CreateBall(int xPosition, int yPosition, int radius)
        {
            return new Ball(xPosition, yPosition, radius);
        }

        public abstract int PosX { get; set; }
        public abstract int PosY { get; set; }
        public abstract int Radius { get; set; }
        public abstract void RandomizeSpeed(int min, int max);
        public abstract void moveBall();

        //public abstract int SpeedX { get; set; }
        //public abstract int SpeedY { get; set; }

        public abstract event PropertyChangedEventHandler? PropertyChanged;

    }
}
