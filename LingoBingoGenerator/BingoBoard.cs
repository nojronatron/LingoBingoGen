using System;
using System.Collections.Generic;
using System.Linq;

namespace LingoBingoGenerator
{
    public class BingoBoard
    {
        // Fields
        private string _longestWord;  // longword.Length() can NO LONGER be discovered by the caller must use LongestWordLen
        private const int boardHeight = 21; // one-space buffer rows above and below each word in each box plus 6 horiz seperator lines
        private int _longestWordLen;
        private int _arraySize;
        // Constructors
        public BingoBoard() { }
        public BingoBoard(string[] stringArray)
        {
            Phrases = stringArray;
            int longWrdLen = stringArray[0].Length;
            foreach (string word in Phrases)
            {
                if (word.Length > longWrdLen)
                {
                    longWrdLen = word.Length;
                    _longestWord = word;
                }
            }
            _longestWordLen = _longestWord.Length;
            _arraySize = stringArray.Count();
        }
        // Properties
        public int Seed { get; set; }
        public string[] Phrases { get; set; }
        public string[] RandomizedPhrases { get; set; }
        public string[] FreeSpacedRandPhrases { get; set; }
        public string LongestWord { get { return this._longestWord; } }
        public int LongestWordLen { get { return this._longestWordLen; } }
        // Methods
        public int GetTileWidth()
        {
            return (LongestWordLen + 2); // longest word length plus a 1-space buffer on either side of each word
        }
        public int GetHorizontalFrameLength()
        {
            return (GetTileWidth() * 5) + 6; // five word boxes in each row plus 6 vertical seperators between word boxes
        }
        public string[] SetFreeSpace()
        {
            List<string> result = new List<string>();
            int wordNum = 1;
            foreach (string word in RandomizedPhrases)
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
        public string MakeBoard()
        { // draws a board
            List<string> Lingo = new List<string>(Phrases);
            RandomizedPhrases = (string[])Randomizer(Lingo.ToArray()); // force the array into a Type string[]
            FreeSpacedRandPhrases = SetFreeSpace(); // insert the FREE space in the middle of the board
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
            for (int row = 1; row <= boardHeight; row++) // rows
            {
                for (int col = 1; col <= numCols; col++) // cols
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
                            lingoWord = FreeSpacedRandPhrases[lingoIndex]; 
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
        {   // DONE: rewrite this to allow it to randomly stuff all Lingo words (at least 24) into a new String[] 
            Random rand = new Random(); // see: https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netframework-4.8
            int[] order = new int[_arraySize];
            for (int counter = 0; counter < _arraySize; counter++)
            {
                order[counter] = rand.Next();
            }
            Array.Sort(order, lingo);
            return lingo;
        }
    }
}
