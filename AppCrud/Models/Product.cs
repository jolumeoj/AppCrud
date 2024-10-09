namespace AppCrud.Models
{
    public class Product
    {
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }

        public ICollection<WishProduct> WishProducts { get; set; }
    }
}
