using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CLIGlobalTool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Welcome to 'Marten Migrations'");
                Console.WriteLine("Usage: dotnet pg add 'migration message'");
                Console.ReadLine();
                return;
            }
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: dotnet pg add 'migration message'");
                Console.ReadLine();
                return;
            }

            if (args[0] != "add")
            {
                Console.WriteLine($"The command {args[0]} does not exist.");
                Console.WriteLine("Usage: dotnet pg add 'migration message'");
                Console.ReadLine();
                return;
            }

            var migrationMessage = args[1];

            var time = DateTime.UtcNow.ToString("yyyyMMddhhmmss").ReplaceSpecialCharacters();
            Console.WriteLine(time);


            var s = await File.ReadAllTextAsync("MigrationTemplate.txt");
            s = s.Replace("#className#", $"{time}_Migration");
            s = s.Replace("#migrationMessage#", migrationMessage);
            var path = Path.Combine(Environment.CurrentDirectory, "MartenMigrations");

            Directory.CreateDirectory(path);
            await File.WriteAllTextAsync(Path.Combine(path, $"{time}_{migrationMessage.ReplaceSpecialCharacters()}.cs"), s);
            Console.ReadLine();
        }
    }

    public static class StringExtensions
    {
        public static string Replace(this string str, string newStr, params string[] replaces)
        {
            return replaces.Aggregate(str, (current, replace) => current.Replace(replace, newStr));
        }

        public static string ReplaceSpecialCharacters(this string str)
        {
            return new[] { "?", "*", "!", ".", "-", "/", " ", ":", "+" }.Aggregate(str, (current, replace) => current.Replace(replace, String.Empty));
        }
    }
}
