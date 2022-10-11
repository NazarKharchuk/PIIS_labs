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
            Console.WriteLine("Lab 2\n");

            Labyrinth labyrinth = new Labyrinth("labyrinth1");

            Game game = new Game(labyrinth);

            game.run();

            Console.ReadLine();
        }
    }
}
