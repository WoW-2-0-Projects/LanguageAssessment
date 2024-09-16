using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Persistence.EntityConfiguration;

/// <summary>
/// Represents the configuration of the quiz option entity.
/// </summary>
public class QuizOptionConfiguration : IEntityTypeConfiguration<QuizOption>
{
    public void Configure(EntityTypeBuilder<QuizOption> builder)
    {
        builder.Property(qo => qo.OptionText).HasMaxLength(1024);

        builder.HasOne(qo => qo.QuizQuestion).WithMany(qq => qq.Answers).HasForeignKey(qq => qq.QuestionId);

        builder.Ignore(user => user.CacheKey);
    }
}