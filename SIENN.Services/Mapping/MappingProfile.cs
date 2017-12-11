namespace SIENN.Services.Mapping
{
    using AutoMapper;
    using Model;
    using Resources;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API
            CreateMap<Type, TypeResource>();
            CreateMap<Unit, UnitResource>();
            CreateMap<Category, CategoryResource>();
            CreateMap<Product, ProductResource>();

            //API to domain
            CreateMap<TypeResource, Type>();
            CreateMap<UnitResource, Unit>();


        }
    }
}