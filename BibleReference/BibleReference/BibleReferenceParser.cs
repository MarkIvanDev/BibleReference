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
using BibleBooks;

namespace BibleReference
{
    public static class BibleReferenceParser
    {
        #region Reference
        public static Reference Parse(string text, CultureInfo culture = null)
        {
            var reference = InternalParse(text, out var errorMessage, culture);
            if (errorMessage != null)
            {
                throw new Exception(errorMessage);
            }
            return reference;
        }

        public static bool TryParse(string text, out Reference reference, CultureInfo culture = null)
        {
            reference = InternalParse(text, out string errorMessage, culture);
            return errorMessage == null;
        }

        private static Reference InternalParse(string text, out string errorMessage, CultureInfo culture = null)
        {
            var splitIndex = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (i != 0 && char.IsDigit(text[i]))
                {
                    splitIndex = i;
                    break;
                }
            }
            var bookPart = text.Substring(0, splitIndex).Trim();
            var book = InternalParseBook(bookPart, culture);

            var maxChapter = book.Key != null ? BibleBooksHelper.GetMaxChapter(book.Key.Value) : int.MaxValue;
            var segmentsPart = text.Substring(splitIndex).Trim();
            var segments = InternalParseSegments(segmentsPart, maxChapter, out errorMessage);
            if (errorMessage != null)
            {
                return default;
            }

            errorMessage = null;
            return new Reference(book.Key, book.Value, segments);
        } 
        #endregion

        #region Book
        public static KeyValuePair<Key?, string> ParseBook(string text, CultureInfo culture = null)
        {
            return InternalParseBook(text, culture);
        }

        public static bool TryParseBook(string text, out KeyValuePair<Key?, string> book, CultureInfo culture = null)
        {
            book = InternalParseBook(text, culture);
            return book.Key.HasValue;
        }

        private static KeyValuePair<Key?, string> InternalParseBook(string text, CultureInfo culture = null)
        {
            var potentialBook = ConvertToArabicNumerals(text, culture);

            var key = BibleBooksHelper.GetKeyForName(text, culture) ??
                BibleBooksHelper.GetKeyForOsisCode(text) ??
                BibleBooksHelper.GetKeyForParatextCode(text) ??
                BibleBooksHelper.GetKeyForStandardAbbreviation(text, culture) ??
                BibleBooksHelper.GetKeyForThompsonAbbreviation(text, culture) ??
                (!text.Equals(string.Empty) ? BibleBooksHelper.GetKeyForAlternativeName(text, culture) : null);

            if (key == null)
            {
                return new KeyValuePair<Key?, string>(null, potentialBook);
            }
            else
            {
                return new KeyValuePair<Key?, string>(key, BibleBooksHelper.GetName(key.Value, culture));
            }
        }

        private static string ConvertToArabicNumerals(string str, CultureInfo culture = null)
        {
            // Break up on all remaining white space
            var parts = (str ?? "").Trim().Split(' ', '\r', '\n', '\t');

            // If the first part is a roman numeral, or spelled ordinal, convert it to arabic
            var number = parts[0];
            switch (number)
            {
                case var n when
                    n.Equals(BibleBooksHelper.GetNumber(Number.First, culture), StringComparison.CurrentCultureIgnoreCase) ||
                    n.Equals("I", StringComparison.CurrentCultureIgnoreCase):

                    parts[0] = "1";
                    break;

                case var n when
                    n.Equals(BibleBooksHelper.GetNumber(Number.Second, culture), StringComparison.CurrentCultureIgnoreCase) ||
                    n.Equals("II", StringComparison.CurrentCultureIgnoreCase):

                    parts[0] = "2";
                    break;

                case var n when
                    n.Equals(BibleBooksHelper.GetNumber(Number.Third, culture), StringComparison.CurrentCultureIgnoreCase) ||
                    n.Equals("III", StringComparison.CurrentCultureIgnoreCase):

                    parts[0] = "3";
                    break;

                case var n when
                    n.Equals(BibleBooksHelper.GetNumber(Number.Fourth, culture), StringComparison.CurrentCultureIgnoreCase) ||
                    n.Equals("IV", StringComparison.CurrentCultureIgnoreCase):

                    parts[0] = "4";
                    break;

                case var n when
                    n.Equals(BibleBooksHelper.GetNumber(Number.Fifth, culture), StringComparison.CurrentCultureIgnoreCase) ||
                    n.Equals("V", StringComparison.CurrentCultureIgnoreCase):

                    parts[0] = "5";
                    break;
            }

            return string.Join(" ", parts);
        }
        #endregion

