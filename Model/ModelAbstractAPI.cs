using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Model
{
    public abstract class ModelAbstractAPI
    {
        public static ModelAbstractAPI CreateAPIInstance()
        {
            return new ModelAPI();
        }

        public abstract void Start();

        public abstract void ClearBalls();

        public abstract ObservableCollection<ICircle> GetCircles();








    }
}
