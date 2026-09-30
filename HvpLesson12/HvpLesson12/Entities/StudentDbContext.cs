using Microsoft.EntityFrameworkCore;
using HvpLesson12.Models;

namespace HvpLesson12.Entities
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }

        public DbSet<StdClass> StdClasses { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Subjects> Subjects { get; set; } = null!;
        public DbSet<Marks> Marks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite Primary Key for Marks: (SubjectId, StudentId)
            modelBuilder.Entity<Marks>()
                .HasKey(m => new { m.SubjectId, m.StudentId });

            // Relationships for Marks
            modelBuilder.Entity<Marks>()
                .HasOne(m => m.Subject)
                .WithMany(s => s.Marks)
                .HasForeignKey(m => m.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Marks>()
                .HasOne(m => m.Student)
                .WithMany(s => s.Marks)
                .HasForeignKey(m => m.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship for Student - StdClass
            modelBuilder.Entity<Student>()
                .HasOne(s => s.StdClass)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique constraints from requirement
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.StudentEmail)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.StudentPhone)
                .IsUnique();

            modelBuilder.Entity<Subjects>()
                .HasIndex(s => s.SubjectName)
                .IsUnique();
        }
    }
}
