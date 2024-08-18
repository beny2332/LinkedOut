using System.ComponentModel.DataAnnotations;

namespace LinkedOut.Models
{
    public class UserModel
    {
        [Key]
        public int id { get; set; }
        public string userName { get; set; }
        public string UNHASHEDPassword { get; set; }
    }
}
