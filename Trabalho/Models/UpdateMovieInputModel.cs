using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trabalho.Models
{
    public sealed class UpdateMovieInputModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Duration { get; set; }
        public string Synopsis { get; set; }
    }
}