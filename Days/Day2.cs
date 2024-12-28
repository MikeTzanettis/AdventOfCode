using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day2
    {
        public static int Solve1(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var safeLevels = 0;
            foreach (var line in lines)
            {
                var lst = line.Split(' ').Select(Int32.Parse)?.ToList();
                var isSafe = IsLineSafe(lst);

                if (isSafe)
                {
                    safeLevels += 1;
                }
            }
            return safeLevels;
        }

        public static int Solve2(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var safeLevels = 0;
            foreach (var line in lines)
            {
                var lst = line.Split(' ').Select(Int32.Parse)?.ToList();
                if (IsLineSafe(lst))
                {
                    safeLevels++;
                    continue;
                }
                for (var i = 0; i < lst.Count; i++)
                {
                    List<int> newList = lst.Where((item, index) => index != i).ToList();
                    if (IsLineSafe(newList))
                    {
                        safeLevels++;
                        break;
                    }
                }
            }
            return safeLevels;
        }

        private static bool IsLineSafe(List<int> line)
        {
            var isIncreasing = false;
            var isDecreasing = false;
            var isSafe = true;

            for (int i = 1; i < line.Count; i++)
            {
                if (line[i] > line[i - 1])
                {
                    isIncreasing = true;
                }
                else
                {
                    isDecreasing = true;
                }

                if (isIncreasing && isDecreasing || Math.Abs(line[i] - line[i - 1]) > 3 || Math.Abs(line[i] - line[i - 1]) < 1)
                {
                    isSafe = false;
                    i = line.Count;
                }
            }

            return isSafe;
        }
    }
}
