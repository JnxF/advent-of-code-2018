using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var FirstPartSolver = new FirstPartSolver(Properties.Resources.input.Trim());
            Console.WriteLine(FirstPartSolver.Solve());

            var SecondPartSolver = new SecondPartSolver(Properties.Resources.input.Trim());
            Console.WriteLine(SecondPartSolver.Solve());

            Console.Read();
        }
    }
}
