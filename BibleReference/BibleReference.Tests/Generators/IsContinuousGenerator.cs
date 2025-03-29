using System.Collections;

namespace BibleReference.Tests.Generators;

public class IsContinuousGenerator : IEnumerable<object?[]>
{
    private readonly List<object?[]> _data =
    [
        [
            ReferenceSegment.MultipleVerses(1, 1, 2),
            ReferenceSegment.SingleVerse(1, 3),
            true
        ],
        [
            ReferenceSegment.MultipleVerses(1, 1, 2),
            ReferenceSegment.SingleVerse(1, 5),
            false
        ],
    ];

    public IEnumerator<object?[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}