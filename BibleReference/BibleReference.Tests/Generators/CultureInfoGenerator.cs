using System.Collections;
using System.Globalization;
using BibleBooks;

namespace BibleReference.Tests.Generators;

public class CultureInfoGenerator : IEnumerable<TheoryDataRow<CultureInfo?>>
{
    private readonly List<TheoryDataRow<CultureInfo?>> _data =
    [
        new TheoryDataRow<CultureInfo?>(null),
        CultureInfos.En,
        CultureInfos.FilPH,
    ];

    public IEnumerator<TheoryDataRow<CultureInfo?>> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
