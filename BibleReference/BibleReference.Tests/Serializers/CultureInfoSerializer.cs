using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using BibleReference.Tests.Serializers;
using Xunit.Sdk;

[assembly: RegisterXunitSerializer(typeof(CultureInfoSerializer), typeof(CultureInfo))]

namespace BibleReference.Tests.Serializers;

public class CultureInfoSerializer : IXunitSerializer
{
    public bool IsSerializable(Type type, object? value, [NotNullWhen(false)] out string? failureReason)
    {
        failureReason = null;
        return true;
    }

    public object Deserialize(Type type, string serializedValue)
    {
        return new CultureInfo(serializedValue);
    }

    public string Serialize(object value)
    {
        return ((CultureInfo)value).Name;
    }
}
