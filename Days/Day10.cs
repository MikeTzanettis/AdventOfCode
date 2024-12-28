using System.Collections.Generic;
using System;
using System.Linq;

namespace AdventOfCode
{
    public class Day10
    {
        private static char[,] _chars;
        private static List<Tuple<int, int>>  _directions = new List<Tuple<int, int>>
            {
                Tuple.Create(0, -1),
                Tuple.Create(0, 1),
                Tuple.Create(-1, 0),
                Tuple.Create(1, 0),
            };
        public static int Solve1(string input)
        {
            _chars = ConvertStringTo2DCharArray(input);
            int sum = 0;
            for (int i = 0; i < _chars.GetLength(0); i++)
            {
                for (int j = 0; j < _chars.GetLength(0); j++)
                {
                    if (int.Parse(_chars[i, j].ToString()) == 0)
                    {
                        var totalPaths = GetTotalPaths(Tuple.Create(i,j));
                        foreach (var path in totalPaths)
                        {
                            sum += path.Item2;
                        }
                        
                    }
                }
            }

            return 0;
        }

        public static int Solve2(string input)
        {
            return 0;
        }

        //private class Graph
        //{
        //    Dictionary<Tuple<int, int>, List<Tuple<int, int>>> edges = new Dictionary<Tuple<int, int>, List<Tuple<int, int>>>();

        //    public void AddEdge(Tuple<int, int> u, int v, int w)
        //    {
        //        if (!edges.ContainsKey(u))
        //            edges.Add(u, new List<Tuple<int, int>>());

        //        edges[u].Add(new Tuple<int, int>(v, w));
        //    }

        //    public List<Tuple<int, int>> GetNeighbors(Tuple<int, int> u)
        //    {
        //        return edges[u];
        //    }
        //}
        static List<Tuple<Tuple<int,int>,int>> GetTotalPaths(Tuple<int,int> start)
        {
            Queue<(int row, int col, int currentNumber)> queue = new Queue<(int, int, int)>();
            queue.Enqueue((start.Item1, start.Item2, int.Parse(_chars[start.Item1, start.Item2].ToString())));
            var ends = new List<Tuple<int,int>>();
            var numOfPathsList = new List<int>();
            var lst = new List<Tuple<Tuple<int, int>, int>>();
            while (queue.Count > 0)
            {
                var (row, col, currentNumber) = queue.Dequeue();

                if (currentNumber == 9)
                {
                    if (ends.Any(x => x.Item1 == row && x.Item2 == col))
                    {
                        var tuple = ends.Where(x => x.Item1 == row && x.Item2 == col).First();
                        var index = ends.IndexOf(tuple);
                        numOfPathsList[index] += 1;
                    }
                    else 
                    {
                        ends.Add(Tuple.Create(row, col));
                        numOfPathsList.Add(1);
                    }
                }

                foreach (var direction in _directions)
                {
                    var newRow = row + direction.Item1;
                    var newCol = col + direction.Item2;

                    if ((newRow >= 0 && newRow <= _chars.GetLength(0) - 1 && newCol >= 0 && newCol <= _chars.GetLength(1) - 1)
                        && currentNumber + 1 == int.Parse(_chars[newRow,newCol].ToString()))
                    {
                        queue.Enqueue((newRow, newCol, int.Parse(_chars[newRow, newCol].ToString())));
                    }
                }
            }

            for (int i = 0; i < ends.Count; i++)
            {
                lst.Add(Tuple.Create(ends[i], numOfPathsList[i]));
            }

            return lst;
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
    }
}

