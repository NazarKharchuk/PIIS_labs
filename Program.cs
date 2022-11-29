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
            Console.WriteLine("\t\tLab 5");

            List<List<double>> table = new List<List<double>>();
            double[] col1 = { 0, 0, 4, 1, 6 };
            double[] col2 = { 0, -2, 1, 0, 2 };
            double[] col3 = { 2, 4, -3, 1, -3 };
            double[] col4 = { 3, 5, -1, 5, 7 };
            double[] col5 = { 5, -2, 2, -1, 0 };
            table.Add(col1.ToList());
            table.Add(col2.ToList());
            table.Add(col3.ToList());
            table.Add(col4.ToList());
            table.Add(col5.ToList());

            SimplexMethod method = new SimplexMethod(table);
            if (method.simplex_method())
            {
                Console.Write("\n\tResult\nBasis plan: ");
                foreach (double item in method.basis_plan)
                {
                    Console.Write($"{item}; ");
                }
                Console.WriteLine("\nMinimum: " + method.min_value);
            }
            Console.ReadLine();
        }
    }
}
