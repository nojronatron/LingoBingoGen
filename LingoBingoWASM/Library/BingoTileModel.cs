using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LingoBingoWASM.Library
{
    public class BingoTileModel
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public bool IsDaubered { get; set; } = false;
        public BingoTileModel() {}
        public BingoTileModel(string word)
        {
            Word = word;
        }
    }
}
