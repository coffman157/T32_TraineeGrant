using Microsoft.EntityFrameworkCore;
using T32_TraineeGrant;

public class BumcOrgContext : DbContext
{
    public virtual DbSet<TrainingRecord> TrainingRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Additional configurations if needed  
    }
}
