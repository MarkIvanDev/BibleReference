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
using System.Linq;
using System.Text;
using BibleBooks;

namespace BibleReference
{
    public class Reference
    {
        public Reference(Key? bookKey, string bookName, params ReferenceSegment[] segments) : this(bookKey, bookName, segments?.ToList() ?? new List<ReferenceSegment>())
        {
        }

        public Reference(Key? bookKey, string bookName, IList<ReferenceSegment> segments)
        {
            if (segments == null)
            {
                throw new ArgumentNullException(nameof(segments));
            }

            BookKey = bookKey;
            BookName = bookName;
            Segments = new ReadOnlyCollection<ReferenceSegment>(segments);
        }

        public Key? BookKey { get; }

        public string BookName { get; }

        public ReadOnlyCollection<ReferenceSegment> Segments { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(BookName);
            if(Segments.Count != 0)
            {
                sb.Append(" ");
                sb.Append(string.Join(";", Segments));
            }
            return sb.ToString();
        }
    }
}
