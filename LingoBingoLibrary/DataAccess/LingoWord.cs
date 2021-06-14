using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingoBingoLibrary.DataAccess
{
    public class LingoWord
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Word { get; set; }
        public virtual LingoCategory LingoCategory { get; set; }
    }
}
