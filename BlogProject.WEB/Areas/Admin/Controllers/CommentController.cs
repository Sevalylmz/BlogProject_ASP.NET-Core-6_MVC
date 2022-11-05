using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.Models.Entities.Concrrete;
using BlogProject.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository commentRepository;

        
       

        public CommentController(ICommentRepository commentRepository )
        {
            this.commentRepository = commentRepository;

        }
        //CHECKLİST

        public IActionResult CheckList()
        {
            List<Comment> comments = commentRepository.GetDefaults(a => a.Statu == Statu.Passive && a.AdminCheck == AdminCheck.Waiting);
            return View(comments);
        }

        //DELETE
        public async Task<IActionResult> Delete(int id)
        {
            Comment comment = commentRepository.GetDefault(a => a.ID == id);
            commentRepository.Delete(comment);
            return RedirectToAction("Detail", "Article", new { id = comment.ArticleID });
        }

        //APPROVE
        public IActionResult Approve(int id)
        {
            Comment comment = commentRepository.GetDefault(a => a.ID == id);
            commentRepository.Approve(comment);
            return RedirectToAction("CheckList");
        }

        //REJECT

        public IActionResult Reject(int id)
        {
            Comment comment = commentRepository.GetDefault(a => a.ID == id);
            commentRepository.Reject(comment);
            return RedirectToAction("CheckList");
        }

        // List
        public IActionResult List()
        {
            List<Comment> comments = commentRepository.GetDefaults(a => a.Statu != Statu.Passive);
            return View(comments);
        }

    }
}
