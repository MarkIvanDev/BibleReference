using System.Collections;
using BibleBooks;

namespace BibleReference.Tests;
public class BookRomanNumeralGenerator : IEnumerable<object?[]>
{
    private readonly List<object?[]> _data = new List<object?[]>()
    {
        new object?[] { CultureInfos.En, BibleBook.John1, "I John" },
        new object?[] { CultureInfos.En, BibleBook.John2, "II John" },
        new object?[] { CultureInfos.En, BibleBook.John3, "III John" },
        new object?[] { CultureInfos.En, BibleBook.John1, "First John" },
        new object?[] { CultureInfos.En, BibleBook.John2, "Second John" },
        new object?[] { CultureInfos.En, BibleBook.John3, "Third John" },

        new object?[] { CultureInfos.FilPH, BibleBook.John1, "I Juan" },
        new object?[] { CultureInfos.FilPH, BibleBook.John2, "II Juan" },
        new object?[] { CultureInfos.FilPH, BibleBook.John3, "III Juan" },
    };

    public IEnumerator<object?[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
