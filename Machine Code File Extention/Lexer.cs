using System;
using System.Collections.Generic;

// define the TokenTypes
public enum TokenType
{
    LDA, STA, ADD, SUB, INP, OUT, HLT, BRZ, BRP, BRA, VAR, NUM, ID, EOF
}

// create token class
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

// create Lexer class
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

    // goto next character in an input/ VAR
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

            // Advances if character is "-" then throws an exception if there isnt a digit
            if (_currentChar == '-')
            {
                var value = "-";
                Advance();

                if (!char.IsDigit(_currentChar))
                    throw new Exception("Expected digit after minus sign.");

                while (char.IsDigit(_currentChar))
                {
                    value += _currentChar;
                    Advance();
                }
                tokens.Add(new Token(TokenType.NUM, value));
                continue;
            }

            // check if character is a digit, then makes number
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

            // checks if character is letter, then makes string
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
        return tokens;
    }
}
