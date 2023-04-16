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
        public override int x { get => _x; set { _x = value; RaisePropertyChanged(); } }
        public override int y { get => _y; set { _y = value; RaisePropertyChanged(); } }

        public override int radius { get => _radius; set { _radius = value; RaisePropertyChanged(); } }

        private int _x { get; set; }
        private int _y { get; set; }
        private int _radius { get; set; }

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
