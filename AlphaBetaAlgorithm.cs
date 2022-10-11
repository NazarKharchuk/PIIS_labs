﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    class AlphaBetaAlgorithm
    {
        public Labyrinth labyrinth { get; set; }
        private int[] rowNum = { 1, 0, -1, 0 };
        private int[] colNum = { 0, 1, 0, -1 };

        public AlphaBetaAlgorithm(Labyrinth _labyrinth, Cell player_start, Cell enemy_start)
        {
            labyrinth = new Labyrinth(_labyrinth);
            labyrinth.start_cell = player_start;
            labyrinth.enemy_start_cell = enemy_start;
        }

        public int alpha_beta(int depth, Cell player, Cell enemy, int alpha, int beta, bool isMax)
        {
            if (depth == 0 || (player.col == labyrinth.finish_cell.col && player.row == labyrinth.finish_cell.row) || (player.col == enemy.col && player.row == enemy.row))
            {
                return score(depth, player, enemy);
            }

            if (isMax)
            {
                int max_value = -1000;
                int value;
                Cell temp;
                for (int i = 0; i < 4; i++)
                {
                    temp = new Cell(player);
                    temp.col += colNum[i];
                    temp.row += rowNum[i];
                    if (check_cell(temp))
                    {
                        value = alpha_beta(depth - 1, temp, enemy, alpha, beta, false);
                        if (max_value < value) max_value = value;
                        if (alpha < value) alpha = value;
                        if (beta <= alpha) break;
                    }
                }
                return max_value;
            }
            else
            {
                int min_value = 1000;
                int value;
                Cell temp;
                for (int i = 0; i < 4; i++)
                {
                    temp = new Cell(enemy);
                    temp.col += colNum[i];
                    temp.row += rowNum[i];
                    if (check_cell(temp))
                    {
                        value = alpha_beta(depth - 1, player, temp, alpha, beta, true);
                        if (min_value > value) min_value = value;
                        if (beta > value) beta = value;
                        if (beta <= alpha) break;
                    }
                }
                return min_value;
            }
        }

        private int score(int depth, Cell player, Cell enemy)
        {
            if (player.col == labyrinth.finish_cell.col && player.row == labyrinth.finish_cell.row) return (900 + depth);
            if (player.col == enemy.col && player.row == enemy.row) return -(900 + depth);
            return ((Math.Abs(player.col - enemy.col) + Math.Abs(player.row - enemy.row)) + (-10 * (Math.Abs(player.col - labyrinth.finish_cell.col) + Math.Abs(player.row - labyrinth.finish_cell.row))));
        }

        private bool check_cell(Cell cell)
        {
            if (cell.col < 0 || cell.row < 0 || cell.col >= labyrinth.columns || cell.row >= labyrinth.rows || labyrinth.labyrinth[cell.row][cell.col] == 0)
            {
                return false;

            }
            else
            {
                return true;
            }
        }

        public int[] path_step(int depth)
        {
            int[] step = { -1, -1 };

            int max_value = -1000;
            int value;
            Cell temp;
            for (int i = 0; i < 4; i++)
            {
                temp = new Cell(labyrinth.start_cell);
                temp.col += colNum[i];
                temp.row += rowNum[i];
                if (check_cell(temp))
                {
                    value = alpha_beta(depth, temp, labyrinth.enemy_start_cell, -1000, 1000, false);
                    if (max_value < value)
                    {
                        max_value = value;
                        step[0] = temp.col;
                        step[1] = temp.row;
                    }
                }
            }

            return step;
        }
    }
}
