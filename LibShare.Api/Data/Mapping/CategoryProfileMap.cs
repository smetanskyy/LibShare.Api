using AutoMapper;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Entities;

namespace LibShare.Api.Data.Mapping
{
    public class CategoryProfileMap : Profile
    {
        public CategoryProfileMap()
        {
            AllowNullCollections = true;
            CreateMap<Category, CategoryApiModel>().ReverseMap();
        }
    }
}
