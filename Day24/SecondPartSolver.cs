using Helpers;

namespace Day24
{
    class SecondPartSolver : ISolver<int>
    {
        private readonly string _input;
        private const int MaxIters = 10000;

        public SecondPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            var originalBattle = InputParser.ParseInput(_input);
            for (int boost = 0; true; ++boost)
            {
                var battle = (Battle)originalBattle.Clone();
                battle.BostSystem(boost);

                for (int it = 0; battle.HasAliveUnits && it < MaxIters; ++it)
                {
                    battle.Fight();
                }

                if (battle.InfectUnits == 0)
                {
                    return battle.SystemUnits;
                }
            }
        }
    }
}