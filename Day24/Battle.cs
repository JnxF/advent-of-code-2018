using System;
using System.Collections.Generic;
using System.Linq;

namespace Day24
{
    class Battle : ICloneable
    {
        public List<Group> System { get; }
        public List<Group> Infect { get; }

        public bool HasAliveUnits => System.Count != 0 && Infect.Count != 0;
        public int SystemUnits => System.Select(i => i.Units).Sum();
        public int InfectUnits => Infect.Select(i => i.Units).Sum();

        public Battle(List<Group> system, List<Group> infect)
        {
            System = system;
            Infect = infect;
        }

        public void Fight()
        {
            // PART 1. Target selection

            // Clear objectives
            foreach (var groups in System.Union(Infect))
            {
                groups.AttackObjective = null;
                groups.IsTargeted = false;
            }
            // System select their targets
            SelectTargets(System, Infect);

            // Infect select their targets
            SelectTargets(Infect, System);

            // PART 2. Attacking
            foreach (var group in System.Union(Infect).OrderByDescending(g => g.Initiative))
            {
                group.Attack();
            }

            // Clean
            System.Where(i => i.Units == 0).ToList().ForEach(i => System.Remove(i));
            Infect.Where(i => i.Units == 0).ToList().ForEach(i => Infect.Remove(i));
        }

        private void SelectTargets(IList<Group> attacker, IList<Group> defender)
        {
            foreach (var s in attacker.OrderByDescending(i => i.EffectivePower).ThenByDescending(i => i.Initiative))
            {
                var possibles = defender
                    .Where(i => i.IsTargeted == false)
                    .OrderByDescending(i => i.HowMuchWouldYouHitMe(s))
                    .ThenByDescending(i => i.EffectivePower)
                    .ThenByDescending(i => i.Initiative);

                if (possibles.Any())
                {
                    var bestToAttack = possibles.First();
                    if (bestToAttack.HowMuchWouldYouHitMe(s) != 0)
                    {
                        s.AttackObjective = bestToAttack;
                        bestToAttack.IsTargeted = true;
                    }
                }
            }
        }

        public void BostSystem(int boost)
        {
            System.ForEach(s => s.Boost(boost));
        }

        public object Clone()
        {
            var newSystem = new List<Group>();
            var newInfect = new List<Group>();
            System.ToList().ForEach(o => newSystem.Add((Group)o.Clone()));
            Infect.ToList().ForEach(o => newInfect.Add((Group)o.Clone()));
            return new Battle(newSystem, newInfect);
        }
    }
}