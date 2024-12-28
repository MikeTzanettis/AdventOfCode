using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day3
    {
        public static int Solve1(string input)
        {
            char[] lines = input.ToCharArray();
            var sum = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                bool isCorrect = true;
                if (lines[i] == 'm' &&
                    lines[i + 1] == 'u' &&
                    lines[i + 2] == 'l' &&
                    lines[i + 3] == '(')
                {
                    i = i + 4;
                    var firstNum = "";
                    while (lines[i] != ',')
                    {
                        
                        if (char.IsDigit(lines[i]))
                        {
                            firstNum += lines[i];
                        }
                        else 
                        {
                            isCorrect = false;
                            break;
                        }
                        i++;

                    }
                    i++;
                    var secondNum = "";
                    while (lines[i] != ')')
                    {
                        if (char.IsDigit(lines[i]))
                        {
                            secondNum += lines[i];
                        }
                        else
                        {
                            isCorrect = false;
                            break;
                        }
                        i++;
                    }
                    if(isCorrect)
                    {
                        var fullNum = int.Parse(firstNum) * int.Parse(secondNum);
                        sum += fullNum;
                    }

                }
            }
            return sum;
        }

        public static int Solve2(string input)
        {
            string pattern = @"mul\(\d+,\d+\)|don't\(\)|do\(\)";
            string numPattern = @"\d+";

            MatchCollection matchlist = Regex.Matches(input, pattern);
            var sum = 0;
            var isEnabled = true;
            for (int i = 0; i < matchlist.Count; i++)
            {
                var match = matchlist[i].ToString();
                if (match.StartsWith("don't("))
                {
                    isEnabled = false;
                }

                if (match.StartsWith("do("))
                {
                    isEnabled = true;
                }

                if (match.StartsWith("mul") && isEnabled)
                {
                    var lstNums = match.Replace("mul(", "").Replace(")", "").Split(',');
                    var num1 = lstNums[0];
                    var num2 = lstNums[1];
                    sum += int.Parse(num1) * int.Parse(num2);
                }
            }
            return sum;
        }
    }
}
