using AutoMapper;
using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.Models.Entities.Concrrete;
using BlogProject.WEB.Areas.Member.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using BlogProject.Models.Enums;
using BlogProject.WEB.Areas.Member.Models.VMs;

namespace BlogProject.WEB.Areas.Member.Controllers
{
    [Area("Member")] // Member Area nın controlleri olduğu belirtilir yazılmadığı taktirde controller isimleri çakışırsa multiendpoint hatası verir.
                     
    public class AppUserController : Controller
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

        // Login sonrası kayıtlı kullanıcının Index sayfası
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

        public async Task<IActionResult> Update()
        {
            IdentityUser identityUser = await _userManager.GetUserAsync(User);
            AppUser user = _appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
            AppUserUpdateDTO updateAppUserDTO = _mapper.Map<AppUserUpdateDTO>(user);
            updateAppUserDTO.Mail = identityUser.Email;
            updateAppUserDTO.oldImage = user.Image;
            updateAppUserDTO.oldPassword = user.Password;
            updateAppUserDTO.Password1 = user.Password1;
            updateAppUserDTO.Password2 = user.Password2;
            updateAppUserDTO.Password3 = user.Password3;
            return View(updateAppUserDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateDTO dto)
        {
            if (ModelState.IsValid)
            {

                string oldPassword = dto.oldPassword;
                string oldImage = dto.oldImage;
                List<string> oldPasswordList = new List<string>() { dto.Password1,dto.Password2,dto.Password3 };
                AppUser appUser = _mapper.Map<AppUser>(dto);
                 
                IdentityUser identityUser = await _userManager.FindByIdAsync(dto.IdentityID);

                if (identityUser != null)
                {
                    var passwordResult = oldPasswordList.Contains(dto.Password);
                    if (passwordResult)
                    {
                        return View(dto);
                    }
                     
                    identityUser.Email = dto.Mail;
                    identityUser.UserName = appUser.UserName;

                    await _userManager.ChangePasswordAsync(identityUser, oldPassword, appUser.Password);
                    IdentityResult result = await _userManager.UpdateAsync(identityUser);

                    if (result.Succeeded)
                    {
                        if (appUser.ImagePath != null)
                        {
                            // Kullanıcı resim seçtiyse eski resim silinir.

                            string imageName = oldImage + ".jpg"; // Resmin adı bulunur

                            string deletedImage = Path.Combine(_webHostEnvironment.WebRootPath, "images", "users", $"{imageName}"); // Dosya yolu oluşturulur.

                            if (System.IO.File.Exists(deletedImage))
                            {
                                System.IO.File.Delete(deletedImage); // Eğer dosya varsa silinir.
                            }

                            // Using anahtar kelimesi ile tanımlanan değişkenler işleri bittikten sonra ramdan kalkarlar.
                            using var image = Image.Load(dto.ImagePath.OpenReadStream());
                            image.Mutate(a => a.Resize(1000, 1000)); // Şekillendirme ve boyutlandırma işlemleri

                            image.Save($"wwwroot/images/users/{appUser.UserName}.jpg");

                            //Veri tabanındaki AppUser tablosunun dosya yolu kolonuna da eklenen resmin dosya yolu kayıt edilir.
                            appUser.Image = ($"/images/users/{appUser.UserName}.jpg");

                        }
                        appUser.Password3 = appUser.Password2;
                        appUser.Password2 = appUser.Password1;
                        appUser.Password1 = appUser.Password;//kullanıcı şifre değiştirdiği için bir önceki şifre arkada tutulacak

                        _appUserRepository.Update(appUser);
                    }
                }

                return RedirectToAction("Index");
            }
            return View(dto);
        }

        // Detail 
        public async Task<IActionResult> Detail()
        {

            IdentityUser identityUser =await _userManager.GetUserAsync(User);//içerdeki online kullanıcı
            AppUser appUser = _appUserRepository.GetDefault(a=>a.IdentityId==identityUser.Id);//girişi onaylanan kullanıcı

            return View(appUser);
        }

        

        //Delete
        public async Task<IActionResult> Delete()
        {
            // İçerideki online kullanıcıyı getiriyor
            IdentityUser identityUser = await _userManager.GetUserAsync(User);

            // Girişi onaylanan kullanıcının AppUser tablosundaki ID si bulunur.
            AppUser appUser = _appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
            _appUserRepository.Delete(appUser); // Statu passive olarak ayarlanır. Giriş yapamaz

            return Redirect("~/");

            //viem update te var 
        }

        //About

        public IActionResult About()
        {
            return View();
        }
      
    }
}
