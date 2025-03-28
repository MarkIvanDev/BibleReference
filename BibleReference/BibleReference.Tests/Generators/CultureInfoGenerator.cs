using System.Collections;
using BibleBooks;

namespace BibleReference.Tests.Generators;

public class CultureInfoGenerator : IEnumerable<object?[]>
{
    private readonly List<object?[]> _data =
    [
        [null],
        [CultureInfos.En],
        [CultureInfos.FilPH],
    ];

    public IEnumerator<object?[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
