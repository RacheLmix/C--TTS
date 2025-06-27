namespace ProductCatalogApi.DTOs
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
} 