using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.Models.Entities.Concrrete;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Models.Enums;

namespace BlogProject.WEB.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleRepository articleRepository;
        private readonly ICommentRepository commentRepository;

        public ArticleController(IArticleRepository articleRepository, ICommentRepository commentRepository)
        {
            this.articleRepository = articleRepository;
            this.commentRepository = commentRepository;
        }
        public async Task<IActionResult> Detail(int id)
        {
            Article article = articleRepository.GetDefault(a => a.ID == id);
            articleRepository.Read(article);
            return View(article);
        }

        // Filter
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
