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
            Console.WriteLine("\t\tLab 4\n" +
                "Please select an algorithm:\n" +
                "1 - Rabin - Karpa algorithm\n" +
                "2 - Dijkstra's algorithm\n" +
                "3 - Prim's algorithm");

            int algo = Int32.Parse(Console.ReadLine());

            switch (algo)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("\tRabin - Karpa algorithm:");
                    Console.WriteLine("Please enter text to search:");
                    string text = Console.ReadLine();
                    Console.WriteLine("Please enter the template you are looking for:");
                    string template = Console.ReadLine();
                    RabinKarpAlgorithm rabin_karp = new RabinKarpAlgorithm(text, template);
                    Console.WriteLine("Count - " + rabin_karp.search());
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("\tDijkstra's algorithm:");
                    Graph graph = new Graph("graph1");
                    DijkstraAlgorithm dijkstra = new DijkstraAlgorithm(graph);
                    dijkstra.find();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("\tPrim's algorithm:");
                    break;
                default:
                    Console.WriteLine("Invalid algorithm(");
                    break;
            }
            Console.ReadLine();
        }
    }
}
