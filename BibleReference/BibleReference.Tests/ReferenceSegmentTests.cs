using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleReference.Tests
{
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

    }
}
