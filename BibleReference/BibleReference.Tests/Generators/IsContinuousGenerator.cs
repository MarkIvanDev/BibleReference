using System.Collections;

namespace BibleReference.Tests.Generators;

public class IsContinuousGenerator : IEnumerable<object?[]>
{
    private readonly List<object?[]> _data =
    [
        [
            ReferenceSegment.SingleVerse(1, 1),
            ReferenceSegment.SingleVerse(1, 2),
            true
        ],
        [
            ReferenceSegment.SingleVerse(1, 1),
            ReferenceSegment.MultipleVerses(1, 2, 4),
            true
        ],
        [
            ReferenceSegment.MultipleVerses(1, 1, 3),
            ReferenceSegment.SingleVerse(1, 4),
            true
        ],
        [
            ReferenceSegment.SingleChapter(1),
            ReferenceSegment.SingleVerse(2, 1),
            true
        ],
        [
            ReferenceSegment.SingleChapter(1),
            ReferenceSegment.SingleChapter(2),
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