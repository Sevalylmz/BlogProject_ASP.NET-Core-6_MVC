--------- BLOG PROJESI ----------

1 - Proje Blank Solution olarak açýlýr.

2 - Solution altýna ilk olarak entitylerin tutulacaðý Class Library projesi SolutionName.Models ismiyle oluþturulur.

3 - Models katmaný içerisine Entities klasörü onun altýna da Abstract ve Concrete isimli klasörlerimiz oluþturulur. Uygulamada kullanýlacak olan soyut ve temel entity classlarý oluþturulur.

4 - Models katmaný altýna Enums klasörü açýlýr. Gerekli olacak Enumlar burada tutulur.

5 - Models katmaný altýna EntityTypeConfiguration isimli klasör açýlýr. Açýlan bu klasörün altýna yine Abstract ve Concrete isimli klasörlerde oluþturulur. Bu klasörlerde soyut ve temel entityler için validation configuration (veritabaný iliþkileri / tablo - kolon özellikleri) larýn yazýlacaðý sýnýflar oluþturulur.

6 - Models => Entities => Abstract klasörü altýna BaseEntity isimli public Abstract türünde class oluþturulur. Bu classýn içerisinde propertyler tanýmlanýr.

NOT 1 =>  Database tablolarýnda bir tablonun kolonlarý sadece diðer tablolardan gelen Foreign Key lerden oluþuyorsa o tablo için ayrýyeten bir PRIMARY ID kolonuna ihtiyaç yoktur. Eðer o tablo kendine has en az 1 adet kolonu varsa o tablo için PRIMARY KEY kolonuna ihtiyaç vardýr.

7 - Models => Enums altýna Statu isimli public Enum tipinde Enum oluþturulur.

8 - Models => Entities => Concrete klasörü altýna AppUser isimli public class oluþturulur. BaseEntity classýndan kalýtým alýr. Burada dosya iþlemlerini kullanmak için NuGet Package Manager dan Microsoft.AspNetCore.Http.Features kütüphanesi kurulmalýdýr.

9 - Models => Entities => Concrete klasörü altýna Article isimli public class oluþturulur. BaseEntity classýndan kalýtým alýr. Burada dosya iþlemlerini kullanmak için NuGet Package Manager dan Microsoft.AspNetCore.Http.Features kütüphanesi kurulmalýdýr.

10 - Models => Entities => Concrete klasörü altýna Comment isimli public class oluþturulur. BaseEntity classýndan kalýtým alýr.

11 - Models => Entities => Concrete klasörü altýna Like isimli public class oluþturulur. BaseEntity classýndan kalýtým ALMAZ!!! ID almadýðý için AppUserID ve ArticleID composite key olacaktýr. Yani tekrarlar yapýlamayacaktýr. Ayrýca bu tablo sadece diðer tablolardan aldýðý Foreing Key lerden ile oluþtuðu için yani kendine has bir kolonu olmadýðý için Primary Key kolonuna ihtiyaç duymaz.

12 - Models => Entities => Concrete klasörü altýna UserFollowedCategory isimli ara tablo olan public class oluþturulur. BaseEntity classýndan kalýtým ALMAZ!!! ID almadýðý için AppUserID ve CategoryID composite key olacaktýr. Yani tekrarlar yapýlamayacaktýr. Ara tablolarda ayrýyeten bir PRIMARY KEY tanýmlanmasýna gerek yoktur. Ayrýca bu tablo sadece diðer tablolardan aldýðý Foreing Key lerden ile oluþtuðu için yani kendine has bir kolonu olmadýðý için Primary Key kolonuna ihtiyaç duymaz.

13 - Models => Entities => Concrete klasörü altýna Category isimli public class oluþturulur. BaseEntity classýndan kalýtým alýr. 

14 - Models => EntityTypeConfiguration => Abstract klasörü altýna BaseMap isimli public Abstract türünde class oluþturulur. Bu classýn içerisinde propertyler için configurationlar tanýmlanýr. Diðer classlara kalýtým verirken kullanmak için GenericType<T> tipinde ve IEntityTypeConfiguration<T> interface inden kalýtým alýr. Bu interface i kullnamk için EF.Core ve EF.Core.SqlServer kütüphaneleri NuGet Package Managerdan kurulur.

