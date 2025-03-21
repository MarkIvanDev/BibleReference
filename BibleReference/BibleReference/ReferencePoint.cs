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
using System.Text;

namespace BibleReference;

public readonly struct ReferencePoint : IEquatable<ReferencePoint>, IComparable<ReferencePoint>
{
    public ReferencePoint(int chapter, int verse)
    {
        if (chapter <= 0)
        {
            throw new ArgumentException("Chapter number cannot be less than or equal to 0", nameof(chapter));
        }

        if (verse < 0)
        {
            throw new ArgumentException("Verse number cannot be less than 0", nameof(verse));
        }

        Chapter = chapter;
        Verse = verse;
    }

    public static ReferencePoint WholeChapter(int chapter)
    {
        return new ReferencePoint(chapter, 0);
    }

    public int Chapter { get; }

    public int Verse { get; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(Chapter);
        if (Verse != 0)
        {
            sb.Append($":{Verse}");
        }

        return sb.ToString();
    }

    #region Equality and Inequality
    public int CompareTo(ReferencePoint other)
    {
        if (Equals(other))
        {
            return 0;
        }
        else if (Chapter == other.Chapter)
        {
            return Verse < other.Verse ? -1 : 1;
        }
        else
        {
            return Chapter < other.Chapter ? -1 : 1;
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is ReferencePoint point && Equals(point);
    }

    public bool Equals(ReferencePoint other)
    {
        return Chapter == other.Chapter &&
               Verse == other.Verse;
    }

    public override int GetHashCode()
    {
        var hashCode = 1671737298;
        hashCode = (hashCode * -1521134295) + Chapter.GetHashCode();
        hashCode = (hashCode * -1521134295) + Verse.GetHashCode();
        return hashCode;
    }

    public static bool operator ==(ReferencePoint point1, ReferencePoint point2)
    {
        return point1.Equals(point2);
    }

    public static bool operator !=(ReferencePoint point1, ReferencePoint point2)
    {
        return !(point1 == point2);
    }

    public static bool operator <(ReferencePoint point1, ReferencePoint point2)
    {
        return point1.CompareTo(point2) < 0;
    }

    public static bool operator <=(ReferencePoint point1, ReferencePoint point2)
    {
        return point1.CompareTo(point2) <= 0;
    }

    public static bool operator >(ReferencePoint point1, ReferencePoint point2)
    {
        return point1.CompareTo(point2) > 0;
    }

    public static bool operator >=(ReferencePoint point1, ReferencePoint point2)
    {
        return point1.CompareTo(point2) >= 0;
    }
    #endregion
}
