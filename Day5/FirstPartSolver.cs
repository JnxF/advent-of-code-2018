using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day5
{
    class FirstPartSolver : ISolver<int>
    {
        private string _input;

        public FirstPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            return MaxReduce(_input).Count();
        }

        string MaxReduce(string s)
        {
            string reduction;
            while (true)
            {
                reduction = Reduce(s);
                if (reduction == s)
                {
                    break;
                }
                s = reduction;
            }
            return s;
        }

        string Reduce(string s)
        {
            for (char c = 'a'; c <= 'z'; ++c)
            {
                s = s.Replace(new string(new[] { c, char.ToUpper(c) }), "");
                s = s.Replace(new string(new[] { char.ToUpper(c), c }), "");
            }
            return s;
        }
    }
}
