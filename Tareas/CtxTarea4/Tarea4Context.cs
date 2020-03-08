using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tareas.CtxTarea4
{
    public partial class Tarea4Context : DbContext
    {
        public Tarea4Context()
        {
        }

        public Tarea4Context(DbContextOptions<Tarea4Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Ataques> Ataques { get; set; }
        public virtual DbSet<Pokemon> Pokemon { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Tipo> Tipo { get; set; }
        public virtual DbSet<TipoPokemon> TipoPokemon { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Tarea4;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Ataques>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pokemon)
                    .WithMany(p => p.Ataques)
                    .HasForeignKey(d => d.PokemonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ataques__Pokemon__412EB0B6");
            });

            modelBuilder.Entity<Pokemon>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Pokemon)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pokemon__RegionI__3A81B327");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rgbcolor)
                    .IsRequired()
                    .HasColumnName("RGBColor")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tipo>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoPokemon>(entity =>
            {
                entity.HasOne(d => d.Pokemon)
                    .WithMany(p => p.TipoPokemon)
                    .HasForeignKey(d => d.PokemonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TipoPokem__Pokem__3D5E1FD2");

                entity.HasOne(d => d.Tipo)
                    .WithMany(p => p.TipoPokemon)
                    .HasForeignKey(d => d.TipoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TipoPokem__TipoI__3E52440B");
            });
        }
    }
}
