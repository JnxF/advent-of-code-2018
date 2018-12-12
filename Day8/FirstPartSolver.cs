using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day8
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
            _input = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";
            var numbers = _input.Split(" ").Select(number => int.Parse(number)).ToArray();
            Tree t = GenerateTree(numbers);
            return SumMetadata(t);
        }

        private Tree GenerateTree(int[] numbers)
        {
            int numberSubtrees = numbers[0];
            int numberMetadata = numbers[1];

            int[] subtreesNumbers = numbers.SkipLast(numberMetadata).Skip(2).ToArray();

            var Tree = new Tree
            {
                Metadata = numbers.Skip(numbers.Length - numberMetadata),
                Subtrees = ExtractNTrees(subtreesNumbers, numberSubtrees)
            };

            return Tree;
        }

        private List<Tree> ExtractNTrees(int[] numbers, int numberSubtrees)
        {
            if (numberSubtrees == 0)
            {
                return new List<Tree>();
            }

            int numChilds = numbers[0];
            int numMetadata = numbers[1];

            // If no childs
            if (numChilds == 0)
            {
                int subsequenceSize = 2 + numMetadata;

                List<Tree> res = new List<Tree>
                {
                    GenerateTree(numbers.Take(subsequenceSize).ToArray())
                };

                res.AddRange(ExtractNTrees(numbers.Skip(subsequenceSize).ToArray(), numberSubtrees - 1));
                return res;
            }

            else
            {
                return new List<Tree>();
            }
        }

        public int SumMetadata(Tree t)
        {
            if (t == null) return 0;
            return t.Metadata.Sum() + t.Subtrees.Select(sbt => SumMetadata(sbt)).Sum();
        }
    }
}
