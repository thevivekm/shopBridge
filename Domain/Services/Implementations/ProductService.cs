using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
		private readonly Shopbridge_Context _dbContext;
        public ProductService(ILogger<ProductService> logger, Shopbridge_Context dbContext)
        {
            _logger = logger;
			_dbContext = dbContext;
			
        }
        public async Task<List<Product>> GetAll()
		{
			return await _dbContext.Product.ToListAsync();
		}

		public async Task<Product> GetById(int id)
		{
			return await _dbContext.Product.FindAsync(id);
		}
		public async Task<Product> AddProduct(Product product)
		{
			var result = await _dbContext.AddAsync(product);
			await _dbContext.SaveChangesAsync();
			return result.Entity;
		}

		public async Task<Product> Update(int id, Product product)
		{
			var prod = await _dbContext.Product.FindAsync(id);
			prod.Name = product.Name;
			prod.Price = product.Price;
			prod.Description = product.Description;
			prod.ProductType = product.ProductType;
			var result = _dbContext.Update(prod);
			await _dbContext.SaveChangesAsync();
			return result.Entity;
		}
		public async Task<bool> Delete(int id)
		{
			var product = await _dbContext.Product.FindAsync(id);
			if (product == null)
			{
				return false;
			}

			_dbContext.Product.Remove(product);
			await _dbContext.SaveChangesAsync();
			return true;
		}

		
	}
}
