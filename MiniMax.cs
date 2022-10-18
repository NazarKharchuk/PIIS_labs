using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    class MiniMax : IAlgorithm
    {
        private PlayingField playing_field;
        private CellContent player;
        private CellContent AI_player;

        public MiniMax(PlayingField field, CellContent _player, CellContent _AI_player)
        {
            playing_field = new PlayingField(field);
            player = _player;
            AI_player = _AI_player;
        }

        public int next_step()
        {
            //Console.WriteLine("MiniMax");
            //System.Threading.Thread.Sleep(500);

            int max_score = -2;
            int score;
            int best_move = -1;
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(playing_field.field[i, j] == CellContent.EmptyCell)
                    {
                        playing_field.field[i, j] = AI_player;
                        score = mini_max(playing_field, false);
                        playing_field.field[i, j] = CellContent.EmptyCell;

                        if (score > max_score)
                        {
                            max_score = score;
                            best_move = playing_field.CoordinatesInNumber(i, j);
                        }
                    }
                }
            }
            return best_move;
        }

        public int mini_max(PlayingField field, bool isMax)
        {
            int winner, one, two, three;
            (winner, one, two, three) = field.check_field();

            if(winner != -1)
            {
                return count_score(field);
            }

            if (isMax)
            {
                int max_score = -2;
                int score;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (field.field[i, j] == CellContent.EmptyCell)
                        {
                            field.field[i, j] = AI_player;
                            score = mini_max(playing_field, false);
                            field.field[i, j] = CellContent.EmptyCell;

                            if (score > max_score)
                            {
                                max_score = score;
                            }
                        }
                    }
                }
                return max_score;
            }
            else
            {
                int min_score = 2;
                int score;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (field.field[i, j] == CellContent.EmptyCell)
                        {
                            field.field[i, j] = player;
                            score = mini_max(playing_field, true);
                            field.field[i, j] = CellContent.EmptyCell;

                            if (score < min_score)
                            {
                                min_score = score;
                            }
                        }
                    }
                }
                return min_score;
            }
        }

        public int count_score(PlayingField field)
        {
            int winner, one, two, three;
            (winner, one, two, three) = field.check_field();

            if (winner == 0)
            {
                return 0;
            }
            else
            {
                if (winner == 1)
                {
                    if (AI_player == CellContent.XCell)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                if (winner == 2)
                {
                    if (AI_player == CellContent.OCell)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            return 0;
        }
    }
}
