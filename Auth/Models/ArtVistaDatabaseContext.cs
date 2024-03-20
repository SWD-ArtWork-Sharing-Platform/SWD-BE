using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Auth.Models;

public partial class ArtVistaDatabaseContext : DbContext
{
    public ArtVistaDatabaseContext()
    {
    }

    public ArtVistaDatabaseContext(DbContextOptions<ArtVistaDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DPackageOfCreator> DPackageOfCreators { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=(local);database= ArtVistaDatabase;uid=sa;pwd=123456;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DPackageOfCreator>(entity =>
        {
            entity.HasKey(e => new { e.PackageId, e.Id });

            entity.ToTable("D_PackageOfCreator", tb => tb.HasTrigger("CalculateExpiry"));

            entity.Property(e => e.PackageId)
                .HasMaxLength(50)
                .HasColumnName("Package_ID");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ExpiredDate)
                .HasColumnType("datetime")
                .HasColumnName("Expired_date");
            entity.Property(e => e.GraceDate)
                .HasColumnType("datetime")
                .HasColumnName("Grace_date");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
