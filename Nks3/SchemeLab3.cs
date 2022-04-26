using System;

namespace Nks3
{
    public enum Mode
    {
        NotLoadedGeneralReserved = 1,
        LoadedGeneralReserved,
        NotLoadedSeparateReserved,
        LoadedSeparateReserved
    }
    
    public class SchemaLab3 : SchemaLab2
    {
        private int _hours;
        private double _qSystem;
        private int _tSystem;
        private double _qReservedSystem;
        private double _pReservedSystem;
        private int _tReservedSystem;
        private double _gQ;
        private double _gP;
        private double _gT;
        private Mode mode = 0;

        public SchemaLab3(int[,] schema, double[] p, int[] input, int[] output, int hours)
            :  base(schema, p, input, output)
        {
           
            _hours = hours;
        }

        public void EvaluateNotLoadedGeneral(int multiplicity)
        {
            EvaluatePSystem();
            _qSystem = 1.0 - _pSystem;
            _tSystem = (int) (-_hours / Math.Log(_pSystem));
            _qReservedSystem = _qSystem / Factorial(multiplicity + 1L);
            _pReservedSystem = 1.0 - _qReservedSystem;
            _tReservedSystem = (int) (-_hours / Math.Log(_pReservedSystem));
            _gQ = _qReservedSystem / _qSystem;
            _gP = _pReservedSystem / _pSystem;
            _gT = (double) _tReservedSystem / _tSystem;
            mode = Mode.NotLoadedGeneralReserved;
        }

        public void EvaluateLoadedGeneral(int multiplicity)
        {
            EvaluatePSystem();
            _qSystem = 1.0 - _pSystem;
            _tSystem = (int) (-_hours / Math.Log(_pSystem));
            _qReservedSystem = Math.Pow(_qSystem, multiplicity + 1L);
            _pReservedSystem = 1.0 - _qReservedSystem;
            _tReservedSystem = (int) (-_hours / Math.Log(_pReservedSystem));
            _gQ = _qReservedSystem / _qSystem;
            _gP = _pReservedSystem / _pSystem;
            _gT = (double) _tReservedSystem / _tSystem;
            mode = Mode.LoadedGeneralReserved;
        }

        public void EvaluateNotLoadedSeparate(int multiplicity)
        {
            EvaluatePSystem();
            _qSystem = 1.0 - _pSystem;
            _tSystem = (int) (-_hours / Math.Log(_pSystem));

            double[] q = new double[_probabilities.Length];
            double[] pReserved = new double[_probabilities.Length];
            double[] qReserved = new double[_probabilities.Length];
            
            var fact = Factorial(multiplicity + 1L);

            for (var i = 0; i < _probabilities.Length; i++)
            {
                q[i] = 1.0 - _probabilities[i];
                qReserved[i] = q[i] / fact;
                pReserved[i] = 1.0 - qReserved[i];
                Console.WriteLine($"Q {i + 1}r = {qReserved[i]}   P{i+1}r = {pReserved[i]}");
            }

            SchemaLab2 schemaReserved = new(_schema, pReserved, _input, _output);
            schemaReserved.EvaluatePSystem();

            _pReservedSystem = schemaReserved._pSystem;
            _qReservedSystem = 1.0 - _pReservedSystem;
            _tReservedSystem = (int) (-_hours / Math.Log(_pReservedSystem));
            _gQ = _qReservedSystem / _qSystem;
            _gP = _pReservedSystem / _pSystem;
            _gT = (double) _tReservedSystem / _tSystem;
            mode = Mode.NotLoadedSeparateReserved;
        }

        public void EvaluateLoadedSeparate(int multiplicity)
        {
            EvaluatePSystem();
            _qSystem = 1.0 - _pSystem;
            _tSystem = (int) (-_hours / Math.Log(_pSystem));

            double[] q = new double[_probabilities.Length];
            double[] pReserved = new double[_probabilities.Length];
            double[] qReserved = new double[_probabilities.Length];

            for (var i = 0; i < _probabilities.Length; i++)
            {
                q[i] = 1.0 - _probabilities[i];
                qReserved[i] = Math.Pow(q[i], multiplicity + 1.0);
                pReserved[i] = 1.0 - qReserved[i];
                Console.WriteLine($"Q {i + 1}r = {qReserved[i]}   P{i+1}r = {pReserved[i]}");
            }

            SchemaLab2 schemaReserved = new(_schema, pReserved, _input, _output);
            schemaReserved.EvaluatePSystem();

            _pReservedSystem = schemaReserved._pSystem;
            _qReservedSystem = 1.0 - _pReservedSystem;
            _tReservedSystem = (int) (-_hours / Math.Log(_pReservedSystem));
            _gQ = _qReservedSystem / _qSystem;
            _gP = _pReservedSystem / _pSystem;
            _gT = (double) (_tReservedSystem) / _tSystem;
            mode = Mode.LoadedSeparateReserved;
        }


        public void ShowResult()
        {
            Console.WriteLine("Mode: " + mode);
            Console.WriteLine("Psystem(" + _hours + ") = " + _pSystem);
            Console.WriteLine("Qsystem(" + _hours + ") = " + _qSystem);
            Console.WriteLine("Tsystem = " + _tSystem);
            Console.WriteLine("PreservedSystem(" + _hours + ") = " + _pReservedSystem);
            Console.WriteLine("QreservedSystem(" + _hours + ") = " + _qReservedSystem);
            Console.WriteLine("TreservedSystem = " + _tReservedSystem);
            Console.WriteLine("gQ = " + _gQ);
            Console.WriteLine("gP = " + _gP);
            Console.WriteLine("gT = " + _gT);
            Console.WriteLine();
        }

        public static long Factorial(long num)
        {
            long factorial = 1;
            
            for (var i = num; i > 0; i--)
                factorial *= i;

            return factorial;
        }
    }
}