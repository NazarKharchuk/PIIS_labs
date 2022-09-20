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

            Console.WriteLine("1 - Lee\t2 - AStar");
            string key = Console.ReadLine();

            Console.Clear();
            switch (key)
            {
                case "1":
                    LeeAlgorithm lee = new LeeAlgorithm(labyrinth);

                    lee.lee_algorithm();
                    lee.show_path();
                    break;
                case "2":
                    AStarAlgorithm a_star = new AStarAlgorithm(labyrinth);

                    a_star.astar_algorithm();
                    a_star.show_path();
                    break;
                default:
                    Console.WriteLine("Eror");
                    break;
            }

            Console.ReadLine();
        }
    }
}
