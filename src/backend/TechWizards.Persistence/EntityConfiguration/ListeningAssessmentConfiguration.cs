using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Persistence.EntityConfiguration;

/// <summary>
/// Represents the configuration of the listening assessment.
/// </summary>
public class ListeningAssessmentConfiguration : IEntityTypeConfiguration<ListeningAssessment>
{
    public void Configure(EntityTypeBuilder<ListeningAssessment> builder)
    {
        builder.Property(la => la.AudioContent).HasMaxLength(32_768);
        
        builder.Ignore(user => user.CacheKey);
    }
}