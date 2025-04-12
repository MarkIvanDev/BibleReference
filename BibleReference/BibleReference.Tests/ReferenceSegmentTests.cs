using BibleReference.Tests.Generators;

namespace BibleReference.Tests;

public class ReferenceSegmentTests
{
    [Fact(DisplayName = "Start Chapter > End Chapter")]
    public void InvalidChapterOrder()
    {
        Assert.Throws<ArgumentException>(() => ReferenceSegment.MultipleChapters(2, 1));
    }

    [Fact(DisplayName = "Start Verse > End Verse")]
    public void InvalidVerseOrder()
    {
        Assert.Throws<ArgumentException>(() => ReferenceSegment.MultipleVerses(1, 2, 1));
    }

    [Theory(DisplayName = "Start Verse == 0 ^ End Verse == 0")]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    public void InvalidVerses(int startVerse, int endVerse)
    {
        Assert.Throws<ArgumentException>(() => ReferenceSegment.MultipleVerses(1, startVerse, endVerse));
    }

    [Fact(DisplayName = "== operator")]
    public void EqualsOperator()
    {
        Assert.True(ReferenceSegment.SingleVerse(1, 1) == ReferenceSegment.SingleVerse(1, 1));
        Assert.False(ReferenceSegment.SingleVerse(1, 1) == ReferenceSegment.SingleVerse(1, 2));
    }

    [Fact(DisplayName = "!= operator")]
    public void NotEqualsOperator()
    {
        Assert.True(ReferenceSegment.SingleVerse(1, 1) != ReferenceSegment.SingleVerse(1, 2));
        Assert.False(ReferenceSegment.SingleVerse(1, 1) != ReferenceSegment.SingleVerse(1, 1));
    }

    [Fact(DisplayName = "Single Chapter ToString")]
    public void SingleChapterToString()
    {
        Assert.Equal("1", ReferenceSegment.SingleChapter(1).ToString());
    }

    [Fact(DisplayName = "Multiple Chapters ToString")]
    public void MultipleChaptersToString()
    {
        Assert.Equal("1-2", ReferenceSegment.MultipleChapters(1, 2).ToString());
    }

    [Fact(DisplayName = "Single Verse ToString")]
    public void SingleVerseToString()
    {
        Assert.Equal("1:1", ReferenceSegment.SingleVerse(1, 1).ToString());
    }

    [Fact(DisplayName = "Multiple Verses ToString")]
    public void MultipleVersesToString()
    {
        Assert.Equal("1:1-5", ReferenceSegment.MultipleVerses(1, 1, 5).ToString());
    }

    [Theory(DisplayName = "Is Inside")]
    [ClassData(typeof(IsInsideGenerator))]
    public void IsInside(ReferenceSegment segment1, ReferenceSegment segment2, bool isInside)
    {
        Assert.Equal(isInside, segment1.IsInside(segment2));
    }

    [Theory(DisplayName = "Has Intersection")]
    [ClassData(typeof(HasIntersectionGenerator))]
    public void HasIntersection(ReferenceSegment segment1, ReferenceSegment segment2, bool hasIntersection)
    {
        Assert.Equal(hasIntersection, segment1.HasIntersection(segment2));
    }

    [Theory(DisplayName = "Is Continuous")]
    [ClassData(typeof(IsContinuousGenerator))]
    public void IsContinuous(ReferenceSegment segment1, ReferenceSegment segment2, bool isContinuous)
    {
        Assert.Equal(isContinuous, segment1.IsContinuous(segment2));
    }

    [Theory(DisplayName = "Simplify Segments")]
    [ClassData(typeof(SimplifySegmentsGenerator))]
    public void Simplify(IList<ReferenceSegment> segments, IList<ReferenceSegment> simplifiedSegments)
    {
        Assert.Equal(simplifiedSegments, segments.Simplify());
    }

}
