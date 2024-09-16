using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechWizards.Domain.Models.Entities;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Persistence.EntityConfiguration;

/// <summary>
/// Represents the configuration of the quiz assessment entity.
/// </summary>
public class QuizAssessmentConfiguration : IEntityTypeConfiguration<QuizAssessment>
{
    public void Configure(EntityTypeBuilder<QuizAssessment> builder)
    {
        builder.Property(qa => qa.Name).HasMaxLength(256);
        
        builder.Property(qa => qa.Type).HasMaxLength(256);
        
        builder.Ignore(qa => qa.CacheKey);
        
        builder.HasOne(qa => qa.Session).WithMany(qas => qas.Assessments).HasForeignKey(qa => qa.SessionId);

        builder.ToTable("Assessments")
            .HasDiscriminator<string>(qa => qa.Type)
            .HasValue<GrammarAssessment>(LanguageAssessmentTypes.Grammar.ToString())
            .HasValue<ListeningAssessment>(LanguageAssessmentTypes.Listening.ToString());
    }
}