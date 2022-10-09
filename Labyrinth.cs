using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PIIS_labs
{
    class Labyrinth
    {
        public int columns { get; set; }
        public int rows { get; set; }

        public Cell start_cell { get; set; }
        public Cell finish_cell { get; set; }
        public Cell enemy_start_cell { get; set; }

        public List<List<int>> labyrinth { get; set; }

        public Labyrinth(string file_name)
        {
            read_labyrinth(file_name);

            Console.WriteLine("Col: " + columns + "\tRow: " + rows + 
                "\nStart: col=" + start_cell.col + "; row=" + start_cell.row + ";\n" +
                "Finish: col=" + finish_cell.col + "; row=" + finish_cell.row + ";\n" +
                "Start(Enemy): col=" + enemy_start_cell.col + "; row=" + enemy_start_cell.row + ";\n" +
                "Labyrinth:");
            for(int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    Console.Write(String.Format("{0,2}", labyrinth[i][j]));
                Console.WriteLine();
            }
        }

        public Labyrinth(Labyrinth _labyrinth)
        {
            columns = _labyrinth.columns;
            rows = _labyrinth.rows;
            start_cell = new Cell();
            finish_cell = new Cell();
            enemy_start_cell = new Cell();

            labyrinth = new List<List<int>>();
            for (int i = 0; i < rows; i++)
            {
                labyrinth.Add(new List<int>());
                for (int j = 0; j < columns; j++)
                {
                    labyrinth[i].Add(_labyrinth.labyrinth[i][j]);
                }
            }

            Console.WriteLine("\t\tNew labyrinth:");
            Console.WriteLine("Col: " + columns + "\tRow: " + rows +
                "\nStart: col=" + start_cell.col + "; row=" + start_cell.row + ";\n" +
                "Finish: col=" + finish_cell.col + "; row=" + finish_cell.row + ";\n" +
                "Start(Enemy): col=" + enemy_start_cell.col + "; row=" + enemy_start_cell.row + ";\n" +
                "Labyrinth:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    Console.Write(String.Format("{0,2}", labyrinth[i][j]));
                Console.WriteLine();
            }
        }

        private void read_labyrinth(string file_name)
        {
            string path = "D:/Code/PIIS_labs/labyrinths/" + file_name + ".txt";

            String line;
            try
            {
                StreamReader sr = new StreamReader(path);
                line = sr.ReadLine();
                columns = Convert.ToInt32(line);

                line = sr.ReadLine();
                rows = Convert.ToInt32(line);

                line = sr.ReadLine();
                string[] start_coordinates = line.Split(' ');
                start_cell = new Cell(Convert.ToInt32(start_coordinates[0]), Convert.ToInt32(start_coordinates[1]));

                line = sr.ReadLine();
                string[] finish_coordinates = line.Split(' ');
                finish_cell = new Cell(Convert.ToInt32(finish_coordinates[0]), Convert.ToInt32(finish_coordinates[1]));

                line = sr.ReadLine();
                string[] enemy_start_coordinates = line.Split(' ');
                enemy_start_cell = new Cell(Convert.ToInt32(enemy_start_coordinates[0]), Convert.ToInt32(enemy_start_coordinates[1]));

                labyrinth = new List<List<int>>();
                for (int i = 0; i < rows; i++)
                {
                    labyrinth.Add(new List<int>());
                    line = sr.ReadLine();
                    string[] content = line.Split(' ');
                    for (int j = 0; j < columns; j++)
                    {
                        labyrinth[i].Add(Convert.ToInt32(content[j]));
                    }
                }

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                        if (labyrinth[i][j] == 1) labyrinth[i][j] = -1;
                }

                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
