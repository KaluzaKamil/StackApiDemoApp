﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StackApiDemo.Contexts;

#nullable disable

namespace StackApiDemo.Migrations
{
    [DbContext(typeof(StackOverflowTagsContext))]
    partial class StackOverflowTagsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StackApiDemo.Models.TagsModels.Collective", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.ToTable("Collectives");
                });

            modelBuilder.Entity("StackApiDemo.Models.TagsModels.ExternalLink", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CollectiveId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CollectiveId");

                    b.ToTable("ExternalLinks");
                });

            modelBuilder.Entity("StackApiDemo.Models.TagsModels.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagsImportId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("count")
                        .HasColumnType("int");

                    b.Property<bool>("has_synonyms")
                        .HasColumnType("bit");

                    b.Property<bool>("is_moderator_only")
                        .HasColumnType("bit");

                    b.Property<bool>("is_required")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("share")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("TagsImportId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("StackApiDemo.Models.TagsModels.TagsImport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("has_more")
                        .HasColumnType("bit");

                    b.Property<int>("quota_max")
                        .HasColumnType("int");

                    b.Property<int>("quota_remaining")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TagsImports");
                });

            modelBuilder.Entity("StackApiDemo.Models.TagsModels.Collective", b =>
                {
                    b.HasOne("StackApiDemo.Models.TagsModels.Tag", "Tag")
                        .WithMany("collectives")
                        .HasForeignKey("TagId");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("StackApiDemo.Models.TagsModels.ExternalLink", b =>
                {
                    b.HasOne("StackApiDemo.Models.TagsModels.Collective", "Collective")
                        .WithMany("external_links")
                        .HasForeignKey("CollectiveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collective");
                });

            modelBuilder.Entity("StackApiDemo.Models.TagsModels.Tag", b =>
                {
                    b.HasOne("StackApiDemo.Models.TagsModels.TagsImport", "TagsImport")
                        .WithMany("items")
                        .HasForeignKey("TagsImportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TagsImport");
                });

            modelBuilder.Entity("StackApiDemo.Models.TagsModels.Collective", b =>
                {
                    b.Navigation("external_links");
                });

            modelBuilder.Entity("StackApiDemo.Models.TagsModels.Tag", b =>
                {
                    b.Navigation("collectives");
                });

            modelBuilder.Entity("StackApiDemo.Models.TagsModels.TagsImport", b =>
                {
                    b.Navigation("items");
                });
#pragma warning restore 612, 618
        }
    }
}
