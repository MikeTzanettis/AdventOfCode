using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day13
    {
        private static int[,] _lookUpTable;
        private static int _maxSteps = 100;
        public static int Solve1(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int counter = 0;
            int posAx = 0;
            int posAy = 0;
            int posBx = 0;
            int posBy = 0;
            int posPrizeX = 0;
            int posPrizeY = 0;
            var total = 0;
            while (counter < lines.Length)
            {
                var buttonA = lines[counter].Split(':');
                posAx = int.Parse(buttonA[1].Split(',')[0].Split('+')[1]);
                posAy = int.Parse(buttonA[1].Split(',')[1].Split('+')[1]);

                var buttonB = lines[counter + 1].Split(':');
                posBx = int.Parse(buttonB[1].Split(',')[0].Split('+')[1]);
                posBy = int.Parse(buttonB[1].Split(',')[1].Split('+')[1]);

                var prize = lines[counter + 2].Split(':');
                posPrizeX = int.Parse(prize[1].Split(',')[0].Split('=')[1]);
                posPrizeY = int.Parse(prize[1].Split(',')[1].Split('=')[1]);

                total += CalculateMinimumTokens(posAx, posAy, posBx, posBy, posPrizeX, posPrizeY);
                counter += 3;
            }

            //var lst = new List<int> { 94, 34, 22, 67, 8400, 5400 };

            return total;
        }

        public static int Solve2(string input)
        {
            return 0;
        }

        private static int CalculateMinimumTokens(int aX, int aY, int bX, int bY, int prizeX, int prizeY)
        {
            int max_x = Math.Max(aX, aY) * _maxSteps;
            int max_y = Math.Max(bX, bY) * _maxSteps;
            InitializeLookUpTable(aX, aY, bX, bY, max_x, max_y);

            int posX = 0;
            int posY = 0;
            for (int i = 0; i < max_x; i++)
            {
                for (int j = 0; j < max_y; j++)
                {
                    if (_lookUpTable[i, j] == int.MaxValue)
                    {
                        continue;
                    }

                    //Press A
                    posX = i + aX;
                    posY = j + aY;

                    if (posX < max_x && posY < max_y)
                    {
                        _lookUpTable[posX, posY] = Math.Min(_lookUpTable[posX, posY], _lookUpTable[i, j] + 3);
                    }

                    if (posX == prizeX && posY == prizeY)
                    {
                        try
                        {
                            return _lookUpTable[posX, posY];
                        }
                        catch
                        {
                            return 0;
                        }
                        
                    }
                    //Press B
                    posX = i + bX;
                    posY = j + bY;
                    if (posX < max_x && posY < max_y)
                    {
                        _lookUpTable[posX, posY] = Math.Min(_lookUpTable[posX, posY], _lookUpTable[i, j] + 1);
                    }

                    if (posX == prizeX && posY == prizeY)
                    {
                        try
                        {
                            return _lookUpTable[posX, posY];
                        }
                        catch
                        {
                            return 0;
                        }
                    }
                }
            }
            return 0;
        }

        private static void InitializeLookUpTable(int aX, int aY, int bX, int bY, int max_x, int max_y)
        {
            _lookUpTable = new int[max_x, max_y];

            for (int i = 0; i < _lookUpTable.GetLength(0); i++)
            {
                for (int j = 0; j < _lookUpTable.GetLength(1); j++)
                {
                    _lookUpTable[i, j] = int.MaxValue;
                }
            }

            _lookUpTable[0, 0] = 0;
        }
    }
}
