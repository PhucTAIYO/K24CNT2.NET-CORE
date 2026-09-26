using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hvp2410900061_exam.Models;

public partial class HvpEmployee2410900061DbContext : DbContext
{
    public HvpEmployee2410900061DbContext()
    {
    }

    public HvpEmployee2410900061DbContext(DbContextOptions<HvpEmployee2410900061DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HvpEmployee> HvpEmployees { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost;Database=Hvp_Employee_2410900061_Db;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HvpEmployee>(entity =>
        {
            entity.ToTable("HvpEmployee");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.HvpActive)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.HvpEmail)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.HvpName).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
