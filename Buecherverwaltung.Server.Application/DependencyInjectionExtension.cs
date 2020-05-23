using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Buecherverwaltung.Server.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BookProfile));
            services.AddSingleton<IBookService, BookService>();
        }
    }
}
