using System.Linq.Expressions;
using TShop.API.DTOs.Requests;
using TShop.API.Models;

namespace TShop.API.Services
{
    public interface IBrandService
    {
        IEnumerable<Brand> GetAll();
        Brand? Get(Expression<Func<Brand, bool>> expression);
        Brand? Add(BrandRequest brandRequest);
        bool Edit(int id, BrandUpdateRequest brandRequest);
        bool Remove(int id);
    }
}
