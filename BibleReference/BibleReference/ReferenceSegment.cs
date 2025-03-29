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
using System.Linq;
using System.Text;

namespace BibleReference;

public readonly struct ReferenceSegment : IEquatable<ReferenceSegment>, IComparable<ReferenceSegment>
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
        => new(ReferencePoint.WholeChapter(chapter), ReferencePoint.WholeChapter(chapter));

    public static ReferenceSegment MultipleChapters(int start, int end)
        => new(ReferencePoint.WholeChapter(start), ReferencePoint.WholeChapter(end));

    public static ReferenceSegment SingleVerse(int chapter, int verse)
        => new(new ReferencePoint(chapter, verse), new ReferencePoint(chapter, verse));

    public static ReferenceSegment MultipleVerses(int chapter, int startVerse, int endVerse)
        => new(new ReferencePoint(chapter, startVerse), new ReferencePoint(chapter, endVerse));

    public ReferencePoint Start { get; }

    public ReferencePoint End { get; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(Start.ToString());
        if (Start != End)
        {
            if (Start.Chapter == End.Chapter)
            {
                sb.Append($"-{End.Verse}");
            }
            else
            {
                sb.Append($"-{End}");
            }
        }
        return sb.ToString();
    }

    #region Equality and Inequality
    public override bool Equals(object? obj)
    {
        return obj is ReferenceSegment segment && Equals(segment);
    }

    public bool Equals(ReferenceSegment other)
    {
        return Start.Equals(other.Start) &&
               End.Equals(other.End);
    }

    public override int GetHashCode()
    {
        var hashCode = -1676728671;
        hashCode = (hashCode * -1521134295) + EqualityComparer<ReferencePoint>.Default.GetHashCode(Start);
        hashCode = (hashCode * -1521134295) + EqualityComparer<ReferencePoint>.Default.GetHashCode(End);
        return hashCode;
    }

    public int CompareTo(ReferenceSegment other)
    {
        if (Equals(other))
        {
            return 0;
        }
        else if (Start == other.Start)
        {
            return End < other.End ? -1 : 1;
        }
        else
        {
            return Start < other.Start ? -1 : 1;
        }
    }

    public static bool operator ==(ReferenceSegment segment1, ReferenceSegment segment2)
    {
        return segment1.Equals(segment2);
    }

    public static bool operator !=(ReferenceSegment segment1, ReferenceSegment segment2)
    {
        return !(segment1 == segment2);
    }

    public static bool operator <(ReferenceSegment segment1, ReferenceSegment segment2)
    {
        return segment1.CompareTo(segment2) < 0;
    }

    public static bool operator <=(ReferenceSegment segment1, ReferenceSegment segment2)
    {
        return segment1.CompareTo(segment2) <= 0;
    }

    public static bool operator >(ReferenceSegment segment1, ReferenceSegment segment2)
    {
        return segment1.CompareTo(segment2) > 0;
    }

    public static bool operator >=(ReferenceSegment segment1, ReferenceSegment segment2)
    {
        return segment1.CompareTo(segment2) >= 0;
    }
    #endregion
}

public static class ReferenceSegmentExtensions
{
    public static string Stringify(this IList<ReferenceSegment> segments)
    {
        var builder = new StringBuilder();
        for (int i = 0; i < segments.Count; i++)
        {
            if (i == 0)
            {
                builder.Append(segments[i].ToString());
            }
            else
            {
                if (segments[i - 1].Start.Chapter == segments[i - 1].End.Chapter &&
                    segments[i].Start.Chapter == segments[i].End.Chapter &&
                    segments[i - 1].Start.Chapter == segments[i].Start.Chapter &&
                    segments[i - 1].Start.Verse != 0 &&
                    segments[i].Start.Verse != 0)
                {
                    if (segments[i].Start.Verse == segments[i].End.Verse)
                    {
                        builder.Append($",{segments[i].Start.Verse}");
                    }
                    else
                    {
                        builder.Append($",{segments[i].Start.Verse}-{segments[i].End.Verse}");
                    }
                }
                else
                {
                    builder.Append($";{segments[i]}");
                }
            }
        }
        return builder.ToString();
    }

