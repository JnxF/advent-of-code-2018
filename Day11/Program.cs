using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var FirstPartSolver = new FirstPartSolver();
            Console.WriteLine(FirstPartSolver.Solve());

            var SecondPartSolver = new SecondPartSolver();
            Console.WriteLine(SecondPartSolver.Solve());

            Console.Read();
        }
    }
}
