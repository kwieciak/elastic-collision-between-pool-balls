using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Model
{
    /* Ta klasa to Ball, ale dla Modelu
     * Jak widzisz tutaj tez jest EventHandler i RaisePropertyChanged() i zrobilem to z mysla o ViewModel, bo zakladam ze on tez czegos takiego bedzie potrzebowal (?)
     * UWAGA na metode UpdateCircle() bo potrafi niezle bagno zrobic
     * 
     */
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

        /* Jak juz wyzej pisalem, jest to dosyc problematyczna metoda.
         * Chyba to jest wywolywane, gdy jakis ball zmieni swoja PosX badz PosY (tzn. tak na 90% tak, ale no, sa to dla mnie nieco magiczne metody
         * 
         * Keep in mind:
         *      Problem pojawia sie gdy:
         *          - PosX nie ma okreslonego gettera 
         *          - PosX nie ma okreslonego settera 
         *      Badz po prostu nie ma do tych rzeczy dostepu.
         *      IDE z jakiegos powodu nie wypluwa bledu w takiej sytuacji
         *      i powoduje to crash procesu (odwolujemy sie do pamieci do ktorej nie mamy dostepu)
         */
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
