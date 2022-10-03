using System.ComponentModel.DataAnnotations;

namespace CATALOG.Dtos
{
    public class UpdateItemDto 
    {
        [Required]
        public string Name {get; init; }
        [Required]
        public decimal Price {get; init;}
    }
}