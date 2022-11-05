using AutoMapper;
using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.Models.Entities.Concrrete;
using BlogProject.WEB.Areas.Member.Models.DTOs;
using BlogProject.WEB.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using BlogProject.Models.Enums;
using Microsoft.EntityFrameworkCore;
using BlogProject.WEB.Areas.Member.Models.DTOs;

namespace BlogProject.WEB.Areas.Member.Controllers
{
    [Area("Member")]

    // Bu controllerde kullanılacak olan Repositoryler ctor un giriş parametresi içerisinde çağrılır. SOLID in D prensibi gereği Constructor Injection yapılır. Program.cs içerisinde AddScope<InterfaceName,ClassName> şeklinde eklenmesi unutulmamalıdır.
    public class ArticleController : Controller
    {
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IArticleRepository articleRepository;
        private readonly IAppUserRepository appUserRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IArticleCategoryRepository articleCategoryRepository;
        private readonly ILikeRepository likeRepository;


        public ArticleController(UserManager<IdentityUser> userManager, IMapper mapper, IArticleRepository articleRepository, IAppUserRepository appUserRepository, ICategoryRepository categoryRepository, IArticleCategoryRepository articleCategoryRepository, ILikeRepository likeRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.articleRepository = articleRepository;
            this.appUserRepository = appUserRepository;
            this.categoryRepository = categoryRepository;
            this.articleCategoryRepository = articleCategoryRepository;
            this.likeRepository = likeRepository;
        }

