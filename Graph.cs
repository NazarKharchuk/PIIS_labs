using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PIIS_labs
{
    class Graph
    {
        public List<List<int>> table;
        public int vertex_count;
        public bool is_oriented;

        public Graph(string file_name)
        {
            read_labyrinth(file_name);

            Console.WriteLine("count: " + vertex_count + "\tis_oriented: " + is_oriented + ";\n");
            for (int i = 0; i < vertex_count; i++)
            {
                for (int j = 0; j < vertex_count; j++)
                    Console.Write(String.Format("{0,2}", table[i][j]));
                Console.WriteLine();
            }
        }

        private void read_labyrinth(string file_name)
        {
            string path = "D:/Code/PIIS_labs/" + file_name + ".txt";

            String line;
            try
            {
                StreamReader file = new StreamReader(path);
                line = file.ReadLine();
                if (line.Contains("true") || line.Contains("True")) is_oriented = true;
                else is_oriented = false;

                line = file.ReadLine();
                vertex_count = Convert.ToInt32(line);

                table = new List<List<int>>();
                for (int i = 0; i < vertex_count; i++)
                {
                    table.Add(new List<int>());
                    for (int j = 0; j < vertex_count; j++)
                    {
                        table[i].Add(0);
                    }
                }

                line = file.ReadLine();
                string[] content = line.Split(',');
                string[] content2, content3;
                int coord0, coord1;
                for (int j = 0; j < content.Length; j++)
                {
                    content2 = content[j].Split('=');
                    content3 = content2[0].Split('-');
                    coord0 = Convert.ToInt32(content3[0]);
                    coord1 = Convert.ToInt32(content3[1]);
                    if (table[coord0 - 1][coord1 - 1] == 0 || table[coord0 - 1][coord1 - 1] > Convert.ToInt32(content2[1]))
                    {
                        table[coord0 - 1][coord1 - 1] = Convert.ToInt32(content2[1]);
                        if (!is_oriented)
                        {
                            table[coord1 - 1][coord0 - 1] = Convert.ToInt32(content2[1]);
                        }
                    }
                }

                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
