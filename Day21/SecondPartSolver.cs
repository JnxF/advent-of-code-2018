using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day21
{
    class SecondPartSolver : ISolver<ulong>
    {
        private readonly int registryPC = 1;
        private readonly string _input;

        private Assembly currentAssembly => GetType().Assembly;
        private IEnumerable<TypeInfo> Operations => currentAssembly
            .DefinedTypes
            .Where(type => type.ImplementedInterfaces.Any(inter => inter == typeof(IOperation)));

        public SecondPartSolver(string input1)
        {
            _input = input1;
        }

        public ulong Solve()
        {
            var instructions = Parse(_input);

            ulong V = 0;
            Parallel.For(3115807, 500000000, valor =>
            {
                if (V != 0 || V == 3115806) return;
                ulong[] p = new ulong[] { (ulong)valor, 0, 0, 0, 0, 0 };
                int pc = 0;
                int instruccionesGastadas = 0;
                while (pc >= 0 && pc < instructions.Count)
                {
                    (var op, var param) = instructions[pc];
                    op.Execute(p, param[0], param[1], param[2]);
                    pc = (int)++p[registryPC];
                    instruccionesGastadas++;
                    if (instruccionesGastadas == 5000) break;
                }

                if (instruccionesGastadas != 5000)
                {
                    V = (ulong)valor;
                }
            });

            return V;
        }

        private IList<(IOperation, ulong[])> Parse(string input)
        {
            bool notEmpty(string n) => n != "";
            var lines = input.Replace("\r", "").Split("\n");

            var res = lines.Select(l => (
                Activator.CreateInstance(Operations.Where(o => o.Name == new string(l.TakeWhile(c => c != ' ').ToArray())).FirstOrDefault()) as IOperation,
                Regex.Split(l, @"\D+").Where(notEmpty).Select(n => Convert.ToUInt64(n)).ToArray()
            ));
            return res.ToList();
        }
    }
}
