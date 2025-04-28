using System.Collections;

namespace BibleReference.Tests.Generators;

public class IsContinuousGenerator : IEnumerable<TheoryDataRow<ReferenceSegment, ReferenceSegment, bool>>
{
    private readonly List<TheoryDataRow<ReferenceSegment, ReferenceSegment, bool>> _data =
    [
        (
            ReferenceSegment.SingleVerse(1, 1),
            ReferenceSegment.SingleVerse(1, 2),
            true
        ),
        (
            ReferenceSegment.SingleVerse(1, 1),
            ReferenceSegment.MultipleVerses(1, 2, 4),
            true
        ),
        (
            ReferenceSegment.MultipleVerses(1, 1, 3),
            ReferenceSegment.SingleVerse(1, 4),
            true
        ),
        (
            ReferenceSegment.SingleChapter(1),
            ReferenceSegment.SingleVerse(2, 1),
            true
        ),
        (
            ReferenceSegment.SingleChapter(1),
            ReferenceSegment.SingleChapter(2),
            true
        ),
        (
            ReferenceSegment.MultipleVerses(1, 1, 2),
            ReferenceSegment.SingleVerse(1, 5),
            false
        ),
    ];

    public IEnumerator<TheoryDataRow<ReferenceSegment, ReferenceSegment, bool>> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}