using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Graphs3 // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            int flag = 0;
            IGraph? graph = null;
            StreamWriter? sw = null;

            for (int i = 0; i < args.Length; i++)
            {
                for (int j = 0; j < args.Length; j++)
                    if (args[j] == "-h")
                    {
                        ShowHelp();
                        flag = 1;
                        break;
                    }
                if (flag == 1)
                {
                    break;
                }
            }
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-m")
                {
                    graph = new GraphMatrix(args[i + 1]);


                }
                else if (args[i] == "-e")
                {
                    graph = new EdgeList(args[i + 1]);

                }
                else if (args[i] == "-l")
                {
                    graph = new AdjacencyGraph(args[i + 1]);
                }
                else if (args[i] == "-o")
                {
                    sw = new StreamWriter(args[i + 1]);
                    sw.AutoFlush = true;
                }

            }
            if (graph == null)
            {
                return;
            }
            if (sw == null)
            {
                sw = new StreamWriter(Console.OpenStandardOutput());
                sw.AutoFlush = true;
                Console.SetOut(sw);
            }
            
            var bridges = graph.find_bridges();
            sw.Write("Bridges:");
            foreach (var bridge in bridges)
            {
                for (int i = 0; i < bridge.Count; i++)
                    sw.WriteLine(bridge[i].Item1 + " " + bridge[i].Item2);

            }
            var ap = graph.GetAP();
            sw.Write("Cut vertices:");
            foreach (var item in ap )
            {
                sw.Write(item.ToString() + " "); 
            }


            void ShowHelp()
            {
                Console.WriteLine("Performed by: Artem Luganskiy\n Group: М3О-225Бк-21 \n Task №4 \n Keys: \n -m - The graph is read from the adjacency matrix \n -e - The graph is read from the list of edges \n -l - The graph is read from the adjacency list \n -o - The result is output to a file \n");
            }
        }






    }

}