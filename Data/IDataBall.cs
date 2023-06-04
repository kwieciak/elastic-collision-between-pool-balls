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

    }
}
