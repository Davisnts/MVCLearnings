using fullstack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace fullstack.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<Evento> Eventos { get; set; }
    }
}