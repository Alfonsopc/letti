using Microsoft.EntityFrameworkCore;
using Letti.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Letti.Data.Configurations;

namespace Letti.Data
{
    public class LettiContext:DbContext
    {
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<ContractorScanning> ContractorScans { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<PersonOfInterest> PersonOfInterests { get; set; }
        public DbSet<PoiScanning> PersonScans { get; set; }
        public DbSet<ContractorRelationshipType> CompanyRelationshipTypes { get; set; }
        public DbSet<OrganizationRelationshipType> OrganizationRelationshipTypes { get; set; }
        public DbSet<PersonalRelationshipType> PersonalRelationshipTypes { get; set; }
        public DbSet<PersonalRelationship> PersonalRelationShips { get; set; }
        public DbSet<PersonCompanyRelationship> PersonCompanyRelationships { get; set; }
        public DbSet<PersonOrganizationRelationship> PersonOrganizationRelationships { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<CaseToReview> Cases { get; set; }
        public LettiContext() : base()
        {

        }

        public LettiContext(DbContextOptions<LettiContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // TODO: Use DI for setup
                string connstring = @"Server=.\SQLEXPRESS;Database=LettiDb;Trusted_Connection=True;";
                //string connstring = @"Server=CAMILLEDATA;Database=SindicatoDigitalDev;User ID=sacamille;Password=r3#m4Ac14t;";
                // string connstring = Configuration["Data:DefaultConnection:ConnectionString"];
                optionsBuilder.UseSqlServer(connstring);

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.RemovePluralizingTableNameConvention();
            //modelBuilder.Query<MissingPayrollUnionMember>();
            modelBuilder.ApplyConfiguration(new CaseToReviewConfiguration());
            modelBuilder.Entity<PersonOfInterest>().Ignore(c => c.FullName);
            
        }
        
    }
}
