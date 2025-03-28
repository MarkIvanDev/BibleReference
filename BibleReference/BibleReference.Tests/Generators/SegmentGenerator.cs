using System.Collections;

namespace BibleReference.Tests.Generators;

public class SegmentGenerator : IEnumerable<object?[]>
{
    private readonly List<object?[]> _data =
    [
        [
            "1:1",
            null,
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleVerse(1, 1)
            },
            true,
        ],
        [
            "1:1",
            1,
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleVerse(1, 1)
            },
            true,
        ],
        [
            "2:1",
            1,
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleVerse(1, 1)
            },
            false,
        ],
        [
            "1:1,3",
            null,
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleVerse(1, 1),
                ReferenceSegment.SingleVerse(1, 3),
            },
            true,
        ],
        [
            "1:1,3",
            1,
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleVerse(1, 1),
                ReferenceSegment.SingleVerse(1, 3),
            },
            true,
        ],
        [
            "2:1,3",
            1,
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleVerse(1, 1),
                ReferenceSegment.SingleVerse(1, 3),
            },
            false,
        ],
        [
            "1:1-3",
            null,
            new List<ReferenceSegment>
            {
                ReferenceSegment.MultipleVerses(1, 1, 3)
            },
            true,
        ],
        [
            "1:1-3",
            1,
            new List<ReferenceSegment>
            {
                ReferenceSegment.MultipleVerses(1, 1, 3)
            },
            true,
        ],
        [
            "2:1-3",
            1,
            new List<ReferenceSegment>
            {
                ReferenceSegment.MultipleVerses(1, 1, 3)
            },
            false,
        ],
        [
            "1:1,3-5",
            null,
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleVerse(1, 1),
                ReferenceSegment.MultipleVerses(1, 3, 5)
            },
            true,
        ],
        [
            "1:1,3-5",
            1,
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleVerse(1, 1),
                ReferenceSegment.MultipleVerses(1, 3, 5)
            },
            true,
        ],
        [
            "2:1,3-5",
            1,
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleVerse(1, 1),
                ReferenceSegment.MultipleVerses(1, 3, 5)
            },
            false,
        ],
        [
            "1:1-3,5-7",
            null,
            new List<ReferenceSegment>
            {
                ReferenceSegment.MultipleVerses(1, 1, 3),
                ReferenceSegment.MultipleVerses(1, 5, 7),
            },
            true,
        ],
        [
            "1:1-3,5-7",
            1,
            new List<ReferenceSegment>
            {
                ReferenceSegment.MultipleVerses(1, 1, 3),
                ReferenceSegment.MultipleVerses(1, 5, 7),
            },
            true,
        ],
        [
            "2:1-3,5-7",
            1,
            new List<ReferenceSegment>
            {
                ReferenceSegment.MultipleVerses(1, 1, 3),
                ReferenceSegment.MultipleVerses(1, 5, 7),
            },
            false,
        ],
    ];

    public IEnumerator<object?[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
