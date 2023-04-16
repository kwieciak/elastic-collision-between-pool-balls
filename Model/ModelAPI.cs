using Logic;
using System.Collections.ObjectModel;
using System.Threading;

namespace Model
{
    internal class ModelAPI : ModelAbstractAPI
    {
        private LogicAbstractAPI _logicAPI;
        private ObservableCollection<ICircle> Circles = new ObservableCollection<ICircle>();

        public ModelAPI()
        {
            _logicAPI = LogicAbstractAPI.CreateAPIInstance();
        }


        public override ObservableCollection<ICircle> GetCircles()
        {
            Circles.Clear();
            foreach(IBall ball in _logicAPI.GetAllBalls())
            {
                ICircle c = ICircle.CreateCircle(ball.PosX, ball.PosY, ball.Radius);
                Circles.Add(c);
                ball.PropertyChanged += c.UpdateCircle!;
            }
            return Circles;
        }


        public override void ClearBalls()
        {
            _logicAPI.ClearBoard();
        }

        public override void Start()
        {
            _logicAPI.AddBalls(5, 2); //zmienic na przekayzwanie
            _logicAPI.StartMovement();
        }


    }
}