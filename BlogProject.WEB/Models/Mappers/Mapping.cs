using AutoMapper;
using BlogProject.Models.Entities.Concrrete;
using BlogProject.WEB.Areas.Member.Models.DTOs;
using BlogProject.WEB.Models.DTOs;

namespace BlogProject.WEB.Models.Mappers
{
    //mapleme bizi fazla kod satırından kurtarır.
    public class Mapping : Profile // AutoMapper kütüphanesinin Profile sınıfından kalıtım alarak otomatik map işlemi yapabilir.
    {
        // Constructor metot içerisinde Mapping atamaları yapılır. Bunun için CreateMap<TSource,TDestination> metodu kullanılır.
        public Mapping()
        {
            // ReverseMap() metodu kullanılarak işlemin her iki tarafa da yapılacağı söylenir. ReverseMap yazdığımız için Source ve Destination nesnelerinin yerleri değiştirilir. 
            // Auto mapping işlemi sırasında hariç tutulması istenen propertyler varsa DoNotValidate metodu ve lambda syntax kullanılarak bu işlem uygulanır
            CreateMap<AppUser, CreateUserDTO>().ReverseMap();

            CreateMap<CreateCategoryDTO,Category>().ReverseMap();

            CreateMap<Category,UpdateCategoryDTO>().ReverseMap();

            CreateMap<Article,ArticleCreateDTO>().ReverseMap();

            CreateMap<Article,ArticleUpdateDTO>().ReverseMap();

            CreateMap<AppUserUpdateDTO, AppUser>().ReverseMap();


        }
    }
}
