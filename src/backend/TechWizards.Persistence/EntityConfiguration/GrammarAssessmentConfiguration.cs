using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Persistence.EntityConfiguration;

/// <summary>
/// Represents the configuration of the grammar assessment.
/// </summary>
public class GrammarAssessmentConfiguration : IEntityTypeConfiguration<GrammarAssessment>
{
    public void Configure(EntityTypeBuilder<GrammarAssessment> builder)
    {
        builder.Ignore(user => user.CacheKey);
    }
}