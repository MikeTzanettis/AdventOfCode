using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day7
    {
        public static int Solve1(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            long solvableEqs = 0;
            foreach (var line in lines)
            {
                var components = line.Split(':');
                var expectedResult = components[0];
                var numbers = components[1].Split(' ').Where(x=> !string.IsNullOrEmpty(x)).ToList().ConvertAll(long.Parse);

                var operators = new[] { "+", "*" };

                var operatorPermutations = GetPermutations(operators, numbers.Count - 1);

                foreach (var permutation in operatorPermutations)
                {
                    var actualResult = EvaluateExpression(numbers, permutation);
                    if (actualResult == long.Parse(expectedResult))
                    {
                        solvableEqs += actualResult;
                        break;
                    }
                }
            }

            return 0;
        }

        public static int Solve2(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            long solvableEqs = 0;
            foreach (var line in lines)
            {
                var components = line.Split(':');
                var expectedResult = components[0];
                var numbers = components[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList().ConvertAll(long.Parse);

                var operators = new[] { "+", "*" };
                var operators2 = new[] { "+", "*", "||" };

                var operatorPermutations = GetPermutations(operators, numbers.Count - 1);
                var operatorPermutations2 = GetPermutations(operators2, numbers.Count - 1);
                var searchWithSpecialOperator = true;

                foreach (var permutation in operatorPermutations)
                {
                    var actualResult = EvaluateExpression(numbers, permutation);
                    if (actualResult == long.Parse(expectedResult))
                    {
                        solvableEqs += actualResult;
                        searchWithSpecialOperator = false;
                        break;
                    }
                }

                if (searchWithSpecialOperator)
                {
                    foreach (var permutation in operatorPermutations2)
                    {
                        string concatenatedString = string.Join("", numbers);
                        if (long.Parse(concatenatedString) == long.Parse(expectedResult))
                        {
                            solvableEqs += long.Parse(expectedResult);
                            break;
                        }

                        var actualResult2 = EvaluateExpression2(numbers, permutation);
                        if (actualResult2 == long.Parse(expectedResult))
                        {
                            solvableEqs += actualResult2;
                            break;
                        }
                    }
                }
            }

            return 0;
        }

        static List<string[]> GetPermutations(string[] operators, int length)
        {
            var results = new List<string[]>();
            GeneratePermutations(new string[length], 0, operators, results);
            return results;
        }

        static void GeneratePermutations(string[] current, int index, string[] operators, List<string[]> results)
        {
            if (index == current.Length)
            {
                results.Add((string[])current.Clone());
                return;
            }

            foreach (var op in operators)
            {
                current[index] = op;
                GeneratePermutations(current, index + 1, operators, results);
            }
        }

        static long EvaluateExpression(List<long> numbers, string[] operators)
        {
            long result = numbers[0];
            for (int i = 0; i < operators.Length; i++)
            {
                switch (operators[i])
                {
                    case "+":
                        result += numbers[i + 1];
                        break;
                    case "*":
                        result *= numbers[i + 1];
                        break;
                }
            }
            return result;
        }

        static long EvaluateExpression2(List<long> numbers, string[] operators)
        {
            long result = numbers[0];
            for (int i = 0; i < operators.Length; i++)
            {
                switch (operators[i])
                {
                    case "+":
                        result += numbers[i + 1];
                        break;
                    case "*":
                        result *= numbers[i + 1];
                        break;
                    case "||":
                        result = long.Parse(result.ToString() + numbers[i+1].ToString());
                        break;
                }
            }
            return result;
        }
    }
}
