namespace FS.Domain.Entities.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public decimal TotalCost { get; set; } = 0;

        public Cart() { }
        public Cart(List<Product> products)
        {
            Products = products;
            CalculateTotalCost();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
            TotalCost += product.Price ?? 0;
        }

        public void CalculateTotalCost()
        {
            TotalCost = Products.Sum(x => x.Price ?? 0);
        }

    }

}
