using AutoMapper;
using LibraryManagement.API.DTOs.Request;
using LibraryManagement.API.DTOs.Response;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book mappings
            CreateMap<BookRequestDTO, Book>();

            CreateMap<Book, BookResponseDTO>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.LibraryBranchName,
                    opt => opt.MapFrom(src => src.LibraryBranch.Name));

            // Author mappings
            CreateMap<AuthorRequestDTO, Author>();
            CreateMap<Author, AuthorResponseDTO>();

            // Category mappings
            CreateMap<CategoryRequestDTO, Category>();
            CreateMap<Category, CategoryResponseDTO>();

            // Member mappings
            CreateMap<MemberRequestDTO, Member>();
            CreateMap<Member, MemberResponseDTO>();

            // Loan mappings
            CreateMap<LoanRequestDTO, Loan>();

            CreateMap<Loan, LoanResponseDTO>()
                .ForMember(dest => dest.BookTitle,
                    opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.MemberFullName,
                    opt => opt.MapFrom(src => $"{src.Member.FirstName} {src.Member.LastName}"));

            // Fine mappings
            CreateMap<FineRequestDTO, Fine>();
            CreateMap<Fine, FineResponseDTO>();

            // Library branch mappings
            CreateMap<LibraryBranchRequestDTO, LibraryBranch>();
            CreateMap<LibraryBranch, LibraryBranchResponseDTO>();

            // User mappings
            CreateMap<UserRequestDTO, User>()
                .ForMember(dest => dest.PasswordHash,
                    opt => opt.MapFrom(src => src.Password));

            CreateMap<User, UserResponseDTO>();

            // BookAuthor mappings
            CreateMap<BookAuthorRequestDTO, BookAuthor>();

            CreateMap<BookAuthor, BookAuthorResponseDTO>()
                .ForMember(dest => dest.BookTitle,
                    opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.AuthorFullName,
                    opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"));
        }
    }
}
