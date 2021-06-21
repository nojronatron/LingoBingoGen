using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using LingoBingoLibrary.DataAccess;

namespace LingoBingoLibrary.Helpers
{
    public class BasicLingoWord : IEquatable<BasicLingoWord>
    {
        internal int ID { get; set; }
        public string Word { get; set; }
        public string Category { get; set; }
        public BasicLingoWord() { }  //  enable LINQ driven data stuffing
        public BasicLingoWord(string word, string category)
        {
            Word = word;
            Category = category;
        }

        public static implicit operator LingoWord(BasicLingoWord b) => new LingoWord { Word = b.Word , LingoCategory = new LingoCategory { Category = b.Category } };
        public static explicit operator BasicLingoWord(LingoWord l) => new BasicLingoWord(l.Word, l.LingoCategory.Category);

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
