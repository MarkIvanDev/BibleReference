using System.Collections;

namespace BibleReference.Tests.Generators;
public class SimplifySegmentsGenerator : IEnumerable<object?[]>
{
    private readonly List<object?[]> _data =
    [
        [
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleChapter(1),
                ReferenceSegment.SingleChapter(1),
            },
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleChapter(1),
            }
        ],
        [
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleVerse(1, 1),
                ReferenceSegment.SingleVerse(1, 1),
            },
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleVerse(1, 1),
            }
        ],
        [
            new List<ReferenceSegment>
            {
                ReferenceSegment.MultipleVerses(1, 1, 3),
                ReferenceSegment.MultipleVerses(1, 1, 3),
            },
            new List<ReferenceSegment>
            {
                ReferenceSegment.MultipleVerses(1, 1, 3),
            }
        ],
        [
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleChapter(1),
                ReferenceSegment.SingleVerse(1, 1),
            },
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleChapter(1),
            }
        ],
        [
            new List<ReferenceSegment>
            {
                ReferenceSegment.MultipleVerses(1, 1, 3),
                ReferenceSegment.SingleVerse(1, 1),
            },
            new List<ReferenceSegment>
            {
                ReferenceSegment.MultipleVerses(1, 1, 3),
            }
        ],
        [
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleVerse(1, 1),
                ReferenceSegment.SingleVerse(1, 2),
                ReferenceSegment.SingleVerse(1, 3),
            },
            new List<ReferenceSegment>
            {
                ReferenceSegment.MultipleVerses(1, 1, 3),
            }
        ],
        [
            new List<ReferenceSegment>
            {
                new(new ReferencePoint(1, 5), new ReferencePoint(2, 4)),
                new(new ReferencePoint(1, 8), new ReferencePoint(1, 15)),
            },
            new List<ReferenceSegment>
            {
                new(new ReferencePoint(1, 5), new ReferencePoint(2, 4)),
            }
        ],
        [
            new List<ReferenceSegment>
            {
                new(new ReferencePoint(1, 8), new ReferencePoint(1, 15)),
                new(new ReferencePoint(1, 5), new ReferencePoint(2, 4)),
            },
            new List<ReferenceSegment>
            {
                new(new ReferencePoint(1, 5), new ReferencePoint(2, 4)),
            }
        ],
        [
            new List<ReferenceSegment>
            {
                new(new ReferencePoint(1, 5), new ReferencePoint(1, 15)),
                new(new ReferencePoint(1, 8), new ReferencePoint(2, 4)),
            },
            new List<ReferenceSegment>
            {
                new(new ReferencePoint(1, 5), new ReferencePoint(2, 4)),
            }
        ],
        [
            new List<ReferenceSegment>
            {
                new(new ReferencePoint(1, 8), new ReferencePoint(2, 4)),
                new(new ReferencePoint(1, 5), new ReferencePoint(1, 15)),
            },
            new List<ReferenceSegment>
            {
                new(new ReferencePoint(1, 5), new ReferencePoint(2, 4)),
            }
        ],
        [
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleVerse(1, 1),
                ReferenceSegment.SingleVerse(1, 3),
            },
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleVerse(1, 1),
                ReferenceSegment.SingleVerse(1, 3),
            }
        ],
        [
            new List<ReferenceSegment>
            {
                new(new ReferencePoint(1, 2), new ReferencePoint(2, 4)),
                ReferenceSegment.SingleVerse(1, 2),
                ReferenceSegment.MultipleVerses(2, 1, 5),
            },
            new List<ReferenceSegment>
            {
                new(new ReferencePoint(1, 2), new ReferencePoint(2, 5)),
            }
        ],
        [
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleChapter(1),
                ReferenceSegment.SingleChapter(2),
            },
            new List<ReferenceSegment>
            {
                ReferenceSegment.MultipleChapters(1, 2),
            }
        ],
        [
            new List<ReferenceSegment>
            {
                ReferenceSegment.SingleChapter(1),
                ReferenceSegment.SingleVerse(2, 1),
            },
            new List<ReferenceSegment>
            {
                new(new ReferencePoint(1, 1), new ReferencePoint(2, 1)),
            }
        ],
    ];

    public IEnumerator<object?[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
