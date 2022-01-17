using System;
using System.ComponentModel.DataAnnotations;

namespace Trabalho.Models
{
    public sealed class NewMovieInputModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Duration { get; set; }
        public string Synopsis { get; set; }

    }
}