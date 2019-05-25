using Microsoft.EntityFrameworkCore;
using SocialCoding.API.Models;

namespace SocialCoding.API.Data
{
    public class SocialCodingContext : DbContext
    {
        public SocialCodingContext(DbContextOptions<SocialCodingContext> options) : base(options) {}

        public DbSet<Valor> Valores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}