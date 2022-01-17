using System.ComponentModel.DataAnnotations;

namespace Trabalho.Models
{
    public class NewSessionInputModel
    {
        [Required]
        public string FilmeId { get; set; }
        [Required]
        public string Date { get; set; }
        public int Seats { get; set; }
        public double Price { get; set; }
    }
}