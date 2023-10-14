using Lesson_14._10._23__EntityFrameWork_.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson_14._10._23__EntityFrameWork_
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) {}

        public DbSet<MobilePhone> MobilePhones { get; set; }
        public DbSet<Brand> PhoneBrands { get; set; }
    }
}
