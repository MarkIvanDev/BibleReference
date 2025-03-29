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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using BibleBooks;

namespace BibleReference;

public sealed class Reference : IEquatable<Reference>
{
    public Reference(BibleBook book, params ReferenceSegment[]? segments) : this(book, segments?.ToList())
    {
    }

    public Reference(BibleBook book, IList<ReferenceSegment>? segments)
    {
        if (segments == null)
        {
            throw new ArgumentNullException(nameof(segments));
        }

        Book = book;
        Segments = new ReadOnlyCollection<ReferenceSegment>(segments);
    }

    public BibleBook Book { get; }

    public ReadOnlyCollection<ReferenceSegment> Segments { get; }

    public override bool Equals(object? obj)
    {
        return obj is Reference reference && Equals(reference);
    }

    public bool Equals(Reference other)
    {
        return Book == other.Book &&
            Segments.SequenceEqual(other.Segments);
    }

    public override int GetHashCode()
    {
        var hashCode = -966783545;
        hashCode = (hashCode * -1521134295) + Book.GetHashCode();
        foreach (var segment in Segments)
        {
            hashCode = (hashCode * -1521134295) + segment.GetHashCode();
        }
        return hashCode;
    }

    public override string ToString()
    {
        return ToString(null);
    }

    public string ToString(CultureInfo? culture)
    {
        var builder = new StringBuilder();
        builder.Append(BibleBooksHelper.GetName(Book, culture));
        if (Segments.Count != 0)
        {
            builder.Append(" ");
            builder.Append(Segments.Stringify());
        }
        return builder.ToString();
    }

    public static bool operator ==(Reference left, Reference right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Reference left, Reference right)
    {
        return !(left == right);
    }
}

public static class ReferenceExtensions
{
    public static Reference Simplify(this Reference reference)
    {
        var simplifiedSegments = reference.Segments.Simplify();
        return new Reference(reference.Book, simplifiedSegments);
    }
}