NOT 2 => Hernagi bir database nesnesi oluþturulurken diðer nesnelere olan baðýmlýlýklar göz önüne alýnarak oluþturulmalýdýr. Bu projede herhangi bir makale nesnesi oluþturabilmek için Kullanýcý ve kategori olmalýdýr. Bu durumdan dolayý diðer tablolara baðýmlý olmayan kategori yada kullanýcý nesneleri ilk olarak oluþturulabilir.

15 - Models => EntityTypeConfiguration => Concrete klasörü altýna Concrete entitiy lerin propertylerinin configurationlarýný tanýmlamak için o EntityNameMap adýnda public türde classlar açýlýr. Açýlan bu classlar ayný Entity kýsmýnda olduðu gibi kalýtým alanlar için BaseMap classýndan kalýtým alýrlar. BaseMapten kalýtým almayanlar IEntityTypeConfiguration<EntityName> þeklinde kalýtým alýrlar. 

NOT 3 => Primary key almadan oluþturulacak olan entity ler için tanýmlanacak olan EntityTypeConfiguration class larýnýn içinde builder.HasKey(a => new { a.ForeignKey, a.ForeignKey }); tanýmlanmasý gerekmektedir. Bu tanýmlanma yapýlmazsa migration iþleminde relationship hatasý alýnacaktýr.

NOT 4 => Navigation propertyler entity içerisinde tanýmlanýrken iliþkili olan bütün propertylerde tanýmlanmasý gerekmektedir. Lakin bu navigation propertyler configuration sýnýfýnda açýklanýrken iliþki tablolarýn sadece birinde açýklanmasý yeterlidir.

16 - Kullanýcý oluþtururken o kullanýcýnýn Rollerinin de atanabilmesi için Identity kütüphanesi öncesinde oluþtrulmalýdýr. Bunun için database ayaða kalkerken Roller hazýr olarak gelmelidir. Models => EntityTypeConfiguration => Concrete altýna IdentityRoleMap public classý oluþturulur. Bu class IEntityTypeConfiguration<IdentityRole> þeklinde kalýtým alýr. Burada IdentityRole sýnýfý Microsoft.AspNetCore.Identity kütüphanesinden gelen hazýr sýnýftýr. Burada seed data olarak Rolleri hazýr olarak getireceðiz. Bunun için Roller, Configure metodu içerisinde tanýmlanýr. Member ve Admin isimli sadece 2 adet Rol vardýr.

*************************************

17 - DataAccessLayer olarak adlandýrýlan Repositoryler ve Context sýnýfýný içeren katman oluþturulur.

18 - DataAccess katmaný oluþturmak için Solution altýna Class Library projesi SolutionName.DAL ismiyle oluþturulur.

19 - SolutionName.DAL altýna Repositories klasörü oluþturulur. Oluþturulan bu klasör altýna Abstract, Concrete ve Interfaces isimli alt klasörler oluþturulur. Abstract ve Concrete klasörleri altýnda ise bu metot imzalarýnýn gövdeleri yazýlacaktýr.

20 - Interfaces klasörünün altýna da Abstract ve Concrete diye 2 klasör açýlýr. Interface klasörünün alt klasörleri içerisinde metotlarýn imzalarýný içeren Interfaceler tanýmlanacaktýr

20 - SolutionName.DAL altýna Context klasörü oluþturulur. Bu klasör içerisinde database baðlantýsýný saðlayacak olan Context sýnýfý oluþturulacaktýr. public tipinde ProjectContext isimli class oluþturulur. Bu sýnýf IdentityDbContext sýnýfýndan kalýtým alýr. Bu kalýtýmý verebilmek için Microsoft.AspNetCore.Identity.EntityFrameworkCore kütüphanesi indirilmelidir.

21 - Ýhtiyaç duyulmasý halinde Models katmanýndan Reference alýnýr.

22 - DAL => Interfaces => Abstract klasörü altýna public tipte IBaseRepository isimli GenericType olacak þekilde bir interface tanýmlanýr. Bu interface içerisinde CRUD iþlemleri için metotlarýn imzalarý yazýlýr.

23 - DAL => Interfaces => Concrete klasörü altýna public tipte her entity isminde interface oluþturulur. Bu interfaceler IBaseRepository<EntityName> interface inden GenericType olarak kalýtým alýr. ILikeRepository ve IUserFollowedCategory, IBaseRepository den kalýtým almaz. ILikeRepository ve IUserFollowedCategory interface in metot imzalarý ayrýca yazýlýr.

