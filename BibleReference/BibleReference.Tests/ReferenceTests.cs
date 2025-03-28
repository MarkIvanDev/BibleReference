using System.Globalization;
using BibleBooks;
using BibleReference.Tests.Generators;

namespace BibleReference.Tests;

public class ReferenceTests
{
    [Theory(DisplayName = "Single Chapter ToString")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void SingleChapterToString(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1", new Reference(book, ReferenceSegment.SingleChapter(1)).ToString(culture));
        }
    }

    [Theory(DisplayName = "Multiple Chapters ToString")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void MultipleChaptersToString(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            if (BibleBooksHelper.GetMaxChapter(book) > 1)
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1-2", new Reference(book, ReferenceSegment.MultipleChapters(1, 2)).ToString(culture));
            }
        }
    }

    [Theory(DisplayName = "Single Verse ToString")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void SingleVerseToString(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1", new Reference(book, ReferenceSegment.SingleVerse(1, 1)).ToString(culture));
        }
    }

    [Theory(DisplayName = "Continuous Verses ToString")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void ContinuousVersesToString(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1-5", new Reference(book, ReferenceSegment.MultipleVerses(1, 1, 5)).ToString(culture));
        }
    }

    [Theory(DisplayName = "Discontinuous Verses ToString")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void DiscontinuousVersesToString(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1,5",
                new Reference(book,
                    ReferenceSegment.SingleVerse(1, 1),
                    ReferenceSegment.SingleVerse(1, 5)
                ).ToString(culture));
        }
    }

    [Theory(DisplayName = "Mixed ToString")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void MixedToString(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1,5,7-9,11,13",
                new Reference(book,
                    ReferenceSegment.SingleVerse(1, 1),
                    ReferenceSegment.SingleVerse(1, 5),
                    ReferenceSegment.MultipleVerses(1, 7, 9),
                    ReferenceSegment.SingleVerse(1, 11),
                    ReferenceSegment.SingleVerse(1, 13)
                ).ToString(culture));
        }
    }

    [Fact(DisplayName = "== operator")]
    public void EqualsOperator()
    {
        Assert.Equal(
            new Reference(
                BibleBook.Genesis,
                ReferenceSegment.MultipleVerses(1, 1, 5),
                ReferenceSegment.MultipleVerses(2, 1, 3)
            ),
            BibleReferenceParser.Parse("Genesis 1:1-5;2:1-3"));
    }

    [Fact(DisplayName = "!= operator")]
    public void NotEqualsOperator()
    {
        Assert.NotEqual(
            new Reference(
                BibleBook.Genesis,
                ReferenceSegment.MultipleVerses(1, 1, 5),
                ReferenceSegment.MultipleVerses(2, 1, 3)
            ),
            BibleReferenceParser.Parse("Genesis 1:1,5;2:1,3"));
    }

}
