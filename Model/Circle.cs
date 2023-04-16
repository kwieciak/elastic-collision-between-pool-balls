using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Model
{
    internal class Circle : ICircle
    {
        public override int _x { get => _x; set { _x = value; RaisePropertyChanged(); } }
        public override int _y { get => _y; set { _y = value; } }

        public override int _radius { get; set; }

        public Circle(int x, int y, int radius)
        {
            _x = x;
            _y = y;
            _radius = radius;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public override void UpdateCircle(Object s, PropertyChangedEventArgs e)
        {
            IBall ball = (IBall)s;
            if (e.PropertyName == "PosX")
            {
                _x = ball.PosX;
            }
            else if (e.PropertyName == "PosY")
            {
                _y = ball.PosY;
            }
        }

        public override void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }



}
