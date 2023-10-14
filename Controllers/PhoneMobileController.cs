using Lesson_14._10._23__EntityFrameWork_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Lesson_14._10._23__EntityFrameWork_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneMobileController : ControllerBase
    {
        private readonly MyDBContext _dbContext;
        public PhoneMobileController(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> AddPhone(MobilePhone phone)
        {
            _dbContext.MobilePhones.Add(phone);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("Update")]
        public async Task<ActionResult> UpdatePhone(int phoneId, MobilePhone newData)
        {
            if (_dbContext.MobilePhones == null)
                return BadRequest();

            _dbContext.MobilePhones.Update(new MobilePhone { Id = phoneId, PhoneName = newData.PhoneName, BrandId = newData.BrandId, ProgramType = newData.ProgramType });
            if (_dbContext.SaveChanges() > 0)
                return Ok();
            else
                return BadRequest();
        }


        [HttpDelete("Delete")]
        public async Task<ActionResult> DeletePhones(int id)
        {
            if (_dbContext.MobilePhones == null)
                return BadRequest();
            MobilePhone phone = await _dbContext.MobilePhones.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.MobilePhones.Remove(phone);
            if (_dbContext.SaveChanges()>0)
                return Ok();
            else 
                return BadRequest();
        }

        [HttpGet("ReadAll")]
        public async Task<ActionResult<IEnumerable<MobilePhone>>> GetAllPhones()
        {
            if (_dbContext.MobilePhones == null)
                return BadRequest();

            return await _dbContext.MobilePhones.ToListAsync();
        }

        [HttpGet("FindBrend")]
        public async Task<ActionResult<IEnumerable<MobilePhone>>> GetBrandPhones(string brand)
        {
            if (_dbContext.MobilePhones == null)
                return BadRequest();
            int brandId =_dbContext.PhoneBrands.First(x=>x.BrandName.ToLower()==brand.ToLower()).Id;
            IEnumerable<MobilePhone> brands = await _dbContext.MobilePhones.Where(x => x.BrandId == brandId).ToListAsync();
            if (brands.Any())
                return Ok(brands);
            else 
                return BadRequest();
        }

        [HttpPost("FindPhoneByParametr")]
        public async Task<ActionResult<IEnumerable<MobilePhone>>> GetParametrPhones(MobilePhone parametrs)
        {
            if (_dbContext.MobilePhones == null)
                return BadRequest();
            var tmp = await _dbContext.MobilePhones.Where(x => x.BrandId == parametrs.BrandId &&
            x.Price >= parametrs.Price - 2000 && x.Price <= parametrs.Price + 2000 && 
            x.Battery >= parametrs.Battery - 500 && x.Battery <= parametrs.Battery + 500 &&
            x.CameraResolution >= parametrs.CameraResolution - 5000 && x.CameraResolution <= parametrs.CameraResolution + 5000 &&
            x.Diagonal >= parametrs.Diagonal  - 2 && x.Diagonal <= parametrs.Diagonal + 2 &&
            x.Memory >= parametrs.Memory - 4 && x.Memory <= parametrs.Memory + 4).ToListAsync();
            return tmp;

        }
    }
}
