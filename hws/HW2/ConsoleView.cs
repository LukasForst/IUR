using System;
using HW2.interfaces;

namespace HW2
{
    public class ConsoleView : IView
    {
        private readonly IPresenter presenter;

        public ConsoleView(IPresenter presenter)
        {
            this.presenter = presenter;
            this.presenter.RegisterView(this);
        }

        public void ShowResponse(string response) => Console.WriteLine(response);

        public void UserInput(string command) => presenter.HandleCommand(command);
    }
}