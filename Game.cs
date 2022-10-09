using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PIIS_labs
{
    class Game
    {
        public Labyrinth labyrinth { get; set; }
        public Cell player_position { get; set; }
        public Cell enemy_position { get; set; }

        public Game(Labyrinth _labyrinth)
        {
            labyrinth = _labyrinth;
            player_position = new Cell(_labyrinth.start_cell);
            enemy_position = new Cell(_labyrinth.enemy_start_cell);
        }

        public void run()
        {
            LeeAlgorithm lee;

            show();

            for(int i = 0; i<15; i++)
            {
                lee = new LeeAlgorithm(labyrinth, new Cell(enemy_position), new Cell(player_position));

                lee.lee_algorithm();

                int[] x = lee.path_step();
                Thread.Sleep(1000);

                enemy_position.col = x[0];
                enemy_position.row = x[1];

                if(player_position == enemy_position)
                {
                    Console.WriteLine("The player lost(");
                }

                show();
            }

        }

        public void show()
        {
            Console.Clear();
            for (int i = 0; i < labyrinth.rows; i++)
            {
                for (int j = 0; j < labyrinth.columns; j++)
                {
                    if (j == player_position.col && i == player_position.row)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(String.Format("{0,3}", ""));
                        Console.ResetColor();
                        continue;
                    }
                    if (j == enemy_position.col && i == enemy_position.row)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(String.Format("{0,3}", ""));
                        Console.ResetColor();
                        continue;
                    }
                    if (j == labyrinth.finish_cell.col && i == labyrinth.finish_cell.row)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write(String.Format("{0,3}", ""));
                        Console.ResetColor();
                        continue;
                    }
                    if (labyrinth.labyrinth[i][j] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(String.Format("{0,3}", ""));
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(String.Format("{0,3}", ""));
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }
}
