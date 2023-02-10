using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibleBooks;

namespace BibleReference.Tests
{
    public class ParseTests
    {
        [Theory(DisplayName = "Book")]
        [ClassData(typeof(CultureInfoGenerator))]
        public void Book(CultureInfo? culture)
        {
            foreach (var book in Enum.GetValues<BibleBook>())
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal(name, BibleReferenceParser.Parse(name, culture).ToString());
            }
        }

        [Theory(DisplayName = "Book Chapter")]
        [ClassData(typeof(CultureInfoGenerator))]
        public void BookChapter(CultureInfo? culture)
        {
            foreach (var book in Enum.GetValues<BibleBook>())
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1", BibleReferenceParser.Parse($"{name} 1", culture).ToString());
            }
        }

        [Theory(DisplayName = "Book Chapter-Chapter")]
        [ClassData(typeof(CultureInfoGenerator))]
        public void BookChapterChapter(CultureInfo? culture)
        {
            foreach (var book in Enum.GetValues<BibleBook>())
            {
                if (BibleBooksHelper.GetMaxChapter(book) > 1)
                {
                    var name = BibleBooksHelper.GetName(book, culture);
                    Assert.Equal($"{name} 1-2", BibleReferenceParser.Parse($"{name} 1-2", culture).ToString());
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
                Assert.Equal($"{name} 1:1", BibleReferenceParser.Parse($"{name} 1:1", culture).ToString());
            }
        }

        [Theory(DisplayName = "Book Chapter:Verse-Verse")]
        [ClassData(typeof(CultureInfoGenerator))]
        public void BookChapterVerseVerse(CultureInfo? culture)
        {
            foreach (var book in Enum.GetValues<BibleBook>())
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1:1-5", BibleReferenceParser.Parse($"{name} 1:1-5", culture).ToString());
            }
        }

        [Theory(DisplayName = "Book Chapter:Verse,Verse")]
        [ClassData(typeof(CultureInfoGenerator))]
        public void BookChapterVerseVerse2(CultureInfo? culture)
        {
            foreach (var book in Enum.GetValues<BibleBook>())
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1:1,5", BibleReferenceParser.Parse($"{name} 1:1,5", culture).ToString());
            }
        }

        [Theory(DisplayName = "Book Chapter:Verse-Verse,Verse-Verse")]
        [ClassData(typeof(CultureInfoGenerator))]
        public void BookChapterVerseVerseVerseVerse(CultureInfo? culture)
        {
            foreach (var book in Enum.GetValues<BibleBook>())
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1:1-5,8-10", BibleReferenceParser.Parse($"{name} 1:1-5,8-10", culture).ToString());
            }
        }

        [Theory(DisplayName = "Book Chapter:Verse-Verse;Chapter:Verse-Verse")]
        [ClassData(typeof(CultureInfoGenerator))]
        public void BookChapterVerseVerseChapterVerseVerse(CultureInfo? culture)
        {
            foreach (var book in Enum.GetValues<BibleBook>())
            {
                if (BibleBooksHelper.GetMaxChapter(book) > 1)
                {
                    var name = BibleBooksHelper.GetName(book, culture);
                    Assert.Equal($"{name} 1:1-5;2:1-3", BibleReferenceParser.Parse($"{name} 1:1-5;2:1-3", culture).ToString());
                }
                
            }
        }

        [Theory(DisplayName = "Book Chapter:Verse-Chapter:Verse")]
        [ClassData(typeof(CultureInfoGenerator))]
        public void BookChapterVerseChapterVerse(CultureInfo? culture)
        {
            foreach (var book in Enum.GetValues<BibleBook>())
            {
                if (BibleBooksHelper.GetMaxChapter(book) > 1)
                {
                    var name = BibleBooksHelper.GetName(book, culture);
                    Assert.Equal($"{name} 1:5-2:3", BibleReferenceParser.Parse($"{name} 1:5-2:3", culture).ToString());
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
                    Assert.Equal($"{name} 1:2", BibleReferenceParser.Parse($"{name} 2", culture).ToString());
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
                    Assert.Equal($"{name} 1:1-2", BibleReferenceParser.Parse($"{name} 1-2", culture).ToString());
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
                    Assert.Equal($"{name} 1:1,2", BibleReferenceParser.Parse($"{name} 1,2", culture).ToString());
                }
            }
        }

        [Theory(DisplayName = "Book Verse-Verse,Verse (for single-chapter books)")]
        [ClassData(typeof(CultureInfoGenerator))]
        public void BookVerseVerseVerse(CultureInfo? culture)
        {
            foreach (var book in Enum.GetValues<BibleBook>())
            {
                if (BibleBooksHelper.GetMaxChapter(book) == 1)
                {
                    var name = BibleBooksHelper.GetName(book, culture);
                    Assert.Equal($"{name} 1:1-3,5", BibleReferenceParser.Parse($"{name} 1-3,5", culture).ToString());
                }
            }
        }

    }
}
