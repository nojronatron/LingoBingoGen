using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LingoBingoWebApp.Models
{
    public class Lingoword
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(45, ErrorMessage = "The longest English word according to Grammerly.com is 45 characters.")]
        public string Word { get; set; }
        public Lingoword(string word)
        {
            Word = word;
        }

    }
}
