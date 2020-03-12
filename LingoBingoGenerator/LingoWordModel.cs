using System.ComponentModel.DataAnnotations;

namespace LingoBingoGenerator
{
    public partial class LingoWordModel
    {
        public LingoWordModel() { }
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(45, ErrorMessage = "The longest English word according to Grammerly.com is 45 characters.")]
        public string Word { get; set; }
        [Required, MaxLength(45, ErrorMessage = "The longest English word according to Grammerly.com is 45 characters.")]
        public string Category { get; set; }
    }
}
