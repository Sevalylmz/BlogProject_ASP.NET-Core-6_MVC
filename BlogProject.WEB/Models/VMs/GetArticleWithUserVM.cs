using BlogProject.Models.Entities.Concrrete;

namespace BlogProject.WEB.Models.VMs
{
    public class GetArticleWithUserVM
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ArticleCategory> ArticleCategories { get; set; } //bir çok kategori olacağı için kategorsini list yapısında tuttum
        public int UserId { get; set; }//userfullname adına tıklarsa bu userıd kişiye git demek için yazdık
        public string UserFullName { get; set; }
        public string Image { get; set; }
        public int ArticleId { get; set; }
    }
}
