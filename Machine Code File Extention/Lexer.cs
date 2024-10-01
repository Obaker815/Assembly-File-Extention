using System;
using System.Collections.Generic;

public enum TokenType
{
    LDA, STA, ADD, SUB, INP, OUT, HLT, BRZ, BRP, BRA, VAR, NUM, ID, EOF
}

public class Token
{
    public TokenType Type { get; }
    public string Value { get; }

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }
}

public class Lexer
{
    private readonly string _input;
    private int _position;
    private char _currentChar;

    public Lexer(string input)
    {
        _input = input;
        _position = 0;
        _currentChar = _input.Length > 0 ? _input[_position] : '\0';
    }

    private void Advance()
    {
        _position++;
        if (_position < _input.Length)
            _currentChar = _input[_position];
        else
            _currentChar = '\0';
    }

    public List<Token> Tokenize()
    {
        var tokens = new List<Token>();

        while (_currentChar != '\0')
        {
            if (char.IsWhiteSpace(_currentChar))
            {
                Advance();
                continue;
            }

            if (char.IsDigit(_currentChar))
            {
                var value = string.Empty;
                while (char.IsDigit(_currentChar))
                {
                    value += _currentChar;
                    Advance();
                }
                tokens.Add(new Token(TokenType.NUM, value));
                continue;
            }

            if (char.IsLetter(_currentChar))
            {
                var value = string.Empty;
                while (char.IsLetter(_currentChar))
                {
                    value += _currentChar;
                    Advance();
                }
                if (Enum.TryParse<TokenType>(value, true, out var type))
                {
                    tokens.Add(new Token(type, value));
                }
                else
                {
                    throw new Exception($"Unknown instruction: {value}");
                }
                continue;
            }

            throw new Exception($"Unexpected character: {_currentChar}");
        }

        tokens.Add(new Token(TokenType.EOF, ""));
        return tokens; // Ensure this is reached
    }
}
