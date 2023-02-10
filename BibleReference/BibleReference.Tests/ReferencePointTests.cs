namespace BibleReference.Tests
{
    public class ReferencePointTests
    {
        [Fact(DisplayName = "Chapter <= 0")]
        public void InvalidChapter()
        {
            Assert.Throws<ArgumentException>(() => new ReferencePoint(0, 1));
            Assert.Throws<ArgumentException>(() => new ReferencePoint(-1, 1));
        }

        [Fact(DisplayName = "Verse < 0")]
        public void InvalidVerse()
        {
            Assert.Throws<ArgumentException>(() => new ReferencePoint(1, -1));
        }

        [Fact(DisplayName = "== operator")]
        public void EqualsOperator()
        {
            Assert.True(new ReferencePoint(1, 1) == new ReferencePoint(1, 1));
            Assert.False(new ReferencePoint(1, 1) == new ReferencePoint(2, 2));
        }

        [Fact(DisplayName = "!= operator")]
        public void NotEqualsOperator()
        {
            Assert.True(new ReferencePoint(1, 1) != new ReferencePoint(2, 2));
            Assert.False(new ReferencePoint(1, 1) != new ReferencePoint(1, 1));
        }

        [Fact(DisplayName = "> operator")]
        public void GreaterThanOperator()
        {
            Assert.True(new ReferencePoint(1, 2) > new ReferencePoint(1, 1));
            Assert.True(new ReferencePoint(2, 1) > new ReferencePoint(1, 2));

            Assert.False(new ReferencePoint(1, 1) > new ReferencePoint(1, 1));
            Assert.False(new ReferencePoint(1, 1) > new ReferencePoint(1, 2));
            Assert.False(new ReferencePoint(1, 2) > new ReferencePoint(2, 1));
        }

        [Fact(DisplayName = ">= operator")]
        public void GreaterThanOrEqualOperator()
        {
            Assert.True(new ReferencePoint(1, 2) >= new ReferencePoint(1, 1));
            Assert.True(new ReferencePoint(2, 1) >= new ReferencePoint(1, 2));
            Assert.True(new ReferencePoint(1, 1) >= new ReferencePoint(1, 1));

            Assert.False(new ReferencePoint(1, 1) >= new ReferencePoint(1, 2));
            Assert.False(new ReferencePoint(1, 2) >= new ReferencePoint(2, 1));
        }

        [Fact(DisplayName = "< operator")]
        public void LessThanOperator()
        {
            Assert.True(new ReferencePoint(1, 1) < new ReferencePoint(1, 2));
            Assert.True(new ReferencePoint(1, 2) < new ReferencePoint(2, 1));

            Assert.False(new ReferencePoint(1, 1) < new ReferencePoint(1, 1));
            Assert.False(new ReferencePoint(1, 2) < new ReferencePoint(1, 1));
            Assert.False(new ReferencePoint(2, 1) < new ReferencePoint(1, 2));
        }

        [Fact(DisplayName = "<= operator")]
        public void LessThanOrEqualOperator()
        {
            Assert.True(new ReferencePoint(1, 1) <= new ReferencePoint(1, 2));
            Assert.True(new ReferencePoint(1, 2) <= new ReferencePoint(2, 1));
            Assert.True(new ReferencePoint(1, 1) <= new ReferencePoint(1, 1));

            Assert.False(new ReferencePoint(1, 2) <= new ReferencePoint(1, 1));
            Assert.False(new ReferencePoint(2, 1) <= new ReferencePoint(1, 2));
        }

        [Fact(DisplayName = "Whole Chapter ToString")]
        public void WholeChapterToString()
        {
            Assert.Equal("1", new ReferencePoint(1, 0).ToString());
        }

        [Fact(DisplayName = "Specific Verse ToString")]
        public void SpecificVerseToString()
        {
            Assert.Equal("1:1", new ReferencePoint(1, 1).ToString());
        }
    }
}