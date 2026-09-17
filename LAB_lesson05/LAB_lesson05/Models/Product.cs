namespace LAB_lesson05.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal? Price { get; set; }
    }
}
