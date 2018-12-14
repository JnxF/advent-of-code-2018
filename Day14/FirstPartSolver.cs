using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Day14
{
    class FirstPartSolver : ISolver<string>
    {
        private readonly int _input = 157901;

        public string Solve()
        {
            List<int> list = new List<int> { 3, 7 };

            int pointer1 = 0;
            int pointer2 = 1;

            // Display(list, pointer1, pointer2);
            while (list.Count < _input + 10)
            {
                Evolve(list, ref pointer1, ref pointer2);
                // Display(list, pointer1, pointer2);
            }

            return list.Skip(_input).Select(i => i.ToString()).Aggregate((s1, s2) => s1 + s2);
        }

        private void Evolve(List<int> list, ref int pointer1, ref int pointer2)
        {
            int newRecipe = list[pointer1] + list[pointer2];
            if (newRecipe >= 10)
            {
                list.Add(1);
            }
            list.Add(newRecipe % 10);
            pointer1 = (pointer1 + 1 + list[pointer1]) % list.Count;
            pointer2 = (pointer2 + 1 + list[pointer2]) % list.Count;
        }

        private void Display(List<int> list, int pointer1, int pointer2)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                if (i == pointer1 && i == pointer2)
                {
                    Console.Write("!" + list[i] + "!");
                }
                else if (i == pointer1)
                {
                    Console.Write("(" + list[i] + ")");
                }
                else if (i == pointer2)
                {
                    Console.Write("[" + list[i] + "]");
                }
                else
                {
                    Console.Write(" " + list[i] + " ");
                }
            }
            Console.WriteLine();
        }
    }
}
