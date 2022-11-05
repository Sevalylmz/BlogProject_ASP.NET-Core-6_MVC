using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WEB.Areas.Member.Models.DTOs
{
    public class GetCategoryDTO 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
