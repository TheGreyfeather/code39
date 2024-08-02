using System.Text.RegularExpressions;

namespace Code39;
public partial class Program
{
    private static void Main()
    {
        while (true)
        {
            Console.WriteLine("Input the string you want to convert to code39");
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Input Cannot Be Empty!");
                continue;
            }
            input = input.ToUpper();
            MatchCollection BadChars = BadCharsRegex().Matches(input);
            if (BadChars.Count > 0)
            {
                Console.WriteLine("Input: " + input);
                Console.WriteLine($"Invalid Character(s) at: ");
                //for (int i = 0; i < input.Length; i++)
                foreach (Match match in BadChars)
                {
                    Console.WriteLine(match.Index + ": " + match.Value);
                }
                continue;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(Code39.Convert('*' + input + '*'));
            }
            Console.WriteLine();
        }
    }

    [GeneratedRegex(@"(?:[^A-Z.\- \d\s*])")]
    private static partial Regex BadCharsRegex();
}