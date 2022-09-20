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

        public LeeAlgorithm(Labyrinth _labyrinth)
        {
            labyrinth = _labyrinth;
            queue = new Queue<Cell>();
        }

        public bool lee_algorithm()
        {
            if (!check_cell(labyrinth.start_cell) || !check_cell(labyrinth.finish_cell))
                return false;

            labyrinth.labyrinth[labyrinth.start_cell.row][labyrinth.start_cell.col] = 1;
            if (labyrinth.start_cell.col == labyrinth.finish_cell.col && labyrinth.start_cell.row == labyrinth.finish_cell.row)
                return true;
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
                            return true;  
                        
                        queue.Enqueue(new_temp_cell);
                    }
                }
            }

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
    }
}
