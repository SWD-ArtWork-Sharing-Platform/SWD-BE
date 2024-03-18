using System;
using System.Collections.Generic;
using Management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Management.Data;

public partial class ArtworkSharingPlatformContext : DbContext
{
    public ArtworkSharingPlatformContext()
    {
    }

    public ArtworkSharingPlatformContext(DbContextOptions<ArtworkSharingPlatformContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DInteraction> DInteractions { get; set; }

    public virtual DbSet<DPackageOfCreator> DPackageOfCreators { get; set; }

    public virtual DbSet<FArtwork> FArtworks { get; set; }

    public virtual DbSet<FConfiguration> FConfigurations { get; set; }

    public virtual DbSet<FPackage> FPackages { get; set; }

    public virtual DbSet<FPost> FPosts { get; set; }

    public virtual DbSet<FReport> FReports { get; set; }
    public virtual DbSet<DCategory> DCategory { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.Ignore<IdentityUserRole<string>>();
        modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Ignore<IdentityUserToken<string>>();
        modelBuilder.Ignore<IdentityUser<string>>();
        modelBuilder.Ignore<ApplicationUser>();

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>();

        modelBuilder.Entity<DInteraction>(entity =>
        {
            entity.HasKey(e => e.InteractionId);

            entity.ToTable("D_Interaction");

            entity.Property(e => e.InteractionId)
                .HasMaxLength(50)
                .HasColumnName("Interaction_ID");
            entity.Property(e => e.Comments).HasMaxLength(255);
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.Id)
                .HasMaxLength(450)
                .HasColumnName("ID");
            entity.Property(e => e.PostId)
                .HasMaxLength(50)
                .HasColumnName("Post_ID");
        });

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

        modelBuilder.Entity<FArtwork>(entity =>
        {
            entity.HasKey(e => e.ArtworkId);

            entity.ToTable("F_Artwork");

            entity.Property(e => e.ArtworkId)
                .HasMaxLength(50)
                .HasColumnName("Artwork_ID");
            entity.Property(e => e.ArtworkName)
                .HasMaxLength(50)
                .HasColumnName("Artwork_Name");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(50)
                .HasColumnName("Category_ID");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Id)
                .HasMaxLength(450)
                .HasColumnName("ID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<FConfiguration>(entity =>
        {
            entity.HasKey(e => e.ConfigurationId);

            entity.ToTable("F_Configuration");

            entity.Property(e => e.ConfigurationId)
                .HasMaxLength(50)
                .HasColumnName("Configuration_ID");
            entity.Property(e => e.AppliedDate)
                .HasColumnType("datetime")
                .HasColumnName("Applied_Date");
            entity.Property(e => e.CommisionFee)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Commision_Fee");
            entity.Property(e => e.Id)
                .HasMaxLength(450)
                .HasColumnName("ID");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<FPackage>(entity =>
        {
            entity.HasKey(e => e.PackageId);

            entity.ToTable("F_Package");

            entity.Property(e => e.PackageId)
                .HasMaxLength(50)
                .HasColumnName("Package_ID");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MaximumArtworks).HasColumnName("Maximum_Artworks");
            entity.Property(e => e.PackageName)
                .HasMaxLength(50)
                .HasColumnName("Package_Name");
            entity.Property(e => e.PackageTime).HasMaxLength(30);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
        });


        modelBuilder.Entity<FPost>(entity =>
        {
            entity.HasKey(e => e.PostId);

            entity.ToTable("F_Post");

            entity.Property(e => e.PostId)
                .HasMaxLength(50)
                .HasColumnName("Post_ID");
            entity.Property(e => e.ArtworkId)
                .HasMaxLength(50)
                .HasColumnName("Artwork_ID");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Tittle).HasMaxLength(255);

            entity.HasOne(d => d.Artwork).WithMany(p => p.FPosts)
                .HasForeignKey(d => d.ArtworkId)
                .HasConstraintName("FK_F_Post_F_Artwork");
        });

        modelBuilder.Entity<FReport>(entity =>
        {
            entity.HasKey(e => e.ReportId);

            entity.ToTable("F_Report");

            entity.Property(e => e.ReportId)
                .HasMaxLength(50)
                .HasColumnName("Report_ID");
            entity.Property(e => e.ActionNote)
                .HasMaxLength(255)
                .HasColumnName("Action_Note");
            entity.Property(e => e.ArtworkId)
                .HasMaxLength(50)
                .HasColumnName("Artwork_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.Detail).HasMaxLength(255);
            entity.Property(e => e.Id)
                .HasMaxLength(450)
                .HasColumnName("ID");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Artwork).WithMany(p => p.FReports)
                .HasForeignKey(d => d.ArtworkId)
                .HasConstraintName("FK_F_Report_F_Artwork");
        });

        modelBuilder.Entity<DCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("D_Category");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(50)
                .HasColumnName("Category_ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("Category_Name");
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("Updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
