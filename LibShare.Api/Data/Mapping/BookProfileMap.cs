using AutoMapper;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Entities;

namespace LibShare.Api.Data.Mapping
{
    public class BookProfileMap : Profile
    {
        public BookProfileMap()
        {
            AllowNullCollections = true;
            CreateMap<Book, BookApiModel>().ReverseMap();
        }
    }
}
