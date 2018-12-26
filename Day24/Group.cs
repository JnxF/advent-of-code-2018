using System;
using System.Collections.Generic;
using System.Linq;

namespace Day24
{
    class Group : ICloneable
    {
        public int Units { get; private set; }
        public int HitPoints { get; }
        public int AttackDamage { get; private set; }
        public string AttackType { get; }
        public int Initiative { get; }
        public IEnumerable<string> Weaknesses { get; }
        public IEnumerable<string> Immunities { get; }

        public Group AttackObjective { get; set; }
        public bool IsTargeted { get; set; }

        public int EffectivePower => Units * AttackDamage;

        public Group(int units, int hitPoints, int attackDamage, string attackType, int initiative, IEnumerable<string> weaknesses, IEnumerable<string> immunities)
        {
            Units = units;
            HitPoints = hitPoints;
            AttackDamage = attackDamage;
            AttackType = attackType;
            Initiative = initiative;
            Weaknesses = weaknesses;
            Immunities = immunities;
        }

        public int HowMuchWouldYouHitMe(Group s)
        {
            if (Immunities.Contains(s.AttackType))
            {
                return 0;
            }
            else if (Weaknesses.Contains(s.AttackType))
            {
                return s.EffectivePower * 2;
            }
            {
                return s.EffectivePower;
            }
        }

        public override string ToString()
        {
            return $"Group containing {Units} units";
        }

        public void Attack()
        {
            if (AttackObjective == null) return;
            AttackObjective.Damage(AttackObjective.HowMuchWouldYouHitMe(this));
        }

        private void Damage(int damage)
        {
            Units -= damage / HitPoints;
            Units = Math.Max(0, Units);
        }

        internal void Boost(int x)
        {
            AttackDamage += x;
        }

        public object Clone()
        {
            return new Group(Units, HitPoints, AttackDamage, AttackType, Initiative, Weaknesses.ToList(), Immunities.ToList());
        }
    }
}
