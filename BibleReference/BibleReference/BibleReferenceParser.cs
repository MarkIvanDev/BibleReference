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
using System.Globalization;
using System.Linq;
using BibleBooks;

namespace BibleReference;

public static class BibleReferenceParser
{
    public static Reference Parse(string? text, CultureInfo? culture = null)
    {
        var referenceResult = InternalParse(text ?? string.Empty, culture);
        if (referenceResult.IsSuccessful && referenceResult.Reference is not null)
        {
            return referenceResult.Reference;
        }
        else
        {
            throw new Exception(referenceResult.ErrorMessage);
        }
    }

    public static bool TryParse(string? text, out Reference? reference, CultureInfo? culture = null)
    {
        var referenceResult = InternalParse(text ?? string.Empty, culture);
        reference = referenceResult.Reference;
        return referenceResult.IsSuccessful;
    }

    public static IList<ReferenceSegment> ParseSegments(string? text)
    {
        return ParseSegments(text, int.MaxValue);
    }

    public static bool TryParseSegments(string? text, out IList<ReferenceSegment>? segments)
    {
        return TryParseSegments(text, int.MaxValue, out segments);
    }

    public static IList<ReferenceSegment> ParseSegments(string? text, int maxChapter)
    {
        var segmentResult = InternalParseSegments(text ?? string.Empty, maxChapter);
        if (segmentResult.IsSuccessful && segmentResult.Segments is not null)
        {
            return segmentResult.Segments;
        }
        else
        {
            throw new Exception(segmentResult.ErrorMessage);
        }
    }

    public static bool TryParseSegments(string? text, int maxChapter, out IList<ReferenceSegment>? segments)
    {
        var segmentsResult = InternalParseSegments(text ?? string.Empty, maxChapter);
        segments = segmentsResult.Segments;
        return segmentsResult.IsSuccessful;
    }

    public static ReferencePoint ParsePoint(string? text)
    {
        return ParsePoint(text, int.MaxValue, null);
    }

    public static bool TryParsePoint(string? text, out ReferencePoint? point)
    {
        return TryParsePoint(text, int.MaxValue, null, out point);
    }

    public static ReferencePoint ParsePoint(string? text, int maxChapter, int? chapterOverride)
    {
        var pointResult = InternalParsePoint(text ?? string.Empty, maxChapter, chapterOverride);
        if (pointResult.IsSuccessful && pointResult.Point.HasValue)
        {
            return pointResult.Point.Value;
        }
        else
        {
            throw new Exception(pointResult.ErrorMessage);
        }
    }

    public static bool TryParsePoint(string? text, int maxChapter, int? chapterOverride, out ReferencePoint? point)
    {
        var pointResult = InternalParsePoint(text ?? string.Empty, maxChapter, chapterOverride);
        point = pointResult.Point;
        return pointResult.IsSuccessful;
    }

    private static ReferenceResult InternalParse(string text, CultureInfo? culture = null)
    {
        var textTrimmed = text.Trim();
        if (string.IsNullOrEmpty(textTrimmed)) return ReferenceResult.Error("Text is empty");

        var splitIndex = 0;
        for (int i = 0; i < textTrimmed.Length; i++)
        {
            if (i != 0 && char.IsDigit(textTrimmed[i]))
            {
                splitIndex = i;
                break;
            }
        }
        var bookPart = textTrimmed
            .Substring(0, splitIndex == 0 ? textTrimmed.Length : splitIndex)
            .Trim();
        var bookResult = InternalParseBook(bookPart, culture);
        if (bookResult.IsSuccessful && bookResult.Book.HasValue)
        {
            if (splitIndex == 0)
            {
                return ReferenceResult.Success(new Reference(bookResult.Book.Value));
            }
            else
            {
                var maxChapter = BibleBooksHelper.GetMaxChapter(bookResult.Book.Value) ?? 0;
                var segmentsPart = textTrimmed.Substring(splitIndex).Trim();
                var segmentsResult = InternalParseSegments(segmentsPart, maxChapter);
                if (segmentsResult.IsSuccessful && segmentsResult.Segments != null)
                {
                    return ReferenceResult.Success(new Reference(bookResult.Book.Value, segmentsResult.Segments));
                }
                else
                {
                    return ReferenceResult.Error(segmentsResult.ErrorMessage ?? string.Empty);
                }
            }
        }
        else
        {
            return ReferenceResult.Error(bookResult.ErrorMessage ?? string.Empty);
        }
    }

    private static ReferenceBookResult InternalParseBook(string text, CultureInfo? culture = null)
    {
        var potentialBook = ConvertToArabicNumerals(text, culture);

        var key = BibleBooksHelper.GetKeyForName(potentialBook, culture) ??
            BibleBooksHelper.GetKeyForOsisCode(potentialBook) ??
            BibleBooksHelper.GetKeyForParatextCode(potentialBook) ??
            BibleBooksHelper.GetKeyForStandardAbbreviation(potentialBook, culture) ??
            BibleBooksHelper.GetKeyForThompsonAbbreviation(potentialBook, culture) ??
            (!potentialBook.Equals(string.Empty) ? BibleBooksHelper.GetKeyForAlternativeName(potentialBook, culture) : null);

        if (key.HasValue)
        {
            return ReferenceBookResult.Success(key.Value);
        }
        else
        {
            return ReferenceBookResult.Error("Unknown book name");
        }
    }

