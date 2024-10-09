namespace AppCrud.Models
{
    public class Feature
    {
        public int IdFeature { get; set; }
        public string FeatureName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
