using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    internal class Board
    {
        public int sizeX { get; set; }
        public int sizeY { get; set; }
        public List<Ball> Balls { get; set; }

        public Board(int sizeX, int sizeY) {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.Balls = new List<Ball>(); //chyba tak powinno to wygladac, idk
        }
    }
}
