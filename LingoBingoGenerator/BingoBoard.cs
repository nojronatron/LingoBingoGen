using System;
using System.Collections.Generic;
using System.Linq;

namespace LingoBingoGenerator
{
    class BingoBoard
    {
        // Fields
        private string[] _phrases;
        private string _longestWord;  // longword.Length() can be discovered by the caller
        private const int _boardHeight = 21; // one-space buffer rows above and below each word in each box plus 6 horiz seperator lines
        private int _longestWordLen;
        // Constructors
        public BingoBoard() { }
        public BingoBoard(string[] stringArray)
        {
            _phrases = stringArray;
        }
        // Properties
        public string[] Phrases
        {
            get { return _phrases; }
            set { _phrases = value; }
        }
        public string LongestWord
        {
            get
            {
                int longLen = 0;
                foreach (string word in Phrases)
                {
                    if (word.Length > longLen)
                    {
                        _longestWord = word; // just set the Field
                        longLen = word.Length;
                    }
                }
                _longestWordLen = _longestWord.Length;
                return _longestWord;
            }
            set
            {
                foreach (string word in Phrases)
                {
                    if (word.Length > _longestWord.Length)
                    {
                        _longestWord = word;
                    }
                };
            }
        }
        // Methods
        public int GetTileWidth()
        {
            return LongestWord.Length + 2; // longest word length plus a 1-space buffer on either side of each word
        }
        public int GetHorizontalFrameLength()
        {
            return (GetTileWidth() * 5) + 6; // five word boxes in each row plus 6 vertical seperators between word boxes
        }
        public string[] SetFreeSpace()
        {
            // string[] result = { };
            List<string> result = new List<string>();
            int wordNum = 1;
            foreach (string word in _phrases)
            {
                if (wordNum == 13)
                {
                    result.Add("Free");
                    result.Add(word);
                }
                else { result.Add(word); }
                wordNum++;
            }
            return result.ToArray();
        }
        public string GetBoard()
        { // method() draws a board
            List<string> Lingo = new List<string>(Phrases);
            Phrases = (string[])Randomizer(Lingo.ToArray()); // force the array into a Type string[]
            // TODO: Instead of storing this in _phrases use algo in here ( GetBoard() ) to JIT insert it in the correct spot
            Phrases = SetFreeSpace(); // insert the FREE space in the middle of the board
            string board = "";
            string lingoWord; // NOTE: Unassigned until rowsWithWordsAndBars containment comparison is done
            int lingoIndex = 0; // manage indexing through the array of lingo terms
            int[] rowsWithAllDashes = { 1, 5, 9, 13, 17, 21 }; // rows with all horizontal dashes
            int[] rowsWithOnlyBars = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 }; // rows with vertical bars
            int[] rowsWithWordsAndBars = { 3, 7, 11, 15, 19 }; // rows with words but not dashes
            int maxWordLenBuf = (1 + GetTileWidth());
            int[] barPositions = { 1, (maxWordLenBuf +1), ((maxWordLenBuf * 2)+1),
                ((maxWordLenBuf * 3)+1), ((maxWordLenBuf * 4)+1), ((maxWordLenBuf * 5)+1)};
            int numCols = GetHorizontalFrameLength(); // total cols to cycle thru for each row
            // rows
            for (int row = 1; row <= _boardHeight; row++)
            {
                // cols
                for (int col = 1; col <= numCols; col++)
                {
                    if ((col) == numCols)
                    {
                        board += "|"; // draw a bar on the last col and force next row to start
                        col = numCols + 1;
                    }
                    else if (rowsWithAllDashes.Contains(row))
                    { // if the row is all dashes just draw them and move to the next row
                        for (int i = 1; i <= numCols; i++)
                        {
                            board += "-";
                        }
                        col += numCols;
                    }
                    else if (rowsWithOnlyBars.Contains(row))
                    {
                        if (barPositions.Contains(col)) { board += "|"; }
                        else { board += " "; }
                        // col++; //this DOES NOT FIX the offset issue
                    }
                    else if (rowsWithWordsAndBars.Contains(row))
                    { // maxWordLenWithBuffer added... MINUS the current word length to fill tile space
                        if (barPositions.Contains(col) && lingoIndex < 25) // only 0-24 allowed on the board
                        {
                            lingoWord = Phrases[lingoIndex]; // TODO: after testing refactor this to be: board+=Phrases[lingoIndex]
                            board += $"| {lingoWord} ";
                            col += (lingoWord.Length + 2);
                            lingoIndex++;
                        }
                        else { board += " "; }
                    }
                }
                board += "\n"; // end of row add a newline
            }
            return board;
        }
        public Array Randomizer(Array lingo)
        {
            Random rand = new Random(); // see: https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netframework-4.8
            double[] order = new double[24];
            for (int counter = 0; counter < 24; counter++)
            {
                order[counter] = rand.NextDouble();
            }
            Array.Sort(order, lingo);
            return lingo;
        }
    }
}
