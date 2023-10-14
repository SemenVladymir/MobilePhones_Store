using Lesson_14._10._23__EntityFrameWork_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lesson_14._10._23__EntityFrameWork_.Controllers
{
    public class BrandController : ControllerBase
    {
        private readonly MyDBContext _dbContext;
        public BrandController(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> AddBrand(string brandName)
        {
            _dbContext.PhoneBrands.Add(new Brand { BrandName = brandName });
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("Update")]
        public async Task<ActionResult> UpdateBrand(int brandId, string newBrandName)
        {
            if (_dbContext.PhoneBrands == null)
                return BadRequest();
                _dbContext.PhoneBrands.Update(new Brand { Id = brandId, BrandName= newBrandName });
                if (_dbContext.SaveChanges() > 0)
                    return Ok();
                else
                    return BadRequest();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            if (_dbContext.PhoneBrands == null)
                return BadRequest();
            Brand brand = await _dbContext.PhoneBrands.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.PhoneBrands.Remove(brand);
            if (_dbContext.SaveChanges() > 0)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("ReadAllBrands")]
        public async Task<ActionResult<IEnumerable<Brand>>> GetAllBrands()
        {
            if (_dbContext.PhoneBrands == null)
                return BadRequest();

            return await _dbContext.PhoneBrands.ToListAsync();
        }

        [HttpGet("FindBrend")]
        public async Task<ActionResult<Brand>> FindBrand(string brand)
        {
            if (_dbContext.PhoneBrands == null)
                return BadRequest();
            return _dbContext.PhoneBrands.First(x => x.BrandName.ToLower() == brand.ToLower());
        }
    }
}
