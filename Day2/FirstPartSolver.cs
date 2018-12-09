using Helpers;
using System.Collections.Generic;

namespace Day2
{
    class FirstPartSolver : ISolver<int>
    {
        private readonly string _input;
        public FirstPartSolver(string input)
        {
            _input = input;
        }

        private static void Analyze(string s, out int twoInWord, out int threeInWord)
        {
            var lettersToCount = new Dictionary<char, int>();
            foreach (var letter in s)
            {
                if (lettersToCount.ContainsKey(letter))
                {
                    lettersToCount[letter] += 1;
                } else
                {
                    lettersToCount.Add(letter, 1);
                }
            }

            twoInWord = 0;
            threeInWord = 0;
            foreach (var item in lettersToCount)
            {
                if (item.Value == 2) twoInWord = 1;
                if (item.Value == 3) threeInWord = 1;
            }
        }

        public int Solve()
        {
            var words = _input.Split("\n");
            int howManyTwo = 0;
            int howManyThree = 0;
            foreach (string word in words)
            {
                Analyze(word, out var twoInWord, out var threeInWord);
                howManyTwo += twoInWord;
                howManyThree += threeInWord;
            }
            return howManyTwo * howManyThree;
        }
    }
}
