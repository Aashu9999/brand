﻿namespace BrandWebApi.Models
{
    public class Brand
    {
        public int id { get; set; } 

        public string name { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty ;

        public string type { get; set; } = string.Empty;

        public string price { get; set; } = string.Empty;
    }
}
