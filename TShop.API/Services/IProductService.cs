using System.Linq.Expressions;
using TShop.API.Models;
using TShop.API.DTOs.Requests;

namespace TShop.API.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? Get(Expression<Func<Product, bool>> expression);

        Product? Add(ProductRequest productRequest);
        bool Edit(int id, ProductUpdateRequest productRequest);
        bool Remove(int id);
    }
}
