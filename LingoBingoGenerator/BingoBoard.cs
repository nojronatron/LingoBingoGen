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
        private int _arraySize;
        private int[] rowsWithAllDashes = { 1, 5, 9, 13, 17, 21 }; // rows with all horizontal dashes
        private int[] rowsWithOnlyBars = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 }; // rows with vertical bars
        private int[] rowsWithWordsAndBars = { 3, 7, 11, 15, 19 }; // rows with words but not dashes
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
            LongestWordLen = _longestWord.Length;
            _arraySize = stringArray.Count();
        }
        // Properties
        private string[] Phrases { get; set; } // TODO: Store as a Generic List<T>
        public int PhraseCount { get { return Phrases.Length; } } // for the caller
        private string[] RandomizedPhrases { get; set; } // TODO: Store as a Generic List<T>
        private string[] FreeSpacedRandPhrases { get; set; } // TODO: Store as a Generic List<T>
        private int LongestWordLen { get; set; }
        public int TileWidth { get { return (LongestWordLen + 2); } }
        public int HorizontalFrameLength { get { return (TileWidth * 5) + 6; } } // for the caller and needed internally
        // Methods
        private string[] SetFreeSpace()
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
            string lingoWord;
            int lingoIndex = 0; // manage indexing through the array of lingo terms
            int maxWordLenBuf = (1 + TileWidth);
            int[] barPositions = { 1, (maxWordLenBuf +1), ((maxWordLenBuf * 2)+1),
                ((maxWordLenBuf * 3)+1), ((maxWordLenBuf * 4)+1), ((maxWordLenBuf * 5)+1)};
            for (int row = 1; row <= boardHeight; row++) // rows
            {
                for (int col = 1; col <= HorizontalFrameLength; col++) // cols
                {
                    if ((col) == HorizontalFrameLength)
                    {
                        board += "|"; // draw a bar on the last col and force next row to start
                        col = HorizontalFrameLength + 1;
                    }
                    else if (rowsWithAllDashes.Contains(row))
                    { // if the row is all dashes just draw them and move to the next row
                        for (int i = 1; i <= HorizontalFrameLength; i++)
                        {
                            board += "-";
                        }
                        col += HorizontalFrameLength;
                    }
                    else if (rowsWithOnlyBars.Contains(row))
                    {
                        if (barPositions.Contains(col)) { board += "|"; }
                        else { board += " "; }
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
        private Array Randomizer(Array lingo)
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
