using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day5
    {
        public static int Solve1(string input)
        {
            string[] inputs = input.Split(new[] { '\r','\n' }, StringSplitOptions.RemoveEmptyEntries);
            var tuples = new List<Tuple<int, int>>();
            var lst = new List<List<int>>();
            foreach (var a in inputs)
            {
                if (a.Contains("|"))
                {
                    var b = a.Split('|');
                    tuples.Add(new Tuple<int,int>(int.Parse(b[0]), int.Parse(b[1])));
                }
                else
                {
                    List<int> intList = a
                        .Split(',')            
                        .Select(int.Parse)      
                        .ToList();

                    lst.Add(intList);
                }
            }
            var sum = 0;
            var unorderedLists = new List<List<int>>();
            foreach (var list in lst)
            {
                var isAccepted = true;
                for (int i = 0; i < list.Count; i++)
                {
                    var el = list[i];
                    
                    for (int j = i; j < list.Count; j++)
                    {
                        if(i == j) { continue; }

                        bool elFirst = true;
                        if(i > j) { elFirst = false; }

                        if (IsInvalid(tuples, list[i], list[j], elFirst))
                        {
                            var temp = list[i];
                            list[i] = list[j];
                            list[j] = temp;
                            isAccepted = false;
                        }

                    }
                   
                }
                if (!isAccepted)
                {
                    int middleIndex = list.Count / 2;
                    int middleElement = list[middleIndex];
                    sum += middleElement;
                }
            }



            return 0;
        }

        
        public static int Solve2(string input)
        {
            return 0;
        }

        private static bool IsInvalid(List<Tuple<int,int>> tuples, int el, int el2, bool elFirst)
        {
            foreach (var tuple in tuples)
            {
                if(tuple.Item2 == el && tuple.Item1 == el2)
                    { return true; }
            }

            return false;
        }
    }
}
