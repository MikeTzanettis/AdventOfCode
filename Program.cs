using System;
using System.IO;
using System.Reflection;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the day and part: ");
            string input = Console.ReadLine();

            string filePath = $"Inputs/Day{input.Split('.')[0]}.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Input file not found.");
                return;
            }

            string puzzleInput = File.ReadAllText(filePath);
            string type = $"AdventOfCode.Day{input.Split('.')[0]}";

            Type classType = Type.GetType(type);

            MethodInfo method = classType.GetMethod($"Solve{input.Split('.')[1]}");
            object instance = Activator.CreateInstance(classType);

            method.Invoke(instance, new object[] { puzzleInput });
        }
    }
}
