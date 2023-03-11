using System.ComponentModel.DataAnnotations;

namespace WebApplicationmvc.Models
{
    public class Check
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }
}
