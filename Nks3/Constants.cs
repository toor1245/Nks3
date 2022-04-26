namespace Nks3
{
    public static class Constants
    {
        public static readonly int[,] Schema =
        {
            {0, 0, 1, 1, 0, 0, 0, 0},
            {0, 0, 1, 0, 1, 0, 0, 0},
            {0, 0, 0, 1, 1, 0, 0, 0},
            {0, 0, 0, 0, 0, 1, 1, 0},
            {0, 0, 0, 0, 0, 1, 0, 1},
            {0, 0, 0, 0, 0, 0, 1, 1},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0}
        };

        public static readonly double[] Probabilities = {0.88, 0.14, 0.93, 0.04, 0.98, 0.56, 0.59, 0.64};
        public static readonly int[] InputIndexes = {0, 1};
        public static readonly int[] OutputIndexes = {6, 7};
        public static readonly int[] Multiplicities = {3, 2};
        public const int Hours = 2167;
    }
}