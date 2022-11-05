using BlogProject.DAL.Repositories.Interfaces.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WEB.Areas.Member.Views.Shared.Components.UserFollowCategory
{
    [ViewComponent(Name="UserFollowCategory")]
    public class UserFollowCategoryViewComponent : ViewComponent
    {
        //cookide olan kullanıcının takip ettiği categorileri gösterelim.

        private readonly ICategoryRepository categoryRepository;

        
        public UserFollowCategoryViewComponent(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke(int id)
        {
            var list = categoryRepository.GetCategoryWithUser(id);
            return View(list);
           
        }
    }
}
