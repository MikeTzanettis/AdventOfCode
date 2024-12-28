using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day18
    {
        private static char[,] _grid = new char[71, 71];
        private static readonly int _numOfBytes = 1024;
        private static List<Tuple<int, int>> _directions = new List<Tuple<int, int>>
            {
                Tuple.Create(0, -1),
                Tuple.Create(0, 1),
                Tuple.Create(-1, 0),
                Tuple.Create(1, 0),
            };

        public int Solve1(string input)
        {
            string[] lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            SimulateBytes(_numOfBytes, lines);

            Queue<(int row, int col, int distance)> queue = new Queue<(int, int, int)>();
            bool[,] visited = new bool[_grid.GetLength(0), _grid.GetLength(1)];
            queue.Enqueue((0, 0, 0));
            visited[0,0] = true;
            var shortestDistance = -1;
            while (queue.Count > 0)
            {
                var (row, col, currentDistance) = queue.Dequeue();

                if (row == _grid.GetLength(0) - 1 && col == _grid.GetLength(1) - 1)
                {
                    shortestDistance = currentDistance;
                }

                foreach (var direction in _directions)
                {
                    var newRow = row + direction.Item1;
                    var newCol = col + direction.Item2;

                    if (newRow >= 0 
                        && newRow <= _grid.GetLength(0) - 1 
                        && newCol >= 0 
                        && newCol <= _grid.GetLength(1) - 1
                        && _grid[newRow, newCol] == '.' 
                        && !visited[newRow, newCol])
                    {
                        queue.Enqueue((newRow, newCol, currentDistance + 1));
                        visited[newRow, newCol] = true;
                    }
                }
            }
            return shortestDistance;
        }

        public int Solve2(string input)
        {
            string[] lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);


            int shortestDistance;

            for (int bytes = _numOfBytes; bytes < lines.Length; bytes++)
            {
                string coordinates = lines[bytes - 1];
                shortestDistance = -1;
                ClearCharGrid();
                SimulateBytes(bytes, lines);
                if (coordinates == "6,1")
                {
                    PrintGrid();
                }
                
                Queue<(int row, int col, int distance)> queue = new Queue<(int, int, int)>();
                bool[,] visited = new bool[_grid.GetLength(0), _grid.GetLength(1)];
                queue.Enqueue((0, 0, 0));
                visited[0, 0] = true;

                while (queue.Count > 0)
                {
                    var (row, col, currentDistance) = queue.Dequeue();

                    if (row == _grid.GetLength(0) - 1 && col == _grid.GetLength(1) - 1)
                    {
                        shortestDistance = currentDistance;
                    }

                    foreach (var direction in _directions)
                    {
                        var newRow = row + direction.Item1;
                        var newCol = col + direction.Item2;

                        if (newRow >= 0
                            && newRow <= _grid.GetLength(0) - 1
                            && newCol >= 0
                            && newCol <= _grid.GetLength(1) - 1
                            && _grid[newRow, newCol] == '.'
                            && !visited[newRow, newCol])
                        {
                            queue.Enqueue((newRow, newCol, currentDistance + 1));
                            visited[newRow, newCol] = true;
                        }
                    }
                }

                if (shortestDistance == -1)
                {
                    break;
                }
            }

            return 0;
        }

        private void SimulateBytes(int numOfBytes, string[] lines)
        {
            for (int i = 0; i < numOfBytes; i++)
            {
                var coordinates = lines[i].Split(',');
                int cord_X = int.Parse(coordinates[0]);
                int cord_Y = int.Parse(coordinates[1]);
                _grid[cord_Y, cord_X] = '#';
            }

            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = 0; j < _grid.GetLength(1); j++)
                {
                    if (_grid[i, j] != '#')
                    {
                        _grid[i, j] = '.';
                    }
                }
            }
        }
        private void ClearCharGrid()
        {
            int rows = _grid.GetLength(0);
            int cols = _grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    _grid[i, j] = ' ';
                }
            }
        }
        static int PrintGrid()
        {
            int counter = 0;
            for (int row = 0; row < _grid.GetLength(0); row++)
            {
                for (int col = 0; col < _grid.GetLength(1); col++)
                {
                    Console.Write(_grid[row, col] + " ");
                }
                Console.WriteLine();
            }

            return counter;
        }
    }
}
