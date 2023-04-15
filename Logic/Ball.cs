using System;
using System.Collections.Generic;
using System.Text;


// DOBRE PYTANIE CZY TA KLASA POWINNA BYĆ W LOGIC CZY DATA NA TEN MOMENT
namespace Logic
{
    internal class Ball
    {
        //mozna sie zastanowic czy int czy float
        private float posX;
        private float posY;
        private float radius;

        private float speedX;
        private float speedY;

        

        public Ball(float posX, float posY, float radius)   // nie wiem czy potrzebujemy tutaj od razu podawac predkosc
        {                                                   // czy jednak powinniśmy dopiero potem to robic
            this.posX = posX;
            this.posY = posY;
            this.radius = radius;
        }

        public void moveBall()
        {
            this.posX += speedX;
            this.posY += speedY;
        }


        // tutaj sa old-fashioned gettery i settery, sa zgrabniejsze i szybsze sposoby napisania tego
        // ale nie wiem w sumie z ktorych jest lepiej korzystac
        // https://codeeasy.io/lesson/properties
        // wyzej masz link do tego jak mozna je zrobic i jak sie one zachowuja (SPOILER: zachowuja sie dziwnie)
        // PS klasa Board ma ten system, mozesz sobie porownac

        public float getPosX(){ return this.posX;}

        public void setPosX(float posX){ this.posX=posX; }

        public float getPosY() { return this.posY; }

        public void setPosY(float posY){ this.posY=posY; }

        public float getRadius() { return this.radius;}

        //public void setRadius(float radius) { this.radius = radius;}      // IDK CZY TO POTRZEBNE

        public float getSpeedX() { return this.speedX; }

        public void setSpeedX(float speedX) {  this.speedX=speedX; }

    }
}
