using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DataAccess.Context
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        // Main Entities
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Member> Members => Set<Member>();
        public DbSet<Loan> Loans => Set<Loan>();
        public DbSet<Fine> Fines => Set<Fine>();
        public DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();
        public DbSet<LibraryBranch> LibraryBranches => Set<LibraryBranch>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category Configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.Description)
                    .HasMaxLength(300);

                entity.HasIndex(c => c.Name)
                    .IsUnique();
            });

            // LibraryBranch Configuration
            modelBuilder.Entity<LibraryBranch>(entity =>
            {
                entity.HasKey(lb => lb.Id);

                entity.Property(lb => lb.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(lb => lb.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(lb => lb.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(lb => lb.PhoneNumber)
                    .HasMaxLength(30);
            });

            // Author Configuration
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.FirstName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(a => a.LastName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(a => a.Nationality)
                    .HasMaxLength(80);
            });

            // Book Configuration
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(b => b.ISBN)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(b => b.Description)
                    .HasMaxLength(500);

                entity.Property(b => b.TotalCopies)
                    .IsRequired();

                entity.Property(b => b.AvailableCopies)
                    .IsRequired();

                entity.Property(b => b.Status)
                    .IsRequired();

                entity.HasIndex(b => b.ISBN)
                    .IsUnique();

                entity.HasOne(b => b.Category)
                    .WithMany(c => c.Books)
                    .HasForeignKey(b => b.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.LibraryBranch)
                    .WithMany(lb => lb.Books)
                    .HasForeignKey(b => b.LibraryBranchId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // BookAuthor Configuration - N:M relationship
            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasKey(ba => ba.Id);

                entity.Property(ba => ba.AssignedAt)
                    .IsRequired();

                entity.HasIndex(ba => new { ba.BookId, ba.AuthorId })
                    .IsUnique();

                entity.HasOne(ba => ba.Book)
                    .WithMany(b => b.BookAuthors)
                    .HasForeignKey(ba => ba.BookId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ba => ba.Author)
                    .WithMany(a => a.BookAuthors)
                    .HasForeignKey(ba => ba.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Member Configuration
            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.FirstName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(m => m.LastName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(m => m.DocumentNumber)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(m => m.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(m => m.PhoneNumber)
                    .HasMaxLength(30);

                entity.Property(m => m.Status)
                    .IsRequired();

                entity.HasIndex(m => m.DocumentNumber)
                    .IsUnique();

                entity.HasIndex(m => m.Email)
                    .IsUnique();
            });

            // Loan Configuration
            modelBuilder.Entity<Loan>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.Property(l => l.LoanDate)
                    .IsRequired();

                entity.Property(l => l.DueDate)
                    .IsRequired();

                entity.Property(l => l.Status)
                    .IsRequired();

                entity.HasOne(l => l.Book)
                    .WithMany(b => b.Loans)
                    .HasForeignKey(l => l.BookId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Member)
                    .WithMany(m => m.Loans)
                    .HasForeignKey(l => l.MemberId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Fine Configuration - 1:1 relationship
            modelBuilder.Entity<Fine>(entity =>
            {
                entity.HasKey(f => f.Id);

                entity.Property(f => f.Amount)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(f => f.Reason)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(f => f.IssuedDate)
                    .IsRequired();

                entity.Property(f => f.Status)
                    .IsRequired();

                entity.HasOne(f => f.Loan)
                    .WithOne(l => l.Fine)
                    .HasForeignKey<Fine>(f => f.LoanId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(f => f.LoanId)
                    .IsUnique();
            });

            // User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(u => u.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(u => u.Role)
                    .IsRequired();

                entity.Property(u => u.IsActive)
                    .IsRequired();

                entity.HasIndex(u => u.Email)
                    .IsUnique();
            });
        }
    }
}
