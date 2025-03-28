using System.Collections;

namespace BibleReference.Tests.Generators;

public class PointGenerator : IEnumerable<object?[]>
{
    private readonly List<object?[]> _data =
    [
        [
            "1:1",
            null,
            null,
            new ReferencePoint(1, 1),
            true,
        ],
        [
            "1:1",
            1,
            null,
            new ReferencePoint(1, 1),
            true,
        ],
        [
            "1:1",
            null,
            2,
            new ReferencePoint(1, 1),
            true,
        ],
        [
            "2",
            null,
            null,
            ReferencePoint.WholeChapter(2),
            true,
        ],
        [
            "2",
            1,
            null,
            new ReferencePoint(1, 2),
            true,
        ],
        [
            "3",
            2,
            null,
            default(ReferencePoint),
            false,
        ],
        [
            "2",
            null,
            1,
            new ReferencePoint(1, 2),
            true,
        ],
    ];

    public IEnumerator<object?[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
