using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoVirus
{
    static class DistanceCalculator
    {
        public static double CalculateDistance(Cell cell1, Cell cell2)
        {
            int x = cell1.X - cell2.X;
            int y = cell1.Y - cell2.Y;
            int z = cell1.Z - cell2.Z;

            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        }
    }
}
