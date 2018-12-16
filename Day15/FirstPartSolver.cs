using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Day15
{
    class FirstPartSolver : ISolver<int>
    {
        private readonly string _input;
        private const char Wall = '#';
        private const char Empty = '.';
        private const char Goblin = 'G';
        private const char Elf = 'E';

        public FirstPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            throw new NotImplementedException();
        }
    }
}
