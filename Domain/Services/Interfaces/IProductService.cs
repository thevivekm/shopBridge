using Microsoft.AspNetCore.Mvc;
using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> Update(int id, Product product);
        Task<bool> Delete(int id);
    }
}
