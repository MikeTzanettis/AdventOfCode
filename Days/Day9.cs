using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day9
    {
        public static int Solve1(string input)
        {
            var charList = input.ToCharArray();
            var flattenedInput = string.Empty;
            var id = 0;
            var newChar = new List<string>();
            for(int i=0; i< charList.Length; i++)
            {
                for (int j = 0; j < int.Parse(charList[i].ToString()); j++)
                {
                    if (i % 2 == 0)
                    {
                        newChar.Add(id.ToString());
                    }
                    else
                    {
                        newChar.Add(".");
                    }
                }

                if (i % 2 == 0) id++;
            }
            var modifiedInput = newChar;

            while (true)
            {
                int lastDigitIndex = -1;

                for (int i = modifiedInput.Count - 1; i >= 0; i--)
                {
                    if (modifiedInput[i] != ".")
                    {
                        lastDigitIndex = i;
                        break;
                    }
                }

                if (IsComplete(modifiedInput))
                    break;

                var lastDigit = modifiedInput[lastDigitIndex];

                int firstDotIndex = modifiedInput.IndexOf(".");

                if (firstDotIndex == -1)
                    break;

                modifiedInput[firstDotIndex] = lastDigit;

                modifiedInput[lastDigitIndex] = ".";
            }
            long sum = 0;
            for (int i = 0; i < modifiedInput.Count; i++)
            {
                if (modifiedInput[i] == ".") break;

                sum += int.Parse(modifiedInput[i].ToString()) * i;
            }
            return 0;
        }
        static bool IsComplete(List<string> input) 
        {
            int firstDotIndex = input.IndexOf(".");

            if (firstDotIndex == -1)
                return true;

            var digitsPart = input.GetRange(0, firstDotIndex); ;
            var dotsPart = input.GetRange(firstDotIndex, input.Count - firstDotIndex);

            return digitsPart.All(x=> x != ".") && dotsPart.All(c => c == ".");
        }
        public static int Solve2(string input)
        {
            var charList = input.ToCharArray();
            var flattenedInput = string.Empty;
            var id = 0;
            var newChar = new List<string>();
            for (int i = 0; i < charList.Length; i++)
            {
                for (int j = 0; j < int.Parse(charList[i].ToString()); j++)
                {
                    if (i % 2 == 0)
                    {
                        newChar.Add(id.ToString());
                    }
                    else
                    {
                        newChar.Add(".");
                    }
                }

                if (i % 2 == 0) id++;
            }

            var numToCheck = int.Parse(newChar[newChar.Count - 1]);
            while (numToCheck > 0)
            {
                var indices = newChar
                .Select((value, index) => new { value, index }) 
                .Where(x => x.value != "." && int.Parse(x.value) == numToCheck)                  
                .Select(x => x.index)                           
                .ToList();

                int dotsNeeded = indices.Count;
                var placements = FindPlacements(newChar, dotsNeeded,indices.First());
                if (placements.Count == 0)
                {
                    numToCheck--;
                    continue;
                }

                for (int i = 0; i < dotsNeeded; i++)
                {
                    var placement = placements[i];
                    var dotPlace = indices[i];
                    newChar[placement] = numToCheck.ToString();
                    newChar[dotPlace] = ".";
                }

                numToCheck--;
            }

            long sum = 0;
            for (int i = 0; i < newChar.Count; i++)
            {
                if (newChar[i] == ".") continue;

                sum += int.Parse(newChar[i].ToString()) * i;
            }

            return 0;
        }

        public static List<int> FindPlacements(List<string> lst, int dotsNeeded,int index)
        {
            List<int> availablePlacements = new List<int>();

            for (int i = 0; i < index; i++)
            {
                if (availablePlacements.Count == dotsNeeded)
                    break;

                if (lst[i] == ".")
                {
                    if (availablePlacements.Count == 0 || availablePlacements.Last() == i - 1)
                    {
                        availablePlacements.Add(i);
                    }
                } 
                else 
                {
                    availablePlacements.Clear();
                }
            }

            if (availablePlacements.Count != dotsNeeded)
                availablePlacements.Clear();
            return availablePlacements;
        }
    }
}
