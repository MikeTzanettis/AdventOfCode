using System;
using System.IO;

namespace AdventOfCode
{
    public class Day4
    {
        public static int Solve1(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int rows = lines.Length;
            int cols = lines[0].Length;
            char[,] charArray = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    charArray[i, j] = lines[i][j];
                }
            }
            var total = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (charArray[i, j] == 'X')
                    {
                        if (i > 2)
                        {
                            if (SearchUp(charArray,i,j))
                                total++;
                        }

                        if (j > 2)
                        {
                            if (SearchLeft(charArray, i, j))
                                total++;
                        }

                        if (j < cols-3)
                        {
                            if (SearchRight(charArray, i, j))
                                total++;
                        }

                        if(i < rows - 3)
                        {
                            if (SearchDown(charArray, i, j))
                                total++;
                        }

                        if (i > 2 && j > 2)
                        {
                            if (SearchDiag1(charArray, i, j))
                                total++;
                        }

                        if (i > 2 && j < cols - 3)
                        {
                            if (SearchDiag2(charArray, i, j))
                                total++;
                        }

                        if (i < rows - 3 && j > 2)
                        {
                            if (SearchDiag3(charArray, i, j))
                                total++;
                        }

                        if (i < rows - 3 && j < cols - 3)
                        {
                            if (SearchDiag4(charArray, i, j))
                                total++;
                        }

                    }
                }
            }
            return total;
        }
        private static bool SearchUp(char[,] charArray, int row, int col)
        {
            if (charArray[row - 1, col] == 'M' && 
                charArray[row - 2, col] == 'A' &&
                charArray[row - 3, col] == 'S')
            { 
                return true; 
            }

            return false;
        }

        private static bool SearchLeft(char[,] charArray, int row, int col)
        {
            if (charArray[row, col - 1] == 'M' &&
                charArray[row, col - 2] == 'A' &&
                charArray[row, col - 3] == 'S')
            {
                return true;
            }

            return false;
        }

        private static bool SearchRight(char[,] charArray, int row, int col)
        {
            if (charArray[row, col + 1] == 'M' &&
                charArray[row, col + 2] == 'A' &&
                charArray[row, col + 3] == 'S')
            {
                return true;
            }

            return false;
        }

        private static bool SearchDown(char[,] charArray, int row, int col)
        {
            if (charArray[row + 1, col] == 'M' &&
                charArray[row + 2, col] == 'A' &&
                charArray[row + 3, col] == 'S')
            {
                return true;
            }

            return false;
        }

        private static bool SearchDiag1(char[,] charArray, int row, int col)
        {
            if (charArray[row - 1, col - 1] == 'M' &&
                charArray[row - 2, col - 2] == 'A' &&
                charArray[row - 3, col - 3] == 'S')
            {
                return true;
            }

            return false;
        }

        private static bool SearchDiag2(char[,] charArray, int row, int col)
        {
            if (charArray[row - 1, col + 1] == 'M' &&
                charArray[row - 2, col + 2] == 'A' &&
                charArray[row - 3, col + 3] == 'S')
            {
                return true;
            }

            return false;
        }

        private static bool SearchDiag3(char[,] charArray, int row, int col)
        {
            if (charArray[row + 1, col - 1] == 'M' &&
                charArray[row + 2, col - 2] == 'A' &&
                charArray[row + 3, col - 3] == 'S')
            {
                return true;
            }

            return false;
        }

        private static bool SearchDiag4(char[,] charArray, int row, int col)
        {
            if (charArray[row + 1, col + 1] == 'M' &&
                charArray[row + 2, col + 2] == 'A' &&
                charArray[row + 3, col + 3] == 'S')
            {
                return true;
            }

            return false;
        }


        private static bool SearchMAS(char[,] charArray, int row, int col)
        {
            try 
            {
                if ((charArray[row + 1, col + 1] == 'M' &&
                    charArray[row - 1, col - 1] == 'S')
                    ||
                    (charArray[row + 1, col + 1] == 'S' &&
                    charArray[row - 1, col - 1] == 'M')
                )
                {
                    if ((charArray[row - 1, col + 1] == 'M' &&
                    charArray[row + 1, col - 1] == 'S')
                    ||
                    (charArray[row - 1, col + 1] == 'S' &&
                    charArray[row + 1, col - 1] == 'M'))
                        return true;
                }
            }
            catch { return false; }


            return false;
        }
        public static int Solve2(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int rows = lines.Length;
            int cols = lines[0].Length;
            char[,] charArray = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    charArray[i, j] = lines[i][j];
                }
            }
            var total = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (charArray[i, j] == 'A')
                    {

                        if (SearchMAS(charArray, i, j))
                                total++;
                       
                    }
                }
            }
            return total;
        }
    }
}
