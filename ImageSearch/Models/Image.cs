using Microsoft.AspNetCore.Http;

namespace ImageSearch.Models
{
    public class Image
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
    }
}
