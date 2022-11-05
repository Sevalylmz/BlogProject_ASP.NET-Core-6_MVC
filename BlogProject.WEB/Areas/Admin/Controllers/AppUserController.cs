using AutoMapper;
using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.Models.Entities.Concrrete;
using BlogProject.Models.Enums;
using BlogProject.WEB.Areas.Admin.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]//bununla yetki ve izin mekanizmasının admin olduğunu söyledik.
    public class AppUserController:Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;


        public AppUserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAppUserRepository appUserRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment,IArticleRepository articleRepository,ICategoryRepository categoryRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appUserRepository = appUserRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
        }

        // Login sonrası Adminin Index sayfası
        public async Task<IActionResult> Index()
        {
            // İçerideki online kullanıcıyı getiriyor
            IdentityUser identityUser = await _userManager.GetUserAsync(User);

            // Girişi onaylanan kullanıcının AppUser tablosundaki ID si bulunur.
            AppUser user = _appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            if (user != null)
            {
                return View(user);
            }
            return Redirect("~/"); // Area sız başlangıç sayfasına yani Home/Index sayfasına yönlendirilir.
            // return RedirectToAction("Index","Home");            
        }

        // Logout. Kullanıcı çıkış işlemi sonucu Area sız başlangıç sayfasına yani Home/Index sayfasına yönlendirilir.
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
        // Detail 
        public async Task<IActionResult> Detail(int id)
        {
           
            AppUser appUser = _appUserRepository.GetDefault(a => a.ID == id);
            var user =await _userManager.FindByIdAsync(appUser.IdentityId);
            string mail =await _userManager.GetEmailAsync(user);

            List<Article> articles = _articleRepository.GetDefaults(a => a.Statu != Statu.Passive && a.AppUserID == appUser.ID);
            List<Category> userFollowedCategories = _categoryRepository.GetCategoryWithUser(appUser.ID);//componentteki takip edilen kategoriler
            AppUserProfileVM getProfileVM = new AppUserProfileVM()
            {
                FullName = appUser.FullName,
                Image = appUser.Image,
                MailAdress =mail,
                Articles = articles,
                Categories = userFollowedCategories,
            };


            return View(getProfileVM);
        }
        //List
        public IActionResult List()
        {
            List<AppUser> appUsers = _appUserRepository.GetDefaults(a=>a.Statu!=Statu.Passive);

            return View(appUsers);
        }

        //CheckList
        public IActionResult CheckList()
        {
            List<AppUser> appUsers = _appUserRepository.GetDefaults(a => a.AdminCheck == AdminCheck.Waiting && a.Statu == Statu.Passive);
           
            return View(appUsers);
        }
        //Admin onaylarsa Approve

        public IActionResult Approve(int id)
        {
            AppUser appUser=_appUserRepository.GetDefault(a=>a.ID== id);//benim id mile defaulttaki appuser id si eşit ise
            _appUserRepository.Approve(appUser);//onayla
            return RedirectToAction("CheckList");
        }

        //Admin Onay vermezse Reject

        public IActionResult Reject(int id)
        {
            AppUser appUser=_appUserRepository.GetDefault(a=>a.ID== id);
            _appUserRepository.Reject(appUser);
            return RedirectToAction("CheckList");
        }

    }
}
