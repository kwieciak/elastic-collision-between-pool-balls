﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data
{
    public abstract class IDataBall
    {
        public abstract double PosX { get; set; }
        public abstract double PosY { get; set; }
        public abstract int Weight { get; set; }
        public abstract double XSpeed { get; set; }
        public abstract double TempXSpeed { get; set; }
        public abstract double TempYSpeed { get; set; }
        public abstract double YSpeed { get; set; }
        public abstract int Radius { get; set; }

        public abstract void Move();

        public abstract event PropertyChangedEventHandler? PropertyChanged;

        public static IDataBall CreateDataBall(int posX, int posY, int weight, int radius, int xSpeed, int ySpeed)
        {
            return new DataBall(posX, posY, weight, radius, xSpeed, ySpeed);
        }
    }
}