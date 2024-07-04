namespace EntityFrameworkProject.Entities.Model
{
    public class ProductShop
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
