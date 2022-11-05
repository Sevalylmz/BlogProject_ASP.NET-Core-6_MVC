using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.Models.Entities.Concrrete;
using BlogProject.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController:Controller
    {
        private readonly IArticleRepository articleRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAppUserRepository appUserRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILikeRepository likeRepository;
        private readonly ICommentRepository commentRepository;

        public ArticleController(IArticleRepository articleRepository,UserManager<IdentityUser> userManager,IAppUserRepository appUserRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment, ILikeRepository likeRepository, ICommentRepository commentRepository)
        {
            this.articleRepository = articleRepository;
            this.userManager = userManager;
            this.appUserRepository = appUserRepository;
            this.categoryRepository = categoryRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.likeRepository = likeRepository;
            this.commentRepository = commentRepository;


        }

        //Lİst
        public async Task<IActionResult> List()
        {
 
            List<Article> articles = articleRepository.GetDefaults(a =>a.Statu != Statu.Passive);
 
            return View(articles);
        }
        //Detail
        public async Task<IActionResult> Detail(int id)
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);//online olanı bul
            AppUser activeUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
            ViewBag.ActiveUserID = activeUser.ID;
            Article article = articleRepository.GetDefault(a => a.ID == id);
            articleRepository.Read(article);
            return View(article);
        }
        //DELETE

        public async Task<IActionResult> Delete(int id)
        {
            Article article = articleRepository.GetDefault(a => a.ID == id);//içerden gelen articlın id si benim id me eşitse
            articleRepository.Delete(article);
            return RedirectToAction("List");
        }
        //CheckList

        public IActionResult CheckList()
        {
            List<Article> articles = articleRepository.GetDefaults(a => a.AdminCheck == AdminCheck.Waiting && a.Statu == Statu.Passive);

            return View(articles);
        }
        //Approve  //baserepo da tanımlandı metotlar

        public IActionResult Approve(int id)
        {
            Article article = articleRepository.GetDefault(a => a.ID == id);
            articleRepository.Approve(article);
            return RedirectToAction("CheckList");

        }

        //Reject  //baserepo da tanımlandı metotlar

        public IActionResult Reject(int id)
        {
            Article article=articleRepository.GetDefault(a => a.ID == id);
            articleRepository.Reject(article);
            return RedirectToAction("CheckList");
        }

        // Filter
        public IActionResult Filter(int id)
        {
            List<Article> allArticles = articleRepository.GetDefaults(a => a.Statu != Statu.Passive);
            List<Article> articles = new List<Article>();

            foreach (var item in allArticles)
            {
                if (item.ArticleCategories.Any(a => a.CategoryID == id)) { articles.Add(item); }
            }
            // Bütün makale listesi içerisinde KategoriID değeri id ye eşit olanlar boş bir listeye eklendi ve bu oluşturulan liste kullanıcıya gösterildi.
            return View(articles.Take(5).ToList());

        }

    }
}
