using AutoMapper;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LibShare.Api.Data.Mapping
{
    public class BookProfileMap : Profile
    {
        public BookProfileMap()
        {
            AllowNullCollections = true;
            CreateMap<Book, BookApiModel>()
                .AfterMap<BookImageResolver>();

            CreateMap<BookApiModel, Book>();
        }
    }

    public class BookImageResolver : IMappingAction<Book, BookApiModel>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IConfiguration _configuration;

        public BookImageResolver(IHttpContextAccessor accessor, IConfiguration configuration)
        {
            _accessor = accessor;
            _configuration = configuration;
        }

        public void Process(Book source, BookApiModel destination, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.Image))
                destination.Image = null;
            else
            {
                var folderName = _configuration.GetValue<string>("UrlBooks");
                var currentUrl = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}";
                destination.Image = Path.Combine(currentUrl, folderName, source.Image);
            }
        }
    }
}
