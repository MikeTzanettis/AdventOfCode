using System;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode
{
    public class Day12
    {
        private static char[,] _chars;
        private static List<Tuple<int, int>> _directions = new List<Tuple<int, int>>
            {
                Tuple.Create(0, 1),
                Tuple.Create(1, 0),
                Tuple.Create(0, -1),
                Tuple.Create(-1, 0)
            };
        private static Graph _graph;
        public static int Solve1(string input)
        {
            _chars = ConvertStringTo2DCharArray(input);
            var graph = ConstructGraph();

            var visited = new List<Tuple<int, int>>();
            int total = 0;
            for (int i = 0; i < _chars.GetLength(0); i++)
            {
                for (int j = 0; j < _chars.GetLength(1); j++)
                {
                    if (visited.Any(x => x.Item1 == i && x.Item2 == j))
                    {
                        continue;
                    }
                    Queue<(int row, int col, char letter)> queue = new Queue<(int, int, char)>();
                    
                    queue.Enqueue((i, j, _chars[i, j]));
                    int area = 0;
                    int perimeter = 0;
                    while (queue.Count > 0)
                    {
                        var (row, col, letter) = queue.Dequeue();
                        visited.Add(Tuple.Create(row, col));
                        var neighbors = graph.GetNeighbors(Tuple.Create(row, col));
                        foreach (var neighbor in neighbors)
                        {
                            if (letter == _chars[neighbor.Item1, neighbor.Item2] && 
                                !visited.Any(x => x.Item1 == neighbor.Item1 && x.Item2 == neighbor.Item2) &&
                                !queue.Any(x => x.row == neighbor.Item1 && x.col == neighbor.Item2))
                            {
                                queue.Enqueue((neighbor.Item1, neighbor.Item2, _chars[neighbor.Item1, neighbor.Item2]));
                            }
                        }

                        area++;
                        int numOfTouches = 0;
                        foreach (var dir in _directions)
                        {
                            var newRowDir = row + dir.Item1;
                            var newColDir = col + dir.Item2;
                            if ((newRowDir >= 0 && newRowDir <= _chars.GetLength(0) - 1 && newColDir >= 0 && newColDir <= _chars.GetLength(1) - 1 )
                                && letter == _chars[newRowDir,newColDir])
                            {
                                numOfTouches++;
                            }
                        }
                        perimeter += (4 - numOfTouches);
                    }

                    total += area * perimeter;
                }
            }
            return 0;
        }

        public static int Solve2(string input)
        {
            _chars = ConvertStringTo2DCharArray(input);
            var graph = ConstructGraph();
            List<Tuple<int,int>> lst = new List<Tuple<int, int>> { Tuple.Create(1,2), Tuple.Create(2,2), Tuple.Create(2,3), Tuple.Create(3,3) };
            var gaps = 0;
            var lookUpTable = new Dictionary<Tuple<int, int>, List<Tuple<int, int>>>();
            foreach (var el in lst)
            {
                var neighbours = graph.GetNeighbors(el);
                var gapList = new List<Tuple<int, int>>();
                foreach (var neighbor in neighbours)
                {
                    var isValid = true;
                    foreach (var kvp in lookUpTable)
                    {
                        var node = kvp.Key;
                        foreach (var gap in kvp.Value)
                        {
                            if (node.Item1 == el.Item1 && gap.Item2 == neighbor.Item2 && Math.Abs(gap.Item1 - neighbor.Item1) == 1 && Math.Abs(node.Item1 - el.Item1) == 1)
                            {
                                isValid = false;
                            }
                        }
                    }
                    if (_chars[neighbor.Item1, neighbor.Item2] != 'C' && isValid)
                    {
                        gaps++;
                        gapList.Add(Tuple.Create(neighbor.Item1, neighbor.Item2));
                    }
                }
                lookUpTable.Add(el, gapList);
            }
            return 0;
        }

        static char[,] ConvertStringTo2DCharArray(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int rows = lines.Length;
            int cols = lines[0].Length;

            char[,] grid = new char[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    grid[i, j] = lines[i][j];
                }
            }

            return grid;
        }

        static Graph ConstructGraph()
        {
            var graph = new Graph();

            for (int i = 0; i < _chars.GetLength(0); i++)
            {
                for (int j = 0; j < _chars.GetLength(1); j++)
                {
                    var node = Tuple.Create(i, j);
                    foreach (var dir in _directions)
                    {
                        var newRow = i + dir.Item1;
                        var newCol = j + dir.Item2;
                        var newTuple = Tuple.Create(i, j);
                        if (newRow >= 0 && newRow <= _chars.GetLength(0) - 1 && newCol >= 0 && newCol <= _chars.GetLength(1) - 1)
                        {

                            graph.AddEdge(newTuple, newRow, newCol);
                        }
                    }
                }
            }

            return graph;
        }
        private class Graph
        {
            Dictionary<Tuple<int, int>, List<Tuple<int, int>>> edges = new Dictionary<Tuple<int, int>, List<Tuple<int, int>>>();

            public void AddEdge(Tuple<int, int> u, int v, int w)
            {
                if (!edges.ContainsKey(u))
                    edges.Add(u, new List<Tuple<int, int>>());

                edges[u].Add(new Tuple<int, int>(v, w));
            }

            public List<Tuple<int, int>> GetNeighbors(Tuple<int, int> u)
            {
                return edges[u];
            }
        }
    }
}
