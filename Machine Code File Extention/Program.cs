using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // redundancy, should only show when building
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the path to a .sack file.");
            return;
        }

        // checks if .sack exists
        string filePath = args[0];

        if (!File.Exists(filePath) || Path.GetExtension(filePath) != ".sack")
        {
            Console.WriteLine("File does not exist or is not a .sack file.");
            return;
        }

        var program = File.ReadAllText(filePath);
        
        // makes a lexer object and tokenizes
        var lexer = new Lexer(program);
        var tokens = lexer.Tokenize();

        // makes a parser object and parses
        var parser = new Parser(tokens);
        var instructions = parser.Parse();

        // makes a interpreter object and runs the "program"
        var interpreter = new Interpreter(instructions);
        interpreter.Run();

        // Wait for user input before closing
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
