using ProductAPI.Models;

namespace ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1000 },
            new Product { Id = 2, Name = "Mouse", Price = 25 },
            new Product { Id = 3, Name = "Keyboard", Price = 50 }
        };

        public List<Product> GetAll()
        {
            return products;
        }

        public Product GetById(int id)
        {
            foreach (Product product in products)
            {
                if (product.Id == id)
                {
                    return product;
                }
            }

            return null;
        }

        public Product Add(Product product)
        {
            product.Id = products.Count + 1;

            products.Add(product);

            return product;
        }

        public Product Update(int id, Product product)
        {
            foreach (Product p in products)
            {
                if (p.Id == id)
                {
                    p.Name = product.Name;
                    p.Price = product.Price;

                    return p;
                }
            }

            return null;
        }

        public bool Delete(int id)
        {
            foreach (Product product in products)
            {
                if (product.Id == id)
                {
                    products.Remove(product);

                    return true;
                }
            }

            return false;
        }
    }
}