NOT 5 => Kullanýcý doðrulama ve yetki iþlemleri gibi metotlar için Identity kütüphanesinin build in it metotlarý kullanýlacaktýr.

24 - DAL => Repositories altýnda bulunan Abstract ve Concrete klasörleri içerisinde interfacelerde imzalarý atýlan metotlarýn gövdeleri yazýlacaktýr.

25 - DAL => Repositories => Abstract altýnda BaseRepository isimli public abstract sýnýf tanýmlanarak IBaseRepository interface inde ki imzalanan metotlarýn gövdeleri yazýlýr. Bu noktada CRUD iþlemleri database e baðlantý yapýlarak yapýlacaðý için CTOR içerisinde context sýnýfý giriþ parametresi olarak alýnarak baðlanto saðlanýr.

NOT 6 => LINQ sorgularý yazarken iþlem sýralamasý önemlidir. Yani önce tablolar birleþtirilir, Gruplanýr, sorgulanýr, sýralanýr ve seçim yapýlarak sonuç döndürülür.

26 - DAL =>Repositories => Concrete altýnda açýlan repository classlarý ihtiyaç varsa BaseRepositoryden ve kendi repository interfaceinden kalýtým alýr. BaseRepository ata sýnýfýnýn CTOR u ProjectContext sýnýfýný parametre olarak ayaða kalktýðý için Context repository sýnýflarýnýn CTOR unun parametresinde ProjextContext sýnýfý yazýlýr ve :base(context) anahtar kelimesiyle atasýna context gönderilir. Construct Chain iþlemi denir.

27 - UserFollowedCategoryRepository sýnýfý yazýlmak zorunda deðildir. UserFollowedCategory ara tablo olduðu için ihtiyaç olmayabilir.

*************************************

28 - Pressentation olarak adlandýrýlan kullanýcý ile iletiþime geçilen katman oluþturulur.

29 - Presentation katmaný oluþturmak için Solution altýna Asp.Net Core WebApp(MVC) projesi SolutionName.WEB ismiyle oluþturulur.

30 - Migration iþlemleri burada yapýlacaðý için Program.cs ve appsetting.js içerisine database için ConnectionString ifadesi yazýlýr. builder.services

31 - IOC pattern deseni CORE için kullanýlýr. AddScope denerek bu durum tanýmlanýr. Yani kodu yazarken interface olarak yazdýðýmýz halde kendisi repository sýnýfýnda gövdeleri olan metotlarý tanýyacaktýr. builder.Services.AddScoped<InterfaceName, ClassName>();

31 - Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Tools Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.SqlServer kütüphanelerine ihtiyaç vardýr.

32 - migration ve update-database iþlemleri için NuGet Package Manager console daki default project : context sýnýfýný içeren katman olarak seçilmelidir. Bu iþlem yapýlýrken WEB sýnýfýný içeren katmanda solution startup project olarak seçilmelidir.

33 - Migration iþlemi için EfCore.Desing paketi kullanýlýr.

34 - add-migration migrationName þeklinde migration iþlemi yapýlýr. Sonrasýnda ise update-database iþlemi ile entityler ve entityTypeConfiguration lara göre database içerisinde tablolar oluþturulur.

35 - WEB altýna Areas klasörü açýlarak tanýmlanacak olan area lar bu klasör altýna tanýmlanýr.

36 - Program.cs içerisine app.MapControllerRoute eklenerek arealar için endpointler yazýlýr. 

37 - WEB => Areas klasörüne Member isimli area eklenir. MVC ana dizinindeki View klasörü kopyalanarak Areas => Member klasörüne yapýþtýrýlýr.

NOT 7 => Area lar için oluþturulan Contollerin üstüne [Area('AreaName')] attribute eklenmelidir.

38 - Program.cs içerisine Identity kütüphanesi özelliklerini kullanmak için builder.Services.AddIdentity komutu eklenir.

39 - Kayýtsýz kullanýcý iþlemleri için WEB katmaný ana dizinindeki controller içerisine UserController isimli controller eklenir.

40 - Tek bir entity nin belirli propertylerini kullanmak veya o propertylere validation eklenmesi sýrasýnda kullanýlan sýnýflara DataTransferObject (DTO) denir.

41 - Bir çok sýnýfýn propertylerii kullanarak iþlem yapýlýrken kullanýlan sýnýfa ViewModel (VM) denir.

