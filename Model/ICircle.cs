using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Model
{
    public abstract class ICircle
    {
        public static ICircle CreateCircle(int x, int y, int radius)
        {
            return new Circle(x, y, radius);
        }


        public abstract int _x { get;set; }
        public abstract int _y { get;set; }
        public abstract int _radius { get; set; }

        public abstract void UpdateCircle(Object s, PropertyChangedEventArgs e);
        public abstract void RaisePropertyChanged([CallerMemberName] string? propertyName = null);
    }
}
