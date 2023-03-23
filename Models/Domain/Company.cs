using Microsoft.Build.Framework;

namespace Kursach.Models.Domain
{
    public class Company
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public string Country { get; set; }
        public string Field { get; set; }

    }
}
