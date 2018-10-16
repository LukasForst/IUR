using System;

namespace lab2
{
    internal class Program
    {   
        public static void Main(string[] args)
        {
            var model = new Model();
            var view = new View(model);
            var conroller = new Controller(model);
            
            while (true)
            {
                Console.WriteLine("x to exit: ");
                conroller.ParseCommand(Console.ReadLine());
            }
        }
    }
}