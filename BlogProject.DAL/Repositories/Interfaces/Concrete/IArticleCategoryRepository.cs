using BlogProject.DAL.Repositories.Concrete;
using BlogProject.Models.Entities.Concrrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlogProject.DAL.Repositories.Interfaces.Concrete
{
    
    
        public interface IArticleCategoryRepository // IBaseRepository den kalıtım almadığı için kendi metot imzaları atılır.
        {
            // Create
            void Create(ArticleCategory entity);

            // Delete
            void Delete(ArticleCategory entity);
        }
    
}
