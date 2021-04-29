using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace LingoBingoLibrary.Models
{
    public class LingoWord : IEquatable<LingoWord>
    {
        [Key]
        internal int ID { get; set; }
        [Required, MaxLength(45, ErrorMessage = "The longest English word according to Grammerly.com is 45 characters.")]
        internal string Word { get; set; }
        [Required, MaxLength(45, ErrorMessage = "The longest English word according to Grammerly.com is 45 characters.")]
        internal string Category { get; set; }
        public LingoWord() { }  //  enable LINQ driven data stuffing
        public LingoWord(string word, string category)
        {
            Word = word;
            Category = category;
        }

        public override bool Equals(object obj)
        {
            return obj is LingoWord word &&
                   Word == word.Word &&
                   Category == word.Category;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Word, Category);
        }

        bool IEquatable<LingoWord>.Equals(LingoWord other)
        {
            if (this != null && other == null)
            {
                return false;
            }

            return (Word == other.Word && Category == other.Category);
        }

        public static bool operator ==(LingoWord left, LingoWord right)
        {
            return EqualityComparer<LingoWord>.Default.Equals(left, right);
        }

        public static bool operator !=(LingoWord left, LingoWord right)
        {
            return !(left == right);
        }
        public override string ToString()
        {
            return $"{ Category }: { Word }";
        }
    }
}
