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
            double[] col1 = { 0, 0, 3, 4, 2 };
            double[] col2 = { 0, 2, 5, 3, 3 };
            double[] col3 = { 1, 2, 2, 2, 3 };
            double[] col4 = { 5, 3, 1, 0, 2 };
            table.Add(col1.ToList());
            table.Add(col2.ToList());
            table.Add(col3.ToList());
            table.Add(col4.ToList());

            SimplexMethod method = new SimplexMethod(table);
            if (method.simplex_method())
            {
                Console.Write("\n\tResult\nBasis plan: ");
                foreach (double item in method.basis_plan)
                {
                    Console.Write($"{item}; ");
                }
                Console.WriteLine("Minimum: " + method.min_value);
            }
            else
            {
                Console.WriteLine("No result(");
            }
            Console.ReadLine();
        }
    }
}
