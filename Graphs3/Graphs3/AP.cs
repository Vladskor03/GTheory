using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs3
{
    static public class AP
    {
        static private HashSet<int> ap= new HashSet<int>();
        static int time = 0;
        

        static void APUtil
            (
                IGraph graph,
                int u, 
                bool[] visited, 
                int[] disc,
                int[] low, 
                int[] parent
            )
        {

            int children = 0;
            visited[u] = true;
            disc[u] = low[u] = ++time;

            foreach (int i in graph.AdjacencyList(u))
            {
                int v = i;

                // Если v еще не посещена, то сделаем ее дочерним элементом u в дереве DFS и рекурсивно повторим
                if (!visited[v])
                {
                    children++;
                    parent[v] = u;
                    APUtil(graph, v, visited, disc, low, parent);

                    // имеет ли поддерево с корнем v соединение с одним из предков u
                    low[u] = Math.Min(low[u], low[v]);

                    // u является точкой сочленения в следующих случаях
                    // u является корнем дерева DFS и имеет двух или более дочерних элементов.
                    if (parent[u] == -1 && children > 1)
                        ap.Add(u);

                    // Если u не является корневым и нижнее значение одного из его дочерних элементов больше, чем значение обнаружения u.
                    if (parent[u] != -1 && low[v] >= disc[u])
                        ap.Add(u);
                }

                
                else if (v != parent[u])
                    low[u] = Math.Min(low[u], disc[v]);
            }
        }

        
        public static HashSet<int> GetAP(this IGraph graph)
        {
           
            var visited = new bool[graph.VertexCount()];
            var disc = new int[graph.VertexCount()];
            var low = new int[graph.VertexCount()];
            var parent = new int[graph.VertexCount()];
            

           
            for (int i = 0; i < graph.VertexCount(); i++)
            {
                parent[i] = -1;
                visited[i] = false;
                ap.Clear();
            }

            
            for (int i = 0; i < graph.VertexCount(); i++)
                if (visited[i] == false)
                    APUtil(graph, i, visited, disc, low, parent);

            
            return ap;
        }
    }
}
