using System.ComponentModel.DataAnnotations;

namespace TShop.API.DTOs.Requests
{
    public class CategoryRequest
    {
        [Required(ErrorMessage = "Name is required!!")]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
