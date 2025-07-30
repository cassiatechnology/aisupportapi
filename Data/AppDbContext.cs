using AiSupportApi.Enums;
using AiSupportApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AiSupportApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<TicketModel> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Converter enum para string no banco
            modelBuilder.Entity<TicketModel>()
                .Property(t => t.Status)
                .HasConversion(new EnumToStringConverter<TicketStatus>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
