using System;

namespace lab2
{
    public class Controller
    {
        private readonly Model model;

        public Controller(Model model)
        {
            this.model = model;
        }

        public void ParseCommand(string command)
        {
            if (command == "x")
            {
                Environment.Exit(0);
            }

            model.City = command;
        }
    }
}