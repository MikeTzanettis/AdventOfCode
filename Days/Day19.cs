using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace AdventOfCode
{
    public class Day19
    {
        private static List<string> _designs = new List<string>();
        public static int Solve1(string input)
        {
            string[] lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> targets = new List<string>();

            string[] firstLineItems = lines[0].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in firstLineItems)
            {
                _designs.Add(item.Trim());
            }

            for (int i = 1; i < lines.Length; i++)
            {
                targets.Add(lines[i].Trim());
            }


            var possibleTargets = 0;
            
            foreach (var target in targets)
            {
                if (IsDesignPossible("", target))
                {
                    possibleTargets++;
                }
            }
            
            return 0;
        }
        static bool IsDesignPossible(string candidate, string target)
        {
            if (candidate == target)
            {
                return true;
            }

            for (int i = 0; i < _designs.Count; i++)
            {
                var start = candidate + _designs[i];
                if (target.StartsWith(start) && IsDesignPossible(start, target))
                {
                    return true;
                }
            }

            return false;
            //var startIndex = 0;
            //var candidate = string.Empty;
            //for (var i = 0; i < designs.Count; i++)
            //{
            //    var design = designs[i];

            //    if (candidate == string.Empty)
            //    {
            //        startIndex = i;
            //    }

            //    if (target.StartsWith(string.Concat(candidate, design)))
            //    {
            //        candidate += design;

            //        if (candidate == target)
            //        {
            //            return true;
            //        }
            //        i = -1;
            //    }

            //    if (i == designs.Count - 1)
            //    {
            //        i = startIndex;
            //        candidate = string.Empty;
            //    }
            //}

            //return false;
        }
        public static int Solve2(string input)
        {
            return 0;
        }
    }
}
