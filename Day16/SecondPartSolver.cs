using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Day16
{
    class SecondPartSolver : ISolver<int>
    {
        private readonly string _input1;
        private readonly string _input2;

        private Assembly currentAssembly => GetType().Assembly;
        private IEnumerable<TypeInfo> operations => currentAssembly
            .DefinedTypes
            .Where(type => type.ImplementedInterfaces.Any(inter => inter == typeof(Operation)));

        public SecondPartSolver(string input1, string input2)
        {
            _input1 = input1;
            _input2 = input2;
        }

        public int Solve()
        {
            var descriptions = InputParsers.ParseDescriptions(_input1);
            var mappings = SolveMappings(descriptions);
            var instructions = InputParsers.ParseInstructions(_input2);
            return Execute(instructions, mappings);
        }

        private int Execute(IEnumerable<int[]> instructions, IDictionary<int, TypeInfo> mappings)
        {
            var state = new[] { 0, 0, 0, 0 };

            foreach (var instruction in instructions)
            {
                var type = mappings[instruction[0]];
                var operation = Activator.CreateInstance(type) as Operation;
                var A = instruction[1];
                var B = instruction[2];
                var C = instruction[3];
                operation.Execute(state, A, B, C);
            }

            return state[0];
        }

        private IDictionary<int, TypeInfo> SolveMappings(HashSet<Description> descriptions)
        {
            var res = new Dictionary<int, TypeInfo>();
            var knownTypes = new HashSet<TypeInfo>();

            while (res.Count != operations.Count())
            {
                var descriptionsWithOnePossibleOperation = descriptions
                 .Select(d => (d.Opcode, DescriptionToPossibleOperations(d, knownTypes)))
                 .Where(par => par.Item2.Count == 1)
                 .Select(_ => (_.Opcode, _.Item2.First())).ToArray();

                foreach (var (opcode, operationType) in descriptionsWithOnePossibleOperation)
                {
                    res[opcode] = operationType;
                    knownTypes.Add(operationType);
                }
            }

            return res;
        }

        private HashSet<TypeInfo> DescriptionToPossibleOperations(Description id, HashSet<TypeInfo> knownTypes)
        {
            var currentAssembly = GetType().Assembly;
            var notKnownOperations = operations.Except(knownTypes);

            var res = new HashSet<TypeInfo>();
            foreach (var op in notKnownOperations)
            {
                var operation = Activator.CreateInstance(op) as Operation;
                var state = id.Before.ToArray();
                operation.Execute(state, id.Input1, id.Input2, id.Output);
                if (id.After.SequenceEqual(state))
                {
                    res.Add(op);
                }
            }
            return res;
        }
    }
}