﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using db;

#nullable disable

namespace ngaq.Migrations
{
    [DbContext(typeof(NgaqDbCtx))]
    partial class NgaqDbCtxModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("ngaq.Core.model.WordKV", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("bl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("ct")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValueSql("(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))");

                    b.Property<string>("kDesc")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("kI64")
                        .HasColumnType("INTEGER");

                    b.Property<string>("kStr")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("kType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("STR");

                    b.Property<long>("ut")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValueSql("(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))");

                    b.Property<string>("vDesc")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("vF64")
                        .HasColumnType("REAL");

                    b.Property<long>("vI64")
                        .HasColumnType("INTEGER");

                    b.Property<string>("vStr")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("vType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("STR");

                    b.HasKey("id");

                    b.HasIndex("bl");

                    b.HasIndex("ct");

                    b.HasIndex("kDesc");

                    b.HasIndex("kI64");

                    b.HasIndex("kStr");

                    b.HasIndex("ut");

                    b.ToTable("WordKV", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
