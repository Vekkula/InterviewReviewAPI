using Microsoft.EntityFrameworkCore;

namespace InterviewReviewAPI.Models;

public class InterviewDbContext : DbContext
{
    public InterviewDbContext(DbContextOptions<InterviewDbContext> options) : base(options)
    {

    }

    public DbSet<InterviewProcess> interviewProcesses { get; set; } = null!;
}