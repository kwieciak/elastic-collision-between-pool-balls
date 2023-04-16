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

        public MainWindowViewModel()
        {
            _modelAPI = ModelAbstractAPI.CreateAPIInstance();
            Start = new RelayCommand(StartProcess);
            Stop = new RelayCommand(StopProcess);

        }

        public void StartProcess()
        {
            _modelAPI.Start();
            RaisePropertyChanged("Circles");
        }

        public void StopProcess() 
        {
            _modelAPI.ClearBalls();
            RaisePropertyChanged("Circles");
        }

    }
}
