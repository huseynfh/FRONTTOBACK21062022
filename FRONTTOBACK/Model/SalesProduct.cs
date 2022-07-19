﻿namespace FRONTTOBACK.Model
{
    public class SalesProduct
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int SaleId { get; set; }

        public Sale Sale { get; set; }

        public double Price { get; set; }

        public int Count { get; set; }

    }
}
