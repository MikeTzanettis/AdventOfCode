using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    public class Day1
    {
        private List<int> _orderedList1;
        private List<int> _orderedList2;
        public int Solve1(string input)
        {
            ConstructLists(input);

            var sum = 0;

            for(int i = 0; i< _orderedList1.Count; i++)
            {
                var distance = Math.Abs(_orderedList1[i] - _orderedList2[i]);
                sum += distance;
            }

            return sum;
        }

        public int Solve2(string input)
        {
            ConstructLists(input);

            var sum = 0;

            foreach (var item in _orderedList1)
            {
                var occurences = _orderedList2.Count(x => x == item);
                sum += occurences * item;
            }
            return sum;
        }

        private void ConstructLists(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var lst1 = new List<int>();
            var lst2 = new List<int>();

            foreach (var line in lines)
            {
                lst1.Add(int.Parse(line.Split(new[] { "  " }, StringSplitOptions.None)[0]));
                lst2.Add(int.Parse(line.Split(new[] { "  " }, StringSplitOptions.None)[1]));
            }

            _orderedList1 = lst1.OrderBy(x => x).ToList();
            _orderedList2 = lst2.OrderBy(x => x).ToList();
        }
    }
}
