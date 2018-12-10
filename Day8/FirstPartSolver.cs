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
        private Tree RootTree = new Tree();
        private int[] numbers;

        public FirstPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            numbers = _input.Split(" ").Select(i => int.Parse(i)).ToArray();
            BuildTree(0, RootTree, out var totalMetadata);
            return totalMetadata;
        }

        private int BuildTree(int x, Tree currentTree, out int totalMetadata)
        {
            int childNodesNumber = numbers[x];
            int metadataEntriesNumber = numbers[x + 1];

            totalMetadata = 0;

            int lastPointer = x+2;
            for (int i = 0; i < childNodesNumber; ++i)
            {
                Tree child = new Tree();
                currentTree.Subtrees.Add(child);
                lastPointer = BuildTree(x + 2, child, out var MiniMetadata);
                totalMetadata += MiniMetadata;
            }

            for (int i = lastPointer; i < lastPointer + metadataEntriesNumber; ++i)
            {
                totalMetadata += numbers[i];
            }
            
            return x + metadataEntriesNumber;
        }
    }
}
