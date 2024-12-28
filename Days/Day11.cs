using System;
using System.Linq;
using System.Collections.Generic;
namespace AdventOfCode
{
    public class Day11
    {
        public static int Solve1(string input)
        {
            var nums = input.Split(' ').Select(long.Parse).ToList();
            Dictionary<long,long> stones = new Dictionary<long,long>();
            foreach (var num in nums)
            {
                stones.Add(num, 1);
            }
            for (int i = 0; i < 75; i++)
            {
                var newStones = new Dictionary<long, long>();
                foreach (var kvp in stones)
                {
                    GetStones(kvp.Key, kvp.Value, newStones);
                }

                stones = newStones;
            }
            var total = stones.Sum(x => x.Value);
            return 0;
        }

        static void GetStones(long num, long count,Dictionary<long,long> stones)
        {
            void FindCount(long key, long value)
            {
                if (stones.ContainsKey(key))
                {
                    stones[key] += value;
                }
                else
                {
                    stones.Add(key, value);
                }
            }
            var newStones = new List<long>();
            if (num == 0)
            {
                FindCount(1, count);
            }
            else if (num.ToString().Length % 2 == 0)
            {
                var mid = num.ToString().Length / 2;
                var first = long.Parse(num.ToString().Substring(0, mid));
                var second = long.Parse(num.ToString().Substring(mid));

                FindCount(first, count);
                FindCount(second, count);
            }
            else 
            {
                FindCount(num * 2024, count);
            }
        }
        static List<long> ApplyRuleForList(long number)
        {
            var num = number.ToString();
            return new List<long> { long.Parse(num.Substring(0, num.Length / 2)), long.Parse(num.Substring(num.Length / 2, num.Length / 2)) };
        }
        public static int Solve2(string input)
        {
            return 0;
        }
    }
}
