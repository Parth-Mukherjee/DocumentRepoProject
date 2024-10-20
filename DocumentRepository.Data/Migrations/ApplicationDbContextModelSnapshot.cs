﻿// <auto-generated />
using System;
using DocumentRepository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DocumentRepository.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DocumentRepository.Models.DocumentModel", b =>
                {
                    b.Property<Guid>("documentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("documentCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("documentExtension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("documentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("documentSize")
                        .HasColumnType("int");

                    b.Property<string>("updatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("uploadedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("uploadedFileDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("uploaded_DateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("documentID");

                    b.ToTable("documentModels");
                });
#pragma warning restore 612, 618
        }
    }
}
