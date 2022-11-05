using BlogProject.DAL.Context;
using BlogProject.DAL.Repositories.Abstract;
using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.Models.Entities.Concrrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DAL.Repositories.Concrete
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository // Hem BaseRepository den hemde kendi Interfaceinden kalıtım alır.
    {
        public ArticleRepository(ProjectContext context) : base(context) // Kalıtım aldığı ata sınıfı ayağa kalkarken Context sınıfını giriş parametresi aldığı için buradada CTOR un giriş parametresine Context sınıfı verilir ve base anahtar kelimesiyle atasına gönderilir.
        {

        }
        public void Read(Article article)
        {
            article.ReadCounter++;//her tıklandığında bir artması için
            _table.Update(article);
            _context.SaveChanges();
        }
    }
}
