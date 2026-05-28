using LibraryManagement.DataAccess.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DataAccess.Seeders
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(LibraryDbContext context)
        {
            // Prevents duplicate data when the application starts multiple times
            if (await context.Categories.AnyAsync())
                return;

            // Categories
            var categories = new List<Category>
        {
            new() { Name = "Programming", Description = "Software development and programming books." },
            new() { Name = "Literature", Description = "Classic and contemporary literature." },
            new() { Name = "History", Description = "Books about history and society." },
            new() { Name = "Science", Description = "Scientific and educational books." },
            new() { Name = "Business", Description = "Business, finance and entrepreneurship books." },
            new() { Name = "Art", Description = "Books about art, design and creativity." },
            new() { Name = "Children", Description = "Books for children and young readers." },
            new() { Name = "Fantasy", Description = "Fantasy and science fiction books." },
            new() { Name = "Biography", Description = "Biographies and memoirs." },
            new() { Name = "Self-Help", Description = "Books about personal development and self-improvement." },
            new() { Name = "Health", Description = "Books about health, wellness and fitness." },
            new() { Name = "Travel", Description = "Travel guides and books about different cultures." },
            new() { Name = "Cooking", Description = "Cookbooks and books about food and cooking." },
            new() { Name = "Poetry", Description = "Collections of poetry and verse." },
            new() { Name = "Philosophy", Description = "Books about philosophy and critical thinking." },
            new() { Name = "Religion", Description = "Books about religion, spirituality and theology." }
        };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();

            // Library branches
            var branches = new List<LibraryBranch>
        {
            new() { Name = "Main Library", Address = "Calle 11 #4-14", City = "Bogota", PhoneNumber = "6010000001" },
            new() { Name = "North Branch", Address = "Carrera 15 #80-20", City = "Bogota", PhoneNumber = "6010000002" },
            new() { Name = "South Branch", Address = "Avenida 19 #50-30", City = "Bogota", PhoneNumber = "6010000003" },
            new() { Name = "East Branch", Address = "Calle 22 #60-10", City = "Bogota", PhoneNumber = "6010000004" },
            new() { Name = "West Branch", Address = "Carrera 10 #70-40", City = "Medellín", PhoneNumber = "6010000005" },
            new() { Name = "Central Branch", Address = "Avenida 5 #30-50", City = "Medellín", PhoneNumber = "6010000006" },
            new() { Name = "North Branch", Address = "Calle 8 #40-20", City = "Medellín", PhoneNumber = "6010000007" },
            new() { Name = "South Branch", Address = "Carrera 20 #90-10", City = "Medellín", PhoneNumber = "6010000008" }
        };

            context.LibraryBranches.AddRange(branches);
            await context.SaveChangesAsync();

            // Authors
            var authors = new List<Author>
        {
            new() { FirstName = "Robert", LastName = "Martin", Nationality = "American", BirthDate = new DateTime(1952, 12, 5) },
            new() { FirstName = "Gabriel", LastName = "Garcia Marquez", Nationality = "Colombian", BirthDate = new DateTime(1927, 3, 6) },
            new() { FirstName = "Yuval", LastName = "Harari", Nationality = "Israeli", BirthDate = new DateTime(1976, 2, 24) },
            new() { FirstName = "Stephen", LastName = "Hawking", Nationality = "British", BirthDate = new DateTime(1942, 1, 8) },
            new() { FirstName = "Eric", LastName = "Ries", Nationality = "American", BirthDate = new DateTime(1978, 9, 22) },
            
           
        };

            context.Authors.AddRange(authors);
            await context.SaveChangesAsync();

            // Books
            var books = new List<Book>
        {
            new()
            {
                Title = "Clean Code",
                ISBN = "9780132350884",
                Description = "A handbook of agile software craftsmanship.",
                PublishedDate = new DateTime(2008, 8, 1),
                TotalCopies = 5,
                AvailableCopies = 5,
                Status = BookStatus.Available,
                CategoryId = categories[0].Id,
                LibraryBranchId = branches[0].Id
            },
            new()
            {
                Title = "One Hundred Years of Solitude",
                ISBN = "9780307474728",
                Description = "A landmark novel of magical realism.",
                PublishedDate = new DateTime(1967, 5, 30),
                TotalCopies = 4,
                AvailableCopies = 4,
                Status = BookStatus.Available,
                CategoryId = categories[1].Id,
                LibraryBranchId = branches[0].Id
            },
            new()
            {
                Title = "Sapiens",
                ISBN = "9780062316097",
                Description = "A brief history of humankind.",
                PublishedDate = new DateTime(2011, 1, 1),
                TotalCopies = 3,
                AvailableCopies = 3,
                Status = BookStatus.Available,
                CategoryId = categories[2].Id,
                LibraryBranchId = branches[1].Id
            },
            new()
            {
                Title = "A Brief History of Time",
                ISBN = "9780553380163",
                Description = "An overview of cosmology for general readers.",
                PublishedDate = new DateTime(1988, 4, 1),
                TotalCopies = 3,
                AvailableCopies = 3,
                Status = BookStatus.Available,
                CategoryId = categories[3].Id,
                LibraryBranchId = branches[1].Id
            },
            new()
            {
                Title = "The Lean Startup",
                ISBN = "9780307887894",
                Description = "A method for building startups and products.",
                PublishedDate = new DateTime(2011, 9, 13),
                TotalCopies = 2,
                AvailableCopies = 2,
                Status = BookStatus.Available,
                CategoryId = categories[4].Id,
                LibraryBranchId = branches[0].Id
            }
        };

            context.Books.AddRange(books);
            await context.SaveChangesAsync();

            // Book-author relationships
            var bookAuthors = new List<BookAuthor>
        {
            new() { BookId = books[0].Id, AuthorId = authors[0].Id },
            new() { BookId = books[1].Id, AuthorId = authors[1].Id },
            new() { BookId = books[2].Id, AuthorId = authors[2].Id },
            new() { BookId = books[3].Id, AuthorId = authors[3].Id },
            new() { BookId = books[4].Id, AuthorId = authors[4].Id }
        };

            context.BookAuthors.AddRange(bookAuthors);
            await context.SaveChangesAsync();

            // Members
            var members = new List<Member>
        {
            new()
            {
                FirstName = "Sandra",
                LastName = "Londono",
                DocumentNumber = "100000001",
                Email = "sandra@example.com",
                PhoneNumber = "3000000001",
                Status = MemberStatus.Active
            },
            new()
            {
                FirstName = "Carlos",
                LastName = "Perez",
                DocumentNumber = "100000002",
                Email = "carlos@example.com",
                PhoneNumber = "3000000002",
                Status = MemberStatus.Active
            },
            new()
            {
                FirstName = "Laura",
                LastName = "Gomez",
                DocumentNumber = "100000003",
                Email = "laura@example.com",
                PhoneNumber = "3000000003",
                Status = MemberStatus.Suspended
            }
        };

            context.Members.AddRange(members);
            await context.SaveChangesAsync();

            // Users
            var users = new List<User>
        {
            new()
            {
                FirstName = "Admin",
                LastName = "Library",
                Email = "admin@library.com",
                PasswordHash = "Admin123",
                Role = UserRole.Admin,
                IsActive = true
            },
            new()
            {
                FirstName = "Librarian",
                LastName = "User",
                Email = "librarian@library.com",
                PasswordHash = "Librarian123",
                Role = UserRole.Librarian,
                IsActive = true
            },
            new()
            {
                FirstName = "Member",
                LastName = "User",
                Email = "member@library.com",
                PasswordHash = "Member123",
                Role = UserRole.Member,
                IsActive = true
            }
        };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }
    }
}
