using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Persistence.EntityConfiguration;

/// <summary>
/// Represents the configuration of the quiz participant answer.
/// </summary>
public class QuizAnswerConfiguration : IEntityTypeConfiguration<QuizAnswer>
{
    public void Configure(EntityTypeBuilder<QuizAnswer> builder)
    {
        builder.HasOne(qa => qa.CorrectOption).WithMany().HasForeignKey(qa => qa.CorrectOptionId);

        builder.HasOne(qa => qa.SubmittedOption).WithOne().HasForeignKey<QuizAnswer>(qa => qa.SubmittedOptionId);

        builder.HasOne(qa => qa.QuizQuestion).WithMany().HasForeignKey(qa => qa.QuestionId);
    }
}