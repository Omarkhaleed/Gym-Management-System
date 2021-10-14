namespace OlympicGym.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class CompanyContext : DbContext
    {
       
        public CompanyContext()
            : base("name=CompanyContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public virtual DbSet<Trainee> Trainees { set; get; }
        public virtual DbSet<Sport> Sports { set; get; }
        public virtual DbSet<Plan> Plans { set; get; }
        public virtual DbSet<Admin> Admins { set; get; }
        public virtual DbSet<Coach> Coaches { set; get; }
       public virtual DbSet<Report> Reports { set; get; }

    }
}