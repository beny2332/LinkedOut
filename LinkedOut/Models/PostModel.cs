namespace LinkedOut.Models
{
    public class PostModel
    {
        public string body {  get; set; }
        public UserModel user { get; set; }
        public int likes { get; set; }
    }
}
