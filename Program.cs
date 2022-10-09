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
            Console.WriteLine("Lab 1\n");

            Labyrinth labyrinth = new Labyrinth("labyrinth1");

            //Labyrinth labyrinth2 = new Labyrinth(labyrinth);

            LeeAlgorithm lee = new LeeAlgorithm(labyrinth, new Cell(1, 1), new Cell(4, 9));

            lee.lee_algorithm();

            int[] x = lee.path_step();

            Console.WriteLine("step"); 
            Console.WriteLine(x[0]);
            Console.WriteLine(x[1]);

            Console.WriteLine("1\n");

            for (int i = 0; i < labyrinth.rows; i++)
            {
                for (int j = 0; j < labyrinth.columns; j++)
                    Console.Write(String.Format("{0,2}", labyrinth.labyrinth[i][j]));
                Console.WriteLine();
            }

            /*Console.WriteLine("2\n");

            for (int i = 0; i < labyrinth2.rows; i++)
            {
                for (int j = 0; j < labyrinth2.columns; j++)
                    Console.Write(String.Format("{0,2}", labyrinth2.labyrinth[i][j]));
                Console.WriteLine();
            }*/


            Console.ReadLine();
        }
    }
}
