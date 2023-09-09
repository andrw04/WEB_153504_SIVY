namespace WEB_153504_SIVY.Domain.Entities
{
    public class CarModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CarBodyType? CarBody { get; set; }
        public int Price { get; set; }
        public string? Image { get; set; }
        public string Mime { get; set; } = "image/png";
    }
}
