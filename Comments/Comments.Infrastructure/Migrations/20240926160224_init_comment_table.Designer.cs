﻿// <auto-generated />
using System;
using Comments.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Comments.Infrastructure.Migrations
{
    [DbContext(typeof(CommentContext))]
    [Migration("20240926160224_init_comment_table")]
    partial class init_comment_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.7.24405.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Comments.Domain.CommentAgg.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("CommentFor")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<long>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("WhyRejected")
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Comments.Domain.CommentAgg.Comment", b =>
                {
                    b.HasOne("Comments.Domain.CommentAgg.Comment", "ParentComment")
                        .WithMany("ChildComments")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ParentComment");
                });

            modelBuilder.Entity("Comments.Domain.CommentAgg.Comment", b =>
                {
                    b.Navigation("ChildComments");
                });
#pragma warning restore 612, 618
        }
    }
}
