using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    class DijkstraAlgorithm
    {
        public Graph graph;
        public List<DijkstraNode> dijkstra_nodes;
        public int start_vertex;

        public DijkstraAlgorithm(Graph _graph)
        {
            graph = _graph;
            dijkstra_nodes = new List<DijkstraNode>();
            for(int i = 0; i< graph.vertex_count; i++)
            {
                dijkstra_nodes.Add(new DijkstraNode(i + 1));
            }
            do
            {
                Console.WriteLine("Please select start vertex:");
                start_vertex = Int32.Parse(Console.ReadLine());
            } while (start_vertex > graph.vertex_count);
        }

        public void find()
        {
            DijkstraNode next_vertex, vertex = dijkstra_nodes[start_vertex - 1];
            vertex.value = 0;
            vertex = dijkstra_nodes[get_min_vertex()];
            int sum;
            while (vertex.vertex != -1)
            {
                vertex.is_visited = true;
                for(int i = 0; i < graph.vertex_count; i++)
                {
                    next_vertex = dijkstra_nodes[i];
                    if (graph.table[vertex.vertex-1][i] != 0 && next_vertex.is_visited == false)
                    {
                        sum = vertex.value + graph.table[vertex.vertex - 1][i];
                        if (next_vertex.value > sum)
                        {
                            next_vertex.value = sum;
                        }
                    }
                }
                if (get_min_vertex() == -1) break;
                vertex = dijkstra_nodes[get_min_vertex()];
            }
            show_result();
        }

        public int get_min_vertex()
        {
            int min = 999, min_vertex = -1;
            for (int i = 0; i < graph.vertex_count; i++)
            {
                if (dijkstra_nodes[i].value < min && dijkstra_nodes[i].is_visited != true)
                {
                    min = dijkstra_nodes[i].value;
                    min_vertex = i;
                }
            }
            if (min_vertex == -1) return min_vertex;
            bool closed = true;
            for (int i = 0; i < graph.vertex_count; i++)
            {
                if(graph.table[min_vertex][i] != 0)
                {
                    closed = false;
                }
            }
            if (!closed) return min_vertex;
            else
            {
                dijkstra_nodes[min_vertex].is_visited = true;
                return -1;
            }
        }

        public void show_result()
        {
            foreach (DijkstraNode item in dijkstra_nodes)
            {
                if (item.value != 999)
                {
                    Console.WriteLine("Vertex - " + item.vertex + "\tpath - " + item.value);
                }
                else
                {
                    Console.WriteLine("Vertex - " + item.vertex + "\tpath not found");
                }
            }
        }
    }

    class DijkstraNode
    {
        public int vertex;
        public int value;
        public bool is_visited;
        public DijkstraNode(int _vertex)
        {
            vertex = _vertex;
            value = 999;
            is_visited = false;
        }
    }
}
