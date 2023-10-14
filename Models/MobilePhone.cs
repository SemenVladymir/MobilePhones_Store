namespace Lesson_14._10._23__EntityFrameWork_.Models
{
    public class MobilePhone
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string PhoneName { get; set; }
        public string ProgramType { get; set; }
        public int Memory { get; set; }
        public int Battery { get; set; }
        public double Diagonal { get; set; } 
        public double CameraResolution { get; set; }
        public double Price { get; set; }
    }
}
