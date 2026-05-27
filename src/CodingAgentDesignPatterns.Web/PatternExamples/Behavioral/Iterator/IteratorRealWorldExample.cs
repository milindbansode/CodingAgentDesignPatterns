namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Iterator;

public static class IteratorRealWorldExample
{
    // The collection decides how traversal works.
    // The caller only sees a small iterator API with MoveNext and Current.
    public static IReadOnlyList<string> Run()
    {
        var collection = new ItemCollection(new[] { "Intro", "Sunrise" });
        var iterator = collection.CreateIterator();
        var visited = new List<string>();

        while (iterator.MoveNext())
        {
            visited.Add(iterator.Current);
        }

        return new[]
        {
            "Playlist iterator",
            $"Visited playlist items: {string.Join(", ", visited)}"
        };
    }

    private sealed class ItemCollection
    {
        private readonly string[] _items;

        public ItemCollection(string[] items) => _items = items;

        public ItemIterator CreateIterator() => new(_items);
    }

    private sealed class ItemIterator
    {
        private readonly string[] _items;
        private int _index = -1;

        public ItemIterator(string[] items) => _items = items;

        public string Current => _items[_index];

        public bool MoveNext()
        {
            if (_index + 1 >= _items.Length)
            {
                return false;
            }

            _index++;
            return true;
        }
    }
}
