using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InventoryContext _context;

        public ProductsController(InventoryContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet("GetProducts")]
#pragma warning disable CS0436 // Type conflicts with imported type
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts(bool? inStock, int? skip, int? take)
#pragma warning restore CS0436 // Type conflicts with imported type
        {

            var products = _context.Products.AsQueryable();

            if (inStock != null) // Adds the condition to check availability 
            {
                products = _context.Products.Where(i => i.AvailableQuantity > 0);
            }

            if (skip != null)
            {
                products = products.Skip((int)skip);
            }

            if (take != null)
            {
                products = products.Take((int)take);
            }

            return await products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("GetProducts/{id}")]
#pragma warning disable CS0436 // Type conflicts with imported type
        public async Task<ActionResult<Products>> GetProducts(int id)
#pragma warning restore CS0436 // Type conflicts with imported type
        {
            var products = await _context.Products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("PutProducts/{id}")]
#pragma warning disable CS0436 // Type conflicts with imported type
        public async Task<IActionResult> PutProducts(int id, Products products)
#pragma warning restore CS0436 // Type conflicts with imported type
        {
            if (id != products.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("PostProducts")]
#pragma warning disable CS0436 // Type conflicts with imported type
        public async Task<ActionResult<Products>> PostProducts(Products products)
#pragma warning restore CS0436 // Type conflicts with imported type
        {
            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = products.ProductId }, products);
        }

        // DELETE: api/Products/5
        [HttpDelete("DeleteProducts/{id}")]
#pragma warning disable CS0436 // Type conflicts with imported type
        public async Task<ActionResult<Products>> DeleteProducts(int id)
#pragma warning restore CS0436 // Type conflicts with imported type
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return products;
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
