using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IUR_p05.Support;

namespace IUR_p05.ViewModels
{
    internal class AlarmManagerViewModel : ViewModelBase
    {
        public ObservableCollection<AlarmItemViewModel> AlarmList { get; set; } = new ObservableCollection<AlarmItemViewModel>();

        public AlarmManagerViewModel()
        {
            AlarmList.Add(new AlarmItemViewModel {AlarmName = "My awesome alarm"});
        }
    }
}