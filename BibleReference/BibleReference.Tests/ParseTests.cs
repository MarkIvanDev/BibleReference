using System.Globalization;
using BibleBooks;
using BibleReference.Tests.Generators;

namespace BibleReference.Tests;

public class ParseTests
{
    [Theory(DisplayName = "Book")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void Book(BibleBook book, CultureInfo? culture)
    {
        var name = BibleBooksHelper.GetName(book, culture);
        Assert.Equal(name, BibleReferenceParser.Parse(name, culture).ToString(culture));
    }

    [Theory(DisplayName = "Book Roman Numerals")]
    [ClassData(typeof(BookRomanNumeralGenerator))]
    public void BookRomanNumeral(CultureInfo culture, BibleBook book, string bookTest)
    {
        var name = BibleBooksHelper.GetName(book, culture);
        Assert.Equal(name, BibleReferenceParser.Parse(bookTest, culture).ToString(culture));
    }

    [Theory(DisplayName = "Book Chapter")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookChapter(BibleBook book, CultureInfo? culture)
    {
        var name = BibleBooksHelper.GetName(book, culture);
        Assert.Equal($"{name} 1", BibleReferenceParser.Parse($"{name} 1", culture).ToString(culture));
    }

    [Theory(DisplayName = "Book Chapter-Chapter")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookChapterToChapter(BibleBook book, CultureInfo? culture)
    {
        if (BibleBooksHelper.GetMaxChapter(book) > 1)
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1-2", BibleReferenceParser.Parse($"{name} 1-2", culture).ToString(culture));
        }
        else
        {
            Assert.Skip("Book is single-chapter");
        }
    }

    [Theory(DisplayName = "Book Chapter:Verse")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookChapterVerse(BibleBook book, CultureInfo? culture)
    {
        var name = BibleBooksHelper.GetName(book, culture);
        Assert.Equal($"{name} 1:1", BibleReferenceParser.Parse($"{name} 1:1", culture).ToString(culture));
    }

    [Theory(DisplayName = "Book Chapter:Verse-Verse")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookChapterVerseToVerse(BibleBook book, CultureInfo? culture)
    {
        var name = BibleBooksHelper.GetName(book, culture);
        Assert.Equal($"{name} 1:1-5", BibleReferenceParser.Parse($"{name} 1:1-5", culture).ToString(culture));
    }

    [Theory(DisplayName = "Book Chapter:Verse,Verse")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookChapterVerseVerse(BibleBook book, CultureInfo? culture)
    {
        var name = BibleBooksHelper.GetName(book, culture);
        Assert.Equal($"{name} 1:1,5", BibleReferenceParser.Parse($"{name} 1:1,5", culture).ToString(culture));
    }

    [Theory(DisplayName = "Book Chapter:Verse,Verse-Verse")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookChapterVerseVerseToVerse(BibleBook book, CultureInfo? culture)
    {
        var name = BibleBooksHelper.GetName(book, culture);
        Assert.Equal($"{name} 1:1,5-7", BibleReferenceParser.Parse($"{name} 1:1,5-7", culture).ToString(culture));
    }

    [Theory(DisplayName = "Book Chapter:Verse-Verse,Verse")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookChapterVerseToVerseVerse(BibleBook book, CultureInfo? culture)
    {
        var name = BibleBooksHelper.GetName(book, culture);
        Assert.Equal($"{name} 1:1-3,8", BibleReferenceParser.Parse($"{name} 1:1-3,8", culture).ToString(culture));
    }

    [Theory(DisplayName = "Book Chapter:Verse-Verse,Verse-Verse")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookChapterVerseToVerseVerseToVerse(BibleBook book, CultureInfo? culture)
    {
        var name = BibleBooksHelper.GetName(book, culture);
        Assert.Equal($"{name} 1:1-5,8-10", BibleReferenceParser.Parse($"{name} 1:1-5,8-10", culture).ToString(culture));
    }

    [Theory(DisplayName = "Book Chapter:Verse-Verse;Chapter:Verse-Verse")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookChapterVerseToVerseAndChapterVerseToVerse(BibleBook book, CultureInfo? culture)
    {
        if (BibleBooksHelper.GetMaxChapter(book) > 1)
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1-5;2:1-3", BibleReferenceParser.Parse($"{name} 1:1-5;2:1-3", culture).ToString(culture));
        }
        else
        {
            Assert.Skip("Book is single-chapter");
        }
    }

    [Theory(DisplayName = "Book Chapter:Verse-Chapter:Verse")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookChapterVerseToChapterVerse(BibleBook book, CultureInfo? culture)
    {
        if (BibleBooksHelper.GetMaxChapter(book) > 1)
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:5-2:3", BibleReferenceParser.Parse($"{name} 1:5-2:3", culture).ToString(culture));
        }
        else
        {
            Assert.Skip("Book is single-chapter");
        }
    }

    [Theory(DisplayName = "Book Verse (for single-chapter books)")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookSingleVerse(BibleBook book, CultureInfo? culture)
    {
        if (BibleBooksHelper.GetMaxChapter(book) == 1)
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:2", BibleReferenceParser.Parse($"{name} 2", culture).ToString(culture));
        }
        else
        {
            Assert.Skip("Book is multi-chapter");
        }
    }

    [Theory(DisplayName = "Book Verse-Verse (for single-chapter books)")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookSingleVerseToVerse(BibleBook book, CultureInfo? culture)
    {
        if (BibleBooksHelper.GetMaxChapter(book) == 1)
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1-2", BibleReferenceParser.Parse($"{name} 1-2", culture).ToString(culture));
        }
        else
        {
            Assert.Skip("Book is multi-chapter");
        }
    }

    [Theory(DisplayName = "Book Verse,Verse (for single-chapter books)")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookSingleVerseVerse(BibleBook book, CultureInfo? culture)
    {
        if (BibleBooksHelper.GetMaxChapter(book) == 1)
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1,2", BibleReferenceParser.Parse($"{name} 1,2", culture).ToString(culture));
        }
        else
        {
            Assert.Skip("Book is multi-chapter");
        }
    }

    [Theory(DisplayName = "Book Verse-Verse,Verse (for single-chapter books)")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookSingleVerseToVerseVerse(BibleBook book, CultureInfo? culture)
    {
        if (BibleBooksHelper.GetMaxChapter(book) == 1)
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1-3,5", BibleReferenceParser.Parse($"{name} 1-3,5", culture).ToString(culture));
        }
        else
        {
            Assert.Skip("Book is multi-chapter");
        }
    }

    [Theory(DisplayName = "Book Verse,Verse-Verse (for single-chapter books)")]
    [ClassData(typeof(BibleBookCultureInfoMatrix))]
    public void BookSingleVerseVerseToVerse(BibleBook book, CultureInfo? culture)
    {
        if (BibleBooksHelper.GetMaxChapter(book) == 1)
        {
            var name = BibleBooksHelper.GetName(book, culture);
            Assert.Equal($"{name} 1:1,3-5", BibleReferenceParser.Parse($"{name} 1,3-5", culture).ToString(culture));
        }
        else
        {
            Assert.Skip("Book is multi-chapter");
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
