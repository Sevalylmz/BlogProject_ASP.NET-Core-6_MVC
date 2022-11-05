using BlogProject.Models.Entities.Concrrete;

namespace BlogProject.WEB.Areas.Member.Models.VMs
{
    public class AppUserProfileVM
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string MailAdress { get; set; }
        public string Image { get; set; }

        //Article için
        public List<Article> Articles { get; set; }

        //Category için

        public List<Category> Categories { get; set; }
    }
}
