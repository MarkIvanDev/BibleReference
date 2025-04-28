using System.Collections;

namespace BibleReference.Tests.Generators;
public class InvalidTextGenerator : IEnumerable<TheoryDataRow<string>>
{
    private readonly List<TheoryDataRow<string>> _data =
    [
        // Leading and Trailing -, ;, and ,
        "Genesis 1:;1",
        "Genesis 1:,1",
        "Genesis 1:-1",
        "Genesis 1:1;",
        "Genesis 1:1,",
        "Genesis 1:1-",

        // Empty citations, segments, or ranges
        "Genesis 1:1;;2:1",
        "Genesis 1:1,,3",
        "Genesis 1:1--2",
    ];

    public IEnumerator<TheoryDataRow<string>> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
