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

            Labyrinth labyrinth = new Labyrinth("labyrinth2");

            /*LeeAlgorithm lee = new LeeAlgorithm(labyrinth);

            if (lee.lee_algorithm())
            {
                Console.WriteLine("yes");
            }
            else
            {
                Console.WriteLine("no");
            }*/

            AStarAlgorithm a_star = new AStarAlgorithm(labyrinth);

            if (a_star.astar_algorithm())
            {
                Console.WriteLine("yes");
            }
            else
            {
                Console.WriteLine("no");
            }


            for (int i = 0; i < labyrinth.rows; i++)
            {
                for (int j = 0; j < labyrinth.columns; j++)
                    Console.Write(String.Format("{0,2}", labyrinth.labyrinth[i][j]));
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
