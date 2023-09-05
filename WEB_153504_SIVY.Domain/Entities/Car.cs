using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_153504_SIVY.Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public int Price { get; set; }
        public string? Image { get; set; }
        public string Mime { get; set; } = "image/png";
    }
}