    public static ReferencePoint ComputedEnd(this ReferenceSegment segment)
    {
        return segment.End.Verse == 0
            ? new ReferencePoint(segment.End.Chapter, int.MaxValue)
            : segment.End;
    }

    public static bool IsSingleVerse(this ReferenceSegment segment)
    {
        return segment.Start == segment.End;
    }

    public static bool HasIntersection(this ReferenceSegment segment1, ReferenceSegment segment2)
    {
        return (segment1.Start <= segment2.Start && segment1.ComputedEnd() >= segment2.ComputedEnd()) ||
            (segment2.Start <= segment1.Start && segment2.ComputedEnd() >= segment1.ComputedEnd()) ||
            (segment1.Start <= segment2.Start && segment2.Start <= segment1.ComputedEnd() && segment1.ComputedEnd() <= segment2.ComputedEnd()) ||
            (segment2.Start <= segment1.Start && segment1.Start <= segment2.ComputedEnd() && segment2.ComputedEnd() <= segment1.ComputedEnd());
    }

    public static bool IsContinuous(this ReferenceSegment segment1, ReferenceSegment segment2)
    {
        return segment1.Start.Chapter == segment2.Start.Chapter && segment1.End.Verse != 0 && segment2.End.Verse != 0 &&
            (segment2.Start.Verse - segment1.End.Verse == 1 || segment1.Start.Verse - segment2.End.Verse == 1);
    }

    public static IList<ReferenceSegment> Union(this ReferenceSegment segment1, ReferenceSegment segment2)
    {
        // Case 1: segment2 is inside segment1
        if (segment1.Start <= segment2.Start && segment1.ComputedEnd() >= segment2.ComputedEnd())
        {
            return [segment1];
        }
        // Case 2: segment1 is inside segment2
        else if (segment2.Start <= segment1.Start && segment2.ComputedEnd() >= segment1.ComputedEnd())
        {
            return [segment2];
        }
        // Case 3: segment1 and segment2 only partially intersect
        else if ((segment1.Start <= segment2.Start && segment2.Start <= segment1.ComputedEnd() && segment1.ComputedEnd() <= segment2.ComputedEnd()) ||
            (segment2.Start <= segment1.Start && segment1.Start <= segment2.ComputedEnd() && segment2.ComputedEnd() <= segment1.ComputedEnd()))
        {
            var newStart = segment1.Start.CompareTo(segment2.Start) < 0 ? segment1.Start : segment2.Start;
            var newEnd = segment1.ComputedEnd().CompareTo(segment2.ComputedEnd()) > 0 ? segment1.End : segment2.End;
            return [new ReferenceSegment(newStart, newEnd)];
        }
        // Case 4: segment1 and segment 2 do not intersect
        else
        {
            // Case 4.a: segment1 and segment2 are continuous
            if (segment1.Start.Chapter == segment2.Start.Chapter && segment1.End.Verse != 0 && segment2.End.Verse != 0 &&
                (segment2.Start.Verse - segment1.End.Verse == 1 || segment1.Start.Verse - segment2.End.Verse == 1))
            {
                var newStart = segment1.Start.CompareTo(segment2.Start) < 0 ? segment1.Start : segment2.Start;
                var newEnd = segment1.ComputedEnd().CompareTo(segment2.ComputedEnd()) > 0 ? segment1.End : segment2.End;
                return [new ReferenceSegment(newStart, newEnd)];
            }
            else
            {
                return [segment1, segment2];
            }
        }
    }

    public static IList<ReferenceSegment> Simplify(this IList<ReferenceSegment> segments)
    {
        var unionSegments = segments.OrderBy(i => i).ToList();
        do
        {
            for (var i = 0; i < unionSegments.Count; i++)
            {
                var i1 = unionSegments[i];
                var i2 = unionSegments
                    .Where(i => i != i1)
                    .FirstOrDefault(i => i1.HasIntersection(i) || i1.IsContinuous(i));
                if (i2 != default)
                {
                    var union = i1.Union(i2);
                    unionSegments = [.. union, .. unionSegments.Where(i => i != i1 && i != i2)];
                    break;
                }
            }
        }
        while (unionSegments.Any(i => unionSegments.Where(j => i != j).Any(j => i.HasIntersection(j) || i.IsContinuous(j))));

        return [.. unionSegments.OrderBy(i => i)];
    }

}