using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day2
{
    class SecondPartSolver : ISolver<string>
    {
        private readonly string _input;
        public SecondPartSolver(string input)
        {
            _input = input;
        }

        private static int Distance(string s1, string s2)
        {
            var pairLetters = s1.Zip(s2, (c1, c2) => (c1, c2));
            int distance = 0;
            foreach (var (letter_s1, letter_s2) in pairLetters)
            {
                if (letter_s1 != letter_s2)
                {
                    ++distance;
                }
                if (distance > 1)
                {
                    return int.MaxValue;
                }
            }
            return distance;
        }

        public string Solve()
        {
            var words = _input.Split("\n");
            (string s1, string s2) = FindCloseWords(words);

            var stringBuilder = new StringBuilder();
            var pairLetters = s1.Zip(s2, (c1, c2) => (c1, c2));
            foreach (var (letter_s1, letter_s2) in pairLetters)
            {
                if (letter_s2 == letter_s1)
                {
                    stringBuilder.Append(s1);
                }
            }
            return stringBuilder.ToString();
        }

        private (string s1, string s2) FindCloseWords(string[] words)
        {
            int n = words.Length;
            for (int i = 0; i < n; ++i)
            {
                for (int j = i + 1; j < n; ++j)
                {
                    var distance = Distance(words[i], words[j]);
                    if (distance == 1)
                    {
                        return (words[i], words[j]);
                    }
                }
            }
            return (default(string), default(string));
        }
    }
}
