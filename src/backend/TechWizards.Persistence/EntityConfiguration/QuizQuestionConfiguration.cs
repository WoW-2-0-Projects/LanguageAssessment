using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Persistence.EntityConfiguration;

/// <summary>
/// Represents the configuration of the quiz question entity.
/// </summary>
public class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
{
    public void Configure(EntityTypeBuilder<QuizQuestion> builder)
    {
        builder.Property(qq => qq.QuestionText).HasMaxLength(1024);

        builder.HasOne(qq => qq.QuizAssessment).WithMany(qa => qa.Questions).HasForeignKey(qq => qq.AssessmentId);

        builder.Ignore(user => user.CacheKey);
    }
}