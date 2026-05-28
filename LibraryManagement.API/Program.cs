
using LibraryManagement.API.Mappings;
using LibraryManagement.DataAccess.Context;
using LibraryManagement.DataAccess.Repositories;
using LibraryManagement.Domain.Interfaces.Repositories;
using LibraryManagement.Domain.Interfaces.Services;
using LibraryManagement.Domain.Services;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.DataAccess.Seeders;

namespace LibraryManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add controllers to the API
            builder.Services.AddControllers();

            // Configure Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Configure SQL Server connection
            builder.Services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<ILoanRepository, LoanRepository>();
            builder.Services.AddScoped<IFineRepository, FineRepository>();
            builder.Services.AddScoped<ILibraryBranchRepository, LibraryBranchRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();

            // Register services
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ILoanService, LoanService>();
            builder.Services.AddScoped<IFineService, FineService>();
            builder.Services.AddScoped<ILibraryBranchService, LibraryBranchService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IBookAuthorService, BookAuthorService>();

            var app = builder.Build();

            // Execute DataSeeder to populate initial data if the database is empty
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
                DataSeeder.SeedAsync(context).Wait();
            }

            // Enable Swagger only in development environment
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