42 - WEB => Models altýna DTOs isimli klasör oluþturulur. Kullanýcý kayýt iþlemleri sýrasýnda kullanýcýya sadece AppUser Entity sýnýfýnýn belirli propertylerini göstereceðim için DTO oluþturulur. Oluþturulan DTOs klasörü altýna CreateUserDTO isimli sýnýf oluþturulur.

43 - CreateUserDTO sýnýfý içerisine iþlem kullanýlmak istenen propertyler, Entity içerisindeki ayný isimlendirme ile eklenir ve istenirse validationlar yazýlýr. Bu sayede hem developer rahat eder hemde otomatik map iþlemi için kütüphane kullanýlabilir. Propertyler için VALIDATION iþlemleri yazýlýr.

44 - Create iþleminin [HTTPPPOST] kýsmýnda IActionResult metodunun giriþ parametresi olarak CreateUserDTO olarak verilir. Çünkü kullanýcý sadece DTO içerisindeki propertyleri önce dolduracak ve bizde doldurulmuþ olan DTO yu kullnarak üye kaydý oluþturacaðýz.

45 - Yeni kullanýcý oluþturma iþlemi Microsoft.AspNetCore.Identity kütüphanesi ile yapýlacaktýr. Yeni kullanýcý doðrulanmýþ bir kullanýcý olduðu için Microsoft.AspNetCore.Identity kütüphanesinin IdentityUser sýnýfýndan bir nesne new lenerek oluþturulur. Bu oluþturulacak nesnesin propertylerine DTO dan alýnan veriler atanacaktýr. Bu iþlem için instance yapýlýrken parametreli constructor kullanýlarak yapýlabilir.

NOT 8 => Yeni kullanýcýnýn yani AppUser tablosunun ID deðeri AspNetUsers tablosunun ID deðerine atanmaz. Çünkü AppUser için ID deðerini SQL kayýt yaparsa verecektir. Bu durumda kullanýcý önce database e eklenip daha sonra AspNetUsers oluþturulmaya  çalýþacaðý için hata verecektir. Bu iki tablo birbirini tanýyacak lakin iliþkisi olmayacaktýr. AppUser tablosuna yeni kullanýcý eklenip id deðeri alýnýnca ve bu id AspNetUsers tablosunun validation larýndna geçmezse yeni kullanýcý AppUser tablosuna eklenir lakin AspNetUsers tablosuna eklenmez bu durumu engellemek için AppUser tablosunda ayrýca bir IdentityID kolonu vardýr ve bu deðer GUID olarak atanýr.

46 - Otomatik Map iþlemi için AutoMapper kütüphanesi WEB katmanýna eklenir. Ayrýca AutoMapper.Extensions.Microsoft.Depen.... kütüphaneside eklenir.

47 - Map leme iþlemleri için WEB => Models altýna Mappers klasörü ve içerisine Mapping isimli class oluþturulur. Bu class AutoMapper kütüphanesinin Profile classýndan kalýtým alýr. Ayrýca Program.cs içerisinde de builder.services.AddAutoMapper() komutu eklenecektir.

48 - Create içerisinde automapping iþlemi yapmak için AutoMapper kütüphanesinin repolarýna ihtiyaç vardýr. Bu repolarý kullanmak için Controller in ctor unda IMapper sýnýfý giriþ parametresi olarak tanýmlanýr.

49 - Fotoðraf iþlemleri SixLabors.ImageSharp ve SixLabors.ImageSharp.Web kütüphaneleri kullanýlacaktýr.

50 - Fotoðrafýn dosya yollarý sadece database e kaydedilecek olup dosyalar wwwroot altýna oluþturulan images klasörüne kayýt edilecektir.

51 - UserController un Create actionuna Empty View eklenir.

52 - AdminLTE nin index3 içerisinde yer alan Form/General Elements altýndaki Quick Example isimli kýsmý inspect kýsmýndan kopyalatýp boþ Viewa yapýþtýrýlýr. Burada post iþlemi kullanýlacaðý için asp-action vb attribute ler eklenir.

53 - DTO larý vb nesneleri View içerisinde Model olarak kullanmak için nesne yollarýný View => ControllerName altýndaki _ViewImports.cshtml dosyasýna eklenir.

