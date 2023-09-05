﻿namespace WEB_153504_SIVY.Domain.Entities
{
    public class SparePart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public float Price { get; set; }
        public string? Image { get; set; }

    }
}
