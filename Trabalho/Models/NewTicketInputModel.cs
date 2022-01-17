using System.ComponentModel.DataAnnotations;

namespace Trabalho.Models
{
    public sealed class NewTicketInputModel
    {
        [Required]
        public string SessionId { get; set; }
        [Required]
        public string ClientName { get; set; }
        [Required]
        public int Amount { get; set; }

    }
}