    private static string ConvertToArabicNumerals(string str, CultureInfo? culture = null)
    {
        // Break up on all remaining white space
        var parts = str
            .Trim()
            .Split(' ', '\r', '\n', '\t')
            .Select(p => p.Trim())
            .Where(p => !string.IsNullOrEmpty(p))
            .ToList();
        if (parts.Count == 0) return string.Empty;

        // If the first part is a roman numeral, or spelled ordinal, convert it to arabic
        var number = parts[0];
        switch (number)
        {
            case var n when
                IsEqual(n, BibleBooksHelper.GetNumber(Number.First, culture), culture) ||
                IsEqual(n, "I", culture):

                parts[0] = "1";
                break;

            case var n when
                IsEqual(n, BibleBooksHelper.GetNumber(Number.Second, culture), culture) ||
                IsEqual(n, "II", culture):

                parts[0] = "2";
                break;

            case var n when
                IsEqual(n, BibleBooksHelper.GetNumber(Number.Third, culture), culture) ||
                IsEqual(n, "III", culture):

                parts[0] = "3";
                break;

            case var n when
                IsEqual(n, BibleBooksHelper.GetNumber(Number.Fourth, culture), culture) ||
                IsEqual(n, "IV", culture):

                parts[0] = "4";
                break;

            case var n when
                IsEqual(n, BibleBooksHelper.GetNumber(Number.Fifth, culture), culture) ||
                IsEqual(n, "V", culture):

                parts[0] = "5";
                break;
        }

        return string.Join(" ", parts);
    }

    private static ReferenceSegmentResult InternalParseSegments(string text, int maxChapter)
    {
        try
        {
            var referenceSegments = new List<ReferenceSegment>();

            if (string.IsNullOrWhiteSpace(text))
            {
                return ReferenceSegmentResult.Success(referenceSegments);
            }

            if (maxChapter == 1 && !text.Contains(":"))
            {
                if (int.TryParse(text, out var i) && i == 1)
                {
                    referenceSegments.Add(ReferenceSegment.SingleChapter(1));
                    return ReferenceSegmentResult.Success(referenceSegments);
                }
                else
                {
                    return InternalParseSegments($"1:{text}", maxChapter);
                }
            }

            var citations = text
                .Split([';'], StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim())
                .Where(c => !string.IsNullOrEmpty(c));
            foreach (var citation in citations)
            {
                int? chapterNumber = null;
                var segments = citation
                    .Split([','], StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrEmpty(s));
                foreach (var segment in segments)
                {
                    var ranges = segment
                        .Split(['-'], StringSplitOptions.RemoveEmptyEntries)
                        .Select(r => r.Trim())
                        .Where(r => !string.IsNullOrEmpty(r))
                        .ToList();
                    switch (ranges.Count)
                    {
                        case 1:
                            var pointResult = InternalParsePoint(ranges[0], maxChapter, chapterNumber);
                            if (pointResult.IsSuccessful && pointResult.Point.HasValue)
                            {
                                chapterNumber = pointResult.ChapterOverride;
                                referenceSegments.Add(ReferenceSegment.SingleVerse(pointResult.Point.Value.Chapter, pointResult.Point.Value.Verse));
                            }
                            else
                            {
                                return ReferenceSegmentResult.Error(pointResult.ErrorMessage ?? string.Empty);
                            }
                            break;

                        case 2:
                            var startPointResult = InternalParsePoint(ranges[0], maxChapter, chapterNumber);
                            if (startPointResult.IsSuccessful && startPointResult.Point.HasValue)
                            {
                                chapterNumber = startPointResult.ChapterOverride;
                            }
                            else
                            {
                                return ReferenceSegmentResult.Error(startPointResult.ErrorMessage ?? string.Empty);
                            }
                            var endPointResult = InternalParsePoint(ranges[1], maxChapter, chapterNumber);
                            if (endPointResult.IsSuccessful && endPointResult.Point.HasValue)
                            {
                                chapterNumber = endPointResult.ChapterOverride;
                            }
                            else
                            {
                                return ReferenceSegmentResult.Error(endPointResult.ErrorMessage ?? string.Empty);
                            }
                            referenceSegments.Add(new ReferenceSegment(startPointResult.Point.Value, endPointResult.Point.Value));
                            break;

                        default:
                            return ReferenceSegmentResult.Error("Invalid range format");
                    }
                }
            }

            return ReferenceSegmentResult.Success(referenceSegments);
        }
        catch (Exception ex)
        {
            return ReferenceSegmentResult.Error(ex.Message);
        }
    }

