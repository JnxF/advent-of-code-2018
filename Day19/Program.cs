using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day19
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Properties.Resources.input;

            var FirstPartSolver = new FirstPartSolver(input);
            Console.WriteLine(FirstPartSolver.Solve());

            var SecondPartSolver = new SecondPartSolver(input);
            Console.WriteLine(SecondPartSolver.Solve());

            Console.Read();
        }
    }
}
