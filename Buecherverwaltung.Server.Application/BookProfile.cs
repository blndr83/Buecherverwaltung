using AutoMapper;
using Buecherverwaltung.Server.Core.Entities;
using Buecherverwaltung.Shared;

namespace Buecherverwaltung.Server.Application
{
    internal class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
        }
    }
}
