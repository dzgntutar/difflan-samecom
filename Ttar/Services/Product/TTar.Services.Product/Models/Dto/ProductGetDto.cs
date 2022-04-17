namespace TTar.Services.Product.Models.Dto
{
    public class ProductGetDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
    }
}
