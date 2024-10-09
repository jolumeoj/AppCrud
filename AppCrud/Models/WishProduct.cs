namespace AppCrud.Models
{
    public class WishProduct
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public Product Product { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }
    }
}
