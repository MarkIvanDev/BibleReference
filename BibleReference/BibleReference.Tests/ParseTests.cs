using System.Globalization;
using BibleBooks;
using BibleReference.Tests.Generators;

namespace BibleReference.Tests;

public class ParseTests
{
    [Theory(DisplayName = "Book")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void Book(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal(name, BibleReferenceParser.Parse(name, culture).ToString(culture));
        }
    }

    [Theory(DisplayName = "Book Roman Numerals")]
    [ClassData(typeof(BookRomanNumeralGenerator))]
    public void BookRomanNumeral(CultureInfo culture, BibleBook book, string bookTest)
    {
        var name = BibleBooksHelper.GetName(book, culture);
        Assert.Equal(name, BibleReferenceParser.Parse(bookTest, culture).ToString(culture));
    }

    [Theory(DisplayName = "Book Chapter")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookChapter(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1", BibleReferenceParser.Parse($"{name} 1", culture).ToString(culture));
        }
    }

    [Theory(DisplayName = "Book Chapter-Chapter")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookChapterToChapter(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            if (BibleBooksHelper.GetMaxChapter(book) > 1)
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1-2", BibleReferenceParser.Parse($"{name} 1-2", culture).ToString(culture));
            }
        }
    }

    [Theory(DisplayName = "Book Chapter:Verse")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookChapterVerse(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1", BibleReferenceParser.Parse($"{name} 1:1", culture).ToString(culture));
        }
    }

    [Theory(DisplayName = "Book Chapter:Verse-Verse")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookChapterVerseToVerse(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1-5", BibleReferenceParser.Parse($"{name} 1:1-5", culture).ToString(culture));
        }
    }

    [Theory(DisplayName = "Book Chapter:Verse,Verse")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookChapterVerseVerse(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1,5", BibleReferenceParser.Parse($"{name} 1:1,5", culture).ToString(culture));
        }
    }

    [Theory(DisplayName = "Book Chapter:Verse,Verse-Verse")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookChapterVerseVerseToVerse(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1,5-7", BibleReferenceParser.Parse($"{name} 1:1,5-7", culture).ToString(culture));
        }
    }

    [Theory(DisplayName = "Book Chapter:Verse-Verse,Verse")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookChapterVerseToVerseVerse(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1-3,8", BibleReferenceParser.Parse($"{name} 1:1-3,8", culture).ToString(culture));
        }
    }

    [Theory(DisplayName = "Book Chapter:Verse-Verse,Verse-Verse")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookChapterVerseToVerseVerseToVerse(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1-5,8-10", BibleReferenceParser.Parse($"{name} 1:1-5,8-10", culture).ToString(culture));
        }
    }

    [Theory(DisplayName = "Book Chapter:Verse-Verse;Chapter:Verse-Verse")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookChapterVerseToVerseAndChapterVerseToVerse(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            if (BibleBooksHelper.GetMaxChapter(book) > 1)
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1:1-5;2:1-3", BibleReferenceParser.Parse($"{name} 1:1-5;2:1-3", culture).ToString(culture));
            }

        }
    }

    [Theory(DisplayName = "Book Chapter:Verse-Chapter:Verse")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookChapterVerseToChapterVerse(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            if (BibleBooksHelper.GetMaxChapter(book) > 1)
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1:5-2:3", BibleReferenceParser.Parse($"{name} 1:5-2:3", culture).ToString(culture));
            }
        }
    }

    [Theory(DisplayName = "Book Verse (for single-chapter books)")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookVerse(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            if (BibleBooksHelper.GetMaxChapter(book) == 1)
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1:2", BibleReferenceParser.Parse($"{name} 2", culture).ToString(culture));
            }
        }
    }

    [Theory(DisplayName = "Book Verse-Verse (for single-chapter books)")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookVerseVerse(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            if (BibleBooksHelper.GetMaxChapter(book) == 1)
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1:1-2", BibleReferenceParser.Parse($"{name} 1-2", culture).ToString(culture));
            }
        }
    }

    [Theory(DisplayName = "Book Verse,Verse (for single-chapter books)")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookVerseVerse2(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            if (BibleBooksHelper.GetMaxChapter(book) == 1)
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1:1,2", BibleReferenceParser.Parse($"{name} 1,2", culture).ToString(culture));
            }
        }
    }

    [Theory(DisplayName = "Book Verse-Verse,Verse (for single-chapter books)")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookVerseToVerseAndVerse(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            if (BibleBooksHelper.GetMaxChapter(book) == 1)
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1:1-3,5", BibleReferenceParser.Parse($"{name} 1-3,5", culture).ToString(culture));
            }
        }
    }

    [Theory(DisplayName = "Book Verse,Verse-Verse (for single-chapter books)")]
    [ClassData(typeof(CultureInfoGenerator))]
    public void BookVerseAndVerseToVerse(CultureInfo? culture)
    {
        foreach (var book in Enum.GetValues<BibleBook>())
        {
            if (BibleBooksHelper.GetMaxChapter(book) == 1)
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1:1,3-5", BibleReferenceParser.Parse($"{name} 1,3-5", culture).ToString(culture));
            }
        }
    }

    [Theory(DisplayName = "Invalid Text for Parsing")]
    [ClassData(typeof(InvalidTextGenerator))]
    public void InvalidText(string text)
    {
        Assert.Throws<Exception>(() => BibleReferenceParser.Parse(text));
    }

    [Theory(DisplayName = "Segment Only Parsing")]
    [ClassData(typeof(SegmentGenerator))]
    public void SegmentParsing(string text, int? maxChapter, IList<ReferenceSegment> segments, bool expected)
    {
        var isSuccessful = maxChapter.HasValue
            ? BibleReferenceParser.TryParseSegments(text, maxChapter.Value, out var result)
            : BibleReferenceParser.TryParseSegments(text, out result);
        if (expected)
        {
            Assert.Equal(result, segments);
        }
        else
        {
            Assert.Null(result);
        }
        Assert.Equal(isSuccessful, expected);
    }

    [Theory(DisplayName = "Point Only Parsing")]
    [ClassData(typeof(PointGenerator))]
    public void PointParsing(string text, int? maxChapter, int? chapterOverride, ReferencePoint point, bool expected)
    {
        var (isSuccessful, result) = (maxChapter, chapterOverride) switch
        {
            (null, null) => (BibleReferenceParser.TryParsePoint(text, out var r), r),
            (not null, null) => (BibleReferenceParser.TryParsePoint(text, maxChapter.Value, null, out var r), r),
            (not null, not null) => (BibleReferenceParser.TryParsePoint(text, maxChapter.Value, chapterOverride.Value, out var r), r),
            (null, not null) => (BibleReferenceParser.TryParsePoint(text, int.MaxValue, chapterOverride.Value, out var r), r),
        };
        if (expected)
        {
            Assert.Equal(result, point);
        }
        else
        {
            Assert.Null(result);
        }
        Assert.Equal(isSuccessful, expected);
    }

}
