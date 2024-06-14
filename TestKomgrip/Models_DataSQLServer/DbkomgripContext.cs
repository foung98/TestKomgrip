using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TestKomgrip.Models;

namespace TestKomgrip.Models_DataSQLServer;

public partial class DbkomgripContext : DbContext
{
    public DbkomgripContext()
    {
    }

    public DbkomgripContext(DbContextOptions<DbkomgripContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbLogin> TbLogins { get; set; }

    public virtual DbSet<TbTimeLog> TbTimeLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\localDB1;Initial Catalog=DBKOMGRIP;User ID=TestUser;Password=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Thai_100_CS_AI");

        modelBuilder.Entity<TbLogin>(entity =>
        {
            entity.ToTable("Tb_Login");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Position)
                .IsUnicode(false)
                .HasColumnName("position");
        });

        modelBuilder.Entity<TbTimeLog>(entity =>
        {
            entity.ToTable("Tb_TimeLog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LastLogin)
                .HasColumnType("datetime")
                .HasColumnName("last_login");
            entity.Property(e => e.NameId).HasColumnName("name_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<TestKomgrip.Models.UserModel> UserModel { get; set; } = default!;
}
