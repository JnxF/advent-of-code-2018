using System;

namespace Day22
{
    class Program
    {
        static void Main(string[] args)
        {
            var depth = 510;
            var targetX = 10;
            var targetY = 10;

            var FirstPartSolver = new FirstPartSolver(depth, targetX, targetY);
            Console.WriteLine(FirstPartSolver.Solve());

            var SecondPartSolver = new SecondPartSolver(depth, targetX, targetY);
            Console.WriteLine(SecondPartSolver.Solve());

            Console.Read();
        }
    }
}
