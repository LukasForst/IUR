using System;

namespace HW2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var model = new Model();
            var presenter = new Presenter(model);
            var consoleView = new ConsoleView(presenter);

            Console.WriteLine(
                "Commands:\n" +
                "setLanguage(en)\t- set language to English\n" +
                "setLanguage(cs)\t- set language to Czech\n" +
                "setUnits(C)\t- set temperature unit to Celsius\n" +
                "setUnits(F)\t- set temperature unit to Fahrenheit" +
                "x\t- exit program");

            while (true)
            {
                consoleView.UserInput(Console.ReadLine());
            }
        }
    }
}