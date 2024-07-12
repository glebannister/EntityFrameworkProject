namespace GlobalMarket.Core.Models.Api
{
    public class ProductUpdateApi
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
        public string NewDescription { get; set; }
        public decimal NewPrice { get; set; }
        public int NewManufactureId { get; set; }
    }
}
