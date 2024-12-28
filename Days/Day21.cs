using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day21
    {
        private static Dictionary<char, Tuple<int, int>> _directions = new Dictionary<char, Tuple<int, int>>();


        private static readonly char[,] _directionalKeypad = {
            { '#', '^', 'A' },
            { '<', 'v', '>' }
        };
        private static readonly char[,] _numericKeypad = {
            { '7', '8', '9' },
            { '4', '5', '6' },
            { '1', '2', '3' },
            { '#', '0', 'A'}
        };
        public static string Solve1(string input)
        {
            var dirs = new List<char> { '>', '^', 'v', '<' };
            var permutations = GetPermutations(dirs, dirs.Count);
            _directions.Add('>', Tuple.Create(0, 1));
            _directions.Add('v', Tuple.Create(1, 0));
            _directions.Add('^', Tuple.Create(-1, 0));
            _directions.Add('<', Tuple.Create(0, -1));

            Tuple<int, int> start;
            var path = "";
            var count = int.MaxValue;
            var smallest_target = new List<char>();
            input = "508A";
            foreach (var directions in permutations)
            {
                var target = "508A".ToCharArray();
                for (int i = 0; i < 26; i++)
                {
                    path = "";
                    char[,] keyPad;

                    if (i == 0)
                    {
                        start = Tuple.Create(3, 2);
                        keyPad = _numericKeypad;
                    }
                    else
                    {
                        start = Tuple.Create(0, 2);
                        keyPad = _directionalKeypad;
                    }

                    foreach (var el in target)
                    {
                        var end = FindInGrid(keyPad, el);
                        path += FindShortestPath(start, end, keyPad,directions);
                        start = end;
                    }
                    target = path.ToCharArray();
                }

                if (target.Count() < count)
                {
                    smallest_target = target.ToList();
                    count = target.Count();
                }
            }

            int numericPart = int.Parse(new string(input.Where(char.IsDigit).ToArray()));
            var prod = numericPart * count;
            return path;
        }

        private static string FindShortestPath(Tuple<int,int> start, Tuple<int,int> end, char[,] keyPad,List<char> directions)
        {
            Queue<(int row, int col, string path)> queue = new Queue<(int, int, string)>();
            queue.Enqueue((start.Item1, start.Item2, ""));
            var visited = new List<Tuple<int, int>>
            {
                start
            };
            while (queue.Count > 0)
            {
                var (row, col, path) = queue.Dequeue();
                if (end.Item1 == row && end.Item2 == col)
                {
                    return path + 'A';
                }
                    
                foreach (var dir in directions)
                {
                    var newRow = row + _directions[dir].Item1;
                    var newCol = col + _directions[dir].Item2;

                    if (newRow >= 0 && 
                        newRow <= keyPad.GetLength(0) - 1 && 
                        newCol >= 0 && 
                        newCol <= keyPad.GetLength(1) - 1
                        && keyPad[newRow,newCol] != '#'
                        && !visited.Contains(Tuple.Create(newRow,newCol)))
                    {
                        queue.Enqueue((newRow, newCol, path + dir));
                        visited.Add(Tuple.Create(newRow, newCol));
                    }
                }
            }

            return "";
        }

        static List<List<T>> GetPermutations<T>(List<T> list, int length)
        {
            if (length == 1)
                return list.ConvertAll(item => new List<T> { item });

            var permutations = new List<List<T>>();
            foreach (var item in list)
            {
                var remaining = new List<T>(list);
                remaining.Remove(item);

                foreach (var perm in GetPermutations(remaining, length - 1))
                {
                    var newPerm = new List<T> { item };
                    newPerm.AddRange(perm);
                    permutations.Add(newPerm);
                }
            }

            return permutations;
        }

        private static Tuple<int, int> FindInGrid(char[,] grid, char el)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == el) return Tuple.Create(i, j);
                }
            }

            return null;
        }
        public static int Solve2(string input)
        {
            return 0;
        }
    }
}
