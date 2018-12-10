using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day10
{
    class SecondPartSolver : ISolver<string>
    {
        private string _input;

        public SecondPartSolver(string input)
        {
            _input = input;
        }

        public string Solve() => "The answer is the number of iterations of the previous exercise";
    }
}
