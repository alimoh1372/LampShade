﻿// <auto-generated />
using System;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InventoryManagement.Infrastructure.EfCore.Migrations
{
    [DbContext(typeof(InventoryContext))]
    partial class InventoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InventoryManagement.Domain.InventoryAgg.Inventory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("FkProductId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsInStock")
                        .HasColumnType("bit");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("InventoryManagement.Domain.InventoryAgg.Inventory", b =>
                {
                    b.OwnsMany("InventoryManagement.Domain.InventoryAgg.InventoryOperation", "InventoryOperations", b1 =>
                        {
                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<long>("Count")
                                .HasColumnType("bigint");

                            b1.Property<long>("CurrentCount")
                                .HasColumnType("bigint");

                            b1.Property<string>("Description")
                                .HasMaxLength(1000)
                                .HasColumnType("nvarchar(1000)");

                            b1.Property<long>("FkInventoryId")
                                .HasColumnType("bigint");

                            b1.Property<long>("FkOperatorId")
                                .HasColumnType("bigint");

                            b1.Property<bool>("Operation")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("OperationDate")
                                .HasColumnType("datetime2");

                            b1.Property<long>("OrderId")
                                .HasColumnType("bigint");

                            b1.HasKey("Id");

                            b1.HasIndex("FkInventoryId");

                            b1.ToTable("InventoryOperations");

                            b1.WithOwner("Inventory")
                                .HasForeignKey("FkInventoryId");

                            b1.Navigation("Inventory");
                        });

                    b.Navigation("InventoryOperations");
                });
#pragma warning restore 612, 618
        }
    }
}
