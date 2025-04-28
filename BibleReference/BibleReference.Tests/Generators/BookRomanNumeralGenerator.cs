using System.Collections;
using System.Globalization;
using BibleBooks;

namespace BibleReference.Tests.Generators;

public class BookRomanNumeralGenerator : IEnumerable<TheoryDataRow<CultureInfo, BibleBook, string>>
{
    private readonly List<TheoryDataRow<CultureInfo, BibleBook, string>> _data =
    [
        (CultureInfos.En, BibleBook.John1, "I John"),
        (CultureInfos.En, BibleBook.John2, "II John"),
        (CultureInfos.En, BibleBook.John3, "III John"),
        (CultureInfos.En, BibleBook.John1, "First John"),
        (CultureInfos.En, BibleBook.John2, "Second John"),
        (CultureInfos.En, BibleBook.John3, "Third John"),

        (CultureInfos.FilPH, BibleBook.John1, "I Juan"),
        (CultureInfos.FilPH, BibleBook.John2, "II Juan"),
        (CultureInfos.FilPH, BibleBook.John3, "III Juan"),
    ];

    public IEnumerator<TheoryDataRow<CultureInfo, BibleBook, string>> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
