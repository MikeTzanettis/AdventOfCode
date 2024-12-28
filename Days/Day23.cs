using System.Collections.Generic;
using System;
using System.Linq;

namespace AdventOfCode
{
    public class Day23
    {
        private static readonly List<string> _triangles = new List<string>();
        private static readonly List<List<string>> _cliques = new List<List<string>>();
        static int _maxCliqueSize = 0;
        private static List<string> _maxClique = new List<string>();

        public int Solve1(string input)
        {
            string[] lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            Graph graph = new Graph();
            foreach (var line in lines)
            {
                var edge = line.Split('-');
                graph.AddEdge(edge[0], edge[1]);
            }
            graph.FindTriangles();
            var numOfTriangles = _triangles.Where(x => x.Split('-').Any(c=>c.StartsWith("t"))).Count();
            return 0;
        }

        public int Solve2(string input)
        {
            string[] lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            Graph graph = new Graph();
            foreach (var line in lines)
            {
                var edge = line.Split('-');
                graph.AddEdge(edge[0], edge[1]);
            }

            List<string> currentClique = new List<string>();
            List<string> nodes = new List<string>(graph._adjacencyMatrix.Keys);

            graph.FindMaxClique(0,nodes,currentClique);
            var password = string.Join(",", _maxClique.OrderBy(x=>x).ToList());
            return 0;
        }

        private class Graph
        {
            public readonly Dictionary<string, List<string>> _adjacencyMatrix;

            public Graph()
            {
                _adjacencyMatrix = new Dictionary<string, List<string>>();
            }

            public void AddEdge(string node1, string node2)
            {
                if (!_adjacencyMatrix.ContainsKey(node1))
                    _adjacencyMatrix[node1] = new List<string>();

                if (!_adjacencyMatrix.ContainsKey(node2))
                    _adjacencyMatrix[node2] = new List<string>();

                _adjacencyMatrix[node1].Add(node2);
                _adjacencyMatrix[node2].Add(node1);
            }

            public void FindTriangles()
            {
                foreach (var u in _adjacencyMatrix.Keys)
                {
                    foreach (var v in _adjacencyMatrix[u])
                    {
                        foreach (var w in _adjacencyMatrix[v])
                        {
                            if (w != u && _adjacencyMatrix[u].Contains(w))
                            {
                                var triangle = new List<string> { u, v, w };
                                triangle.Sort();
                                var triangleIdentifier = string.Join("-", triangle);

                                if (!_triangles.Contains(triangleIdentifier))
                                {
                                    _triangles.Add(triangleIdentifier);
                                }
                            }
                        }
                    }
                }
            }
            public void FindMaxClique(int currentIndex, List<string> nodes, List<string> currentClique)
            {
                if (currentIndex == nodes.Count)
                {
                    if (currentClique.Count > _maxCliqueSize)
                    {
                        _maxCliqueSize = currentClique.Count;
                        _maxClique = new List<string>(currentClique);
                    }
                    return;
                }

                string currentNode = nodes[currentIndex];

                if (IsClique(currentClique, currentNode))
                {
                    currentClique.Add(currentNode);
                    FindMaxClique(currentIndex + 1, nodes, currentClique);
                    currentClique.RemoveAt(currentClique.Count - 1);
                }

                FindMaxClique(currentIndex + 1, nodes, currentClique);
            }

            bool IsClique(List<string> currentClique, string node)
            {
                foreach (string v in currentClique)
                {
                    if (!_adjacencyMatrix[v].Contains(node))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
