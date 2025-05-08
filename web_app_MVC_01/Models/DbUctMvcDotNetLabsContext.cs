using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace web_app_MVC_01.Models;

public partial class DbUctMvcDotNetLabsContext : DbContext
{
    public DbUctMvcDotNetLabsContext()
    {
    }

    public DbUctMvcDotNetLabsContext(DbContextOptions<DbUctMvcDotNetLabsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DemoTable> DemoTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=db_UCT_MVC_DotNet_Labs;User Id=sa;Password=123456;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DemoTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("demo_table");

            entity.Property(e => e.ColumnName).HasColumnName("column_name");
            entity.Property(e => e.Id).HasColumnName("id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