        #region Segments
        public static IList<ReferenceSegment> ParseSegments(string text, int maxChapter)
        {
            var segments = InternalParseSegments(text, maxChapter, out var errorMessage);
            if (errorMessage != null)
            {
                throw new Exception(errorMessage);
            }
            return segments;
        }

        public static bool TryParseSegments(string text, int maxChapter, out IList<ReferenceSegment> segments)
        {
            segments = InternalParseSegments(text, maxChapter, out var errorMessage);
            return errorMessage == null;
        }

        private static IList<ReferenceSegment> InternalParseSegments(string text, int maxChapter, out string errorMessage)
        {
            try
            {
                errorMessage = null;
                var referenceSegments = new Collection<ReferenceSegment>();

                var citations = text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var citation in citations)
                {
                    // Skip if citation is empty or the given text ends with ;
                    if (string.IsNullOrWhiteSpace(citation)) continue;

                    var segments = citation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    int? chapterNumber = null;
                    foreach (var segment in segments)
                    {
                        // Skip if citation is empty or the given text ends with ;
                        if (string.IsNullOrWhiteSpace(segment)) continue;

                        var ranges = segment.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        switch (ranges.Length)
                        {
                            case 1:
                                var point = InternalParsePoint(ranges[0], maxChapter, ref chapterNumber, out errorMessage);
                                if (errorMessage != null)
                                {
                                    return referenceSegments;
                                }
                                referenceSegments.Add(new ReferenceSegment(point));
                                break;
                            case 2:
                                var startPoint = InternalParsePoint(ranges[0], maxChapter, ref chapterNumber, out errorMessage);
                                if (errorMessage != null)
                                {
                                    return referenceSegments;
                                }
                                var endPoint = InternalParsePoint(ranges[1], maxChapter, ref chapterNumber, out errorMessage);
                                if (errorMessage != null)
                                {
                                    return referenceSegments;
                                }
                                referenceSegments.Add(new ReferenceSegment(startPoint, endPoint));
                                break;
                            default:
                                errorMessage = "Invalid range format";
                                return referenceSegments;
                        }
                    }
                }

                return referenceSegments;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return new Collection<ReferenceSegment>();
            }
        }
        #endregion

        #region Points
        public static ReferencePoint ParsePoint(string text, int maxChapter, ref int? chapterOverride)
        {
            var point = InternalParsePoint(text, maxChapter, ref chapterOverride, out var errorMessage);
            if (errorMessage != null)
            {
                throw new Exception(errorMessage);
            }
            return point;
        }

        public static bool TryParsePoint(string text, int maxChapter, ref int? chapterOverride, out ReferencePoint point)
        {
            point = InternalParsePoint(text, maxChapter, ref chapterOverride, out var errorMessage);
            return errorMessage == null;
        }

        private static ReferencePoint InternalParsePoint(string text, int maxChapter, ref int? chapterOverride, out string errorMessage)
        {
            try
            {
                errorMessage = null;
                var colonIndex = text.IndexOf(':');
                if (colonIndex != -1)
                {
                    var parts = text.Split(':');
                    if (!int.TryParse(parts[0], out var chapter))
                    {
                        errorMessage = "Chapter is not a number";
                        return default;
                    }

                    if(chapter <= 0)
                    {
                        errorMessage = "Chapter cannot be less than or equal to 0";
                        return default;
                    }

                    if (chapter > maxChapter)
                    {
                        errorMessage = "Chapter exceeds max chapter";
                        return default;
                    }

                    if (!int.TryParse(parts[1], out var verse))
                    {
                        errorMessage = "Verse is not a number";
                        return default;
                    }

                    chapterOverride = chapter;
                    return new ReferencePoint(chapter, verse);
                }
                else
                {
                    if (!int.TryParse(text, out var i))
                    {
                        errorMessage = "Invalid format";
                        return default;
                    }

                    if (maxChapter == 1)
                    {
                        chapterOverride = 1;
                        return new ReferencePoint(1, i);
                    }
                    else if (chapterOverride.HasValue)
                    {
                        return new ReferencePoint(chapterOverride.Value, i);
                    }
                    else
                    {
                        if (i > maxChapter)
                        {
                            errorMessage = "Chapter exceed max chapter";
                            return default;
                        }

                        if(i <= 0)
                        {
                            errorMessage = "Chapter cannot be less than or equal to 0";
                            return default;
                        }

                        chapterOverride = i;
                        return new ReferencePoint(i);
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return default;
            }
        } 
        #endregion

    }
}
