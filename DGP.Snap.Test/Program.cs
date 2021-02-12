using System;

namespace DGP.Snap.Test
{
    internal class Program
    {
        private static void Main(string[] args) => Test();

        public static void Test()
        {
            // Attach to events of interest:
            FiddlerApplication.AfterSessionComplete += session => Console.WriteLine(session.fullUrl);
            // Start:
            FiddlerApplication.Startup(8888, true, false);
            Console.ReadLine();
            // Shutdown:
            FiddlerApplication.Shutdown();

        }
    }
}
