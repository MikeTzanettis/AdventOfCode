using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace AdventOfCode
{
    public class Day22
    {
        class ListComparer : IEqualityComparer<List<int>>
        {
            public bool Equals(List<int> x, List<int> y)
            {

                if (x == null || y == null)
                    return x == y;

                return x.SequenceEqual(y);
            }

            public int GetHashCode(List<int> obj)
            {
                if (obj == null) return 0;

                return obj.Aggregate(0, (hash, item) => hash ^ item.GetHashCode());
            }
        }
        public static int Solve1(string input)
        {
            List<long> nums = input.Split('\n').Select(x => long.Parse(x)).ToList();
            Dictionary<long,List<Tuple<int, List<int>>>> map = new Dictionary<long,List<Tuple<int, List<int>>>>();
            foreach (var num in nums)
            {
                var secret = num;
                var price = GetPrice(secret);
                var lst = new List<int>();
                var bananasList = new List<Tuple<int, List<int>>>();
                for (int i = 0; i < 2000; i++)
                {
                    secret = GetSecret(secret);

                    var nextPrice = GetPrice(secret);
                    var change = nextPrice - price;
                    price = nextPrice;

                    lst.Add(change);
                    var newList = new List<int>();
                    if (lst.Count < 4)
                    {
                        continue;
                    }
                    else if (lst.Count == 5)
                    {
                        newList = lst.Skip(1).ToList();
                    }
                    else 
                    {
                        newList = lst.Skip(i-3).ToList();
                    }

                    var tuple = Tuple.Create(price, newList);
                    bananasList.Add(tuple);

                    if (map.ContainsKey(num))
                    {
                        map[num] = bananasList;
                    }
                    else 
                    {
                        map.Add(num,bananasList);
                    }
                }
            }
            var allSequences = map.Values
                  .SelectMany(tupleList => tupleList.Select(t => t.Item2))
                  .ToList();
            var uniqueS = new List<List<int>>();
            var globalSum = 0;
            
            foreach (var sequence in allSequences)
            {
                if (uniqueS.Any(x => x.SequenceEqual(sequence)))
                {
                    continue;
                }

                uniqueS.Add(sequence);
                var sum = 0;
                foreach (var kvp in map)
                {
                    var seq = kvp.Value.FirstOrDefault(x => x.Item2.SequenceEqual(sequence));
                    if (seq != null)
                    {
                        sum += seq.Item1;
                    }
                    
                }

                if (sum > globalSum)
                {
                    globalSum = sum;
                }
            }
            return 0;
        }
        private static long GetSecret(long secret)
        {
            var mul = secret * 64;
            secret = Mix(secret, mul);
            secret = Prune(secret);

            var div = (int)Math.Floor(secret / 32.0);
            secret = Mix(secret, div);
            secret = Prune(secret);

            var mul2 = secret * 2048;
            secret = Mix(secret, mul2);
            secret = Prune(secret);

            return secret;
        }

        private static int GetPrice(long secret) 
            => int.Parse(secret.ToString().Substring(secret.ToString().Length - 1, 1));
        private static long Mix(long secret, long value) => secret ^ value;
        private static long Prune(long secret) => secret % 16777216;
        public static int Solve2(string input)
        {
            return 0;
        }
    }
}
