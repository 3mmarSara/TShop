using System.Linq.Expressions;
using TShop.API.Data;
using TShop.API.DTOs.Requests;
using TShop.API.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace TShop.API.Services
{
    public class BrandService(ApplicationDbContext context) : IBrandService
    {
        private readonly ApplicationDbContext _context = context;

        public Brand? Add(BrandRequest brandrequest)
        {

            var file = brandrequest.Logo;
            var brand = brandrequest.Adapt<Brand>();

            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                brand.Logo = fileName;
                _context.Brands.Add(brand);
                _context.SaveChanges();
                return brand;
            }

            return null;
        }

        public bool Edit(int id, BrandUpdateRequest brandRequest)
        {
            var brandInDB = _context.Brands.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (brandInDB is null) return false;

            var updatedBrand = brandRequest.Adapt<Brand>();
            var file = brandRequest.Logo;

            if (file != null && file.Length > 0)
            {
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "images", brandInDB.Logo);
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

                updatedBrand.Logo = fileName;
            }
            else
            {
                updatedBrand.Logo = brandInDB.Logo;
            }

            updatedBrand.Id = id;
            _context.Brands.Update(updatedBrand);
            _context.SaveChanges();
            return true;
        }

        public Brand? Get(Expression<Func<Brand, bool>> expression)
        {
            return _context.Brands.FirstOrDefault(expression);
        }

        public IEnumerable<Brand> GetAll()
        {
            return [.. _context.Brands];
        }

        public bool Remove(int id)
        {
            var brand = _context.Brands.Find(id);
            if (brand is null) return false;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", brand.Logo);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return true;
        }
    }
}
