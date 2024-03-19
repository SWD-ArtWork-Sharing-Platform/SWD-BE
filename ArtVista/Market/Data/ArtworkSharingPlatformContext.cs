using System;
using System.Collections.Generic;
using Market.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Market.Data;

public partial class ArtworkSharingPlatformContext : IdentityDbContext<ApplicationUser>
{
    public ArtworkSharingPlatformContext()
    {
    }

    public ArtworkSharingPlatformContext(DbContextOptions<ArtworkSharingPlatformContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DCategory> DCategories { get; set; }

    public virtual DbSet<DOrderDetail> DOrderDetails { get; set; }

    public virtual DbSet<DPackageOfCreator> DPackageOfCreators { get; set; }

    public virtual DbSet<DWishlistDetail> DWishlistDetails { get; set; }
    public virtual DbSet<FArtwork> FArtworks { get; set; }

    public virtual DbSet<FOrder> FOrders { get; set; }

    public virtual DbSet<FPackage> FPackages { get; set; }

    public virtual DbSet<FPayment> FPayments { get; set; }
    public virtual DbSet<FWishlist> FWishlists { get; set; }
    public virtual DbSet<FPost> FPosts { get; set; }
    public virtual DbSet<DBankAccount> DBankAccounts { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public virtual DbSet<DPaymentResponse> DPaymentResponses { get; set; }
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

        modelBuilder.Entity<DWishlistDetail>(entity =>
        {
            entity.HasKey(e => e.WishlistDetailId);

            entity.ToTable("D_WishlistDetail");

            entity.Property(e => e.WishlistDetailId)
                .HasMaxLength(50)
                .HasColumnName("WishlistDetail_ID");
            entity.Property(e => e.ArtworkId)
                .HasMaxLength(50)
                .HasColumnName("Artwork_ID");
            entity.Property(e => e.WishlistId)
                .HasMaxLength(50)
                .HasColumnName("Wishlist_ID");
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
        });

        modelBuilder.Entity<FWishlist>(entity =>
        {
            entity.HasKey(e => e.WishlistId);

            entity.ToTable("F_Wishlist");

            entity.Property(e => e.WishlistId)
                .HasMaxLength(50)
                .HasColumnName("Wishlist_ID");
            entity.Property(e => e.Id)
                .HasMaxLength(450)
                .HasColumnName("ID");
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");
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

        modelBuilder.Entity<DOrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId);

            entity.ToTable("D_OrderDetail");

            entity.Property(e => e.OrderDetailId)
                .HasMaxLength(50)
                .HasColumnName("OrderDetail_ID");
            entity.Property(e => e.ArtworkId)
                .HasMaxLength(50)
                .HasColumnName("Artwork_ID");
            entity.Property(e => e.OrderId)
                .HasMaxLength(50)
                .HasColumnName("Order_ID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Artwork).WithMany(p => p.DOrderDetails)
                .HasForeignKey(d => d.ArtworkId)
                .HasConstraintName("FK_D_OrderDetail_F_Artwork");

            entity.HasOne(d => d.Order).WithMany(p => p.DOrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_D_OrderDetail_F_Order");
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

        OnModelCreatingPartial(modelBuilder);

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

            entity.HasOne(d => d.Category).WithMany(p => p.FArtworks)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_F_Artwork_D_Category");
        });

        modelBuilder.Entity<FOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("F_Order");

            entity.Property(e => e.OrderId)
                .HasMaxLength(50)
                .HasColumnName("Order_ID");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.Id)
                .HasMaxLength(450)
                .HasColumnName("ID");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .HasColumnName("Order_Status");
            entity.Property(e => e.PaymentId)
                .HasMaxLength(50)
                .HasColumnName("Payment_ID");
            entity.Property(e => e.SoldDate)
                .HasColumnType("datetime")
                .HasColumnName("Sold_Date");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");
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
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<FPayment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("F_Payment");

   
            entity.Property(e => e.PaymentId)
                .HasMaxLength(50)
                .HasColumnName("Payment_ID");
            entity.Property(e => e.PaymentName)
                .HasMaxLength(50)
                .HasColumnName("Payment_Name");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasColumnName("Payment_Status");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

      
        });
        modelBuilder.Entity<DBankAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__BankAcco__349DA5A6AA3EADB5");

            entity.ToTable("D_BankAccount");

            entity.HasIndex(e => e.UserId, "UQ__BankAcco__1788CC4DE7AD3D4A").IsUnique();

            entity.Property(e => e.AccountNumber).HasMaxLength(50);
            entity.Property(e => e.AccountType).HasMaxLength(50);
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
           
        });

        modelBuilder.Entity<DPaymentResponse>(entity =>
        {
            entity.HasKey(e => e.PaymentResponseId).HasName("PK__D_Paymen__766E687AB63AE698");

            entity.ToTable("D_PaymentResponse");

            entity.Property(e => e.OrderDescription)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OrderId)
                .HasMaxLength(50)
                .HasColumnName("Order_Id");
            entity.Property(e => e.PayDate).HasColumnType("date");
            entity.Property(e => e.PaymentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Token)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VnPayResponseCode)
                .HasMaxLength(50)
                .IsUnicode(false);
        });
        modelBuilder.Entity<DPaymentResponse>(entity =>
        {
            entity.HasKey(e => e.PaymentResponseId).HasName("PK__D_Paymen__766E687AB63AE698");

            entity.ToTable("D_PaymentResponse");

            entity.Property(e => e.OrderDescription)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OrderId)
                .HasMaxLength(50)
                .HasColumnName("Order_Id");
            entity.Property(e => e.PayDate).HasColumnType("date");
            entity.Property(e => e.PaymentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
