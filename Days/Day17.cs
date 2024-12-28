using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;

namespace AdventOfCode
{
    public class Day17
    {
        private static long _registerA = 0;
        private static long _registerB = 0;
        private static long _registerC = 0;
        private static int _pointer = 0;
        private static List<long> _output = new List<long>();
        private static List<long> _opcodes = new List<long> { 2, 4, 1, 3, 7, 5, 0, 3, 4, 1, 1, 5, 5, 5, 3, 0 };
        public int Solve1(string input)
        {
            var counter = 1_000_000_000;
            while (!_opcodes.SequenceEqual(_output))
            {
                _registerA = counter + 1;
                _pointer = 0;
                while (_pointer < _opcodes.Count - 1)
                {
                    if (_output.Count > 0 && _output[_output.Count - 1] != _opcodes[_output.Count - 1])
                    {
                        _output.Clear();
                        break;
                    }
                    long opcode = _opcodes[_pointer];
                    ExecuteInstruction(opcode, _opcodes[_pointer + 1]);

                    if (_opcodes.SequenceEqual(_output))
                    {
                        break;
                    }
                }
                counter++;
            }


            var output = string.Join(",", _output);
            return 0;
        }

        public static int Solve2(string input)
        {
            return 0;
        }

        private void ExecuteInstruction(long opcode, long operand_num)
        {
            long operand = GetOperand(opcode, operand_num);
            switch (opcode)
            {
                case 0: 
                    ExecuteAdv(operand);
                    break;
                case 1:
                    ExecuteBxl(operand);
                    break;
                case 2:
                    ExecuteBst(operand);
                    break;
                case 3:
                    ExecuteJnz(operand);
                    break;
                case 4:
                    ExecuteBxc(operand);
                    break;
                case 5:
                    ExecuteOut(operand);
                    break;
                case 6:
                    ExecuteBdv(operand);
                    break;
                default:
                    ExecuteCdv(operand);
                    break;
            }
        }

        private long GetOperand(long opcode,long operand_num)
        {
            var operand = operand_num;
            switch (opcode)
            {
                case 0:
                    return GetComboOperand(operand_num);
                case 1:
                    return operand;
                case 2:
                    return GetComboOperand(operand_num);
                case 3:
                    return operand;
                case 4:
                    return operand;
                case 5:
                    return GetComboOperand(operand_num);
                case 6:
                    return GetComboOperand(operand_num);
                case 7:
                    return GetComboOperand(operand_num);
            }

            return -1;
        }

        private long GetComboOperand(long operand)
        {
            switch (operand)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    return operand;
                case 4:
                    return _registerA;
                case 5:
                    return _registerB;
                case 6:
                    return _registerC;
                default:
                    throw new InvalidOperationException();
            }
        }
            
        private void ExecuteAdv(long combo_op)
        {
            var numerator = _registerA;
            var denominator = Math.Pow(2 , combo_op);
            _registerA = (long)(numerator / denominator);
            _pointer += 2;
        }

        private void ExecuteBxl(long literal_op)
        {
            _registerB = _registerB ^ literal_op;
            _pointer += 2;
        }

        private void ExecuteBst(long combo_op)
        {
            _registerB = combo_op % 8;
            _pointer += 2;
        }

        private void ExecuteJnz(long literal_op)
        {
            if (_registerA == 0) 
            {
                _pointer++;
                return; 
            }
            _pointer = (int)literal_op;
        }

        private void ExecuteBxc(long literal_op)
        {
            _registerB = _registerB ^ _registerC;
            _pointer += 2;
        }

        private void ExecuteOut(long combo_op)
        {
            _output.Add(combo_op % 8);
            _pointer += 2;
        }

        private void ExecuteBdv(long combo_op)
        {
            var numerator = _registerA;
            var denominator = Math.Pow(2, combo_op);
            _registerB = (long)(numerator / denominator);
            _pointer += 2;
        }

        private void ExecuteCdv(long combo_op)
        {
            var numerator = _registerA;
            var denominator = Math.Pow(2, combo_op);
            _registerC = (long)(numerator / denominator);
            _pointer += 2;
        }
    }
}
