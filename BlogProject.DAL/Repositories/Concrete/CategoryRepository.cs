using BlogProject.DAL.Context;
using BlogProject.DAL.Repositories.Abstract;
using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.Models.Entities.Concrrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DAL.Repositories.Concrete
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository // Hem BaseRepository den hemde kendi Interfaceinden kalıtım alır.
    {
        public CategoryRepository(ProjectContext context) : base(context) // Kalıtım aldığı ata sınıfı ayağa kalkarken Context sınıfını giriş parametresi aldığı için buradada CTOR un giriş parametresine Context sınıfı verilir ve base anahtar kelimesiyle atasına gönderilir.
        {
        }
        //kullanıcını takip ettiği categorileri alan componentte kullanacağımız metot

        public List<Category> GetCategoryWithUser(int id)
        {
            return _context.UserFollowedCategories.Include(a => a.AppUser).Include(a => a.Category).Where(a => a.AppUserID==id).Select(a=>a.Category).ToList();
        }
    }
}
