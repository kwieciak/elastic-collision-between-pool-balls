using System;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateAPIInstance()
        {
            return new LogicAPI(); //tutaj pewnie bedzie trzeba przekazac jakies parametry
        }
        public abstract void AddBall(int x, int y, int radius); // dobre pytanie czy powinnismy w ogole ta funkcje robic
                                                                      // bo mozna po prostu w CreateBoard przekazywac liczbe kulek do utworzenia
                                                                      // i podawac ich promien, skoro potem i tak nie mozna wiecej kulek dodawac

        public abstract void MoveBall();

        public abstract void CreateBoard(int x, int y, int ballAmount); //ta funkcja chyba powinna tworzyc boarda i nowy thread
                                                        // i tam powinna sie w srodku jakas logika dziac, ze np.
                                                        // rusza tymi kulkami ktore sa w boardzie itd.


        // W INSTRUKCJI JEST COS O OKRESOWYM WYSYLANIU POLOZENIA KUL
        // nie wiem jak to interpretowac, jest opcja ze po prostu wysyla tablice z polozeniami wszystkich kul (i chyba to to)
        // moze wysyla po prostu info na temat jakiejs konkretnej kuli

        // zagwozdka jest taka, czy powinno sie wysylac obiekty kul, czy tylko x,y,radius jako zwykle floaty
        // imho to drugie



    }
}
