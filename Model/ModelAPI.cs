using Logic;
using System.Collections.ObjectModel;
using System.Threading;

namespace Model
{
    internal class ModelAPI : ModelAbstractAPI
    {
        private LogicAbstractAPI _logicAPI;
        private ObservableCollection<ICircle> Circles = new ObservableCollection<ICircle>();
        private int _radius;

        public ModelAPI()
        {
            _logicAPI = LogicAbstractAPI.CreateAPIInstance();  
        }


        public override ObservableCollection<ICircle> GetCircles()
        {
            Circles.Clear();
            foreach(IBall ball in _logicAPI.GetAllBalls())
            {
                ICircle c = ICircle.CreateCircle((int)ball.PosX, (int)ball.PosY, _radius);
                Circles.Add(c);                                  //Ponizej dodajemy metode, ktora bedzie wywolywana za kazdym razem, gdy ball zglosi PropertyChanged
                ball.PropertyChanged += c.UpdateCircle!;         //wykrzyknik nie jest konieczny, to tylko mowi kompilatorowi ze metoda UpdateCircle nie bedzie NULLem
            }
            return Circles;
        }


        public override void ClearBalls()
        {
            _logicAPI.ClearBoard();
        }

        public override void Start(int BallsAmount, int Radius)
        {
            _radius = Radius;
            _logicAPI.AddBalls(BallsAmount, Radius); 
        }


    }
}
