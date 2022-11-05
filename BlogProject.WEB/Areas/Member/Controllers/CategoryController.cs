using AutoMapper;
using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.Models.Entities.Concrrete;
using BlogProject.Models.Enums;
using BlogProject.WEB.Areas.Member.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WEB.Areas.Member.Controllers
{
    [Area("Member")] //hangi controller olduğunu belli etmemiz için başına route nu eklemeliyiz yoksa yollarını şaşırır.
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAppUserRepository appUserRepository;
        private readonly IUserFollowedCategoryRepository userFollowedCategoryRepository;

        public CategoryController(ICategoryRepository categoryRepository,IMapper mapper,UserManager<IdentityUser> userManager,IAppUserRepository appUserRepository,IUserFollowedCategoryRepository userFollowedCategoryRepository)//sonra globale taşı...usermanageri sonra ekledik
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.userManager = userManager;
            this.appUserRepository = appUserRepository;
            this.userFollowedCategoryRepository = userFollowedCategoryRepository;
        }

        //CREATE

        [HttpGet]
        public IActionResult Create()
        { 
            return View(); 
        }
       

        [HttpPost]
        public IActionResult Create(CreateCategoryDTO dto)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category();
                category.Name = dto.Name;
                category.Description = dto.Description; 
                categoryRepository.Create(category);

                return RedirectToAction("List");
            }
            return View(dto);
        }
        //List

        //[HttpGet] //geti kullanınca hata veriyor
        public async Task<IActionResult> List()
        {
            IdentityUser ıdentityUser = await userManager.GetUserAsync(User); //aktif oaln kullanıcı bulunur

            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == ıdentityUser.Id);//aktif olan kullanıcı

            //Aktif olan kullanıcının takip ettiği userfollowedkategorileri list halinde tuttuk
            List<UserFollowedCategory> userFollowedCategories = userFollowedCategoryRepository.GetUserFollowedCategories(a => a.AppUserID == appUser.ID);

            //oluşturulan listeyi viewbag içine attık
            ViewBag.list = userFollowedCategories;

            List<Category> list = categoryRepository.GetDefaults(a => a.Statu != Statu.Passive);
            return View(list);
        }

        //UPDATE

        [HttpGet]
        public IActionResult Update(int id)
        {
            Category category = categoryRepository.GetDefault(a => a.ID == id);
            var updateCategory=mapper.Map<UpdateCategoryDTO>(category);
            //<...>içerisindeki benim varış noktam ve ben elimde dolu bir dto m olsun istediğim için categoryDTO yu varış noktam olarak seçiyorum(destinition),(...) benim de başlangıç noktam o da categoriden başlıyorum....
            //şimdi  bu maplemeyi mappin altına eklememiz lazım

            return View(updateCategory);
        }
        [HttpPost]

        public IActionResult Update(UpdateCategoryDTO dto)
        {
            if (ModelState.IsValid) //validasyon kontrollerini geçti ise
            {
                var category = mapper.Map<Category>(dto);//ben ona dto vericem o bana Category verecek
                categoryRepository.Update(category);
                return RedirectToAction("List");
            }
            return View(dto);
        }


        //Follow
        public async Task<IActionResult> Follow(int id)
        {
            //Takip etmek istediğimiz categoryi getirir
            Category category=categoryRepository.GetDefault(a => a.ID == id);

            //Aktif olan kullanıcıyı buluruz
            IdentityUser ıdentityUser =await userManager.GetUserAsync(User);

            //Hangi kullanıcı aktifse onun IdentityID si ile AppUser tablosundaki kullanıcıya ulaşırız.
            AppUser appUser =appUserRepository.GetDefault(a => a.IdentityId == ıdentityUser.Id);

            //categori nesnesi içerisinde List<Userfollewedcategory> var ve biz bunu newleyerek içine sahip  olduğu özellikleri ekliyoruz

            category.UserFollowedCategories.Add(new UserFollowedCategory
            {
                Category = category,
                CategoryID = category.ID,
                AppUser = appUser,
                AppUserID = appUser.ID
            });
            categoryRepository.Update(category);
            return RedirectToAction("List");

        }

        //UnFollow

        public async Task<IActionResult> UnFollow(int id)
        {
            //Takipten çıkamak istediğimiz categoriyi getirir
            Category category= categoryRepository.GetDefault(a => a.ID == id);

            //Aktif olan kullanıcyı Identity kütüphanesiyle buluruz
            IdentityUser ıdentityUser=await userManager.GetUserAsync(User);

            //Hangi kullanıcı aktifse onun IdentityID si ile AppUser tablosundaki kullanıcıya ulaşırız.
            AppUser appUser =appUserRepository.GetDefault(a => a.IdentityId == ıdentityUser.Id);

            UserFollowedCategory userFollowedCategory=userFollowedCategoryRepository.GetDefault(a => a.CategoryID == category.ID && a.AppUserID == appUser.ID);

            userFollowedCategoryRepository.Delete(userFollowedCategory);
            return RedirectToAction("List");




        }

      
    }
}
