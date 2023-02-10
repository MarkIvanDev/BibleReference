// MIT License

// Copyright(c) 2020 Mark Ivan Basto

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE. 

using System;
using System.Collections.Generic;
using System.Text;

namespace BibleReference
{
    public readonly struct ReferenceSegment : IEquatable<ReferenceSegment>
    {
        public ReferenceSegment(ReferencePoint start, ReferencePoint end)
        {
            if (start > end)
            {
                throw new ArgumentException("Start point is greater than the end point");
            }

            if (start.Verse == 0 ^ end.Verse == 0)
            {
                throw new ArgumentException("Start and end verses are only valid when both are 0 or both are greater than 0");
            }

            Start = start;
            End = end;
        }

        public static ReferenceSegment SingleChapter(int chapter)
            => new ReferenceSegment(ReferencePoint.WholeChapter(chapter), ReferencePoint.WholeChapter(chapter));

        public static ReferenceSegment MultipleChapters(int start, int end)
            => new ReferenceSegment(ReferencePoint.WholeChapter(start), ReferencePoint.WholeChapter(end));

        public static ReferenceSegment SingleVerse(int chapter, int verse)
            => new ReferenceSegment(new ReferencePoint(chapter, verse), new ReferencePoint(chapter, verse));

        public static ReferenceSegment MultipleVerses(int chapter, int startVerse, int endVerse)
            => new ReferenceSegment(new ReferencePoint(chapter, startVerse), new ReferencePoint(chapter, endVerse));

        public ReferencePoint Start { get; }

        public ReferencePoint End { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Start.ToString());
            if(Start != End)
            {
                if(Start.Chapter == End.Chapter)
                {
                    sb.Append($"-{End.Verse}");
                }
                else
                {
                    sb.Append($"-{End.ToString()}");
                }
            }
            return sb.ToString();
        }

        #region Equality
        public override bool Equals(object obj)
        {
            return obj is ReferenceSegment && Equals((ReferenceSegment)obj);
        }

        public bool Equals(ReferenceSegment other)
        {
            return Start.Equals(other.Start) &&
                   End.Equals(other.End);
        }

        public override int GetHashCode()
        {
            var hashCode = -1676728671;
            hashCode = hashCode * -1521134295 + EqualityComparer<ReferencePoint>.Default.GetHashCode(Start);
            hashCode = hashCode * -1521134295 + EqualityComparer<ReferencePoint>.Default.GetHashCode(End);
            return hashCode;
        }

        public static bool operator ==(ReferenceSegment segment1, ReferenceSegment segment2)
        {
            return segment1.Equals(segment2);
        }

        public static bool operator !=(ReferenceSegment segment1, ReferenceSegment segment2)
        {
            return !(segment1 == segment2);
        } 
        #endregion
    }
}
