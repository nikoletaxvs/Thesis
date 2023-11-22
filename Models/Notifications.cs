using System.ComponentModel.DataAnnotations;

namespace ThesisOct2023.Models
{
    public partial class Notifications
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = null;
        public string Message { get; set; } = null;
        public string MessageType { get; set; } = null;
        public DateTime NotificationDateTime { get; set; }
    }
}
