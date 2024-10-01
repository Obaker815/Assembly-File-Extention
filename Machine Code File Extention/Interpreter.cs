using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class Interpreter
{
    private int[] _memory = new int[256];
    private int _accumulator = 0;
    private int _instructionPointer = 0;
    private readonly List<Instruction> _instructions;

    public Interpreter(List<Instruction> instructions)
    {
        _instructions = instructions;
    }

    public void Run()
    {
        while (_instructionPointer < _instructions.Count)
        {
            var instruction = _instructions[_instructionPointer];

            switch (instruction.Type)
            {
                case TokenType.LDA:
                    _accumulator = _memory[instruction.Address];
                    break;
                case TokenType.STA:
                    _memory[instruction.Address] = _accumulator;
                    break;
                case TokenType.ADD:
                    _accumulator += _memory[instruction.Address];
                    break;
                case TokenType.SUB:
                    _accumulator -= _memory[instruction.Address];
                    break;
                case TokenType.INP:
                    Console.Write("Input: ");
                    _accumulator = int.Parse(Console.ReadLine());
                    break;
                case TokenType.OUT:
                    Console.WriteLine($"Output: {_accumulator}");
                    break;
                case TokenType.HLT:
                    return;
                case TokenType.BRZ:
                    if (_accumulator == 0)
                    {
                        _instructionPointer = instruction.Address;
                        continue;
                    }
                    break;
                case TokenType.BRP:
                    if (_accumulator >= 0)
                    {
                        _instructionPointer = instruction.Address;
                        continue;
                    }
                    break;
                case TokenType.BRA:
                    _instructionPointer = instruction.Address;
                    continue;
                case TokenType.VAR:
                    _memory[instruction.Address] = instruction.Value;
                    break;
            }

            _instructionPointer++;
        }
    }
}
