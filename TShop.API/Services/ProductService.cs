using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TShop.API.Data;
using TShop.API.DTOs.Requests;
using TShop.API.Models;

namespace TShop.API.Services
{
    public class ProductService(ApplicationDbContext context) : IProductService
    {
        private readonly ApplicationDbContext _context = context;

        public Product? Add(ProductRequest productRequest)
        {
            var file = productRequest.mainImg;
            var product = productRequest.Adapt<Product>();

            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                product.MainImg = fileName;
                _context.Products.Add(product);
                _context.SaveChanges();
                return product;
            }

            return null;
        }

        public bool Edit(int id, ProductUpdateRequest productRequest)
        {
            var productInDB = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (productInDB is null) return false;

            var updatedProduct = productRequest.Adapt<Product>();
            var file = productRequest.MainImg;

            if (file != null && file.Length > 0)
            {
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "images", productInDB.MainImg);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                updatedProduct.MainImg = fileName;
            }
            else
            {
                updatedProduct.MainImg = productInDB.MainImg;
            }

            updatedProduct.Id = id;
            _context.Products.Update(updatedProduct);
            _context.SaveChanges();
            return true;
        }

        public Product? Get(Expression<Func<Product, bool>> expression)
        {
            return _context.Products.FirstOrDefault(expression);
        }

        public IEnumerable<Product> GetAll()
        {
            return [.. _context.Products];
        }

        public bool Remove(int id)
        {
            var product = _context.Products.Find(id);
            if (product is null) return false;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", product.MainImg);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
    }
}
