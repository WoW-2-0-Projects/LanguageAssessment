using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Persistence.EntityConfiguration;

public class QuizAssessmentSessionConfiguration : IEntityTypeConfiguration<QuizAssessmentSession>
{
    public void Configure(EntityTypeBuilder<QuizAssessmentSession> builder)
    {
        builder.Property(qas => qas.Step).HasMaxLength(256);
        
        builder.Ignore(user => user.CacheKey);
        
        builder.Property(qa => qa.ParticipantIpAddress).HasMaxLength(64);
    }
}