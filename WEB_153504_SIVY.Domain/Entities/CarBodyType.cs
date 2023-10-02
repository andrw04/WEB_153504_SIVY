using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_153504_SIVY.Domain.Entities
{
    public class CarBodyType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
