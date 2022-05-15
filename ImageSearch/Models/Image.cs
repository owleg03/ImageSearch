using Microsoft.AspNetCore.Http;

namespace ImageSearch.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public List<string> Keywords { get; set; }
    }
}
