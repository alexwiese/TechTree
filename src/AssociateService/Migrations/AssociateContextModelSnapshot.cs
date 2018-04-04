﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TechTree.AssociateService.Database;

namespace TechTree.AssociateService.Migrations
{
    [DbContext(typeof(AssociateContext))]
    partial class AssociateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TechTree.AssociateService.Models.Associate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Associates");
                });

            modelBuilder.Entity("TechTree.AssociateService.Models.Association", b =>
                {
                    b.Property<int>("AssociateId");

                    b.Property<int>("NodeId");

                    b.Property<string>("AssociationType");

                    b.HasKey("AssociateId", "NodeId");

                    b.ToTable("Association");
                });

            modelBuilder.Entity("TechTree.AssociateService.Models.Association", b =>
                {
                    b.HasOne("TechTree.AssociateService.Models.Associate", "Associate")
                        .WithMany("Associations")
                        .HasForeignKey("AssociateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
