using AG_MT2.Models;
using Microsoft.EntityFrameworkCore;

namespace AG_MT2.Models
{
    public class SRMSContext : DbContext
    {
        public SRMSContext(DbContextOptions<SRMSContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
