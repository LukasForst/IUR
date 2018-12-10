using ContactListTP.ViewModel;

namespace ContactListTP.Configuration
{
    public class ViewModelLocator
    {
        public MyViewModel MyViewModel => IocKernel.Get<MyViewModel>();
    }
}