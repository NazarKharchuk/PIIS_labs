using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    class LeeAlgorithm
    {
        public Labyrinth labyrinth { get; set; }
        private Queue<Cell> queue { get; set; }

        private int[] rowNum = { 1, 0, -1, 0 };
        private int[] colNum = { 0, 1, 0, -1 };

        public bool find_path;

        public LeeAlgorithm(Labyrinth _labyrinth)
        {
            labyrinth = _labyrinth;
            queue = new Queue<Cell>();
        }

        public bool lee_algorithm()
        {
            if (!check_cell(labyrinth.start_cell) || !check_cell(labyrinth.finish_cell))
            {
                find_path = false;
                return false;
            }

            labyrinth.labyrinth[labyrinth.start_cell.row][labyrinth.start_cell.col] = 1;
            if (labyrinth.start_cell.col == labyrinth.finish_cell.col && labyrinth.start_cell.row == labyrinth.finish_cell.row)
            {
                find_path = true;
                return true;
            }
                
            queue.Enqueue(labyrinth.start_cell);

            while (queue.Count() != 0)
            {
                Cell temp_cell = queue.Dequeue();

                Cell new_temp_cell;
                for (int t = 0; t < 4; t++)
                {
                    new_temp_cell = new Cell(temp_cell.col + colNum[t], temp_cell.row + rowNum[t]);
                    if (check_cell(new_temp_cell) && labyrinth.labyrinth[new_temp_cell.row][new_temp_cell.col] == -1)
                    {
                        labyrinth.labyrinth[new_temp_cell.row][new_temp_cell.col] = 1 + labyrinth.labyrinth[temp_cell.row][temp_cell.col];

                        if (new_temp_cell.col == labyrinth.finish_cell.col && new_temp_cell.row == labyrinth.finish_cell.row)
                        {
                            find_path = true;
                            return true;
                        }
                       
                        queue.Enqueue(new_temp_cell);
                    }
                }
            }

            find_path = false;
            return false;
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

        public void show_path()
        {
            Console.WriteLine("Lee algorithm");
            if (find_path)
            {
                Cell curr = labyrinth.finish_cell;
                List<Cell> path = new List<Cell>();
                Cell new_temp_cell;
                while (curr.col != labyrinth.start_cell.col || curr.row != labyrinth.start_cell.row)
                {
                    path.Add(curr);

                    for (int i = 0; i < 4; i++)
                    {
                        new_temp_cell = new Cell(curr.col + colNum[i], curr.row + rowNum[i]);
                        if (check_cell(new_temp_cell) && labyrinth.labyrinth[new_temp_cell.row][new_temp_cell.col] != 0)
                        {
                            if (labyrinth.labyrinth[curr.row][curr.col] - 1 == labyrinth.labyrinth[new_temp_cell.row][new_temp_cell.col])
                            {
                                curr = new_temp_cell;
                                break;
                            }
                        }
                    }
                }
                path.Add(curr);

                for (int i = 0; i < labyrinth.rows; i++)
                {
                    for (int j = 0; j < labyrinth.columns; j++)
                    {
                        if (j == labyrinth.start_cell.col && i == labyrinth.start_cell.row)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0,3}", ""));
                            Console.ResetColor();
                            continue;
                        }
                        if (j == labyrinth.finish_cell.col && i == labyrinth.finish_cell.row)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write(String.Format("{0,3}", ""));
                            Console.ResetColor();
                            continue;
                        }
                        if (path.Contains(new Cell(j, i)))
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0,3}", labyrinth.labyrinth[i][j]));
                            Console.ResetColor();
                        }
                        else
                        {
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
                    }
                    Console.WriteLine();
                }
                for (int i = 0; i < labyrinth.rows; i++)
                {
                    for (int j = 0; j < labyrinth.columns; j++)
                        Console.Write(String.Format("{0,3}", labyrinth.labyrinth[i][j]));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Path not found");
            }
        }
    }
}
