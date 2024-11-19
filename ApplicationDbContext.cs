using SAPPortal.Components.Pages;
using SAPPortal.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using SAPPortal.Components.Services;
using SAPbobsCOM;

namespace SAPPortal
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
        {
        }

        public DbSet<ParameterMaster> ParameterMasters { get; set; }
        public DbSet<AS_SAP_CONFIG> AS_SAP_CONFIGs { get; set; }
        public DbSet<M_User> AS_M_Users { get; set; }
        public DbSet<M_USERGROUP> AS_M_USERGROUP { get; set; }
        public DbSet<M_MENU> AS_M_MENU { get; set; }
        public DbSet<M_MODULE> AS_M_MODULE { get; set; }
        public DbSet<M_USERRIGHTTYPE> AS_M_USERRIGHTTYPE { get; set; }

        public DbSet<M_GROUPRIGHTMST> AS_M_GROUPRIGHTMST { get; set; }
        public DbSet<M_GROUPRIGHTDTL> AS_M_GROUPRIGHTDTL { get; set; }
        public DbSet<MenuMapUser> MenuMapUsers { get; set; }
        public DbSet<M_COMPANY> AS_M_COMPANY { get; set; }
      
        public DbSet<IncomingQC> IncomingQCs { get; set; }
        public DbSet<QCParameter> QCParameters { get; set; }

        public DbSet<ItemParameter> ItemParameters { get; set; }

        public DbSet<ParameterRow> ParameterRows { get; set; }

        // DbSet is optional if you are using stored procedures only
        public DbSet<AuthenticationModel> Authentication { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthenticationModel>().HasNoKey();

            // Other configurations if needed
        }

    }
}
