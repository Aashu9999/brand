using BrandWebApi.Context;
using BrandWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrandWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandDbContext _dbContext;

        public BrandController(BrandDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Brand>>>GetBrands()
        {
            if(_dbContext.brands==null)
            {
                return NotFound();
            }

            return await _dbContext.brands.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Brand>> GetBrands(int id)
        {
            if (_dbContext.brands == null)
            {
                return NotFound();
            }

            var brand = await _dbContext.brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        [HttpPost]

        public async Task<ActionResult<Brand>> PostBrand(Brand brand) 
        { 
            _dbContext.brands.Add(brand);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrands), new { id = brand.id}, brand);
        
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Brand>> PutBrand(int id , Brand brand)
        {
            if(id != brand.id)
            {
                return BadRequest();
            }
            _dbContext.Entry(brand).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { 

                if (!BrandAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
             return Ok();
        }

        private bool BrandAvailable(int id )
        {
            return (_dbContext.brands?.Any(x => x.id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Brand>> DeleteBrand(int id)
        {
            if(_dbContext.brands == null)
            {
                return NotFound();
            }

            var brand =await _dbContext.brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            _dbContext.brands.Remove(brand);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }


    }
}
