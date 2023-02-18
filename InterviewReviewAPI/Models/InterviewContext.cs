using Microsoft.EntityFrameworkCore;

namespace InterviewReviewAPI.Models;

public class InterviewContext : DbContext
{
    public InterviewContext(DbContextOptions<InterviewContext> options) : base(options)
    {

    }

    public DbSet<InterviewProcess> interviewProcesses { get; set; } = null!;
}