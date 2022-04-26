using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nks3
{
    public class SchemaLab2
    {
        protected readonly int[,] _schema;
        protected readonly double[] _probabilities;
        protected readonly int[] _input;
        protected readonly int[] _output;
        protected internal double _pSystem;
        private List<int[]> _workableStates;
        private List<double> _pStates;


        public SchemaLab2(int[,] schema, double[] p, int[] input, int[] output)
        {
            _schema = schema;
            _input = input;
            _output = output;
            _probabilities = p;
        }

        public void EvaluatePSystem()
        {
            EvaluateWorkable();
            GetPStates();
            _pSystem = _pStates.Sum();
        }

        private string GetWorkableStates()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Workable states (state - p)");
            for (var i = 0; i < _workableStates.Count; i++)
            {
                for (var j = 0; j < _workableStates[i].Length; j++)
                {
                    stringBuilder.Append(_workableStates[i][j]).Append(' ');
                }

                stringBuilder.Append(" P(state) = ").Append(_pStates[i]);
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        private string GetPSystem()
        {
            return "P(system) = " + _pSystem;
        }

        private string GetSchema()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Schema");
            
            Console.WriteLine("Schema");
            for (var i = 0; i < _schema.GetLength(0); i++)
            {
                for (var j = 0; j < _schema.GetLength(1); j++)
                {
                    stringBuilder.Append(_schema[i, j]);
                }

                stringBuilder.Append(" P(").Append(i + 1).Append(") = ").Append(_probabilities[i]);
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        private int[,] GetStates()
        {
            int[,] states = new int[_schema.GetLength(0), (int) Math.Pow(2, _schema.GetLength(0))];
            for (var i = 0; i < states.GetLength(0); i++)
            {
                var a = GetPeriod(i + 1);
                var isInvert = true;
                for (int j = 0, k = 0; j < states.GetLength(1); j++, k++)
                {
                    if (k == a)
                    {
                        isInvert = !isInvert;
                        k = 0;
                    }

                    states[i, j] = isInvert ? 0 : 1;
                }
            }

            return states;
        }

        private void EvaluateWorkable()
        {
            _workableStates = new List<int[]>();
            int[,] states = GetStates();
            for (var i = 0; i < states.GetLength(1); i++)
            {
                int[] state = new int[states.GetLength(0)];
                for (var j = 0; j < state.Length; j++)
                {
                    state[j] = states[j, i];
                }

                if (IsWork(state))
                {
                    _workableStates.Add(state);
                }
            }
        }

        private void GetPStates()
        {
            _pStates = _workableStates.Select(GetPState).ToList();
        }

        private double GetPState(IReadOnlyList<int> state)
        {
            double p_state = 1;
            for (var i = 0; i < state.Count; i++)
            {
                p_state *= state[i] == 0 ? 1.0 - _probabilities[i] : _probabilities[i];
            }

            return p_state;
        }

        private bool IsWork(IReadOnlyList<int> states) =>
            _input.Any(k => _output.Select(i => GetPath(k, i, states, new List<int>())).Any(path => path.Count > 0));

        private List<int> GetPath(int from, int to, IReadOnlyList<int> states, ICollection<int> prev)
        {
            List<int> path = new();

            if (states[from] == 0 || states[to] == 0)
            {
                return path;
            }

            if (_schema[from, to] == 1)
            {
                path.Add(from);
                path.Add(to);
                return path;
            }

            for (var i = 0; i < _schema.GetLength(1); i++)
            {
                if (_schema[from, i] != 1 || states[i] != 1 || prev.Contains(i)) continue;
                List<int> nPrev = new(prev) {i};
                List<int> p = GetPath(i, to, states, nPrev);
                if (p.Count <= 0) continue;
                path.Add(from);
                path.AddRange(p);
                return path;
            }

            return path;
        }

        private static int GetPeriod(int column)
        {
            return (int) Math.Pow(2, column) / 2;
        }

        public override string ToString()
        {
            return GetSchema() + GetWorkableStates() + GetPSystem();
        }
    }
}