namespace WordFinderConsole
{
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
}
