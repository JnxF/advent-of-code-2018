using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Day14
{
    class SecondPartSolver : ISolver<ulong>
    {
        private static readonly int _input = 157901;
        private static readonly int[] inputChars = new int[] { 1, 5, 7, 9, 0, 1 };

        public ulong Solve()
        {
            List<int> list = new List<int> { 3, 7 };

            int pointer1 = 0;
            int pointer2 = 1;
            int[] window = new int[6];
            int seeing = 5;
            // Display(list, pointer1, pointer2);

            for (int iteration = 0; true; ++iteration)
            {
                Evolve(list, ref pointer1, ref pointer2);
                // Display(list, pointer1, pointer2);

                if (iteration == 6)
                {
                    for (int i = 0; i < 6; ++i)
                    {
                        window[i] = list[i];
                    }
                }

                if (iteration > 6)
                {
                    do
                    {
                        for (int i = 0; i < 5; ++i)
                        {
                            window[i] = window[i + 1];
                        }
                        ++seeing;
                        window[5] = list[seeing];

                        // Console.WriteLine(window.Select(i => i.ToString()).Aggregate((a, b) => a + b));

                        bool ok = true;
                        for (int i = 0; i < 6 && ok; ++i)
                        {
                            ok = inputChars[i] == window[i];
                        }
                        if (ok) return (ulong)(seeing - 5);

                    } while (seeing != list.Count - 1);
                }
            }
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
