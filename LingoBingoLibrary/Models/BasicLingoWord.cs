using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace LingoBingoLibrary.Models
{
    public class BasicLingoWord : IEquatable<BasicLingoWord>
    {
        [Key]
        internal int ID { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(45, ErrorMessage = "The longest English word according to Grammerly.com is 45 characters.")]
        internal string Word { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(45, ErrorMessage = "The longest English word according to Grammerly.com is 45 characters.")]
        internal string Category { get; set; }
        public BasicLingoWord() { }  //  enable LINQ driven data stuffing
        public BasicLingoWord(string word, string category)
        {
            Word = word;
            Category = category;
        }

        public override bool Equals(object obj)
        {
            return obj is BasicLingoWord word &&
                   Word == word.Word &&
                   Category == word.Category;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Word, Category);
        }

        bool IEquatable<BasicLingoWord>.Equals(BasicLingoWord other)
        {
            if (this != null && other == null)
            {
                return false;
            }

            return (Word == other.Word && Category == other.Category);
        }

        public static bool operator ==(BasicLingoWord left, BasicLingoWord right)
        {
            return EqualityComparer<BasicLingoWord>.Default.Equals(left, right);
        }

        public static bool operator !=(BasicLingoWord left, BasicLingoWord right)
        {
            return !(left == right);
        }
        public override string ToString()
        {
            return $"{ Category }: { Word }";
        }
    }
}
