namespace EntityFrameworkProject.Entities.Dto
{
    public class ProductApiDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ManufactureId { get; set; }
    }
}
