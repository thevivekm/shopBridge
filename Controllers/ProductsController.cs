using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;

namespace Shopbridge_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductsController> logger;
        public ProductsController(IProductService _productService, ILogger<ProductsController> _logger)
        {
            this.productService = _productService;
            this.logger = _logger;
        }

       
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            try
            {
                logger.LogInformation("Getproducts started");
				var result = await productService.GetAll();
                if(result!=null)
                {
					logger.LogInformation("Getproducts success");
					return result;
                }
                else
                {
					logger.LogInformation("Products not found");
					return NotFound();
                }
			}
            catch (Exception ex)
            {
				logger.LogError(ex, "An error occurred while getting products.");
				return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting products. Please try again later.");
			}
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
				logger.LogInformation("GetProductById started");
				var result =  await productService.GetById(id);
                if (result != null)
                {
                    return result;
                }
                else
                {
					logger.LogInformation("Product with given id not found");
					return NotFound();
                }
			}
            catch(Exception ex)
            {

				logger.LogError(ex, "An error occurred while getting products.");
				return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting products. Please try again later.");
			}
            
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
			try
			{
				logger.LogInformation("PutProduct started");
				var result = await productService.Update(id,product);
				return Ok(result);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while adding products.");
				return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding products. Please try again later.");
			}
		}

        
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
			try
			{
				logger.LogInformation("PostProduct started");
				var result = await productService.AddProduct(product);
				return result;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while updating products.");
				return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating products. Please try again later.");
			}
		}

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
			try
			{
				logger.LogInformation("deleteProduct started");
				var result = await productService.Delete(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while deleting products.");
				return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting products. Please try again later.");
			}
		}

        private bool ProductExists(int id)
        {
            return false;
        }
    }
}
