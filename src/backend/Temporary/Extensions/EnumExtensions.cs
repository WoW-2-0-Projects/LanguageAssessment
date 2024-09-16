using System.ComponentModel;
using System.Reflection;

namespace Temporary.Extensions;

/// <summary>
/// Contains extensions for enums
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the description of the enum value.
    /// </summary>
    /// <param name="value">The enum value.</param>
    /// <returns>The description if available, otherwise the name of the enum value.</returns>
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }

    /// <summary>
    /// Gets all values of the enum as a list of key-value pairs.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <returns>A list of key-value pairs where the key is the enum value and the value is the description or name.</returns>
    public static List<KeyValuePair<T, string>> GetAllValuesAndDescriptions<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(e => new KeyValuePair<T, string>(e, e.GetDescription()))
            .ToList();
    }
    
    /// <summary>
    /// Gets all values of the enum
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <returns>A list of key-value pairs where the key is the enum value and the value is the description or name.</returns>
    public static List<string> GetAllDescriptions<T>() where T : Enum
    {
        return GetAllValuesAndDescriptions<T>()
            .Select(value => value.Value).ToList();
    }
}