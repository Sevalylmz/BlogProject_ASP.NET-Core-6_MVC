using BlogProject.Models.Entities.Abstract;

namespace BlogProject.Models.Entities.Concrrete
{
    public class Category : BaseEntity // Categorylerinde ID vb ortak özelliklerini alabilmek için BaseEntity den kalıtım alır.
    {
        // Class içerisindeki list yapıları kullanılabilmesi için List yapıları ctor içerisinde instance edilir.
        public Category()
        {
            ArticleCategories = new List<ArticleCategory>();
            UserFollowedCategories = new List<UserFollowedCategory>();
        }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation Property

        // 1 kategorinin in çokça makalesi olabilir. 1 makalenin çokça kategorisi olabilir. Çoka çok ilişki
        public  virtual List<ArticleCategory> ArticleCategories { get; set; }

        // 1 kategorinin nin çokça takip edeni olabilir. 1 kullanıcının çokça takip ettiği kategori olabilir. Ara tablo kullanılacaktır.
        public virtual List<UserFollowedCategory> UserFollowedCategories { get; set; }
    }
}