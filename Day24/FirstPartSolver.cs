using Helpers;
using System;

namespace Day24
{
    class FirstPartSolver : ISolver<int>
    {
        private readonly string _input;

        public FirstPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            var battle = InputParser.ParseInput(_input);
            while (battle.HasAliveUnits)
            {
                battle.Fight();
            }
            return Math.Max(battle.SystemUnits, battle.InfectUnits);
        }
    }
}