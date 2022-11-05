using AutoMapper;
using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.Models.Entities.Concrrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Models.Enums;
using BlogProject.WEB.Areas.Admin.Models.DTOs;

namespace BlogProject.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAppUserRepository appUserRepository;
        private readonly IUserFollowedCategoryRepository userFollowedCategoryRepository;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper, UserManager<IdentityUser> userManager, IAppUserRepository appUserRepository, IUserFollowedCategoryRepository userFollowedCategoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.userManager = userManager;
            this.appUserRepository = appUserRepository;
            this.userFollowedCategoryRepository = userFollowedCategoryRepository;
        }

        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDTO dto)
        {
            Category category = new Category() { Name = dto.Name, Description = dto.Description };
            categoryRepository.Create(category);
            return RedirectToAction("List");
        }
        
        //LİST
        public IActionResult List()
        {
            List<Category> model =categoryRepository.GetDefaults(a=>a.Statu!=Statu.Passive);
            return View(model);
        }
        //UPDATE

        [HttpGet]
        public IActionResult Update(int id)
        {
            Category category = categoryRepository.GetDefault(a => a.ID == id);
           
            //<...>içerisindeki benim varış noktam ve ben elimde dolu bir dto m olsun istediğim için categoryDTO yu varış noktam olarak seçiyorum(destinition),(...) benim de başlangıç noktam o da categoriden başlıyorum....
            //şimdi  bu maplemeyi mappin altına eklememiz lazım

            return View(category);
        }
        [HttpPost]

        public IActionResult Update(UpdateCategoryDTO dto)
        {
            if (ModelState.IsValid) //validasyon kontrollerini geçti ise
            {
                Category updatedCategory = categoryRepository.GetDefault(a => a.ID == dto.ID);//ben ona dto vericem o bana Category verecek
                updatedCategory.Name=dto.Name;
                updatedCategory.Description=dto.Description;
                categoryRepository.Update(updatedCategory);
                return RedirectToAction("List");
            }
            return View(dto);
        }
        //DELETE

        public IActionResult Delete(int id)
        {
            Category category=categoryRepository.GetDefault(a => a.ID == id);
            categoryRepository.Delete(category);
            return RedirectToAction("List");

        }

        //CHECKLİST

        public IActionResult CheckList()
        {
            List<Category> categories = categoryRepository.GetDefaults(a => a.Statu == Statu.Passive && a.AdminCheck == AdminCheck.Waiting);
            return View(categories);
        }

        //APPROVE
        public IActionResult Approve(int id)
        {
            Category category=categoryRepository.GetDefault(a=>a.ID == id);
            categoryRepository.Approve(category);
            return RedirectToAction("CheckList");
        }

        //REJECT

        public IActionResult Reject(int id)
        {
            Category category=categoryRepository.GetDefault(a=>a.ID==id);
            categoryRepository.Reject(category);
            return RedirectToAction("List");
        }



    }
}
