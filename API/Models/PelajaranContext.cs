using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace API.Models
{
    public partial class PelajaranContext : DbContext
    {
        public PelajaranContext()
        {
        }

        public PelajaranContext(DbContextOptions<PelajaranContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbMMataPelajaran> TbMMataPelajarans { get; set; }
        public virtual DbSet<TbMSiswa> TbMSiswas { get; set; }
        public virtual DbSet<TbTPelajaran> TbTPelajarans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-SJCJBLM;Database=Pelajaran;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TbMMataPelajaran>(entity =>
            {
                entity.ToTable("TB_M_MataPelajaran");

                entity.Property(e => e.Name)
                    .HasMaxLength(15)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbMSiswa>(entity =>
            {
                entity.ToTable("TB_M_Siswa");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbTPelajaran>(entity =>
            {
                entity.ToTable("TB_T_Pelajaran");

                entity.HasOne(d => d.MataPelajaran)
                    .WithMany(p => p.TbTPelajarans)
                    .HasForeignKey(d => d.MataPelajaranId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_T_Pelajaran_TB_M_MataPelajaran");

                entity.HasOne(d => d.Siswa)
                    .WithMany(p => p.TbTPelajarans)
                    .HasForeignKey(d => d.SiswaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_T_Pelajaran_TB_M_Siswa");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
