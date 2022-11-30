using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Laba #6");

            double[] first_point = { 0, 1, 2};
            PolyhedronSearch method = new PolyhedronSearch(Function, 3, first_point, 100, 0.001, 1, 2.5, 0.5);
            Console.WriteLine(method.search_min(500));

            Console.ReadLine();
        }

        public static double Function(double[] x)
        {
            return (-4 * x[0] * Math.Pow(x[1], 2) + 2 * Math.Pow(x[0], 2) * x[1] - 3 * x[0] * x[1] * x[2] + 7 * Math.Pow(x[0], 2) * Math.Pow(x[2], 2));
        }
    }
}
