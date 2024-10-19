using System.ComponentModel.DataAnnotations;

namespace SportsNewsWebApp.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
