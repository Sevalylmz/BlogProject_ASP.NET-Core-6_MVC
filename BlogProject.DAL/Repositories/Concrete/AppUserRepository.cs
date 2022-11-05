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
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository // Hem BaseRepository den hemde kendi Interfaceinden kalıtım alır.
    {
        public AppUserRepository(ProjectContext context) : base(context) // Kalıtım aldığı ata sınıfı ayağa kalkarken Context sınıfını giriş parametresi aldığı için buradada CTOR un giriş parametresine Context sınıfı verilir ve base anahtar kelimesiyle atasına gönderilir.
        {
        }
    }
}
