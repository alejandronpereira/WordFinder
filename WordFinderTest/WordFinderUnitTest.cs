using WordFinderConsole;

namespace WordFinderTest
{
    public class WordFinderUnitTest
    {
        public class WordFinderUnitTests
        {
            private readonly List<string> _matrix = new List<string>
            {
                "chillx",
                "oxxxxx",
                "ldxxxx",
                "dxxxxx",
                "windex",
                "xxxxxx"
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
}