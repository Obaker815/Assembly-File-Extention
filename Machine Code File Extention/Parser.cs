using System;
using System.Collections.Generic;

// instruction class
public class Instruction
{
    public TokenType Type { get; }
    public int Address { get; }
    public int Value { get; } // New property for VAR instruction

    public Instruction(TokenType type, int address, int value = 0) // Updated constructor
    {
        Type = type;
        Address = address;
        Value = value;
    }
}

// parser class (shitty)
public class Parser
{
    private readonly List<Token> _tokens;
    private int _currentTokenIndex;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
        _currentTokenIndex = 0;
    }

    private Token CurrentToken => _tokens[_currentTokenIndex];

    private void Consume(TokenType type)
    {
        if (CurrentToken.Type == type)
        {
            _currentTokenIndex++;
        }
        else
        {
            throw new Exception($"Unexpected token: {CurrentToken.Value}");
        }
    }

    public List<Instruction> Parse()
    {
        var instructions = new List<Instruction>();

        while (CurrentToken.Type != TokenType.EOF)
        {
            var token = CurrentToken;

            if (token.Type == TokenType.LDA || token.Type == TokenType.STA || token.Type == TokenType.ADD || token.Type == TokenType.SUB || token.Type == TokenType.BRZ || token.Type == TokenType.BRP || token.Type == TokenType.BRA)
            {
                Consume(token.Type);
                if (CurrentToken.Type != TokenType.NUM)
                {
                    throw new Exception($"Expected a number after {token.Type}, but got {CurrentToken.Value}.");
                }
                instructions.Add(new Instruction(token.Type, int.Parse(CurrentToken.Value)));
                Consume(TokenType.NUM);
            }
            else if (token.Type == TokenType.INP || token.Type == TokenType.OUT || token.Type == TokenType.HLT)
            {
                Consume(token.Type);
                instructions.Add(new Instruction(token.Type, -1));
            }
            else if (token.Type == TokenType.VAR)
            {
                Consume(TokenType.VAR);
                if (CurrentToken.Type != TokenType.NUM)
                {
                    throw new Exception($"Expected a memory address after VAR, but got {CurrentToken.Value}.");
                }
                var address = int.Parse(CurrentToken.Value);
                Consume(TokenType.NUM);
                if (CurrentToken.Type != TokenType.NUM)
                {
                    throw new Exception($"Expected a value after memory address, but got {CurrentToken.Value}.");
                }
                var value = int.Parse(CurrentToken.Value);
                instructions.Add(new Instruction(TokenType.VAR, address, value)); // Update Instruction class to handle this.
                Consume(TokenType.NUM);
            }
            else
            {
                throw new Exception($"Unexpected token: {CurrentToken.Value}");
            }
        }

        return instructions;
    }

}
