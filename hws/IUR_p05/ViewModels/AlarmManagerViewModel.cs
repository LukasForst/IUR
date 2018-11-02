using System.Collections.ObjectModel;
using System.Linq;
using IUR_p05.Support;

namespace IUR_p05.ViewModels
{
    public class AlarmManagerViewModel : ViewModelBase
    {
        private const string DefaultAlarmName = "Alarm";

        public ObservableCollection<AlarmItemViewModel> AlarmList { get; set; } = new ObservableCollection<AlarmItemViewModel>();

        public RelayCommand AddCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        public AlarmManagerViewModel()
        {
            DeleteCommand = new RelayCommand(
                x => AlarmList.Remove(SelectedAlarmDetail),
                x => AlarmList.Any() && SelectedAlarmDetail != null
            );

            AddCommand = new RelayCommand(
                x => AlarmList.Add(new AlarmItemViewModel
                    {
                        AlarmName = $"{DefaultAlarmName} {AlarmList.Count + 1}",
                        Type = AlarmType.Min
                    }
                ),
                x => AlarmList.Count < 10
            );
        }

        private int _selectedAlarmIndex;
        public int SelectedAlarmIndex
        {
            get => _selectedAlarmIndex;
            set
            {
                _selectedAlarmIndex = value;
                OnPropertyChanged(nameof(SelectedAlarmIndex));
            }
        }

        private AlarmItemViewModel _selectedAlarmDetail;
        public AlarmItemViewModel SelectedAlarmDetail
        {
            get => _selectedAlarmDetail;
            set
            {
                _selectedAlarmDetail = value;
                OnPropertyChanged(nameof(SelectedAlarmDetail));
            }
        }
    }
}