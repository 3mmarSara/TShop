using System.ComponentModel.DataAnnotations;

namespace TShop.API.DTOs.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
