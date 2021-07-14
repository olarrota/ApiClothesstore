using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ApiDatos.Models
{
    public partial class BDLATAMContext : DbContext
    {
        public BDLATAMContext()
        {
        }

        public BDLATAMContext(DbContextOptions<BDLATAMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Imagenes> Imagenes { get; set; }
        public virtual DbSet<Paises> Paises { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<ReglasDtc> ReglasDtcs { get; set; }
        public virtual DbSet<TiposImagen> TiposImagens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=Localhost;Database=BDLATAM;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Imagenes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.ProductosId).HasColumnName("productosID");

                entity.Property(e => e.Ruta)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("ruta");

                entity.Property(e => e.TiposImagenId).HasColumnName("tiposImagenID");

                entity.HasOne(d => d.Productos)
                    .WithMany(p => p.Imagenes)
                    .HasForeignKey(d => d.ProductosId)
                    .HasConstraintName("fk_imagenes_Productos");

                entity.HasOne(d => d.TiposImagen)
                    .WithMany(p => p.Imagenes)
                    .HasForeignKey(d => d.TiposImagenId)
                    .HasConstraintName("fk_imagenes_tiposImagen");
            });

            modelBuilder.Entity<Paises>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Productos>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Descuento)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("descuento");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.PaisId).HasColumnName("paisID");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("precio");

                entity.Property(e => e.Visualizaciones).HasColumnName("visualizaciones");

                entity.HasOne(d => d.Pais)
                    .WithMany(p => p.InversePais)
                    .HasForeignKey(d => d.PaisId)
                    .HasConstraintName("fk_Productos_Paises");
            });

            modelBuilder.Entity<ReglasDtc>(entity =>
            {
                entity.ToTable("ReglasDtc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PaisId).HasColumnName("paisID");

                entity.Property(e => e.ValorDtc)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("valorDtc");

                entity.HasOne(d => d.Pais)
                    .WithMany(p => p.ReglasDtcs)
                    .HasForeignKey(d => d.PaisId)
                    .HasConstraintName("fk_reglasDtc_Paises");
            });

            modelBuilder.Entity<TiposImagen>(entity =>
            {
                entity.ToTable("TiposImagen");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
