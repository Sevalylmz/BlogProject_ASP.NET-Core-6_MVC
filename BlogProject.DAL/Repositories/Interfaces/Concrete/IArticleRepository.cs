using BlogProject.DAL.Repositories.Interfaces.Abstract;
using BlogProject.Models.Entities.Concrrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DAL.Repositories.Interfaces.Concrete
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        void Read(Article article);//okunma sayısı için çünkü bunu base de tanımlamadığımız için burada belirttik
    }
       
    
}
