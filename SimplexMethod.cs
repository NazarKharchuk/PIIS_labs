using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    class SimplexMethod
    {
        private List<List<double>> simplex_table;
        public List<double> basis_plan;
        public double min_value;
        private int m;
        private int n;

        public SimplexMethod(List<List<double>> input_table)
        {
            simplex_table = new List<List<double>>();
            for(int i = 0; i < input_table.Count; i++)
            {
                simplex_table.Add(new List<double>());
                for (int j = 0; j < input_table[i].Count; j++)
                {
                    simplex_table[i].Add(input_table[i][j]);
                }
            }
            min_value = 0;
            m = input_table.Count - 2;
            n= m + input_table[0].Count - 2;
        }

        public bool simplex_method()
        {
            while (true)
            {
                Console.WriteLine($"{simplex_table[0][0]}\t{simplex_table[1][0]}\t{simplex_table[2][0]}\t{simplex_table[3][0]}\t");
                Console.WriteLine($"{simplex_table[0][1]}\t{simplex_table[1][1]}\t{simplex_table[2][1]}\t{simplex_table[3][1]}\t");
                Console.WriteLine($"{simplex_table[0][2]}\t{simplex_table[1][2]}\t{simplex_table[2][2]}\t{simplex_table[3][2]}\t");
                Console.WriteLine($"{simplex_table[0][3]}\t{simplex_table[1][3]}\t{simplex_table[2][3]}\t{simplex_table[3][3]}\t");
                Console.WriteLine($"{simplex_table[0][4]}\t{simplex_table[1][4]}\t{simplex_table[2][4]}\t{simplex_table[3][4]}\t");

                if (checking_optimal())
                {
                    save_result();
                    return true;
                }

                int column = find_column();
                if (column == -1) return false;
                Console.WriteLine("col - " + column);

                int row = find_row(column);
                if (row == -1) return false;
                Console.WriteLine("row - " + row);

                //double element = simplex_table[1][row] / simplex_table[column][row];
                double element = simplex_table[column][row];
                Console.WriteLine("element - " + element);

                simplex_table = new_table(column, row, element);
            }
        }

        private bool checking_optimal()
        {
            for(int i = 1; i <= (simplex_table.Count - 1); i++)
            {
                if (simplex_table[i][1] > 0) return false;
            }
            return true;
        }

        private void save_result()
        {
            basis_plan = new List<double>();
            for (int j = 0; j < n; j++) { basis_plan.Add(0); }

            for(int i = 2; i < n - m + 2; i++)
            {
                basis_plan[(int)simplex_table[0][i]-1] = simplex_table[1][i];
            }
            min_value = simplex_table[1][1];
            return;
        }

        private int find_column()
        {
            int max_col = -1;
            double max_value = -1;

            for (int i = 2; i < m + 2; i++)
            {
                if(simplex_table[i][1] > 0 && simplex_table[i][1] > max_value)
                {
                    max_value = simplex_table[i][1];
                    max_col = i;
                    //max_col = (int)simplex_table[i][1];
                }
            }

            return max_col;
        }

        private int find_row(int col)
        {
            int row = -1;
            double min_value = 999;
            double value;

            for (int i = 2; i < n - m + 2; i++)
            {
                value = simplex_table[1][i] / simplex_table[col][i];
                if (value < min_value)
                {
                    min_value = value;
                    row = i;
                }
            }

            return row;
        }

        private List<List<double>> new_table(int col, int row, double element)
        {
            List<List<double>>  table = new List<List<double>>();
            for (int i = 0; i < simplex_table.Count; i++)
            {
                table.Add(new List<double>());
                for (int j = 0; j < simplex_table[i].Count; j++)
                {
                    table[i].Add(simplex_table[i][j]);
                }
            }

            table[0][row] = simplex_table[col][0];
            table[col][0] = simplex_table[0][row];

            for (int i = 1; i < simplex_table.Count; i++)
            {
                for (int j = 1; j < simplex_table[i].Count; j++)
                {
                    table[i][j] = simplex_table[i][j] - ((simplex_table[i][row] * simplex_table[col][j]) / element);
                }
            }

            for (int i = 1; i <= n - m + 1; i++)
            {
                table[col][i] = simplex_table[col][i] / element * (-1);
            }

            for (int i = 1; i <= m+1; i++)
            {
                table[i][row] = simplex_table[i][row] / element;
            }

            table[col][row] = 1 / simplex_table[col][row];


            return table;
        }
    }
}
