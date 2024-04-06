using Microsoft.EntityFrameworkCore;
using TutorialApp.Infrastructure.Identity;
using TutorialApp.Infrastructure.Models;
using LkpCountry = TutorialApp.Infrastructure.Models.LkpCountry;

namespace TutorialApp.Infrastructure.DB;

public partial class TutorialAppContext : DbContext
{
    public TutorialAppContext()
    {
    }

    public TutorialAppContext(DbContextOptions<TutorialAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
    public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
    public virtual DbSet<AspNetUserOtp> AspNetUserOtp { get; set; } = null!;
    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
    public virtual DbSet<ChapterQuestion> ChapterQuestions { get; set; } = null!;
    public virtual DbSet<ExamSubject> ExamSubjects { get; set; } = null!;
    public virtual DbSet<ExamType> ExamTypes { get; set; } = null!;
    public virtual DbSet<LkpCountry> LkpCountries { get; set; } = null!;
    public virtual DbSet<LkpExamTypes> LkpExamTypes { get; set; } = null!;
    public virtual DbSet<LkpStatus> LkpStatuses { get; set; } = null!;
    public virtual DbSet<QuestionOption> QuestionOptions { get; set; } = null!;
    public virtual DbSet<SubjectChapter> SubjectChapters { get; set; } = null!;
    public virtual DbSet<UserExamType> UserExamTypes { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlServer(
                "Server=sql.bsite.net\\MSSQL2016;Database=tutorialapp_2024;User Id=tutorialapp_2024;Password=Prne6ak6@1;TrustServerCertificate=True;MultipleActiveResultSets=true;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);

            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.AspNetRoleClaims)
                .HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);

            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles)
                .WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");

                        j.ToTable("AspNetUserRoles");

                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User)
                .WithMany(p => p.AspNetUserClaims)
                .HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User)
                .WithMany(p => p.AspNetUserLogins)
                .HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User)
                .WithMany(p => p.AspNetUserTokens)
                .HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<ChapterQuestion>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Chapter)
                .WithMany(p => p.ChapterQuestions)
                .HasForeignKey(d => d.ChapterId)
                .HasConstraintName("FK__ChapterQu__Chapt__634EBE90");

            entity.HasOne(d => d.Status)
                .WithMany(p => p.ChapterQuestions)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__ChapterQuestions__Status__6442E2C9");
        });

        modelBuilder.Entity<ExamSubject>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

            entity.HasOne(d => d.ExamType)
                .WithMany(p => p.ExamSubjects)
                .HasForeignKey(d => d.ExamTypeId)
                .HasConstraintName("FK__ExamSubje__ExamT__5BAD9CC8");

            entity.HasOne(d => d.Status)
                .WithMany(p => p.ExamSubjects)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__ExamSubject__Status__5CA1C101");
        });

        modelBuilder.Entity<ExamType>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.ExamType1).HasColumnName("ExamType");

            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Country)
                .WithMany(p => p.ExamTypes)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__ExamTypes__Count__57DD0BE4");

            entity.HasOne(d => d.Status)
                .WithMany(p => p.ExamTypes)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__ExamTypes__Status__58D1301D");
        });

        modelBuilder.Entity<LkpCountry>(entity =>
        {
            entity.ToTable("LkpCountry");

            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<LkpExamTypes>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<LkpStatus>(entity =>
        {
            entity.ToTable("LkpStatus");

            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.StatusName).IsUnicode(false);
        });

        modelBuilder.Entity<QuestionOption>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

            entity.Property(e => e.Option1).IsUnicode(false);

            entity.Property(e => e.Option2).IsUnicode(false);

            entity.Property(e => e.Option3).IsUnicode(false);

            entity.Property(e => e.Options4).IsUnicode(false);

            entity.HasOne(d => d.Question)
                .WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__QuestionO__Quest__671F4F74");

            entity.HasOne(d => d.Status)
                .WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__QuestionOptions__Status__681373AD");
        });

        modelBuilder.Entity<SubjectChapter>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Status)
                .WithMany(p => p.SubjectChapters)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__SubjectChapters__Status__607251E5");

            entity.HasOne(d => d.Subject)
                .WithMany(p => p.SubjectChapters)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__SubjectCh__Subje__5F7E2DAC");
        });

        modelBuilder.Entity<UserExamType>(entity =>
        {
            entity.ToTable("UserExamType");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.ExamType)
                .WithMany(p => p.UserExamTypes)
                .HasForeignKey(d => d.ExamTypeId)
                .HasConstraintName("FK__UserExamT__ExamT__76619304");

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserExamTypes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserExamT__UserI__7755B73D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}