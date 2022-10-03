using CATALOG.Dtos;
using CATALOG.Entities;

namespace CATALOG
{
    public static class Extension 
    {
        public static ItemDto AsDto(this Item item) 
        {
            return new ItemDto
            {
                Id = item.Id, 
                Name = item.Name, 
                Price = item.Price, 
                CreatedDate = item.CreatedDate
            };

        }
    }
}