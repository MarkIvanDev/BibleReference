using System.Collections;

namespace BibleReference.Tests.Generators;

public class IsInsideGenerator : IEnumerable<object?[]>
{
    private readonly List<object?[]> _data =
    [
        [
            ReferenceSegment.SingleChapter(1),
            ReferenceSegment.SingleChapter(1),
            true
        ],
        [
            ReferenceSegment.SingleVerse(1, 1),
            ReferenceSegment.SingleVerse(1, 1),
            true
        ],
        [
            ReferenceSegment.MultipleVerses(1, 1, 4),
            ReferenceSegment.MultipleVerses(1, 1, 4),
            true
        ],
        [
            ReferenceSegment.SingleVerse(1, 2),
            ReferenceSegment.MultipleVerses(1, 1, 3),
            true
        ],
        [
            ReferenceSegment.SingleVerse(1, 1),
            ReferenceSegment.SingleChapter(1),
            true
        ],
        [
            ReferenceSegment.MultipleVerses(1, 1, 3),
            ReferenceSegment.SingleChapter(1),
            true
        ],
    ];

    public IEnumerator<object?[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}