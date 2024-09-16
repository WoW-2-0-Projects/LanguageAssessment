namespace Temporary;

/// <summary>
/// Contains extensions for stored items
/// </summary>
public static class StorageExtensions
{
    /// <summary>
    /// Converts path value to URL
    /// </summary>
    /// <param name="path">Path to convert</param>
    /// <returns>Converted path</returns>
    public static string ToUrl(this string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path));

        return path.Replace(Path.DirectorySeparatorChar, '/');
    }

    /// <summary>
    /// Converts URL value to path
    /// </summary>
    /// <param name="url">URL to convert</param>
    /// <returns>Converted path</returns>
    public static string ToPath(this string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentNullException(nameof(url));

        return url.Replace('/', Path.DirectorySeparatorChar);
    }
}