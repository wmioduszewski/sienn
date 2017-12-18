namespace SIENN.Services.Mapping
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
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

            CreateMap<Product, SiennProductResource>()
                .ForMember(pr => pr.ProductDescription, opt => opt.MapFrom(p => $"({p.Code}) {p.Description}"))
                .ForMember(pr => pr.Price, opt => opt.MapFrom(p => $"{p.Price:n2} zł"))
                .ForMember(pr => pr.IsAvailable, opt => opt.MapFrom(p => p.IsAvailable ? "Dostępny" : "Niedostępny"))
                .ForMember(pr => pr.DeliveryDate, opt => opt.MapFrom(p => p.DeliveryDate.ToString("dd.MM.yyyy")))
                .ForMember(pr => pr.CategoriesCount, opt => opt.MapFrom(p => p.Categories.Count))
                .ForMember(pr => pr.Type, opt => opt.MapFrom(p => $"({p.Type.Code}) {p.Type.Description}"))
                .ForMember(pr => pr.Unit, opt => opt.MapFrom(p => $"({p.Unit.Code}) {p.Unit.Description}"));

            //API to domain
            CreateMap<FilterResource, Filter>();
            CreateMap<TypeResource, Type>()
                .ForMember(t=>t.Id, opt=>opt.Ignore());
            CreateMap<UnitResource, Unit>()
                .ForMember(u => u.Id, opt => opt.Ignore());
            CreateMap<CategoryResource, Category>()
                .ForMember(c=>c.Products, opt => opt.Ignore());

            CreateMap<ProductResource, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p=>p.Categories, opt => opt.Ignore())
                .ForMember(p => p.Categories, opt => opt.Ignore())
                .AfterMap((pr, p) =>
                {
                    //remove unselected categories
                    var removedCategories = p.Categories.Where(c => !pr.Categories.Contains(c.CategoryId)).ToList();
                    foreach (var categoryToProduct in removedCategories)
                    {
                        p.Categories.Remove(categoryToProduct);
                    }

                    //add added categories
                    var addedCategories = pr.Categories.Where(id => p.Categories.All(c => c.CategoryId != id))
                        .Select(id => new CategoryToProduct() { CategoryId = id, ProductId = pr.Id }).ToList();
                    foreach (var categoryToProduct in addedCategories)
                    {
                        p.Categories.Add(categoryToProduct);
                    }
                });
        }
    }
}