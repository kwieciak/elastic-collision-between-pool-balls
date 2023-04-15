using System;
using System.Collections.Generic;
using System.Text;


namespace Logic
{
    internal class Ball
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Radius { get; set; }

        public int SpeedX { get; set; }
        public int SpeedY { get; set; }



        public Ball(int posX, int posY, int radius)   // nie wiem czy potrzebujemy tutaj od razu podawac predkosc
        {                                                   // czy jednak powinniśmy dopiero potem to robic
            this.PosX = posX;
            this.PosY = posY;
            this.Radius = radius;
        }

        public void moveBall()
        {
            this.PosX += SpeedX;
            this.PosY += SpeedY;
        }

    }
}
