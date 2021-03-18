using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Http;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {        
          
       private readonly ShopContext _context;

       public ProductsController(ShopContext context) 
       {
           _context = context;
           _context.Database.EnsureCreated();
       }

       /*[HttpGet]
       public IEnumerable<Product> GetAllProducts() => _context.Products.ToArray();
       */

       [HttpGet]
       public async Task<IActionResult> GetAllProducts() => Ok(await _context.Products.ToArrayAsync());

       [HttpGet("{id}")]
       //public IActionResult GetProduct(int id) => Ok(_context.Products.Find(id));
       public async Task<IActionResult> GetProduct(int id)
       {
           var product = await _context.Products.FindAsync(id);
           return (product == null)? NotFound(): Ok(product);
       }


    }
}
