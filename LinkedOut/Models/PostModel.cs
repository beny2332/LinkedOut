using System.ComponentModel.DataAnnotations;

namespace LinkedOut.Models
{
    public class PostModel
    {
        [Key] 
        public int id { get; set; }
        public string body {  get; set; }
        public UserModel user { get; set; }
        public int likes { get; set; }
    }
}
