namespace AppCrud.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<WishProduct> WishProducts { get; set; }
    }
}
