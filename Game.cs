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
            MinimaxAlgorithm minimax;
            AlphaBetaAlgorithm alpha_beta;

            char algo;
            Console.WriteLine("1 - minimax; 2 - alpha-beta;");
            algo = Console.ReadLine()[0];


            show();

            //for(int i = 0; i < 15; i++)
            while(true)
            {
                if (algo == '1') Console.WriteLine("minimax;");
                else Console.WriteLine("alpha-beta;");

                if (player_position.col == enemy_position.col && player_position.row == enemy_position.row)
                {
                    Console.WriteLine("The player lost(");
                    break;
                }

                if (player_position.col == labyrinth.finish_cell.col && player_position.row == labyrinth.finish_cell.row)
                {
                    Console.WriteLine("The player won)");
                    break;
                }

                lee = new LeeAlgorithm(labyrinth, new Cell(enemy_position), new Cell(player_position));

                lee.lee_algorithm();

                int[] x = lee.path_step();

                if (x[0] != -1 && x[1] != -1)
                {
                    enemy_position.col = x[0];
                    enemy_position.row = x[1];
                }

                if (player_position.col == enemy_position.col && player_position.row == enemy_position.row)
                {
                    Console.WriteLine("The player lost(");
                    break;
                }

                if (player_position.col == labyrinth.finish_cell.col && player_position.row == labyrinth.finish_cell.row)
                {
                    Console.WriteLine("The player won)");
                    break;
                }

                int[] y;
                if (algo == '1')
                {
                    minimax = new MinimaxAlgorithm(labyrinth, new Cell(player_position), new Cell(enemy_position));
                    y = minimax.path_step();
                }
                else
                {
                    alpha_beta = new AlphaBetaAlgorithm(labyrinth, new Cell(player_position), new Cell(enemy_position));

                    y = alpha_beta.path_step();
                }

                if (y[0] != -1 && y[1] != -1)
                {
                    player_position.col = y[0];
                    player_position.row = y[1];
                }

                Thread.Sleep(100);

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
