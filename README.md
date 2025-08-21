# WordFinder

## Overview

`WordFinder` is a class to search for words in a character matrix. Words can appear:

- Horizontally, left to right  
- Vertically, top to bottom  

The `Find` method returns the **top 10 words** from a given word stream that are found in the matrix. Each word is counted only once, even if it appears multiple times in the stream.

---

## Matrix Example

Matrix (6x6):

abcdef
ghiwoc
chilld
pqnsde
uvdxyz
mnopqr

Word Stream: `["cold", "wind", "snow", "chill"]`  
Result: `["cold", "wind", "chill"]` — "snow" not found.



Word Stream: `["cold", "wind", "snow", "chill"]`  
Result: `["cold", "wind", "chill"]` — "snow" not found.

---

## Implementation

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class WordFinder
{
    private readonly List<string> _searchSpace;

    public WordFinder(IEnumerable<string> matrix)
    {
        var grid = matrix.ToList();
        int rows = grid.Count;
        int cols = grid[0].Length;

        _searchSpace = new List<string>();

        _searchSpace.AddRange(grid);

        for (int c = 0; c < cols; c++)
        {
            char[] column = new char[rows];
            for (int r = 0; r < rows; r++)
                column[r] = grid[r][c];

            _searchSpace.Add(new string(column));
        }
    }

    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        var uniqueWords = new HashSet<string>(wordstream);
        var foundWords = new Dictionary<string, int>();

        foreach (var word in uniqueWords)
        {
            foreach (var line in _searchSpace)
            {
                if (line.Contains(word))
                {
                    foundWords[word] = foundWords.GetValueOrDefault(word, 0) + 1;
                    break;
                }
            }
        }

        return foundWords
            .OrderByDescending(kvp => kvp.Value)
            .ThenBy(kvp => kvp.Key)
            .Take(10)
            .Select(kvp => kvp.Key);
    }
}
```
## How It Works

The class stores all rows and columns of the matrix.

It checks each unique word in the stream against these rows and columns.

Only words found in the matrix are counted.

Returns up to 10 words, sorted by count and then alphabetically.

## Unit Tests (xUnit)

```csharp
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WordFinderTests
{
    public class WordFinderUnitTests
    {
        private readonly List<string> _matrix = new List<string>
        {
            "abcdef",
            "ghiwoc",
            "chilld",
            "pqnsde",
            "uvdxyz",
            "mnopqr"
        };

        [Fact]
        public void Find_ShouldReturnFoundWords()
        {
            var wordFinder = new WordFinder(_matrix);
            var wordStream = new List<string> { "cold", "wind", "snow", "chill" };
            var result = wordFinder.Find(wordStream).ToList();

            Assert.Contains("cold", result);
            Assert.Contains("wind", result);
            Assert.Contains("chill", result);
            Assert.DoesNotContain("snow", result);
        }

        [Fact]
        public void Find_ShouldReturnEmpty_WhenNoWordsFound()
        {
            var wordFinder = new WordFinder(_matrix);
            var wordStream = new List<string> { "banana", "apple", "pear" };
            var result = wordFinder.Find(wordStream);

            Assert.Empty(result);
        }

        [Fact]
        public void Find_ShouldIgnoreDuplicatesInStream()
        {
            var wordFinder = new WordFinder(_matrix);
            var wordStream = new List<string> { "chill", "chill", "chill" };
            var result = wordFinder.Find(wordStream).ToList();

            Assert.Single(result);
            Assert.Equal("chill", result.First());
        }

        [Fact]
        public void Find_ShouldReturnTop10WordsOnly()
        {
            var bigStream = new List<string>();
            for (int i = 0; i < 20; i++)
                bigStream.Add("chill" + i); 
            bigStream.Add("chill"); 

            var wordFinder = new WordFinder(_matrix);
            var result = wordFinder.Find(bigStream).ToList();

            Assert.True(result.Count <= 10);
            Assert.Contains("chill", result);
        }
    }
}
```
## How to Run

- Clone the repository
- Open in Visual Studio / Rider / VS Code
- Run the Console App to check functionality and/or the Unit Test project to check coverage.

## Usage Example

```csharp
var matrix = new List<string>
{
    "abcdef",
    "ghiwoc",
    "chilld",
    "pqnsde",
    "uvdxyz",
    "mnopqr"
};

var wordStream = new List<string> { "cold", "wind", "snow", "chill" };

var finder = new WordFinder(matrix);
var topWords = finder.Find(wordStream);

foreach (var word in topWords)
{
    Console.WriteLine(word);
}
```

Output:

cold
wind
chill
