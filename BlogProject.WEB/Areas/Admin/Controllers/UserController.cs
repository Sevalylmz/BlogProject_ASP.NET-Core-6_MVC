using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.Models.Entities.Concrrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        
        // Bu controllerde kullanılacak olan Repositoryler ctor un giriş parametresi içerisinde çağrılır. SOLID in D prensibi gereği Constructor Injection yapılır. Program.cs içerisinde AddScope<InterfaceName,ClassName> şeklinde eklenmesi unutulmamalıdır.

        private readonly IAppUserRepository _appUserRepository;
        private readonly UserManager<IdentityUser> _userManager; // Microsoft.AspNetCore.Identity kütüphanesinin kullanıcı işlemleri için kullanılan Repositoryleri içeren sınıftır.

        private readonly IUserFollowedCategoryRepository categoryRepository;

        public UserController(IAppUserRepository appUserRepository, UserManager<IdentityUser> userManager, IUserFollowedCategoryRepository categoryRepository)
        {
            _appUserRepository = appUserRepository; // Kendi AppUser işlemlerimiz için Repositoryleri tutan interfacedir.
            _userManager = userManager; // Microsoft.AspNetCore.Identity kütüphanesinin kullanıcı işlemleri için kullanılan Repositoryleri içeren sınıftır.

            this.categoryRepository = categoryRepository;
        }
        public IActionResult Detail(int id)

        {

            AppUser appUser = _appUserRepository.GetDefault(a => a.ID == id);
            ViewBag.AllCategory = categoryRepository.GetUserFollowedCategories(a => a.AppUserID == id);

            return View(appUser);
        }
    }
}
