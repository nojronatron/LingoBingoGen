using System.ComponentModel.DataAnnotations;

namespace LingoBingoGen
{
    public partial class LingoWord
    {
        public LingoWord() { }
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(45,ErrorMessage ="The longest English word according to Grammerly.com is 45 characters.")]
        public string Word { get; set; }
        [Required, MaxLength(45, ErrorMessage = "The longest English word according to Grammerly.com is 45 characters.")]
        public string Category { get; set; }
    }
}
