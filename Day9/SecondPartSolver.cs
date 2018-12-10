using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day9
{
    class SecondPartSolver: ISolver<ulong>
    {
        private const int NumPlayers = 459;
        private const int NumRounds = 71790 * 100;

        public ulong Solve()
        {
            Dictionary<int, ulong> points = new Dictionary<int, ulong>();
            LinkedList<int> circle = new LinkedList<int>();
            circle.AddLast(0);

            LinkedListNode<int> listNode = circle.First;

            for (int round = 1; round <= NumRounds; ++round)
            {
                // Special ball
                if (round % 23 == 0)
                {
                    for (int i = 0; i < 7; ++i)
                        listNode = listNode == circle.First ? circle.Last : listNode.Previous;
                    int valor = listNode.Value;

                    listNode = listNode.Next == null ? circle.First : listNode.Next;

                    circle.Remove(listNode.Previous == null ? circle.Last : listNode.Previous);

                    int player = round % NumPlayers;
                    if (!points.ContainsKey(player))
                    {
                        points.Add(player, 0);
                    }
                    points[player] += (ulong)(valor + round);

                }
                else
                {
                    listNode = listNode.Next == null ? circle.First : listNode.Next;
                    circle.AddAfter(listNode, round);
                    listNode = listNode.Next == null ? circle.First : listNode.Next;
                }

                /*
                Console.Write($"[{round%NumPlayers}] ");
                var en = circle.First;
                while (en != null)
                {
                    if (en == listNode) Console.Write("*");
                    Console.Write(en.Value);
                    if (en == listNode) Console.Write("*");
                    Console.Write(" ");
                    en = en.Next;
                }
                Console.WriteLine();
                */
            }

            return points.Select(t => t.Value).Max();
        }
    }
}
