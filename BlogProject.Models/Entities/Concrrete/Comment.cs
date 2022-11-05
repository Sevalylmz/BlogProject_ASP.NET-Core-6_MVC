using BlogProject.Models.Entities.Abstract;

namespace BlogProject.Models.Entities.Concrrete
{
    public class Comment : BaseEntity // Comment lerinde ID vb ortak özelliklerini alabilmek için BaseEntity den kalıtım alır.
    {
        public string Text { get; set; }

        // Navigation Property
        // 1 yorum 1 kişiye aittir. 1 kişinin birçok yorumu vardır.
        public int AppUserID { get; set; }
        public virtual AppUser AppUser { get; set; }

        // 1 yorum 1 makaleye aittir. 1 makalenin birçok yorumu vardır.
        public int ArticleID { get; set; }
        public virtual Article Article { get; set; }

    }
}