        //CREATE

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);//online olanı bul
            AppUser user = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            ArticleCreateDTO dto = new ArticleCreateDTO()//articlecreatedto gelsin istiyorum 
            {
                Categories = categoryRepository.GetByDefaults//categorileri getcategori tipinden döneceğim için getbydefaults tipini kuklandık get category tipini istememdeki neden içinden ıd ve name ini istedğim için getcategorydto oluşturdum
                (
                     selector: a => new GetCategoryDTO
                     {
                         ID = a.ID,
                         Name = a.Name
                     },
                     expression: a => a.Statu != BlogProject.Models.Enums.Statu.Passive

                ),
                AppUserID = user.ID//geri kalan bilgileri buradan öğrenicem
            };
            return View(dto);
        }
        [HttpPost]
        public IActionResult Create(ArticleCreateDTO dto)
        {
            if (ModelState.IsValid) //validasyon tamamsa
            {
                //sqldeki eklemeye doğru gitmeye gidiyorum
                var article = mapper.Map<Article>(dto);//varış noktamız article bize dto veriyor
                if (article.ImagePath != null) //fotoğraf alınabildiyse
                {
                    // Using anahtar kelimesi ile tanımlanan değişkenler işleri bittikten sonra ramdan kalkarlar.
                    using var image = Image.Load(dto.ImagePath.OpenReadStream());
                    image.Mutate(a => a.Resize(200, 200)); // Şekillendirme ve boyutlandırma işlemleri
                                                           // Fotoğraf kayıdı için dosya yolu söylenir. Dosya adı olarak kullanıcıAdı.jpg olarak kayıt edecek

                    Guid guid = Guid.NewGuid();//makalenin şuan eşşiz bir datası yok yani kayıt işlemi yapılmadığı için sql bize ıd v ermedi bizim  bir ıd vermemiz lazım.bunu da guid ıd ile yapıyorum

                    image.Save($"wwwroot/images/{guid}.jpg");
                    // Veri tabanındaki Article tablosunun dosya yolu kolonuna da eklenen resmin dosya yolu kayıt edilir.
                    article.Image = ($"/images/{guid}.jpg");

                    // veritanabına dosya yolu kaydediyor
                    foreach (var item in dto.CategoriesID)
                    {
                        article.ArticleCategories.Add(new ArticleCategory { ArticleID = article.ID, Article = article, CategoryID = item });
                    }
                    articleRepository.Create(article);

                    return RedirectToAction("List"); // İlk kez kayıt olan kullanıcıyı login sayfasına yönlendir.
                }
            }
            //ToDo= validasyonu geçemezse categories patlamasın diye listeyi doldurup return view alalım!
            return View(dto);
        }

        //Lİst
        public async Task<IActionResult> List()
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);//online olanı bul
            AppUser user = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);


            List<Article> articles = articleRepository.GetDefaults(a => a.AppUserID == user.ID && a.Statu == Statu.Active).OrderByDescending(a=>a.CreateDate).ToList();
            
            return View(articles);
            
        }

        //UPDATE

        public IActionResult Update(int id)
        {
            Article article = articleRepository.GetDefault(a => a.ID == id);//içerden gelenarticleın id si benim id me eşitse 
            var updatedArticle = mapper.Map<ArticleUpdateDTO>(article);//mapping sınıfına git ekle

            //getcategorydto yu dolduramaz bizim yaomamız lazım

            updatedArticle.Categories = categoryRepository.GetByDefaults

                (
                     selector: a => new GetCategoryDTO
                     {
                         ID = a.ID,
                         Name = a.Name
                     },
                     expression: a => a.Statu != Statu.Passive

                );
            return View(updatedArticle);



        }

        [HttpPost]

        public IActionResult Update(ArticleUpdateDTO dto)
        {
            if (ModelState.IsValid)
            {
                var article = mapper.Map<Article>(dto);
                if (article.ImagePath != null) //fotoğraf alınabildiyse
                {
                    // Using anahtar kelimesi ile tanımlanan değişkenler işleri bittikten sonra ramdan kalkarlar.
                    using var image = Image.Load(dto.ImagePath.OpenReadStream());
                    image.Mutate(a => a.Resize(150, 150)); // Şekillendirme ve boyutlandırma işlemleri
                                                           // Fotoğraf kayıdı için dosya yolu söylenir. Dosya adı olarak kullanıcıAdı.jpg olarak kayıt edecek

                    Guid guid = Guid.NewGuid();//makalenin şuan eşşiz bir datası yok yani kayıt işlemi yapılmadığı için sql bize ıd v ermedi bizim  bir ıd vermemiz lazım.bunu da guid ıd ile yapıyorum

                    image.Save($"wwwroot/images/{guid}.jpg");
                    // Veri tabanındaki Article tablosunun dosya yolu kolonuna da eklenen resmin dosya yolu kayıt edilir.
                    article.Image = ($"/images/users/{guid}.jpg");

                    // veritanabına dosya yolu kaydediyor

                    articleRepository.Update(article);
                    return RedirectToAction("List"); // İlk kez kayıt olan kullanıcıyı login sayfasına yönlendir.
                    //ToDo: photo güncellendiğinde eski fotoları uçuralım
                }
            }
            return View(dto);
        }
        //DELETE

        public async Task<IActionResult> Delete(int id)
        {
            Article article = articleRepository.GetDefault(a => a.ID == id);//içerden gelen articlın id si benim id me eşitse
            articleRepository.Delete(article);
            return RedirectToAction("List");
        }

        //Detail
        public async Task<IActionResult> Detail(int id)
        {
            // Article sayfasında da profil bilgileri gösterileceği için ArticleDetailVM isimli VM kullanılır

            Article article = articleRepository.GetDefault(a => a.ID == id);
           
            string mail = userManager.Users.FirstOrDefault(a => a.Id == article.AppUser.IdentityId).Email;
            IdentityUser identityUser = await userManager.GetUserAsync(User); // Aktif olan kullanıcı
            ViewBag.ActiveAppUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
            ArticleDetailVM articleDetailVM = new ArticleDetailVM()
            {
                Article = article,                
                Mail = mail,
            };
            articleRepository.Read(article);

            return View(articleDetailVM);
        }

        // Filter
        public IActionResult Filter(int id)
        {
            List<Article> allArticles = articleRepository.GetDefaults(a => a.Statu != Statu.Passive);
            List<Article> articles = new List<Article>();

            foreach (var item in allArticles)
            {
                if (item.ArticleCategories.Any(a => a.CategoryID == id)) { articles.Add(item); }
            }
            // Bütün makale listesi içerisinde KategoriID değeri id ye eşit olanlar boş bir listeye eklendi ve bu oluşturulan liste kullanıcıya gösterildi.
            return View(articles.Take(5).ToList());

        }

        // Like
        public async Task<IActionResult> Like(int id)
        {
            Article article = articleRepository.GetDefault(a => a.ID == id);//içerden gelen articlın id si benim id me eşitse

            IdentityUser identityUser = await userManager.GetUserAsync(User);//online olanı bul

            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);//aktif olan kullanıcı


            Like like = new Like()//like içi dolu gelir
            {
                AppUser = appUser,
                AppUserID = appUser.ID,
                Article = article,
                ArticleID = article.ID
            };

            likeRepository.Create(like);

            return RedirectToAction("Detail", new
            {
                id = id
            }); // Tekrar makalenşn okunmas sayfasına gelir
        }

        //UnLike
        public async Task<IActionResult> Unlike(int id)
        {
            Article article = articleRepository.GetDefault(a => a.ID == id);

            IdentityUser identityUser = await userManager.GetUserAsync(User);

            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            Like like = likeRepository.GetDefault(a => a.ArticleID == article.ID && a.AppUserID == appUser.ID);//like lanan article ile o like ın appuser id si eşitse

            likeRepository.Delete(like);//like ı sil ve
            return RedirectToAction("Detail", new { id = id });//tekrar makalenin okunma sayfasına gel
        }

    }
}
