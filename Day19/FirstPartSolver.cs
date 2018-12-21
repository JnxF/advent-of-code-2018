using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Day19
{
    class FirstPartSolver : ISolver<int>
    {
        private readonly int registryPC = 1;
        private readonly string _input;

        private Assembly currentAssembly => GetType().Assembly;
        private IEnumerable<TypeInfo> Operations => currentAssembly
            .DefinedTypes
            .Where(type => type.ImplementedInterfaces.Any(inter => inter == typeof(Operation)));

        public FirstPartSolver(string input1)
        {
            _input = input1;
        }

        public int Solve()
        {
            var instructions = Parse(_input);
            int[] p = new int[] { 0, 0, 0, 0, 0, 0 };
            int pc = 0;
            while (pc >= 0 && pc < instructions.Count)
            {
                (var op, var param) = instructions[pc];
                op.Execute(p, param[0], param[1], param[2]);
                pc = ++p[registryPC];
            }
            return p[0];
        }

        private IList<(Operation, int[])> Parse(string input)
        {
            bool notEmpty(string n) => n != "";
            var lines = input.Replace("\r", "").Split("\n");
            
            var res = lines.Select(l => (
                Activator.CreateInstance(Operations.Where(o => o.Name == new string(l.TakeWhile(c => c != ' ').ToArray())).FirstOrDefault() ) as Operation,
                Regex.Split(l, @"\D+").Where(notEmpty).Select(n => int.Parse(n)).ToArray()
            ));
            return res.ToList();
        }
    }
}
