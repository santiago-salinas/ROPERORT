﻿using Services.Models.Exceptions;

namespace Services.Models
{
    public class Product
    {
        public Product()
        { }
        private double _priceUYU;
        private int _stock;
        public int Id { get; set; }
        public string Name { get; set; }
        public double PriceUYU
        {
            get { return _priceUYU; }
            set
            {
                if (value <= 0)
                {
                    throw new Models_ArgumentException("Product price cannot be below zero or less");
                }
                _priceUYU = value;
            }
        }
        public string Description { get; set; }
        public int Stock
        {
            get { return _stock; }
            set
            {
                _stock = value;
            }
        }
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public List<Colour> Colours { get; set; }
    }
}
