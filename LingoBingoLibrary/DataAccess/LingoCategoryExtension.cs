using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingoBingoLibrary.DataAccess
{
    public partial class LingoCategory : IEquatable<LingoCategory>
    {
        public override bool Equals(object obj)
        {
            return obj is LingoCategory category &&
                   Category == category.Category &&
                   EqualityComparer<ICollection<LingoWord>>.Default.Equals(LingoWords, category.LingoWords);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Category, LingoWords);
        }

        bool IEquatable<LingoCategory>.Equals(LingoCategory other)
        {
            if (other == null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return (this.Category == other.Category);
        }

        public static bool operator ==(LingoCategory left, LingoCategory right)
        {
            return EqualityComparer<LingoCategory>.Default.Equals(left, right);
        }

        public static bool operator !=(LingoCategory left, LingoCategory right)
        {
            return !(left == right);
        }
    }
}
