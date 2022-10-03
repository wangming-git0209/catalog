using System.ComponentModel.DataAnnotations;

namespace CATALOG.Dtos
{
    public class CreateItemDto 
    {
        [Required]
        public string Name {get; init; }
        [Required]
        public decimal Price {get; init;}
    }
}