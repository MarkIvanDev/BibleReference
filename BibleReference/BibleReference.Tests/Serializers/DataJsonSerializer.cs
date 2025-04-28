using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using BibleReference;
using BibleReference.Tests.Serializers;
using Xunit.Sdk;

[assembly: RegisterXunitSerializer(typeof(DataJsonSerializer), typeof(ReferenceSegment), typeof(ReferencePoint))]

namespace BibleReference.Tests.Serializers;

public class DataJsonSerializer : IXunitSerializer
{
    public bool IsSerializable(Type type, object? value, [NotNullWhen(false)] out string? failureReason)
    {
        failureReason = null;
        return true;
    }

    public object Deserialize(Type type, string serializedValue)
    {
        return JsonSerializer.Deserialize(serializedValue, type) ?? throw new InvalidOperationException();
    }

    public string Serialize(object value)
    {
        return JsonSerializer.Serialize(value);
    }
}
