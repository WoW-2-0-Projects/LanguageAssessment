using Microsoft.EntityFrameworkCore;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Persistence.DataContexts;

/// <summary>
/// Represents the main application database context.
/// </summary>
/// <param name="dbContextOptions"></param>
public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    # region Assessments Infrastructure

    public DbSet<GrammarAssessment> GrammarAssessments => Set<GrammarAssessment>();
    
    public DbSet<ListeningAssessment> ListeningAssessments => Set<ListeningAssessment>();
    
    public DbSet<QuizAssessmentSession> AssessmentSessions => Set<QuizAssessmentSession>();
    
    public DbSet<QuizQuestion> Questions => Set<QuizQuestion>();

    public DbSet<QuizOption> Options => Set<QuizOption>();
    
    public DbSet<QuizAnswer> Answers => Set<QuizAnswer>();

    # endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}