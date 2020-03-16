using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Tareas.CtxTarea5
{
    public partial class Tarea5Context : IdentityDbContext
    {
        public Tarea5Context()
        {
        }

        public Tarea5Context(DbContextOptions<Tarea5Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Publicacion> Publicacion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Tarea5;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Publicacion>(entity =>
            {
                entity.Property(e => e.Contenido)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.FechaPublicacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdUser)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
