using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    class PolyhedronSearch
    {
        private Func<double[], double> function;
        private double t;
        private double epsilon;
        private double alpha;
        private double beta;
        private double gamma;
        private double[][] simplex;
        private int variables_number;

        public PolyhedronSearch(Func<double[], double> function, int variables_number, double[] first_point, double t, double epsilon, double alpha, double beta, double gamma)
        {
            this.function = function;
            this.variables_number = variables_number;
            this.t = t;
            this.epsilon = epsilon;
            this.alpha = alpha;
            this.beta = beta;
            this.gamma = gamma;
            fill_simplex(first_point);
        }

        private void fill_simplex(double[] first_point)
        {
            simplex = new double[variables_number + 5][];
            for (int i = 0; i < variables_number + 5; i++)
            {
                simplex[i] = new double[variables_number];
            }

            for (int j = 0; j < variables_number; j++)
            {
                simplex[0][j] = first_point[j];
            }

            double d_first = (t / (variables_number * Math.Sqrt(2))) * (Math.Sqrt(variables_number + 1) + variables_number - 1);
            double d_second = (t / (variables_number * Math.Sqrt(2))) * (Math.Sqrt(variables_number + 1) - 1);

            for (int i = 1; i < variables_number + 1; i++)
            {
                for (int j = 0; j < variables_number; j++)
                {
                    if(j == i - 1) { simplex[i][j] = simplex[0][j] + d_first; }
                    else { simplex[i][j] = simplex[0][j] + d_second; }
                }
            }
        }

        public double search_min(int max_k)
        {
            int max = -1, min = -1;
            int k = 1;

            while(k != max_k)
            {
                //Визначити вершини багатогранника, в яких функція набуває найбільшого та найменшого значення:
                max_and_min(ref max, ref min);

                if (k % 25 == 0)
                {
                    Console.WriteLine($"Iteration #{k}");
                    Console.WriteLine($"min{function(simplex[min])}");
                }

                //знаходження центру ваги багатогранника
                center_of_gravity(max);

                //перевірити виконання умови виходу
                double value = 0;
                for(int m = 0; m < variables_number + 1; m++)
                {
                    value += Math.Pow((function(simplex[m]) - function(simplex[variables_number + 1])), 2);
                }
                if (Math.Pow((value / (variables_number + 1)), 0.5) <= epsilon)
                {
                    Console.WriteLine($"Find min{function(simplex[min])}\nMin point:{simplex[min]}");
                    return function(simplex[min]);
                }

                //Відображення
                reflection(max);
                //Console.WriteLine($"Reflection");

                if (function(simplex[variables_number + 2]) <= function(simplex[min]))
                {
                    //Розтягнення
                    expansion();
                    //Console.WriteLine($"Expansion");

                    if (function(simplex[variables_number + 3]) <= function(simplex[min]))
                    {
                        change_point(max, variables_number + 3);
                        //Console.WriteLine($"min{function(simplex[min])}");
                        k++;
                        continue;
                    }
                    else
                    {
                        change_point(max, variables_number + 2);
                        //Console.WriteLine($"min{function(simplex[min])}");
                        k++;
                        continue;
                    }
                }
                else
                {
                    if (function(simplex[variables_number + 2]) <= function(simplex[max]))
                    {
                        //Стиснення
                        contraction(max);
                        //Console.WriteLine($"Contraction");
                        change_point(max, variables_number + 4);
                        //Console.WriteLine($"min{function(simplex[min])}");
                        k++;
                        continue;
                    }
                    else
                    {
                        //Редукція
                        shrink(min);
                        //Console.WriteLine($"Shrink");
                        //Console.WriteLine($"min{function(simplex[min])}");
                        k++;
                        continue;
                    }
                }
            }
            max_and_min(ref max, ref min);
            return function(simplex[min]);
        }

        private void max_and_min(ref int max, ref int min)
        {
            double max_value = function(simplex[0]);
            max = 0;
            double min_value = function(simplex[0]);
            min = 0;
            double value;
            for(int i = 1; i < variables_number + 1; i++)
            {
                value = function(simplex[i]);
                if (max_value < value)
                {
                    max_value = value;
                    max = i;
                    continue;
                }
                if (min_value > value)
                {
                    min_value = value;
                    min = i;
                }
            }
        }

        private void center_of_gravity(int max)
        {
            double sum;
            for(int j = 0; j < variables_number; j++)
            {
                sum = 0;
                for (int i = 0; i < variables_number + 1; i++)
                {
                    sum += simplex[i][j];
                }
                sum -= simplex[max][j];
                simplex[variables_number + 1][j] = sum / variables_number;
            }
        }

        private void reflection(int max)
        {
            for (int n = 0; n < variables_number; n++)
            {
                simplex[variables_number + 2][n] = simplex[variables_number + 1][n] + alpha * (simplex[variables_number + 1][n] - simplex[max][n]);
            }
        }

        private void expansion()
        {
            for (int n = 0; n < variables_number; n++)
            {
                simplex[variables_number + 3][n] = simplex[variables_number + 1][n] + gamma * (simplex[variables_number + 2][n] - simplex[variables_number + 1][n]);
            }
        }

        private void contraction(int max)
        {
            for (int n = 0; n < variables_number; n++)
            {
                simplex[variables_number + 4][n] = simplex[variables_number + 1][n] + beta * (simplex[max][n] - simplex[variables_number + 1][n]);
            }
        }

        private void shrink(int min)
        {
            for (int m = 0; m < variables_number + 1; m++)
            {
                for (int n = 0; n < variables_number; n++)
                {
                    simplex[m][n] = simplex[min][n] + 0.5 * (simplex[m][n] - simplex[min][n]);
                }
            }
        }

        private void change_point(int befor, int after)
        {
            for (int n = 0; n < variables_number; n++)
            {
                simplex[befor][n] = simplex[after][n];
            }
        }
    }
}
