using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day8
    {
        public static int Solve1(string input)
        {
            var grid = ConvertStringTo2DCharArray(input);

            Dictionary<char, List<Tuple<int, int>>> mapper = new Dictionary<char, List<Tuple<int, int>>>();

            var uniqueAntiNodes = 0;
            var overlaps = new List<Tuple<int, int>>();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == '.' || grid[i,j] == '#') continue;

                    if (!mapper.ContainsKey(grid[i, j]))
                    {
                        mapper[grid[i, j]] = new List<Tuple<int, int>> { Tuple.Create(i, j) };
                        continue;
                    }

                    var prevAntennas = mapper[grid[i, j]];
                    var antenna = Tuple.Create(i, j);

                    foreach (var prevAntenna in prevAntennas)
                    {
                        var antiNode1Row = prevAntenna.Item1 - (antenna.Item1 - prevAntenna.Item1);
                        var antiNode1Col = prevAntenna.Item2 - (antenna.Item2 - prevAntenna.Item2);

                        if (0 <= antiNode1Row && antiNode1Row < grid.GetLength(0) && 0 <= antiNode1Col && antiNode1Col < grid.GetLength(1))
                        {
                            if (grid[antiNode1Row, antiNode1Col] != '#')
                            {
                                if (grid[antiNode1Row, antiNode1Col] == '.')
                                {
                                    grid[antiNode1Row, antiNode1Col] = '#';
                                    uniqueAntiNodes++;
                                }
                                else
                                {
                                    if (!overlaps.Any(x => x.Item1 == antiNode1Row && x.Item2 == antiNode1Col))
                                    {
                                        overlaps.Add(Tuple.Create(antiNode1Row, antiNode1Col));
                                        uniqueAntiNodes++;
                                    }

                                }
                            }
                        }


                        var antiNode2Row = antenna.Item1 + (antenna.Item1 - prevAntenna.Item1);
                        var antiNode2Col = antenna.Item2 + (antenna.Item2 - prevAntenna.Item2);

                        if (0 <= antiNode2Row && antiNode2Row < grid.GetLength(0) && 0 <= antiNode2Col && antiNode2Col < grid.GetLength(1))
                        {
                            if (grid[antiNode2Row, antiNode2Col] != '#')
                            {
                                if (grid[antiNode2Row, antiNode2Col] == '.')
                                {
                                    grid[antiNode2Row, antiNode2Col] = '#';
                                    uniqueAntiNodes++;
                                }
                                else
                                {
                                    if (!overlaps.Any(x => x.Item1 == antiNode2Row && x.Item2 == antiNode2Col))
                                    {
                                        overlaps.Add(Tuple.Create(antiNode2Row, antiNode2Col));
                                        uniqueAntiNodes++;
                                    }
                                }
                            }
                        }
                    }

                    mapper[grid[i, j]].Add(Tuple.Create(i, j));
                }
            }

            return uniqueAntiNodes;
        }

        public static int Solve2(string input)
        {
            var grid = ConvertStringTo2DCharArray(input);

            Dictionary<char, List<Tuple<int, int>>> mapper = new Dictionary<char, List<Tuple<int, int>>>();

            var uniqueAntiNodes = 0;
            var overlaps = new List<Tuple<int, int>>();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == '.' || grid[i, j] == '#') continue;

                    if (!mapper.ContainsKey(grid[i, j]))
                    {
                        mapper[grid[i, j]] = new List<Tuple<int, int>> { Tuple.Create(i, j) };
                        continue;
                    }

                    var prevAntennas = mapper[grid[i, j]];
                    var antenna = Tuple.Create(i, j);
                    
                    foreach (var prevAntenna in prevAntennas)
                    {
                        var antiNode1Row = prevAntenna.Item1 - (antenna.Item1 - prevAntenna.Item1);
                        var antiNode1Col = prevAntenna.Item2 - (antenna.Item2 - prevAntenna.Item2);

                        while (0 <= antiNode1Row && antiNode1Row < grid.GetLength(0) && 0 <= antiNode1Col && antiNode1Col < grid.GetLength(1))
                        {
                            if (grid[antiNode1Row, antiNode1Col] != '#')
                            {
                                if (grid[antiNode1Row, antiNode1Col] == '.')
                                {
                                    grid[antiNode1Row, antiNode1Col] = '#';
                                    uniqueAntiNodes++;
                                }
                                else
                                {

                                        overlaps.Add(Tuple.Create(antiNode1Row, antiNode1Col));
                                        uniqueAntiNodes++;
                                    
                                }
                            }

                            antiNode1Row = antiNode1Row - (antenna.Item1 - prevAntenna.Item1);
                            antiNode1Col = antiNode1Col - (antenna.Item2 - prevAntenna.Item2);
                        }


                        var antiNode2Row = antenna.Item1 + (antenna.Item1 - prevAntenna.Item1);
                        var antiNode2Col = antenna.Item2 + (antenna.Item2 - prevAntenna.Item2);

                        while (0 <= antiNode2Row && antiNode2Row < grid.GetLength(0) && 0 <= antiNode2Col && antiNode2Col < grid.GetLength(1))
                        {
                            if (grid[antiNode2Row, antiNode2Col] != '#')
                            {
                                if (grid[antiNode2Row, antiNode2Col] == '.')
                                {
                                    grid[antiNode2Row, antiNode2Col] = '#';
                                    uniqueAntiNodes++;
                                }
                                else
                                {

                                        overlaps.Add(Tuple.Create(antiNode2Row, antiNode2Col));
                                        uniqueAntiNodes++;
                                    
                                }
                            }

                            antiNode2Row = antiNode2Row + (antenna.Item1 - prevAntenna.Item1);
                            antiNode2Col = antiNode2Col + (antenna.Item2 - prevAntenna.Item2);
                        }
                    }
                    mapper[grid[i, j]].Add(Tuple.Create(i, j));
                }
            }

            int total = PrintGrid(grid);

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

        static int PrintGrid(char[,] grid)
        {
            int counter = 0;
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    if (grid[row, col] != '.')
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }
    }
}