54 - Oluþturulan boþ View içerisine model olarak CreateUserDTO nesnesi eklenir. Bu sayede kullanýcýdan alýnan veriler CreateUserDTO nun propertylerine atanabilir.

55 - Login iþlemi Home Controller içerisinde yapýlýr. Login iþlemi için Identity kütüphanesi kullanýlacaktýr. Bunun için ctor içerisinde kütüphanenin UserManager ve SignInManager sýnýflarýnýn metotlarý kullanýlmak için tanýmlanýr.

56 - Kullanýcý giriþi için E-mail adresi ve þifre kullanýlacaktýr. Bunun için LoginDTO sýnýfý oluþturulur. Bu sýnýf içerisinde sadece mail ve þifre propertyleri olur.

57 - DTO ile alýnan bilgiler ile önce mail adresi var mý ? Varsa þifresi doðru mu þeklinde sýrasýyla kontrol edilir ve doðruysa kullanýcýnýn rolüne göre area ya yönlendirilir.

NOT 9 => Cookie ler için Program.cs dosyasýna gerekli kodlarýn eklenmesi gerekmektedir. Hem doðrulama hemde yetkilendirme kodlarý eklenir. builder.service ve app. olarak

58 - Üyelerin kullanabileceði üye paneli oluþturulacaktýr. Bunun için öncelikle layout hazýrlanýr. AdminLte 3 layoutu ve kütüphaneleri kullanýlacaktýr. Area => Member _Layout güncellenecektir. Burada cs ve script dosya yollarý düzenlenir.

59 - Areas => Member => Controller altýna AppUserController isimli controller eklenir ve [Area("Member")] attribute namespace altýna tanýmlanýr. Bu sayede ayný controller name olmasýna raðmen hangi area için çalýþacaðýný bilir.

60 - Bu controller altýnda kayýtlý kullanýcý için yapýlabilecek olan actionlar tanýmlanýr.

61 - Layoutta kullanýcý bilgilerinin ve resminin görüneceði kýsma @RenderSection oluþturulur.

62 - AppUserController in Index actionu için View oluþturulur. Bu Viewda AppUser tipindeki sýnýfý model olarak kullanýlýr _ViewImports içinde AppUser sýnýfýnýn yolu tanýmlanýr. Layout içerisindeki RenderSection bölümü için section oluþturulur.

63 - Category oluþturmak için Areas => Member altýnda CategoryController isimli controller oluþturulur ve içerisine CRUD iþlemleri için actionlar oluþturulur.

64- Create ve Update iþlemlerinde DTO lar kullanýlýr.Kategori DTO su Areas => Member => Models => DTOs klasörü oluþturulur.Bu klasör altýna gerekli olan DTO lar tanýmlanýr.

65- Auto mapper iþlemi yapmak için Areas => Member => Models => Mappers klasörü oluþturulur. Bu klasör altýna Mapping sýnýfý oluþturulur ve gerekli Mapping iþlemleri yazýlýr.

66- Update action yazýlýrken UpdateCategoryDTO sýnýfý oluþturulur.Bu sýnýf Update in View ý içerisinde model olarak gönderilip Update in [HTTPPOST] kýsmýnda giriþ parametresi olarak kullanýlacaktýr.

67- Update iþlemleri için mapper sýnýfýna ekleme yapýlýr.

68-  Article oluþturmak için Areas => Member altýnda ArticleController isimli controller oluþturulur ve içerisine CRUD iþlemleri için actionlar oluþturulur.

69- Create ve Update iþlemlerinde kullanýlýr.Kategori DTO su Areas => Member => Models => DTOs klasörü oluþturulur.Bu klasör altýna gerekli olan DTO lar tanýmlanýr.

70- Auto Mapper iþlemi yapmak için Areas => Member => Models => Mapper klasörü oluþturulur.Bu klasör altýna Mapping sýnýfý oluþturulur ve gerekli mappimg iþlemleri yapýlýr.

NOT 10 => Article List iþleminde ,Article  tablosunda sadece kullanýcýnýn ID deðeri tutulur.Makaleyi yazan kullanýcýnýn FirstName gibi AppUser tablosunda tutulan verilerine ulaþmak için EAGER LOADING yönteminde tablolarýn INCLUDE edilmesi gerekmektedir

NOT 11 => Birden fazla entity içindeki propertyler kullanarak oluþturulan sýnýflar için ViewModel yani VM oluþturulur.

