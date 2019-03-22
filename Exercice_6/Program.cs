using System;
using System.Collections.Generic;
using System.Linq;

/*******
 * Read input from Console
 * Use Console.WriteLine to output your result.
 * Use:
 *       Utils.LocalPrint( variable); 
 * to display simple variables in a dedicated area.
 * 
 * Use:
 *      
		Utils.LocalPrintArray( collection)
 * to display collections in a dedicated area.
 * ***/

namespace CSharpContestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new NodeDico();
            var count = int.Parse(Console.ReadLine());
            for (int index = 0; index < count; index++)
            {
                var readLine = Console.ReadLine().Split(' ');
                var node = graph.Get(readLine[0]);
                for (int i = 1; i < readLine.Length; i++)
                {
                    node.AddLink(graph.Get(readLine[i]));
                }
            }
            var inMax = string.Empty;
            graph.Map( (x) =>
            {
                var text = x.Longest();
                if (text.Length > inMax.Length)
                {
                    inMax = text;
                }
            });
            Console.WriteLine(inMax);
        }
    }

    public class GraphNode
    {
        private readonly List<GraphNode> linked = new List<GraphNode>();

        public string Id { get; }
        public IEnumerable<GraphNode> Links => this.linked;

        public GraphNode(string word)
        {
            this.Id = word;
        }

        public string Longest()
        {
            var result = Id;
            if (Links.Count() > 0)
            {
                result += "\\";
                var longest = string.Empty;
                foreach (var graphNode in Links)
                {
                    var sub = graphNode.Longest();
                    if (sub.Length > longest.Length)
                    {
                        longest = sub;
                    }
                }
                result += longest;
            }
            return result;
        }

        public void AddLink(GraphNode link)
        {
            this.linked.Add(link);
        }

        public bool AttemptLink(GraphNode other)
        {
            if (this.HeuriDist(other) == 1)
            {
                this.AddLink(other);
                other.AddLink(this);
                return true;
            }
            return false;
        }

        public int HeuriDist(GraphNode other)
        {
            var dist = 0;

            return dist;
        }

        public int DistTo(GraphNode neighbor)
        {
            return this.linked.Contains(neighbor) ? 1 : int.MaxValue;
        }
    }

    public class NodeDico
    {
        private readonly Dictionary<string, GraphNode> nodes = new Dictionary<string, GraphNode>();

        public GraphNode Get(string word)
        {
            if (!this.nodes.ContainsKey(word))
            {
                this.nodes.Add(word, new GraphNode(word));
            }
            return this.nodes[word];
        }

        public List<GraphNode> FindPathAStar(GraphNode start, GraphNode goal)
        {
            // The set of nodes already evaluated
            var closedSet = new List<GraphNode>(this.nodes.Count);
            // The set of currently discovered nodes that are not evaluated yet.
            // Initially, only the start node is known.
            var openSet = new List<GraphNode>(this.nodes.Count) {start};

            // For each node, which node it can most efficiently be reached from.
            // If a node can be reached from many nodes, cameFrom will eventually contain the
            // most efficient previous step.
            var cameFrom = new Dictionary<GraphNode, GraphNode>(this.nodes.Count);

            // For each node, the cost of getting from the start node to that node.
            var gScore = new Dictionary<GraphNode, int>(this.nodes.Count);
            this.nodes.Values.Map((node) => gScore[node] = int.MaxValue);

            // The cost of going from start to start is zero.
            gScore[start] = 0;

            // For each node, the total cost of getting from the start node to the goal
            // by passing by that node. That value is partly known, partly heuristic.
            var fScore = new Dictionary<GraphNode, int>(this.nodes.Count);
            this.nodes.Values.Map((node) => fScore[node] = int.MaxValue);

            // For the first node, that value is completely heuristic.
            fScore[start] = start.HeuriDist(goal);

            while (openSet.Count > 0)
            {
                // node having the lowest fScore value (approx closest one)
                var current = openSet.SelectMin((item) => fScore[item]);

                if (current == goal) // we have reached our goal
                {
                    var path = new List<GraphNode>() {current};
                    while (cameFrom.ContainsKey(current))
                    {
                        current = cameFrom[current];
                        path.Insert(0, current);
                    }
                    return path;
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (var neighbor in current.Links)
                {
                    if (closedSet.Contains(neighbor))
                    {

                        // already evaluated
                        continue;
                    }
                    if (!openSet.Contains(neighbor))
                    {
                        // new node
                        openSet.Add(neighbor);
                    }
                    // The distance from start to a neighbor
                    var tentativeGScore = gScore[current] + current.DistTo(neighbor);
                    if (tentativeGScore >= gScore[neighbor])
                    {
                        continue; // This is not a better path.
                    }
                    // This path is the best until now. Record it!
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + neighbor.HeuriDist(goal);
                }
            }

            return null;
        }

        public List<GraphNode> BruteSinglePass(GraphNode start, GraphNode goal, out int minDistance)
        {
            var usedNodes = new HashSet<GraphNode>();

            minDistance = int.MaxValue;
            var result = BruteRecurse(start, goal, usedNodes, ref minDistance);
            return result;
        }

        private static List<GraphNode> BruteRecurse(GraphNode start, GraphNode goal, HashSet<GraphNode> usedNodes,
            ref int minDistance)
        {
            List<GraphNode> result = null;
            if (start == goal)
            {
                return new List<GraphNode> {start};
            }
            var nodes = new HashSet<GraphNode>(usedNodes) {start};
            var curDistance = minDistance == int.MaxValue ? 0 : minDistance;
            foreach (var neighbor in start.Links)
            {
                if (nodes.Contains(neighbor))
                {
                    continue;
                }
                var furtherDist = curDistance + start.DistTo(neighbor);
                var scan = BruteRecurse(neighbor, goal, nodes, ref furtherDist);
                if (scan != null && furtherDist < minDistance)
                {
                    result = new List<GraphNode> {start};
                    result.AddRange(scan);
                    minDistance = furtherDist;
                }
            }
            return result;
        }

        public void Map(Action<GraphNode> mapper)
        {
            this.nodes.Values.Map(mapper);
        }
    }

    public static class ReduceHelper
    {
        public static void Map<T>(this IEnumerable<T> enums, Action<T> mapper)
        {
            foreach (var entry in enums)
            {
                mapper(entry);
            }
        }

        public static TU Reduce<T, TU>(this IEnumerable<T> enums, TU init, Func<T, TU, TU> reducer)
        {
            foreach (var entry in enums)
            {
                init = reducer(entry, init);
            }
            return init;
        }

        public static T SelectMin<T>(this IEnumerable<T> enums, Func<T, double> metric)
        {
            var minValue = double.MaxValue;
            var minItem = default(T);
            foreach (var item in enums)
            {
                var current = metric(item);
                if (!(current < minValue)) continue;
                minItem = item;
                minValue = current;
            }
            return minItem;
        }

        public static T SelectMax<T>(this IEnumerable<T> enums, Func<T, double> metric)
        {
            var maxValue = double.MinValue;
            var maxItem = default(T);
            foreach (var item in enums)
            {
                var current = metric(item);
                if (!(current > maxValue)) continue;
                maxItem = item;
                maxValue = current;
            }
            return maxItem;
        }
    }


}