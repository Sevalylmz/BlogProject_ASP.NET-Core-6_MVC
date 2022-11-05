using BlogProject.Models.Entities.Concrrete;

namespace BlogProject.WEB.Areas.Member.Models.VMs
{
    public class GetArticleVM
    {
        public int ArticleID { get; set; }//listeleme sayfasında update-delete giderken gerekli
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string UserFullName { get; set; }//oluşturanın tam adı
        public List<ArticleCategory> ArticleCategories { get; set; }//bir çok kategori olacağı için list yapısında tuttum.
    }
}
