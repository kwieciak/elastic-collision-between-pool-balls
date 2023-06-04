using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace Data
{
    public abstract class IDataBall
    {
        public abstract Vector2 Position { get;}
        public abstract Vector2 Speed { get; set; }

        public abstract int ID { get;}

        public abstract void Dispose();

        public abstract event EventHandler<DataEventArgs> ChangedPosition;

        public static IDataBall CreateDataBall(int posX, int posY, int radius, int weight, int xSpeed, int ySpeed, DataLoggerAPI logger, int id)
        {
            return new DataBall(posX, posY, radius, weight, xSpeed, ySpeed, logger, id);
        }
    }
}
