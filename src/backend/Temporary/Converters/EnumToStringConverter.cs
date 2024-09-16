using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Temporary.Extensions;

namespace Temporary.Converters;

/// <summary>
/// Represents a JSON converter for enums using their string representations.
/// </summary>
public class EnumToStringConverter<T> : StringEnumConverter where T : struct, Enum
{
    private readonly Dictionary<string, T> _stringToEnum;
    private readonly Dictionary<T, string> _enumToString;

    public EnumToStringConverter()
    {
        _stringToEnum = Enum.GetValues(typeof(T))
            .Cast<T>()
            .ToDictionary(
                e => e.ToString(),
                e => e,
                StringComparer.OrdinalIgnoreCase);

        _enumToString = _stringToEnum.ToDictionary(
            kvp => kvp.Value,
            kvp => kvp.Key);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is T enumValue)
        {
            var stringValue = _enumToString[enumValue];
            writer.WriteValue(stringValue);
        }
        else
        {
            throw new JsonSerializationException("Expected enum object value.");
        }
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var enumString = reader.Value?.ToString();
        if (enumString != null && _stringToEnum.TryGetValue(enumString, out var enumValue))
        {
            return enumValue;
        }

        throw new JsonSerializationException($"Unknown enum value '{enumString}' for type '{typeof(T)}'.");
    }
}