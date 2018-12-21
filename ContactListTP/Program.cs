using System;
using ContactListTP.View;

namespace ContactListTP
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new App();
            app.Run();
        }
    }
}