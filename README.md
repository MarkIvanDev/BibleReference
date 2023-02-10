# BibleReference
A library to parse text into bible references in multiple locales.

## Features

### Supported String Formats
- Book (e.g. *Genesis*)
- Book Chapter (e.g. *Genesis 1*)
- Book Chapter-Chapter (e.g. *Genesis 1-2*)
- Book Chapter:Verse (e.g. *Genesis 1:1*)
- Book Chapter:Verse-Verse (e.g. *Genesis 1:1-5*)
- Book Chapter:Verse,Verse (e.g. *Genesis 1:1,5*)
- Book Chapter:Verse-Verse,Verse-Verse (e.g. *Genesis 1:1-5,8-10*)
- Book Chapter:Verse-Verse;Chapter:Verse-Verse (e.g. *Genesis 1:1-5;2:1-3*)
- Book Chapter:Verse-Chapter:Verse (e.g. *Genesis 1:5-2:3*)
- Book Verse (for single-chapter books) (e.g. *2 John 9*)
- Book Verse,Verse (for single-chapter books) (e.g. *2 John 3,5*)
- Book Verse-Verse (for single-chapter books) (e.g. *2 John 3-5*)
- Book Verse-Verse,Verse (for single-chapter books) (e.g. *2 John 3-5,9*)

### Supported Locales
- English
- Filipino

## Usage

You can use the `BibleReferenceParser` static class to parse.

```csharp
var reference = BibleReferenceParser.Parse("Genesis 1:1-5;2:1-3");
Console.WriteLine(reference ==
    new Reference(
      BibleBook.Genesis,
      ReferenceSegment.MultipleVerses(1, 1, 5),
      ReferenceSegment.MultipleVerses(2, 1, 3)
    )); // prints true
```

## Support
If you like my work and want to support me, buying me a coffee would be awesome! Thanks for your support!

<a href="https://www.buymeacoffee.com/markivandev" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/v2/default-blue.png" alt="Buy Me A Coffee" style="height: 60px !important;width: 217px !important;" ></a>

---------
**Mark Ivan Basto** &bullet; **GitHub**
**[@MarkIvanDev](https://github.com/MarkIvanDev)** &bullet; **Twitter**
**[@Rivolvan_Speaks](https://twitter.com/Rivolvan_Speaks)**