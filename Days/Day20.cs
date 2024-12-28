using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace AdventOfCode
{
    public class Day20
    {
        private static (int, int) _start;
        private static (int, int) _end;
        private static char[,] _grid;
        private static readonly int _maxCheatPicoSeconds = 2;
        private static Dictionary<(int, int), int> _picoMap = new Dictionary<(int, int), int>();
        private static readonly List<Tuple<int, int>> _directions = new List<Tuple<int, int>>
            {
                Tuple.Create(0, 1),
                Tuple.Create(1, 0),
                Tuple.Create(0, -1),
                Tuple.Create(-1, 0)
            };
        public static int Solve1(string input)
        {
            ConvertStringTo2DCharArray(input);

            InitializePositions();

            InitializePicoMap();

            var current_pos = _start;
            var c = 1;
            var cheats = 0;
            while (true)
            {
                foreach (var direction in _directions)
                {
                    var newRow = current_pos.Item1 + direction.Item1;
                    var newCol = current_pos.Item2 + direction.Item2;
                    if (_grid[newRow, newCol] == '#')
                    {
                        foreach (var dir in _directions)
                        {
                            var newWallRow = newRow + dir.Item1;
                            var newWallCol = newCol + dir.Item2;

                            if (newWallRow < 0
                                || newWallRow > _grid.GetLength(0) - 1
                                || newWallCol < 0
                                || newWallCol > _grid.GetLength(1) - 1
                                || (newWallRow, newWallCol) == current_pos)
                            {
                                continue;
                            }

                            if (_grid[newWallRow, newWallCol] == '.'
                                || _grid[newWallRow, newWallCol] == 'E')
                            {
                                var cheatPos = (newWallRow, newWallCol);
                                var currentTrackPos = _picoMap[current_pos];
                                var cheatTrackPos = _picoMap[cheatPos];

                                var skipPicos = cheatTrackPos - currentTrackPos - 2;

                                if (skipPicos >= 100)
                                {
                                    cheats++;
                                }
                            }
                        }
                    }
                }
                

                if (c == _picoMap.Count())
                    break;

                current_pos = _picoMap.FirstOrDefault(kvp => kvp.Value == c).Key;
                c++;
            }
            return 0;
        }

        private static void InitializePicoMap()
        {
            var current_pos = _start;
            var pico = 0;
            _picoMap.Add(current_pos, pico);

            var hasEnded = false;

            while (true)
            {
                if (hasEnded)
                    break;

                foreach (var dir in _directions)
                {
                    var newRow = current_pos.Item1 + dir.Item1;
                    var newCol = current_pos.Item2 + dir.Item2;

                    if ((newRow, newCol) == _end)
                    {
                        hasEnded = true;
                        pico++;
                        _picoMap.Add(_end, pico);
                        break;
                    }

                    if (_grid[newRow, newCol] == '.'
                        && (newRow, newCol) != current_pos
                        && !_picoMap.ContainsKey((newRow, newCol)))
                    {
                        pico++;
                        _picoMap.Add((newRow, newCol), pico);
                        current_pos = (newRow, newCol);
                    }
                }
            }
        }
        private static void InitializePositions()
        {
            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = 0; j < _grid.GetLength(1); j++)
                { 
                    if (_grid[i, j] == 'S')
                    {
                        _start = (i, j);
                    }

                    if (_grid[i, j] == 'E')
                    {
                        _end = (i, j);
                    }
                }
            }
        }

        public static int Solve2(string input)
        {
            return 0;
        }

        static void ConvertStringTo2DCharArray(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int rows = lines.Length;
            int cols = lines[0].Length;

            _grid = new char[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    _grid[i, j] = lines[i][j];
                }
            }
        }
    }
}
