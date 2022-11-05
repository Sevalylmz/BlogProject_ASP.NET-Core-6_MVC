using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.WEB.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Models.Enums;
using Microsoft.EntityFrameworkCore;
using BlogProject.Models.Entities.Concrrete;

namespace BlogProject.WEB.Views.Shared.Components.Articles
{
    [ViewComponent(Name = "Articles")]
    public class ArticlesViewComponent : ViewComponent
    {
        private readonly IArticleRepository articleRepository;
        private readonly ICategoryRepository categoryRepository;

        //componetleri ınvoke ile çağrıyoruz

        //oluşma tarihine göre güncel on makaleyi göstericez
        public ArticlesViewComponent(IArticleRepository articleRepository,ICategoryRepository categoryRepository)
        {
            this.articleRepository = articleRepository;
            this.categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {

            List<Article> articles = articleRepository.GetDefaults(a => a.Statu == Statu.Active);
            @ViewBag.AllCategory = categoryRepository.GetDefaults(a => a.Statu != Statu.Passive);
            return View(articles.Take(10).ToList());
        }
    }
}
