using System.Collections;
using BibleBooks;

namespace BibleReference.Tests
{
    public class CultureInfoGenerator : IEnumerable<object?[]>
    {
        private readonly List<object?[]> _data = new List<object?[]>()
        {
            new object?[] { null },
            new object?[] { CultureInfos.En },
            new object?[] { CultureInfos.Fil },
        };

        public IEnumerator<object?[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
