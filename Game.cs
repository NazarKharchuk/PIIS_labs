using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    class Game
    {
        private PlayingField playing_field;
        private CellContent player;
        private CellContent AI_player;
        private bool finish;

        public Game()
        {
            playing_field = new PlayingField();
            finish = false;
        }

        public void start()
        {
            Console.WriteLine("1 - NegaMax\t2 - AlphaBetaNegaMax\t3 - NegaScout\t4 - MiniMax");
            string choice_algorithm = Console.ReadLine();

            Console.WriteLine("\nX/O?");
            string choice = Console.ReadLine();
            if (choice == "X" || choice == "x")
            {
                player = CellContent.XCell;
                AI_player = CellContent.OCell;
            }
            else
            {
                if (choice == "O" || choice == "o")
                {
                    player = CellContent.OCell;
                    AI_player = CellContent.XCell;
                }
                else return;
            }

            string new_player_step;
            if (player == CellContent.XCell)
            {
                playing_field.ShowField();
                Console.WriteLine("Your step");
                new_player_step = Console.ReadLine();
                playing_field.step(Int32.Parse(new_player_step), player);
            }

            IAlgorithm algorithm;
            int next_AI_step;

            while(finish == false)
            {
                playing_field.ShowField();
                finish = check_finish();
                if (finish) break;

                switch(choice_algorithm){
                    case "1":
                        algorithm = new NegaMax(playing_field, player, AI_player);
                        break;
                    case "2":
                        algorithm = new AlphaBetaNegaMax(playing_field, player, AI_player);
                        break;
                    case "3":
                        algorithm = new NegaScout(playing_field, player, AI_player);
                        break;
                    default:
                        algorithm = new MiniMax(playing_field, player, AI_player);
                        break;
                }

                next_AI_step = algorithm.next_step();
                playing_field.step(next_AI_step, AI_player);

                playing_field.ShowField();
                finish = check_finish();
                if (finish) break;

                Console.WriteLine("Your step");
                new_player_step = Console.ReadLine();
                playing_field.step(Int32.Parse(new_player_step), player);
            }
        }

        private bool check_finish()
        {
            int winner, one, two, three;
            (winner, one, two, three) = playing_field.check_field();

            if(winner == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("tie");
                Console.ResetColor();
                return true;
            }
            else
            {
                if(winner == 1)
                {
                    if(player == CellContent.XCell)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("you won)");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("you lost(");
                        Console.ResetColor();
                    }
                    return true;
                }
                if (winner == 2)
                {
                    if (player == CellContent.OCell)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("you won)");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("you lost(");
                        Console.ResetColor();
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
