using Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Day1
{
    class FirstPartSolver : ISolver<int>
    {
        private readonly string _input;
        public FirstPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            return _input.Split("\n").Select(i => Cleaner.CleanNumberString(i)).Sum();
        }
    }
}
