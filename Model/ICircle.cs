using Logic;
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


        public abstract int x { get;set; }
        public abstract int y { get;set; }
        public abstract int radius { get; set; }

        public abstract void UpdateCircle(Object s, LogicEventArgs e);

        public abstract event PropertyChangedEventHandler? PropertyChanged;
        //public abstract void RaisePropertyChanged([CallerMemberName] string? propertyName = null);
    }
}
