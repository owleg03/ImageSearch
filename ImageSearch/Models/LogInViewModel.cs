using System.ComponentModel.DataAnnotations;

namespace ImageSearch.Models
{
    public class LogInViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
