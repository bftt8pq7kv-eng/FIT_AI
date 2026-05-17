using Microsoft.EntityFrameworkCore;
using FIT_AI.Models;

namespace FIT_AI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<OutfitHistory> OutfitHistories { get; set; }
    }
}