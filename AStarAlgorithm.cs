using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    class AStarAlgorithm
    {
        private List<List<AStarNode>> node_matrix { get; set; }
        public Labyrinth labyrinth { get; set; }
        private List<Cell> open_list { get; set; }
        private List<Cell> close_list { get; set; }

        private int[] rowNum = { 1, 1, 0, -1, -1, -1, 0, 1 };
        private int[] colNum = { 0, 1, 1, 1, 0, -1, -1, -1 };

        public bool find_path;

        public AStarAlgorithm(Labyrinth _labyrinth)
        {

            labyrinth = _labyrinth;
            open_list = new List<Cell>();
            close_list = new List<Cell>();

            node_matrix = new List<List<AStarNode>>();
            for (int i = 0; i < labyrinth.rows; i++)
            {
                node_matrix.Add(new List<AStarNode>());
                for (int j = 0; j < labyrinth.columns; j++)
                {
                    node_matrix[i].Add(new AStarNode(new Cell(j, i), heuristic(new Cell(j, i))));
                }
            }
        }

        private int heuristic(Cell cell)
        {
            return (10 * (Math.Abs(cell.col-labyrinth.finish_cell.col)+ Math.Abs(cell.row - labyrinth.finish_cell.row)));
        }

        public bool astar_algorithm()
        {
            if (!check_cell(labyrinth.start_cell) || !check_cell(labyrinth.finish_cell))
            {
                find_path = false;
                return false;
            }
                

            open_list.Add(new Cell(labyrinth.start_cell.col, labyrinth.start_cell.row));
            node_matrix[labyrinth.start_cell.row][labyrinth.start_cell.col].g_value = 0;
            node_matrix[labyrinth.start_cell.row][labyrinth.start_cell.col].f_value = node_matrix[labyrinth.start_cell.row][labyrinth.start_cell.col].h_value;

            Cell curr;
            while (open_list.Count() != 0)
            {
                curr = get_min_f();

                if (curr.col == labyrinth.finish_cell.col && curr.row == labyrinth.finish_cell.row)
                {
                    find_path = true;
                    return true;
                }
                    

                add_children(curr);
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

        private Cell get_min_f()
        {
            Cell min_f = open_list[0];
            foreach(Cell c in open_list)
            {
                if(node_matrix[min_f.row][min_f.col].f_value> node_matrix[c.row][c.col].f_value)
                {
                    min_f = c;
                }
            }
            open_list.Remove(min_f);
            close_list.Add(min_f);
            return min_f;
        }

        private void add_children(Cell parent)
        {
            Cell new_temp_cell;
            int temp_g;
            int step;
            for (int t = 0; t < 8; t++)
            {
                new_temp_cell = new Cell(parent.col + colNum[t], parent.row + rowNum[t]);
                if (check_cell(new_temp_cell) && labyrinth.labyrinth[new_temp_cell.row][new_temp_cell.col] == -1 && !close_list.Contains(new_temp_cell))
                {
                    if (Math.Abs(colNum[t]) + Math.Abs(rowNum[t]) == 2) {
                        if (labyrinth.labyrinth[parent.row + rowNum[t]][parent.col] != -1 || labyrinth.labyrinth[parent.row][parent.col + colNum[t]] != -1)
                            continue;
                        step = 14;
                    } 
                    else step = 10;

                    temp_g = node_matrix[parent.row][parent.col].g_value + step;

                    if (open_list.Contains(new_temp_cell))
                    {
                        if (node_matrix[new_temp_cell.row][new_temp_cell.col].g_value > temp_g)
                        {
                            node_matrix[new_temp_cell.row][new_temp_cell.col].parent = parent;
                            node_matrix[new_temp_cell.row][new_temp_cell.col].g_value = temp_g;
                            node_matrix[new_temp_cell.row][new_temp_cell.col].f_value = temp_g + node_matrix[new_temp_cell.row][new_temp_cell.col].h_value;
                        }
                    }
                    else
                    {
                        open_list.Add(new_temp_cell);
                        node_matrix[new_temp_cell.row][new_temp_cell.col].parent = parent;
                        node_matrix[new_temp_cell.row][new_temp_cell.col].g_value = temp_g;
                        node_matrix[new_temp_cell.row][new_temp_cell.col].f_value = temp_g + node_matrix[new_temp_cell.row][new_temp_cell.col].h_value;
                    }
                }
            }
        }

        public void show_path()
        {
            Console.WriteLine("A Star algorithm");
            if (find_path)
            {
                Cell curr = labyrinth.finish_cell;
                List<Cell> path = new List<Cell>();
                while(curr.col != labyrinth.start_cell.col || curr.row != labyrinth.start_cell.row)
                {
                    path.Add(curr);
                    curr = node_matrix[curr.row][curr.col].parent;
                }
                path.Add(curr);

                for (int i = 0; i < labyrinth.rows; i++)
                {
                    for (int j = 0; j < labyrinth.columns; j++)
                    {
                        if(node_matrix[i][j].coordinates.col == labyrinth.start_cell.col && node_matrix[i][j].coordinates.row == labyrinth.start_cell.row)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0,3}", ""));
                            Console.ResetColor();
                            continue;
                        }
                        if (node_matrix[i][j].coordinates.col == labyrinth.finish_cell.col && node_matrix[i][j].coordinates.row == labyrinth.finish_cell.row)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write(String.Format("{0,3}", ""));
                            Console.ResetColor();
                            continue;
                        }
                        if (path.Contains(node_matrix[i][j].coordinates))
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0,3}", "* "));
                            Console.ResetColor();
                        }
                        else
                        {
                            if(labyrinth.labyrinth[i][j]==0)
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
            }
            else
            {
                Console.WriteLine("Path not found");
            }
        }
    }
}
