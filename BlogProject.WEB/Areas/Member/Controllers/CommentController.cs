using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.Models.Entities.Concrrete;
using BlogProject.WEB.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WEB.Areas.Member.Controllers
{
    [Area("Member")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAppUserRepository _appUserRepository;

        public CommentController(ICommentRepository commentRepository, IArticleRepository articleRepository, UserManager<IdentityUser> userManager, IAppUserRepository appUserRepository)
        {
            _commentRepository = commentRepository;
            _articleRepository = articleRepository;
            _userManager = userManager;
            _appUserRepository = appUserRepository;

        }
        //  CREATE
        [HttpPost]
        public async Task<IActionResult> Create(ArticleDetailVM articleDetailVM)
        {
            Article article = _articleRepository.GetDefault(a => a.ID == articleDetailVM.ActiveArticleID);
          

            if (ModelState.IsValid)
            {


                // Yorumu yapan kullanıcı bulunur.

                IdentityUser identityUser = await _userManager.GetUserAsync(User);
                AppUser appUser = _appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

                // Vm içerisinden gerekli yerler alınarak Comment nesnesi oluşturulur.

              Comment comment = new Comment()
                {
                   
                    AppUserID = appUser.ID,                   
                    ArticleID =article.ID,
                    Text = articleDetailVM.CommentText
                };

                _commentRepository.Create(comment);
                      
            }
            else
            {
                TempData["Message"] = "Lüten Minimum karakter sayısından fazla yazınız";
            }            
            return RedirectToAction("Detail", "Article", new { id = article.ID });
        }
        //DELETE

        public async Task<IActionResult> Delete(int id)
        {
            Comment comment = _commentRepository.GetDefault(a => a.ID == id);
            _commentRepository.Delete(comment);
            return RedirectToAction("Detail", "Article", new { id = comment.ArticleID });
        }

        //UPDATE
        [HttpPost]
        public IActionResult Update(ArticleDetailVM vm)
        {
            Comment comment = _commentRepository.GetDefault(a => a.ID == vm.CommentID);
            comment.Text = vm.CommentText;
            _commentRepository.Update(comment);
            return RedirectToAction("Detail", "Article", new { id = comment.ArticleID });
        }
    }
}
