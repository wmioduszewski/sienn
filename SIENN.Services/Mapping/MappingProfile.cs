namespace SIENN.Services.Mapping
{
    using System.Linq;
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

            CreateMap<Product, ProductResource>()
                .ForMember(pr => pr.Categories, opt => opt.MapFrom(p => p.Categories.Select(pc => pc.CategoryId)));

            //API to domain
            CreateMap<TypeResource, Type>();
            CreateMap<UnitResource, Unit>();
            CreateMap<CategoryResource, Category>()
                .ForMember(x=>x.Products, opt => opt.Ignore());

            CreateMap<ProductResource, Product>()
                .ForMember(p => p.Categories, opt => opt.Ignore())
                .AfterMap((pr, p) =>
                {
                    //remove unselected categories
                    var removedCategories = p.Categories.Where(c => !pr.Categories.Contains(c.CategoryId)).ToList();
                    foreach (var category in removedCategories)
                    {
                        p.Categories.Remove(new CategoryToProduct(){CategoryId = category.CategoryId,ProductId = p.Id});
                    }

                    var addedCategories = pr.Categories.Where(id => p.Categories.All(c => c.CategoryId != id))
                        .Select(id => new CategoryToProduct(){CategoryId = id,ProductId = pr.Id}).ToList();
                    foreach (var categoryToProduct in addedCategories)
                    {
                        p.Categories.Add(categoryToProduct);
                    }
                });
        }
    }
}