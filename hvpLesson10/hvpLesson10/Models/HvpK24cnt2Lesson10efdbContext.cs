using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace hvpLesson10.Models;

public partial class HvpK24cnt2Lesson10efdbContext : DbContext
{
    public HvpK24cnt2Lesson10efdbContext()
    {
    }

    public HvpK24cnt2Lesson10efdbContext(DbContextOptions<HvpK24cnt2Lesson10efdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<HvpMember> HvpMembers { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost;Database=HVP_K24CNT2_LESSON10EFDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Books_CategoryId");

            entity.HasIndex(e => e.PublisherId, "IX_Books_PublisherId");

            entity.Property(e => e.BookId).HasMaxLength(10);
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Picture).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.Books).HasForeignKey(d => d.CategoryId);

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books).HasForeignKey(d => d.PublisherId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<HvpMember>(entity =>
        {
            entity.ToTable("hvp_member");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.HvpEmail)
                .HasMaxLength(50)
                .HasColumnName("hvp_email");
            entity.Property(e => e.HvpFullname)
                .HasMaxLength(50)
                .HasColumnName("hvp_fullname");
            entity.Property(e => e.HvpPassword)
                .HasMaxLength(50)
                .HasColumnName("hvp_password");
            entity.Property(e => e.HvpPhone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("hvp_phone");
            entity.Property(e => e.HvpStatus).HasColumnName("hvp_status");
            entity.Property(e => e.HvpUserName)
                .HasMaxLength(20)
                .HasColumnName("Hvp_userName");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(18);
            entity.Property(e => e.PublisherName).HasMaxLength(160);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
