using static System.Console;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Code39;
public partial class Program
{
    private static void Main()
    {
        while (true)
        {
            WriteLine("Input the string you want to convert to code39");
            string? input = ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                WriteLine("Input Cannot Be Empty!");
                continue;
            }
            input = input.ToUpper();
            MatchCollection BadChars = BadCharsRegex().Matches(input);
            if (BadChars.Count > 0)
            {
                WriteLine("Input: " + input);
                WriteLine($"Invalid Character(s) at: ");
                //for (int i = 0; i < input.Length; i++)
                foreach (Match match in BadChars)
                {
                    WriteLine(match.Index + ": " + match.Value);
                }
                continue;
            }
            else
            {
                WriteLine();
                WriteLine(Code39.Convert('*' + input + '*'));
            }
            WriteLine();
        }
    }   

    private class Code39
    {
        private static readonly Dictionary<char, string> Codes = new()
        {
            {'*', "1 1001"},
            {'A', "011 10"},
            {'B', "101 10"},
            {'C', "001 11"},
            {'D', "110 10"},
            {'E', "010 11"},
            {'F', "100 11"},
            {'G', "111 00"},
            {'H', "011 01"},
            {'I', "101 01"},
            {'J', "110 01"},
            {'K', "0111 0"},
            {'L', "1011 0"},
            {'M', "0011 1"},
            {'N', "1101 0"},
            {'O', "0101 1"},
            {'P', "1001 1"},
            {'Q', "1110 0"},
            {'R', "0110 1"},
            {'S', "1010 1"},
            {'T', "1100 1"},
            {'U', "0 1110"},
            {'V', "1 0110"},
            {'W', "0 0111"},
            {'X', "1 1010"},
            {'Y', "0 1011"},
            {'Z', "1 0011"},
            {'.', "0 1101"},
            {'-', "1 1100"},
            {' ', "1 0101"},
            {'0', "11 001"},
            {'1', "01 110"},
            {'2', "10 110"},
            {'3', "00 111"},
            {'4', "11 010"},
            {'5', "01 011"},
            {'6', "10 011"},
            {'7', "11 100"},
            {'8', "01 101"},
            {'9', "10 101"}
        };
        internal static string Convert(string input)
        {
            string output = string.Empty;
            char last = '\0';
            foreach (char c in input)
            {
                string k = Codes.GetValueOrDefault(c)!;
                foreach (char d in k)
                {
                    if (d == '0' && last == '0')
                    {
                        output += " ";
                    }
                    output += d switch
                    {
                        '0' => '█',
                        '1' => '│',
                        ' ' => "  ",
                        _ => string.Empty
                    };

                    last = d;
                }
            }
            return output;
        }
    }

    [GeneratedRegex(@"(?:[^A-Z.\- \d\s*])")]
    private static partial Regex BadCharsRegex();
}