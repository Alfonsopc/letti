﻿// <auto-generated />
using System;
using Letti.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Letti.Data.Migrations
{
    [DbContext(typeof(LettiContext))]
    [Migration("20210729050555_missing_contract_properties_3")]
    partial class missing_contract_properties_3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Letti.Model.CaseToReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CaseType")
                        .HasColumnType("int");

                    b.Property<int>("ContractorId")
                        .HasColumnType("int");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContractorId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("Letti.Model.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bidding")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Concept")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContractObjectType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContractType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ContractorId")
                        .HasColumnType("int");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ContractorId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Letti.Model.Contractor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComercialName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FiscalAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FoundedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("OfficialName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contractors");
                });

            modelBuilder.Entity("Letti.Model.ContractorRelationshipType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RelationshipType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CompanyRelationshipTypes");
                });

            modelBuilder.Entity("Letti.Model.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganizationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganizationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Letti.Model.OrganizationRelationshipType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RelationshipType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("OrganizationRelationshipTypes");
                });

            modelBuilder.Entity("Letti.Model.PersonCompanyRelationship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("ContractorRelationshipTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PersonOfInterestId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ContractorRelationshipTypeId");

                    b.HasIndex("PersonOfInterestId");

                    b.ToTable("PersonCompanyRelationships");
                });

            modelBuilder.Entity("Letti.Model.PersonOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("CURP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOficial")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RFC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondLastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PersonOfInterests");
                });

            modelBuilder.Entity("Letti.Model.PersonOrganizationRelationship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Job")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<int>("OrganizationRelationshipTypeId")
                        .HasColumnType("int");

                    b.Property<int>("PersonOfInterestId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("OrganizationRelationshipTypeId");

                    b.HasIndex("PersonOfInterestId");

                    b.ToTable("PersonOrganizationRelationships");
                });

            modelBuilder.Entity("Letti.Model.PersonalRelationship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsOficial")
                        .HasColumnType("bit");

                    b.Property<int>("MainPersonId")
                        .HasColumnType("int");

                    b.Property<int>("PersonalRelationshipTypeId")
                        .HasColumnType("int");

                    b.Property<int>("SecondaryPersonId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MainPersonId");

                    b.HasIndex("PersonalRelationshipTypeId");

                    b.HasIndex("SecondaryPersonId");

                    b.ToTable("PersonalRelationShips");
                });

            modelBuilder.Entity("Letti.Model.PersonalRelationshipType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InverseRelationshipType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RelationshipType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PersonalRelationshipTypes");
                });

            modelBuilder.Entity("Letti.Model.SuspiciousRelationship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CaseToReviewId")
                        .HasColumnType("int");

                    b.Property<int>("ContractorPoiId")
                        .HasColumnType("int");

                    b.Property<string>("Job")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizationPoiId")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Relationship")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CaseToReviewId");

                    b.HasIndex("ContractorPoiId");

                    b.HasIndex("OrganizationPoiId");

                    b.ToTable("SuspiciousRelationship");
                });

            modelBuilder.Entity("Letti.Model.CaseToReview", b =>
                {
                    b.HasOne("Letti.Model.Contractor", "Contractor")
                        .WithMany()
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Letti.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contractor");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Letti.Model.Contract", b =>
                {
                    b.HasOne("Letti.Model.Contractor", "Contractor")
                        .WithMany()
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Letti.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contractor");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Letti.Model.PersonCompanyRelationship", b =>
                {
                    b.HasOne("Letti.Model.Contractor", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Letti.Model.ContractorRelationshipType", "ContractorRelationshipType")
                        .WithMany()
                        .HasForeignKey("ContractorRelationshipTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Letti.Model.PersonOfInterest", "PersonOfInterest")
                        .WithMany()
                        .HasForeignKey("PersonOfInterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("ContractorRelationshipType");

                    b.Navigation("PersonOfInterest");
                });

            modelBuilder.Entity("Letti.Model.PersonOrganizationRelationship", b =>
                {
                    b.HasOne("Letti.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Letti.Model.OrganizationRelationshipType", "OrganizationRelationshipType")
                        .WithMany()
                        .HasForeignKey("OrganizationRelationshipTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Letti.Model.PersonOfInterest", "PersonOfInterest")
                        .WithMany()
                        .HasForeignKey("PersonOfInterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("OrganizationRelationshipType");

                    b.Navigation("PersonOfInterest");
                });

            modelBuilder.Entity("Letti.Model.PersonalRelationship", b =>
                {
                    b.HasOne("Letti.Model.PersonOfInterest", "MainPerson")
                        .WithMany()
                        .HasForeignKey("MainPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Letti.Model.PersonalRelationshipType", "PersonalRelationshipType")
                        .WithMany()
                        .HasForeignKey("PersonalRelationshipTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Letti.Model.PersonOfInterest", "SecondaryPerson")
                        .WithMany()
                        .HasForeignKey("SecondaryPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainPerson");

                    b.Navigation("PersonalRelationshipType");

                    b.Navigation("SecondaryPerson");
                });

            modelBuilder.Entity("Letti.Model.SuspiciousRelationship", b =>
                {
                    b.HasOne("Letti.Model.CaseToReview", "CaseToReview")
                        .WithMany("SuspiciousRelationships")
                        .HasForeignKey("CaseToReviewId");

                    b.HasOne("Letti.Model.PersonOfInterest", "ContractorPoi")
                        .WithMany()
                        .HasForeignKey("ContractorPoiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Letti.Model.PersonOfInterest", "OrganizationPoi")
                        .WithMany()
                        .HasForeignKey("OrganizationPoiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CaseToReview");

                    b.Navigation("ContractorPoi");

                    b.Navigation("OrganizationPoi");
                });

            modelBuilder.Entity("Letti.Model.CaseToReview", b =>
                {
                    b.Navigation("SuspiciousRelationships");
                });
#pragma warning restore 612, 618
        }
    }
}
