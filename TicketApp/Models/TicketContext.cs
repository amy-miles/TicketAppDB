using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TicketApp.Models
{
    public class TicketContext : IdentityDbContext<User>
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options) { }
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Status>().HasData(
                new Status { StatusID = "todo", StatusName = "To Do" },
                new Status { StatusID = "inprogress", StatusName = "In Progress" },
                new Status { StatusID = "qa", StatusName = "Quality Assurance" },
                new Status { StatusID = "done", StatusName = "Done" });

        }

    }
}
