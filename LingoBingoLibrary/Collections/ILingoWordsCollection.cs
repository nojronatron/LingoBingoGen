using LingoBingoLibrary.DataAccess;
using System.Collections.Generic;

namespace LingoBingoLibrary.Collections
{
    public interface ILingoWordsCollection
    {
        LingoWord this[int idx] { get; set; }

        IList<string> Categories { get; }
        IList<LingoCategory> LingoCategories { get; }
        int CategoryCount { get; }

        bool GetListWithFreeSpace(ref List<string> words);
        List<string> GetNewBingoBoard(string category);
        List<string> GetRandomWords(string category, int howMany);
        List<string> GetWordsInCategory(string category);
    }
}