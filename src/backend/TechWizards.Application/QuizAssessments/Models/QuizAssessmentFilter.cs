using TechWizards.Domain.Models.Enums;
using Temporary.Common.Queries;

namespace TechWizards.Application.QuizAssessments.Models;

/// <summary>
/// Represents a filter for querying quiz assessments with pagination support.
/// </summary>
public class QuizAssessmentFilter : FilterPagination
{
    /// <summary>
    /// Gets or sets the assessment level to filter by.
    /// </summary>
    public AssessmentLevel? Level { get; init; }

    /// <summary>
    /// Gets or sets the topic to filter by.
    /// </summary>
    public List<string> Topics { get; init; } = new();

    /// <summary>
    /// Computes the hash code for the filter.
    /// </summary>
    /// <returns>The hash code for the filter.</returns>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(PageSize);
        hashCode.Add(PageToken);
        hashCode.Add(Level);
        foreach (var topic in Topics)
        {
            hashCode.Add(topic);
        }

        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current filter.
    /// </summary>
    /// <param name="obj">The object to compare with the current filter.</param>
    /// <returns>True if the specified object is equal to the current filter; otherwise, false.</returns>
    public override bool Equals(object? obj) =>
        obj is QuizAssessmentFilter filter && filter.GetHashCode() == GetHashCode();
}