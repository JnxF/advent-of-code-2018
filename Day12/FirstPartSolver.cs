using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace Day12
{
    class FirstPartSolver : ISolver<int>
    {
        private string _input;
        private string _initialState = "##.##..#.#....#.##...###.##.#.#..###.#....##.###.#..###...#.##.#...#.#####.###.##..#######.####..#";
        private string _rules =
@".##.. => #
#...# => .
####. => #
##..# => #
..##. => .
.###. => .
..#.# => .
##### => .
##.#. => #
...## => #
.#.#. => .
##.## => #
#.##. => .
#.... => .
#..## => .
..#.. => #
.#..# => #
.#.## => #
...#. => .
.#... => #
###.# => #
#..#. => #
.#### => #
#.### => #
.##.# => #
#.#.. => .
###.. => #
..... => .
##... => .
....# => .
#.#.# => #
..### => #";

        public FirstPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            var _rulesList = _rules.Split("\n").Select(rule => (rule.Substring(0, 5), rule[9]));

            _initialState = new string('.', 2000) + _initialState + new string('.', 2000);

            int n = _initialState.Length;
            for (int i = 0; i <= 20; ++i)
            {
                Console.Write($"{i}: ");
                Console.WriteLine(_initialState);
                if (i == 20) break;
                _initialState = Iterate(_initialState, _rulesList);
            }


            int total = 0;
            for (int i = 0; i < _initialState.Length; ++i)
            {
                if (_initialState[i] == '#')
                {
                    total += i - 2000;
                }
            }
           
            return total;
        }

        private string Iterate(string initialState, IEnumerable<(string, char)> rulesList)
        {
            int n = initialState.Length;
            var res = new StringBuilder(new string('.', n));
            for (int i = 2; i < n - 2; ++i)
            {
                string sub = initialState.Substring(i - 2, 5);

                var existing = rulesList.Where(w => w.Item1 == sub && w.Item2 != '.');
                if (existing.Count() != 0)
                {
                    res[i] = '#';
                }

            }
            return res.ToString();

        }
    }
}
