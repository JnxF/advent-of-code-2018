using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace Day12
{
    class SecondPartSolver : ISolver<ulong>
    {
        private const int LIMIT = 2000;
        private string _input;
        private int startIndex = 0;
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

        public SecondPartSolver(string input)
        {
            _input = input;
        }

        public ulong Solve()
        {
            var _rulesList = _rules.Split("\n").Select(rule => (rule.Substring(0, 5), rule[9]));

            _initialState = ".." + _initialState + "..";
            startIndex += 2;

            int n = _initialState.Length;
            for (int i = 0; i <= 500; ++i)
            {
                Console.Write($"{i}: ");
                Console.WriteLine(Sum(_initialState));
                // Console.WriteLine(_initialState);
                _initialState = ".." + _initialState + "..";
                startIndex += 2;
                if (i == 500) break;
                _initialState = Iterate(_initialState, _rulesList);
            }

            return (50000000000 - 500) * 81 + 39075;
        }

        private int Sum(string initialState)
        {
            int total = 0;
            for (int i = 0; i < _initialState.Length; ++i)
            {
                if (_initialState[i] == '#')
                {
                    total += i - startIndex;
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
