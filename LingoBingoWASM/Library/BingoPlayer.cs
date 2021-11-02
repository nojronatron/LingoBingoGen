using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LingoBingoWASM.Library
{
    public class BingoPlayer
    {
        [Required]
        [StringLength(40, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
    }
}
