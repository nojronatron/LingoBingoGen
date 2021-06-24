using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingoBingoLibrary.DataAccess
{
    public partial class LingoCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Category { get; set; }
        public virtual ICollection<LingoWord> LingoWords { get; set; }
    }
}
