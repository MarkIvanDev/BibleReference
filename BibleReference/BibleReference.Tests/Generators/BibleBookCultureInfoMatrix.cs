using System.Globalization;
using BibleBooks;

namespace BibleReference.Tests.Generators;

public class BibleBookCultureInfoMatrix : MatrixTheoryData<BibleBook, CultureInfo?>
{
    public BibleBookCultureInfoMatrix() : base(Enum.GetValues<BibleBook>(), [null, CultureInfos.En, CultureInfos.FilPH])
    {
    }
}