    private static ReferencePointResult InternalParsePoint(string text, int maxChapter, int? chapterOverride)
    {
        try
        {
            var colonIndex = text.IndexOf(':');
            if (colonIndex != -1)
            {
                var parts = text.Split(':');
                if (!int.TryParse(parts[0], out var chapter))
                {
                    return ReferencePointResult.Error("Chapter is not a number");
                }

                if (chapter <= 0)
                {
                    return ReferencePointResult.Error("Chapter cannot be less than or equal to 0");
                }

                if (chapter > maxChapter)
                {
                    return ReferencePointResult.Error("Chapter exceeds max chapter");
                }

                if (!int.TryParse(parts[1], out var verse))
                {
                    return ReferencePointResult.Error("Verse is not a number");
                }

                return ReferencePointResult.Success(new ReferencePoint(chapter, verse), chapter);
            }
            else
            {
                if (!int.TryParse(text, out var i))
                {
                    return ReferencePointResult.Error("Invalid format");
                }

                if (maxChapter == 1)
                {
                    chapterOverride = 1;
                    if (i == 1)
                    {
                        return ReferencePointResult.Success(new ReferencePoint(i, 0), i);
                    }
                    else
                    {
                        return ReferencePointResult.Success(new ReferencePoint(1, i), 1);
                    }
                }
                else if (chapterOverride.HasValue)
                {
                    return ReferencePointResult.Success(new ReferencePoint(chapterOverride.Value, i), chapterOverride.Value);
                }
                else
                {
                    if (i > maxChapter)
                    {
                        return ReferencePointResult.Error("Chapter exceeds max chapter");
                    }

                    if (i <= 0)
                    {
                        return ReferencePointResult.Error("Chapter cannot be less than or equal to 0");
                    }

                    return ReferencePointResult.Success(ReferencePoint.WholeChapter(i), chapterOverride);
                }
            }
        }
        catch (Exception ex)
        {
            return ReferencePointResult.Error(ex.Message);
        }
    }

    private static bool IsEqual(string? a, string? b, CultureInfo? culture)
    {
        var comparer = culture?.CompareInfo?.GetStringComparer(CompareOptions.IgnoreCase) ?? StringComparer.OrdinalIgnoreCase;
        return comparer.Equals(a, b);
    }

    private class ReferenceResult
    {
        private ReferenceResult(bool isSuccessful, Reference? reference, string? errorMessage)
        {
            IsSuccessful = isSuccessful;
            Reference = reference;
            ErrorMessage = errorMessage;
        }

        public static ReferenceResult Success(Reference reference)
            => new ReferenceResult(true, reference, null);

        public static ReferenceResult Error(string errorMessage)
            => new ReferenceResult(false, null, errorMessage);

        public bool IsSuccessful { get; }

        public Reference? Reference { get; }

        public string? ErrorMessage { get; }
    }

    private class ReferenceBookResult
    {
        private ReferenceBookResult(bool isSuccessful, BibleBook? book, string? errorMessage)
        {
            IsSuccessful = isSuccessful;
            Book = book;
            ErrorMessage = errorMessage;
        }

        public static ReferenceBookResult Success(BibleBook book)
            => new ReferenceBookResult(true, book, null);

        public static ReferenceBookResult Error(string errorMessage)
            => new ReferenceBookResult(false, null, errorMessage);

        public bool IsSuccessful { get; }

        public BibleBook? Book { get; }

        public string? ErrorMessage { get; }
    }

    private class ReferenceSegmentResult
    {
        private ReferenceSegmentResult(bool isSuccessful, IList<ReferenceSegment>? segments, string? errorMessage)
        {
            IsSuccessful = isSuccessful;
            Segments = segments;
            ErrorMessage = errorMessage;
        }

        public static ReferenceSegmentResult Success(IList<ReferenceSegment> segments)
            => new ReferenceSegmentResult(true, segments, null);

        public static ReferenceSegmentResult Error(string errorMessage)
            => new ReferenceSegmentResult(false, null, errorMessage);

        public bool IsSuccessful { get; }

        public IList<ReferenceSegment>? Segments { get; }

        public string? ErrorMessage { get; }
    }

    private class ReferencePointResult
    {
        private ReferencePointResult(bool isSuccessful, ReferencePoint? point, int? chapterOverride, string? errorMessage)
        {
            IsSuccessful = isSuccessful;
            Point = point;
            ChapterOverride = chapterOverride;
            ErrorMessage = errorMessage;
        }

        public static ReferencePointResult Success(ReferencePoint point, int? chapterOverride)
            => new ReferencePointResult(true, point, chapterOverride, null);

        public static ReferencePointResult Error(string errorMessage)
            => new ReferencePointResult(false, null, null, errorMessage);

        public bool IsSuccessful { get; }

        public ReferencePoint? Point { get; }

        public int? ChapterOverride { get; }

        public string? ErrorMessage { get; }
    }
}
