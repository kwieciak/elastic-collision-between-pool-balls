using Model;
using System;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ModelAbstractAPI _modelAPI;

        public ObservableCollection<ICircle> Circles => _modelAPI.GetCircles();
        public RelayCommand Start { get; }

        public RelayCommand Stop { get; }

        private String _BallsAmount = "";

        private int _ballRadius = 10;

        public String BallsAmount
        {
            get => _BallsAmount;
            set
            {
                _BallsAmount = value;
                RaisePropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            _modelAPI = ModelAbstractAPI.CreateAPIInstance();
            Start = new RelayCommand(StartProcess);
            Stop = new RelayCommand(StopProcess);

        }

        public void StartProcess()
        {
            int BallsAmountInt = int.Parse(BallsAmount);
            _modelAPI.Start(BallsAmountInt,_ballRadius);
            RaisePropertyChanged("Circles");
        }

        public void StopProcess() 
        {
            _modelAPI.ClearBalls();
            RaisePropertyChanged("Circles");
        }

       

    }
}
