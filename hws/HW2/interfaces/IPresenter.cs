namespace HW2.interfaces
{
    public interface IPresenter
    {
        void HandleCommand(string command);

        void RegisterView(IView view);

        void DeregisterView(IView view);
    }
}