using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.Models.Entities.Concrrete;
using BlogProject.WEB.Models;
using BlogProject.WEB.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogProject.WEB.Controllers
{
    public class HomeController : Controller
    {
        // Bu controllerde kullanılacak olan Repositoryler ctor un giriş parametresi içerisinde çağrılır. SOLID in D prensibi gereği Constructor Injection yapılır. Program.cs içerisinde AddScope<InterfaceName,ClassName> şeklinde eklenmesi unutulmamalıdır.

        private readonly UserManager<IdentityUser> _userManager; // Microsoft.AspNetCore.Identity kütüphanesinin kullanıcı doğrulama işlemleri için kullanılan Repositoryleri içeren sınıftır.
        private readonly SignInManager<IdentityUser> _signInManager; // Microsoft.AspNetCore.Identity kütüphanesinin kullanıcı giriş ve çıkış işlemleri için kullanılan Repositoryleri içeren sınıftır.
        private readonly IAppUserRepository _appUserRepository; //kullanıcı girmeye çalışınca appUser tablosunda aktif mi diye bakıcaz
      

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,IAppUserRepository appUserRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appUserRepository = appUserRepository;
     
        }


        // Login. Kullanıcı giriş işlemi
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dTO)
        {
            if (ModelState.IsValid) // Kullanıcı View form contoller i doğru doldurduysa
            {
                IdentityUser identityUser = await _userManager.FindByEmailAsync(dTO.Mail); // AspNetUsers tablosunda bu mail adresinde bu mail adresine sahip bir kullanıcı var mı ?
               
                bool isUserActive =_appUserRepository.Any(a=>a.IdentityId==identityUser.Id); // Kendi kullanıcı listemize bakıyor



                if (identityUser != null && isUserActive) // Böyle bir kullanıcı varsa ve statusü passive değilse
                {
                    await _signInManager.SignOutAsync(); // içeride hali hazırda kullanıcı varsa önce o kullanıcıyı çıkarsın
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(identityUser, dTO.Password, true, true);

                    if (result.Succeeded) // Kullanıcı giriş işlemi sonucu doğruysa
                    {
                        string role = (await _userManager.GetRolesAsync(identityUser)).FirstOrDefault(); // Kişinin rolünü getir
                        return RedirectToAction("Index", "AppUser", new { area = role }); // Kullanıcının rolüne göre area ya yönlendirilir.
                    }
                }
            }
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult About()
        {
            return View();
        }
    }
}