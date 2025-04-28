using System.Collections;

namespace BibleReference.Tests.Generators;

public class HasIntersectionGenerator : IEnumerable<TheoryDataRow<ReferenceSegment, ReferenceSegment, bool>>
{
    private readonly List<TheoryDataRow<ReferenceSegment, ReferenceSegment, bool>> _data =
    [
        (
            ReferenceSegment.SingleChapter(1),
            ReferenceSegment.SingleChapter(1),
            true
        ),
        (
            ReferenceSegment.SingleVerse(1, 1),
            ReferenceSegment.SingleVerse(1, 1),
            true
        ),
        (
            ReferenceSegment.MultipleVerses(1, 1, 4),
            ReferenceSegment.MultipleVerses(1, 1, 4),
            true
        ),
        (
            ReferenceSegment.SingleVerse(1, 2),
            ReferenceSegment.MultipleVerses(1, 1, 3),
            true
        ),
        (
            ReferenceSegment.SingleVerse(1, 1),
            ReferenceSegment.SingleChapter(1),
            true
        ),
        (
            ReferenceSegment.MultipleVerses(1, 1, 3),
            ReferenceSegment.SingleChapter(1),
            true
        ),
        (
            ReferenceSegment.SingleChapter(1),
            ReferenceSegment.SingleVerse(1, 1),
            true
        ),
        (
            ReferenceSegment.MultipleVerses(1, 1, 3),
            ReferenceSegment.SingleVerse(1, 1),
            true
        ),
        (
            new ReferenceSegment(new ReferencePoint(1, 5), new ReferencePoint(2, 4)),
            new ReferenceSegment(new ReferencePoint(1, 8), new ReferencePoint(1, 15)),
            true
        ),
        (
            new ReferenceSegment(new ReferencePoint(1, 8), new ReferencePoint(1, 15)),
            new ReferenceSegment(new ReferencePoint(1, 5), new ReferencePoint(2, 4)),
            true
        ),
        (
            new ReferenceSegment(new ReferencePoint(1, 5), new ReferencePoint(1, 15)),
            new ReferenceSegment(new ReferencePoint(1, 8), new ReferencePoint(2, 4)),
            true
        ),
        (
            new ReferenceSegment(new ReferencePoint(1, 8), new ReferencePoint(2, 4)),
            new ReferenceSegment(new ReferencePoint(1, 5), new ReferencePoint(1, 15)),
            true
        ),
        (
            ReferenceSegment.SingleVerse(1, 1),
            ReferenceSegment.SingleVerse(1, 3),
            false
        ),
    ];

    public IEnumerator<TheoryDataRow<ReferenceSegment, ReferenceSegment, bool>> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}