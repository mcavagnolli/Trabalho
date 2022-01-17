using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trabalho.Models
{
    public sealed class UpdateSessionInputModel
    {
        [Required]
        public string MovieId { get; set; }
        [Required]
        public string Date { get; set; }
        public int Seats { get; set; }
        public double Price { get; set; }
    }
}
