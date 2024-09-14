using GlobalMarket.Core.Models;

namespace GlobalMarket.Core.Dto
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Manufacture Manufacture { get; set; }
    }
}
