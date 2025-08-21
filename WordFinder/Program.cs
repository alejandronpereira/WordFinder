using WordFinderConsole;

var matrix = new List<string>
            {
                "abcdef",
                "ghwioc",
                "chilld",
                "pqnsdd",
                "uvdxyz"
            };

var wordStream = new List<string> { "cold", "wind", "snow", "chill" };

var finder = new WordFinder(matrix);
var foundWords = finder.Find(wordStream);

Console.WriteLine("Words found in the matrix:");
foreach (var word in foundWords)
{
    Console.WriteLine($"- {word}");
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
