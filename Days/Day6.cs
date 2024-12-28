using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day6
    {
        public static int Solve1(string input)
        {
            char[,] grid = ConvertStringTo2DCharArray(input);

            var visitedPositions = new List<Tuple<int, int>>();
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            Tuple<int,int> current_pos = Tuple.Create(0,0);
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == '^')
                    {
                        current_pos = Tuple.Create(i, j);
                    }
                }
            }
            
            visitedPositions.Add(current_pos);
            var direction = "Up";
            while (true)
            {
                if (current_pos.Item1 == 0 || current_pos.Item2 == 0 || current_pos.Item1 == rows - 1 || current_pos.Item2 == cols - 1)
                    break;

                
                if (direction == "Up")
                {
                    try
                    {
                        while (grid[current_pos.Item1 - 1, current_pos.Item2] != '#')
                        {
                            if (!visitedPositions.Any(x => x.Item1 == current_pos.Item1 - 1 && x.Item2 == current_pos.Item2))
                                visitedPositions.Add(Tuple.Create(current_pos.Item1 - 1, current_pos.Item2));

                            current_pos = Tuple.Create(current_pos.Item1 - 1, current_pos.Item2);
                        }
                    }
                    catch 
                    {
                        break;
                    }
                }

                if (direction == "Right")
                {   try
                    {
                        while (grid[current_pos.Item1, current_pos.Item2 + 1] != '#')
                        {
                            if (!visitedPositions.Any(x => x.Item1 == current_pos.Item1 && x.Item2 == current_pos.Item2 + 1))
                                visitedPositions.Add(Tuple.Create(current_pos.Item1, current_pos.Item2 + 1));

                            current_pos = Tuple.Create(current_pos.Item1, current_pos.Item2 + 1);
                        }
                    }
                    catch
                    {
                        break;
                    }
                }

                if (direction == "Down")
                {
                    try
                    {
                        while (grid[current_pos.Item1 + 1, current_pos.Item2] != '#')
                        {
                            if (!visitedPositions.Any(x => x.Item1 == current_pos.Item1 + 1 && x.Item2 == current_pos.Item2))
                                visitedPositions.Add(Tuple.Create(current_pos.Item1 + 1, current_pos.Item2));

                            current_pos = Tuple.Create(current_pos.Item1 + 1, current_pos.Item2);
                        }
                    }
                    catch { break; }
                }

                if (direction == "Left")
                {
                    try
                    {
                        while (grid[current_pos.Item1, current_pos.Item2 - 1] != '#')
                        {
                            if (!visitedPositions.Any(x => x.Item1 == current_pos.Item1 && x.Item2 == current_pos.Item2 - 1))
                                visitedPositions.Add(Tuple.Create(current_pos.Item1, current_pos.Item2 - 1));

                            current_pos = Tuple.Create(current_pos.Item1, current_pos.Item2 - 1);
                        }
                    }
                    catch { break; }
                }

                direction = GetDirection(direction);
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
        static string GetDirection(string direction)
        {
            switch (direction)
            {
                case "Up":
                    return "Right";
                case "Right":
                    return "Down";
                case "Down":
                    return "Left";
                case "Left":
                    return "Up";

            }

            return direction;
        }
        public static int Solve2(string input)
        {
            char[,] grid = ConvertStringTo2DCharArray(input);

            var visitedPositions = new List<Tuple<int, int,string>>();
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            Tuple<int, int> current_pos = Tuple.Create(0, 0);
            Tuple<int, int> starting_pos = Tuple.Create(0, 0);
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == '^')
                    {
                        current_pos = Tuple.Create(i, j);
                        starting_pos = Tuple.Create(i, j);
                    }
                }
            }

            var direction = "Up";
            
            var cycles = 0;
            for (int i = 0; i< rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    grid[i, j] = '%';
                    current_pos = starting_pos;
                    visitedPositions.Clear();
                    visitedPositions.Add(Tuple.Create(current_pos.Item1, current_pos.Item2, direction));
                    while (true)
                    {
                        var hasEscaped = false;  
                        var foundCycle = false;
                        if (direction == "Up")
                        {
                            try
                            {
                                
                                while (true)
                                {
                                    if (current_pos.Item1 == 0 || current_pos.Item2 == 0 || current_pos.Item1 == rows - 1 || current_pos.Item2 == cols - 1)
                                    {
                                        hasEscaped = true;
                                        break;
                                    }

                                    if (grid[current_pos.Item1 - 1, current_pos.Item2] == '#' || grid[current_pos.Item1 - 1, current_pos.Item2] == '%')
                                    {
                                        break;
                                    }
                                    var value = grid[current_pos.Item1 -1, current_pos.Item2];

                                    if (!visitedPositions.Any(x => x.Item1 == current_pos.Item1 - 1 && x.Item2 == current_pos.Item2 && x.Item3 == direction))
                                    {
                                        visitedPositions.Add(Tuple.Create(current_pos.Item1 - 1, current_pos.Item2, direction));
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Found Cycle, cycles: {cycles + 1}");
                                        cycles++;
                                        foundCycle = true;
                                        grid[i, j] = '.';
                                        break;
                                    }

                                    current_pos = Tuple.Create(current_pos.Item1 - 1, current_pos.Item2);
                                }

                                if (hasEscaped) break;

                                if (foundCycle)
                                {
                                    break;
                                }
                            }
                            catch(Exception ex)
                            {
                                
                            }
                        }

                        if (direction == "Right")
                        {
                            try
                            {
                                while (true)
                                {
                                    if (current_pos.Item1 == 0 || current_pos.Item2 == 0 || current_pos.Item1 == rows - 1 || current_pos.Item2 == cols - 1)
                                    {
                                        hasEscaped = true;
                                        break;
                                    }

                                    if (grid[current_pos.Item1, current_pos.Item2 + 1] == '#' || grid[current_pos.Item1, current_pos.Item2 + 1] == '%')
                                    {
                                        break;
                                    }

                                    if (!visitedPositions.Any(x => x.Item1 == current_pos.Item1 && x.Item2 == current_pos.Item2 + 1 && x.Item3 == direction))
                                    {
                                        visitedPositions.Add(Tuple.Create(current_pos.Item1, current_pos.Item2 + 1, direction));
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Found Cycle, cycles: {cycles + 1}");
                                        cycles++;
                                        foundCycle = true;
                                        grid[i, j] = '.';
                                        break;
                                    }

                                    current_pos = Tuple.Create(current_pos.Item1, current_pos.Item2 + 1);
                                }
                                if (hasEscaped) break;

                                if (foundCycle)
                                {
                                    break;
                                }
                            }
                            catch
                            {
                                break;
                            }
                        }

                        if (direction == "Down")
                        {
                            try
                            {
                                while (true)
                                {
                                    if (current_pos.Item1 == 0 || current_pos.Item2 == 0 || current_pos.Item1 == rows - 1 || current_pos.Item2 == cols - 1)
                                    {
                                        hasEscaped = true;
                                        break;
                                    }

                                    if (grid[current_pos.Item1 + 1, current_pos.Item2] == '#' || grid[current_pos.Item1 + 1, current_pos.Item2] == '%')
                                    {
                                        break;
                                    }
                                    if (!visitedPositions.Any(x => x.Item1 == current_pos.Item1 + 1 && x.Item2 == current_pos.Item2 && x.Item3 == direction))
                                    {
                                        visitedPositions.Add(Tuple.Create(current_pos.Item1 + 1, current_pos.Item2, direction));
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Found Cycle, cycles: {cycles + 1}");
                                        cycles++;
                                        foundCycle = true;
                                        grid[i, j] = '.';
                                        break;
                                    }

                                    current_pos = Tuple.Create(current_pos.Item1 + 1, current_pos.Item2);
                                }
                                if (hasEscaped) break;

                                if (foundCycle)
                                {
                                    break;
                                }
                            }
                            catch { break; }
                        }

                        if (direction == "Left")
                        {
                            try
                            {
                                while (true)
                                {
                                    if (current_pos.Item1 == 0 || current_pos.Item2 == 0 || current_pos.Item1 == rows - 1 || current_pos.Item2 == cols - 1)
                                    {
                                        hasEscaped = true;
                                        break;
                                    }

                                    if (grid[current_pos.Item1, current_pos.Item2 - 1] == '#' || grid[current_pos.Item1, current_pos.Item2 - 1] == '%')
                                    {
                                        break;
                                    }

                                    if (!visitedPositions.Any(x => x.Item1 == current_pos.Item1 && x.Item2 == current_pos.Item2 - 1 && x.Item3 == direction))
                                    {
                                        visitedPositions.Add(Tuple.Create(current_pos.Item1, current_pos.Item2 - 1, direction));
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Found Cycle, cycles: {cycles + 1}");
                                        cycles++;
                                        foundCycle = true;
                                        grid[i, j] = '.';
                                        break;
                                    }

                                    current_pos = Tuple.Create(current_pos.Item1, current_pos.Item2 - 1);
                                }
                                if (hasEscaped) break;

                                if (foundCycle)
                                {
                                    break;
                                }
                            }
                            catch { break; }
                        }

                        direction = GetDirection(direction);
                    }

                }

            }
            return 0;
        }
        static char[] GetColumn(char[,] array, int columnIndex)
        {
            int rowCount = array.GetLength(0); // Number of rows
            char[] column = new char[rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                column[i] = array[i, columnIndex];
            }

            return column;
        }
    }
}
