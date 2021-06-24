using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingoBingoLibrary.DataAccess
{
    public partial class LingoWord : IEquatable<LingoWord>
    {
        public override bool Equals(object obj)
        {
            return obj is LingoWord word &&
                   Word == word.Word &&
                   EqualityComparer<LingoCategory>.Default.Equals(LingoCategory, word.LingoCategory);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Word, LingoCategory);
        }

        bool IEquatable<LingoWord>.Equals(LingoWord other)
        {
            if (other == null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return (this.Word == other.Word &&
                    this.LingoCategory.Category == other.LingoCategory.Category);
        }

        public static bool operator ==(LingoWord left, LingoWord right)
        {
            return EqualityComparer<LingoWord>.Default.Equals(left, right);
        }

        public static bool operator !=(LingoWord left, LingoWord right)
        {
            return !(left == right);
        }
    }
}
