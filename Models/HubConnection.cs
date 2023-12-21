using System.ComponentModel.DataAnnotations;

namespace ThesisOct2023.Models
{
    public class HubConnection
    {
        [Key]

        public int Id { get; set; }
        public string ConnectionId { get; set; } = null;
        public string Username { get; set; } = null;
    }
}
