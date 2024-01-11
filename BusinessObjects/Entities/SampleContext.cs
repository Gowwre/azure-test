using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Entities;

public partial class SampleContext : DbContext
{
    public SampleContext()
    {
    }

    public SampleContext(DbContextOptions<SampleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(GetConnectionStrings());

    private static string GetConnectionStrings()
    {
        try
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();

            var result = config.GetConnectionString("DefaultDB");

            return result;
        }
        catch (Exception e)
        {
            var config = new ConfigurationManager();
            return config["ConnectionStrings:DefaultDB"];
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Accounts__A9D10535FCB7E1FB");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(20);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA388AC54465D");

            entity.Property(e => e.SubjectId)
                .HasMaxLength(20)
                .HasColumnName("SubjectID");
            entity.Property(e => e.SubjectName).HasMaxLength(150);
            entity.Property(e => e.TeacherEmail).HasMaxLength(50);

            entity.HasOne(d => d.TeacherEmailNavigation).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.TeacherEmail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subjects_Accounts");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
