using Microsoft.EntityFrameworkCore;

namespace EntityRepo.CovidAppModels
{
    public partial class CovidAppContext : DbContext
    {
        public CovidAppContext()
        {
        }

        public CovidAppContext(DbContextOptions<CovidAppContext> options)
            : base(options)
        {
        }
        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CovidApp;Integrated Security=True
        public virtual DbSet<PatientAddress> PatientAddress { get; set; }
        public virtual DbSet<PatientDetails> PatientDetails { get; set; }
        public virtual DbSet<PatientHospital> PatientHospital { get; set; }
        public virtual DbSet<PatientHospitalCovidTest> PatientHospitalCovidTest { get; set; }
        public virtual DbSet<PatientNextOfKin> PatientNextOfKins { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CovidApp;Integrated Security=True");
                //Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CovidApp;Integrated Security = True"
            }
        }

        //Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=CovidApp;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<PatientAddress>(entity =>
            {
                entity.ToTable("PatientAddress");

                entity.Property(e => e.PatientDetailsFk).HasColumnName("PatientDetailsFK");

                entity.Property(e => e.State)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Suburb)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.PatientDetailsFkNavigation)
                    .WithMany(p => p.PatientAddresses)
                    .HasForeignKey(d => d.PatientDetailsFk)
                    .HasConstraintName("FK_PatientAddress_PatientDetails1");

            });

            modelBuilder.Entity<PatientDetails>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PatientHospital>(entity =>
            {
                entity.ToTable("PatientHospital");

                entity.Property(e => e.DateOfTest).HasColumnType("datetime");

                entity.Property(e => e.HealthCarerName)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.HospitalName)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.PatientDetailsIdFkNavigation)
                    .WithMany(p => p.PatientHospital)
                    .HasForeignKey(d => d.PatientDetailsIdFk)
                    .HasConstraintName("FK_PatientHospital_PatientDetails1");
            });

            modelBuilder.Entity<PatientHospitalCovidTest>(entity =>
            {
                entity.ToTable("PatientHospitalCovidTest");

                entity.Property(e => e.Notes)
                    .HasMaxLength(900)
                    .IsUnicode(false);

                entity.Property(e => e.Result)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.HasOne(d => d.PatientDetailsIdFkNavigation)
                      .WithMany(p => p.PatientHospitalCovidTest)
                      .HasForeignKey(d => d.PatientDetailsIdFk)
                      .HasConstraintName("FK_PatientHospitalCovidTest_PatientDetails1");

                entity.HasOne(d => d.PatientHospitalIdFkNavigation)
                    .WithMany(p => p.PatientHospitalCovidTests)
                    .HasForeignKey(d => d.PatientHospitalIdFk)
                    .HasConstraintName("FK_PatientHospitalCovidTest_PatientHospital1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(90)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.HasKey(e => e.UserClaim1);

                entity.ToTable("UserClaim");

                entity.Property(e => e.UserClaim1)
                    .ValueGeneratedNever()
                    .HasColumnName("UserClaim");

                entity.Property(e => e.ClaimType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserIdFk).HasColumnName("UserID_FK");

                entity.HasOne(d => d.UserIdFkNavigation)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.UserIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserClaim");
            });

            modelBuilder.Entity<PatientNextOfKin>(entity =>
            {
                entity.ToTable("PatientNextOfKin");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PatientDetailsFk).HasColumnName("PatientDetailsFK");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Relationship)
                    .HasMaxLength(90)
                    .IsUnicode(false);

                entity.HasOne(d => d.PatientDetailsFkNavigation)
                    .WithMany(p => p.PatientNextOfKins)
                    .HasForeignKey(d => d.PatientDetailsFk)
                    .HasConstraintName("FK_PatientNextOfKin_PatientDetails1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
