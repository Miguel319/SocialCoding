using Microsoft.EntityFrameworkCore;
using SocialCoding.API.Models;

namespace SocialCoding.API.Data {
    public class SocialCodingContext : DbContext {
        public SocialCodingContext (DbContextOptions<SocialCodingContext> options) : base (options) { }

        public DbSet<Valor> Valores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<MeGusta> MeGustas { get; set; }

        protected override void OnModelCreating (ModelBuilder builder) {
            builder.Entity<MeGusta> ()
                .HasKey (k => new { k.MeGustadorId, k.MeGustaaId });

            builder.Entity<MeGusta> ()
                .HasOne (x => x.MeGustaa)
                .WithMany (x => x.MeGustadores)
                .HasForeignKey (x => x.MeGustaaId)
                .OnDelete (DeleteBehavior.Restrict);

            builder.Entity<MeGusta> ()
                .HasOne (x => x.MeGustador)
                .WithMany (x => x.MeGustas)
                .HasForeignKey (x => x.MeGustadorId)
                .OnDelete (DeleteBehavior.Restrict);
        }
    }
}