﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibleBooks;

namespace BibleReference.Tests
{
    public class ReferenceTests
    {
        [Theory(DisplayName = "Single Chapter ToString")]
        [ClassData(typeof(CultureInfoGenerator))]
        public void SingleChapterToString(CultureInfo? culture)
        {
            foreach (var book in Enum.GetValues<BibleBook>())
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1", new Reference(book, ReferenceSegment.SingleChapter(1)).ToString());
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
                    Assert.Equal($"{name} 1-2", new Reference(book, ReferenceSegment.MultipleChapters(1, 2)).ToString());
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
                Assert.Equal($"{name} 1:1", new Reference(book, ReferenceSegment.SingleVerse(1, 1)).ToString());
            }
        }

        [Theory(DisplayName = "Multiple Verses ToString")]
        [ClassData(typeof(CultureInfoGenerator))]
        public void MultipleVersesToString(CultureInfo? culture)
        {
            foreach (var book in Enum.GetValues<BibleBook>())
            {
                var name = BibleBooksHelper.GetName(book, culture);
                Assert.Equal($"{name} 1:1-5", new Reference(book, ReferenceSegment.MultipleVerses(1, 1, 5)).ToString());
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
                    ).ToString());
            }
        }

    }
}
