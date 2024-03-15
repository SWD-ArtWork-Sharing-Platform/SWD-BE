﻿// <auto-generated />
using System;
using Management.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Management.Migrations
{
    [DbContext(typeof(ArtworkSharingPlatformContext))]
    partial class ArtworkSharingPlatformContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Management.Models.DCategory", b =>
                {
                    b.Property<string>("CategoryId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Category_ID");

                    b.Property<string>("CategoryName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Category_Name");

                    b.Property<string>("Note")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Updated_by");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_date");

                    b.HasKey("CategoryId");

                    b.ToTable("D_Category", (string)null);
                });

            modelBuilder.Entity("Management.Models.DInteraction", b =>
                {
                    b.Property<string>("InteractionId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Interaction_ID");

                    b.Property<string>("Comments")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_On");

                    b.Property<string>("Id")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ID");

                    b.Property<int?>("Like")
                        .HasColumnType("int");

                    b.Property<string>("PostId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Post_ID");

                    b.HasKey("InteractionId");

                    b.HasIndex("PostId");

                    b.ToTable("D_Interaction", (string)null);
                });

            modelBuilder.Entity("Management.Models.DPackageOfCreator", b =>
                {
                    b.Property<string>("PackageId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Package_ID");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ID");

                    b.Property<DateTime?>("ExpiredDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Expired_date");

                    b.Property<DateTime?>("GraceDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Grace_date");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<int>("Remain")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PackageId", "Id");

                    b.ToTable("D_PackageOfCreator", (string)null);
                });

            modelBuilder.Entity("Management.Models.FArtwork", b =>
                {
                    b.Property<string>("ArtworkId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Artwork_ID");

                    b.Property<string>("ArtworkName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Artwork_Name");

                    b.Property<string>("CategoryId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Category_ID");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<string>("Id")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ID");

                    b.Property<string>("ImageLocalPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ArtworkId");

                    b.ToTable("F_Artwork", (string)null);
                });

            modelBuilder.Entity("Management.Models.FConfiguration", b =>
                {
                    b.Property<string>("ConfigurationId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Configuration_ID");

                    b.Property<DateTime?>("AppliedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Applied_Date");

                    b.Property<decimal?>("CommisionFee")
                        .HasColumnType("decimal(18, 0)")
                        .HasColumnName("Commision_Fee");

                    b.Property<string>("Id")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ID");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ConfigurationId");

                    b.ToTable("F_Configuration", (string)null);
                });

            modelBuilder.Entity("Management.Models.FPackage", b =>
                {
                    b.Property<string>("PackageId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Package_ID");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<int?>("MaximumArtworks")
                        .HasColumnType("int")
                        .HasColumnName("Maximum_Artworks");

                    b.Property<string>("PackageName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Package_Name");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 0)");

                    b.HasKey("PackageId");

                    b.ToTable("F_Package", (string)null);
                });

            modelBuilder.Entity("Management.Models.FPost", b =>
                {
                    b.Property<string>("PostId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Post_ID");

                    b.Property<string>("ArtworkId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Artwork_ID");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_On");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Tittle")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("PostId");

                    b.HasIndex("ArtworkId");

                    b.ToTable("F_Post", (string)null);
                });

            modelBuilder.Entity("Management.Models.FReport", b =>
                {
                    b.Property<string>("ReportId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Report_ID");

                    b.Property<string>("ActionNote")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Action_Note");

                    b.Property<string>("ArtworkId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Artwork_ID");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Created_By");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_On");

                    b.Property<string>("Detail")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Id")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ID");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ReportId");

                    b.HasIndex("ArtworkId");

                    b.ToTable("F_Report", (string)null);
                });

            modelBuilder.Entity("Management.Models.DInteraction", b =>
                {
                    b.HasOne("Management.Models.FPost", "Post")
                        .WithMany()
                        .HasForeignKey("PostId");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Management.Models.DPackageOfCreator", b =>
                {
                    b.HasOne("Management.Models.FPackage", "Package")
                        .WithMany("DPackageOfCreators")
                        .HasForeignKey("PackageId")
                        .IsRequired()
                        .HasConstraintName("FK_D_PackageOfCreator_F_Package");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("Management.Models.FPost", b =>
                {
                    b.HasOne("Management.Models.FArtwork", "Artwork")
                        .WithMany("FPosts")
                        .HasForeignKey("ArtworkId")
                        .HasConstraintName("FK_F_Post_F_Artwork");

                    b.Navigation("Artwork");
                });

            modelBuilder.Entity("Management.Models.FReport", b =>
                {
                    b.HasOne("Management.Models.FArtwork", "Artwork")
                        .WithMany("FReports")
                        .HasForeignKey("ArtworkId")
                        .HasConstraintName("FK_F_Report_F_Artwork");

                    b.Navigation("Artwork");
                });

            modelBuilder.Entity("Management.Models.FArtwork", b =>
                {
                    b.Navigation("FPosts");

                    b.Navigation("FReports");
                });

            modelBuilder.Entity("Management.Models.FPackage", b =>
                {
                    b.Navigation("DPackageOfCreators");
                });
#pragma warning restore 612, 618
        }
    }
}
