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
            Console.WriteLine("X/O?");
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

            if(player == CellContent.XCell)
            {
                playing_field.ShowField();
                Console.WriteLine("Your step");
                string first_step = Console.ReadLine();
                playing_field.step(Int32.Parse(first_step), player);
            }

            while(finish == false)
            {
                playing_field.ShowField();
                finish = check_finish();
                if (finish) break;

            }
        }

        private bool check_finish()
        {
            int winner, one, two, three;
            (winner, one, two, three) = playing_field.check_field();

            if(winner == 0)
            {
                Console.WriteLine("tie");
                return true;
            }
            else
            {
                if(winner == 1)
                {
                    if(player == CellContent.XCell)
                    {
                        Console.WriteLine("you won)");
                    }
                    else
                    {
                        Console.WriteLine("you lost(");
                    }
                    return true;
                }
                if (winner == 2)
                {
                    if (player == CellContent.OCell)
                    {
                        Console.WriteLine("you won)");
                    }
                    else
                    {
                        Console.WriteLine("you lost(");
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
