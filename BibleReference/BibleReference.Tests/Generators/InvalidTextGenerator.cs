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

        // Negative and zero values
        "Genesis -1",
        "Genesis 0",
        "Genesis 1:0",
        "Genesis 0:1",

        // Invalid Book names
        "ABC 1",

        // Invalid types
        "1 1:1",
        "Genesis a:1",
        "Genesis 1:a",
    ];

    public IEnumerator<TheoryDataRow<string>> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
