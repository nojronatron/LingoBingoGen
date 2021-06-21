using LingoBingoLibrary.Collections;
using LingoBingoLibrary.DataAccess;
using System.Collections.Generic;

namespace LingoBingoWebApp.Models
{
    public interface ILingoBingoPage
    {
        LingoWordsContext LingoContext { get; }
        IList<Lingoword> Lingowords { get; set; }
        LingoWordsCollection LingoWordsCollection { get; set; }
    }
}