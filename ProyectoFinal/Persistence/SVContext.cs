using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models.Entities;

namespace Persistence
{
    public partial class SVContext : DbContext
    {
        public SVContext()
        {
        }

        public SVContext(DbContextOptions<SVContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidatos> Candidatos { get; set; }
        public virtual DbSet<Ciudadanos> Ciudadanos { get; set; }
        public virtual DbSet<Elecciones> Elecciones { get; set; }
        public virtual DbSet<EstadosElecciones> EstadosElecciones { get; set; }
        public virtual DbSet<Partidos> Partidos { get; set; }
        public virtual DbSet<PuestosElectivos> PuestosElectivos { get; set; }
        public virtual DbSet<Votantes> Votantes { get; set; }
        public virtual DbSet<Votos> Votos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
             optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=SistemaVotaciones;Trusted_Connection=True;");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Candidatos>(entity =>
            {
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FotoPerfil).IsUnicode(false);

                entity.HasOne(d => d.Ciudadano)
                    .WithMany(p => p.Candidatos)
                    .HasForeignKey(d => d.CiudadanoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Candidato__Ciuda__44FF419A");

                entity.HasOne(d => d.Eleccion)
                    .WithMany(p => p.Candidatos)
                    .HasForeignKey(d => d.EleccionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Candidato__Elecc__47DBAE45");

                entity.HasOne(d => d.Partido)
                    .WithMany(p => p.Candidatos)
                    .HasForeignKey(d => d.PartidoId)
                    .HasConstraintName("FK__Candidato__Parti__45F365D3");

                entity.HasOne(d => d.Puesto)
                    .WithMany(p => p.Candidatos)
                    .HasForeignKey(d => d.PuestoId)
                    .HasConstraintName("FK__Candidato__Puest__46E78A0C");
            });

            modelBuilder.Entity<Ciudadanos>(entity =>
            {
                entity.HasIndex(e => e.DocIdentidad)
                    .HasName("UQ__Ciudadan__72B90F181F91F494")
                    .IsUnique();

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DocIdentidad)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Elecciones>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estados)
                    .WithMany(p => p.Elecciones)
                    .HasForeignKey(d => d.EstadosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Eleccione__Estad__4222D4EF");
            });

            modelBuilder.Entity<EstadosElecciones>(entity =>
            {
                entity.Property(e => e.DescripcionEstado)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Partidos>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PuestosElectivos>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Votantes>(entity =>
            {
                entity.HasOne(d => d.Ciudadano)
                    .WithMany(p => p.Votantes)
                    .HasForeignKey(d => d.CiudadanoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Votantes__Ciudad__4BAC3F29");

                entity.HasOne(d => d.Eleccion)
                    .WithMany(p => p.Votantes)
                    .HasForeignKey(d => d.EleccionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Votantes__Elecci__4CA06362");
            });

            modelBuilder.Entity<Votos>(entity =>
            {
                entity.HasOne(d => d.Candidato)
                    .WithMany(p => p.Votos)
                    .HasForeignKey(d => d.CandidatoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Votos__Candidato__5070F446");

                entity.HasOne(d => d.Eleccion)
                    .WithMany(p => p.Votos)
                    .HasForeignKey(d => d.EleccionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Votos__EleccionI__4F7CD00D");
            });
        }
    }
}
