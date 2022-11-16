using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Taller.Web.Models
{
    public class AddCarModel
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Make { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Color { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
