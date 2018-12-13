using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var FirstPartSolver = new FirstPartSolver(Properties.Resources.input);
            Console.WriteLine(FirstPartSolver.Solve());

            var SecondPartSolver = new SecondPartSolver(Properties.Resources.input);
            Console.WriteLine(SecondPartSolver.Solve());

            Console.Read();
        }
    }
}
