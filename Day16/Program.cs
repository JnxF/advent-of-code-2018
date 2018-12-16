using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            var input1 = Properties.Resources.input1;
            var input2 = Properties.Resources.input2;

            var FirstPartSolver = new FirstPartSolver(input1);
            Console.WriteLine(FirstPartSolver.Solve());

            var SecondPartSolver = new SecondPartSolver(input1, input2);
            Console.WriteLine(SecondPartSolver.Solve());

            Console.Read();
        }
    }
}
