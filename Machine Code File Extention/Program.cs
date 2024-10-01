using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the path to a .sack file.");
            return;
        }

        string filePath = args[0];

        if (!File.Exists(filePath) || Path.GetExtension(filePath) != ".sack")
        {
            Console.WriteLine("File does not exist or is not a .sack file.");
            return;
        }

        var program = File.ReadAllText(filePath);

        var lexer = new Lexer(program);
        var tokens = lexer.Tokenize();

        var parser = new Parser(tokens);
        var instructions = parser.Parse();

        var interpreter = new Interpreter(instructions);
        interpreter.Run();

        // Wait for user input before closing
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