71- Areas => Models klasörü altýna VMs klasörü altýna GetArticleVM isimli VM sýnýfý açýlýr.

72- Update action yazýlýrken UpdateArticleDTO sýnýfý oluþturulur.Bu sýnýf Update in View ý içerisinde model olarak gönderilip Update in [HTTPPOST] kýsmýnda giriþ parametresi olarak kullanýlacaktýr.

73- Update iþelmleri için mapper sýnýfýna ekleme yapýlýr.

NOT 12 => Birbirine komþu olmayan tablolarý baðlamak için ThenInclude(a=>a.Entity) metodu kullanýlýr.Bu metot en son yazýlan Include() metotu baz alýnarak kullanýlýr.Article tablosundan include(a=>a.Category).ThenInclude(a=>a.UserFollowedCategory) gibi

74- Ortak yerlerde gösterilecek Viewlar için Component oluþturulur.Bunun için Global alanda Views => Shared klasörünün altýna Component isimli kalsör oluþturulur ve Componentler bu klasör altýnda kendine has klasör isimleri ile birlikte oluþturulur.

75- Component klasörü altýndaki Article klaasörü içerisine ArticleViewComponent isminde sýnýf eklenir.Bu sýnýf ViewComponent sýnýfýndan kalýtým alýr.Ayrýva namespace altýna [ViewComponent(Name="Articles")] attribu eklenir.Bu VM ile ana sayfadaki makalelerin Card larý oluþturulacaktýr.

76- ViewComponentleri tetiklemek için Invoke metotu kullanýlýr.

77- ViewComponent içerisinde farklý sýnýflarýn propertyleri alýnarak bir VM gösterileceði için Global alanda Models klasörü altýna VMs klasörü açýlýr ve global alan VM leri burada tanýmlanýr.

78- Componentlerin Viewlarý kendi klasörleri altýnda oluþturulmak zorundadýr.Ýlk oluþturulan Component View ýnýn adý Default olmalýdýr.Bu View in model kýsmýna List<GetArticleWithUserVM> atanýr.

79- Anasayfanýn View ýna oluþturulan Default View gönderilir.Bu iþlem için Anasayfa Viewýnda @await Component.InvokeAsync("ViewComponentName") metotu çaðrýlýr.

80- Kayýtlý kullanýcýda giriþ yaptýðý zaman Member area sýnýn ana sayfasýnda bu ViewComponent i görmesi için AppUser altýndaki IndexView ýna @await Component.InvokeAsync("ViewComponentName") eklenir.

81- Areas içerisinde de Member özel ViewComponent yapmak için ayný iþlemler izlenir.



NOT 13 => IWebHostEnvironment interface’i, Asp.NET Core mimarisi dahilinde gelen ve projenin bulunduðu sunucu hakkýnda bizlere gerekli ortamsal bilgileri getirecek alanlarý barýndýran bir yapýya sahiptir. Bu interfacei controller içerisinde constructor injection yaparak kullanýlýrýz. Builder Design Pattern kullanýlarak oluþturulmuþ olan ve genellikle yeni Asp.net Core projesi oluþturduðunuzda Program.cs yada Startup.cs içerisinde WebHostBuilder sýnýfý kullanýlarak projenin belli baþlý özellikleri ayarlanarak ayaða kaldýrýlýr ve UseContentRoot adýnda bir metodunun olduðunu göreceksiniz bu metod ile ContentRootPath’deki yolu kendinize göre kiþiselleþtirebilirsiniz.

NOT 14 => Member/AppUser/Update içerisinde. Class burada AppUser oldUser = _appUserRepository.GetDefault(a => a.ID == dto.ID); çaðrýldýðý zaman update yaparken The instance of entity type 'AppUser' cannot be tracked because another instance with the same key value for {'ID'} is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.' hatasý vermektedir. Bunu aþmak için UpdateAppUserDTO nesnesine oldImage ve oldPassword propertyleri eklendi.

NOT 15 => cshtml içerisinde herhangi bir inputun name attributune atanan isim controllerin actiona submit edilirse o actionda yazýlan name deðeri input olarak alýnabilir. Örneðin checkbox ýn name özelliðine example[] þeklinde atama yapýlýrsa ve ListWithFilters actionuna post edilirse actionun giriþ parametresi olarak ListWithFilters(string[] example) þeklinde yakalanabilir.