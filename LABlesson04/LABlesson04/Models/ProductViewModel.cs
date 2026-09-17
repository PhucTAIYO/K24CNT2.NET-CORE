namespace LABlesson04.Models
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public int? SelectedCategoryId { get; set; }
    }
}
