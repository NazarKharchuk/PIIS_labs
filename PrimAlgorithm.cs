using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    class PrimAlgorithm
    {
        public Graph graph;
        public List<int> verified_vertices;
        public List<Edge> edges;

        public PrimAlgorithm(Graph _graph)
        {
            graph = _graph;
            verified_vertices = new List<int>();
            edges = new List<Edge>();
        }

        public void find()
        {
            verified_vertices.Add(0);
            Edge curent_edge = find_min_edge();
            int curent_vertex = curent_edge.second_vertex;
            while (curent_vertex != -1)
            {
                edges.Add(curent_edge);
                verified_vertices.Add(curent_vertex);

                curent_edge = find_min_edge();
                curent_vertex = curent_edge.second_vertex;
            }
            show_result();
        }

        public void show_result()
        {
            Console.WriteLine("\tMinimum spanning tree:\nEdge \tValue");
            foreach (var item in edges)
            {
                Console.WriteLine($"{item.first_vertex + 1} - {item.second_vertex + 1}\t{item.value};");
            }
        }

        private Edge find_min_edge()
        {
            int min = 999;
            Edge edge = new Edge(-1, -1, -1);
            for(int i = 0; i < graph.vertex_count; i++)
            {
                if (verified_vertices.Contains(i))
                {
                    for (int j = 0; j < graph.vertex_count; j++)
                    {
                        if(!verified_vertices.Contains(j) && graph.table[i][j] != 0 && graph.table[i][j] < min)
                        {
                            min = graph.table[i][j];
                            edge.first_vertex = i;
                            edge.second_vertex = j;
                            edge.value = graph.table[i][j];
                        }
                    }
                }
            }
            return edge;
        }
    }

    struct Edge
    {
        public int first_vertex;
        public int second_vertex;
        public int value;

        public Edge(int first, int second, int v)
        {
            first_vertex = first;
            second_vertex = second;
            value = v;
        }
    }
}
