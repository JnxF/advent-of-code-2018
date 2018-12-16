using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Day16
{
    class FirstPartSolver : ISolver<int>
    {
        private readonly string _input1;

        private Assembly currentAssembly => GetType().Assembly;
        private IEnumerable<TypeInfo> operations => currentAssembly
            .DefinedTypes
            .Where(type => type.ImplementedInterfaces.Any(inter => inter == typeof(Operation)));

        public FirstPartSolver(string input1)
        {
            _input1 = input1;
        }

        public int Solve()
        {
            return InputParsers.ParseDescriptions(_input1)
                .Where(i => HowManyPossibles(i) >= 3)
                .Count();
        }

        private int HowManyPossibles(Description id)
        {
            
            int res = 0;
            foreach (var op in operations)
            {
                var operation = Activator.CreateInstance(op) as Operation;
                var state = id.Before.ToArray();
                operation.Execute(state, id.Input1, id.Input2, id.Output);
                if (id.After.SequenceEqual(state))
                {
                    ++res;
                }
            }
            return res;
        }
    }
}
