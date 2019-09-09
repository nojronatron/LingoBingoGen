using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LingoBingoGenerator
{
    public class BingoBoard
    { 
        // Fields
        private const int BOARD_HEIGHT = 21; // one-space buffer rows above and below each word in each box plus 6 horiz seperator lines
        private int[] rowsWithAllDashes = { 1, 5, 9, 13, 17, 21 }; // rows with all horizontal dashes
        private int[] rowsWithOnlyBars = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 }; // rows with vertical bars
        private int[] rowsWithWordsAndBars = { 3, 7, 11, 15, 19 }; // rows with words but not dashes
        private List<string> _randomizedPhrases;
        private List<string> _freeSpacedRandPhrases;

        // Constructors
        public BingoBoard() { }
        public BingoBoard(string[] stringArray)
        {
            LongestWordLen = stringArray[0].Length;
            foreach (string word in stringArray)
            {
                if (word.Length > LongestWordLen)
                {
                    LongestWordLen = word.Length;
                    LongestWord = word;
                }
            }
            PhraseCount = stringArray.Length;
            Phrases = stringArray;
            _randomizedPhrases = new List<string>(LongestWordLen);
            _freeSpacedRandPhrases = new List<string>(LongestWordLen + 1);
        }
        // Properties
        private int LongestWordLen { get; set; }
        private string[] Phrases { get; set; }
        private int TileWidth { get { return (LongestWordLen + 2); } }
        public string LongestWord { get; private set; }
        public int PhraseCount { get; set; } // auto-prop, does NOT include FREE space
        public int HorizontalFrameLength { get { return (TileWidth * 5) + 6; } } // for the caller and internal
        // Methods
        private void SetFreeSpace()
        {
            if (_freeSpacedRandPhrases.Count > 0)
            {
                _freeSpacedRandPhrases.Clear();
            }
            int wordNum = 1;
            foreach (string word in _randomizedPhrases)
            {
                if (wordNum == 13)
                {
                    _freeSpacedRandPhrases.Add("Free");
                    _freeSpacedRandPhrases.Add(word);
                }
                else
                {
                    _freeSpacedRandPhrases.Add(word);
                }
                wordNum++;
            }
        }
        public void SetRandomization()
        {
            Randomizer(Phrases.ToArray());  // randomize the order of the words once
        }
        public string MakeGuiBoard()
        { // draws a board
            SetRandomization();
            SetFreeSpace();                 // insert the FREE space in the middle of the board
            string board = "";
            string lingoWord;
            int lingoIndex = 0; // manage indexing through the array of lingo terms
            int maxWordLenBuf = (1 + TileWidth);
            int[] barPositions = { 1, (maxWordLenBuf +1), ((maxWordLenBuf * 2)+1),
                ((maxWordLenBuf * 3)+1), ((maxWordLenBuf * 4)+1), ((maxWordLenBuf * 5)+1)};
            for (int row = 1; row <= BOARD_HEIGHT; row++) // rows
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
                            lingoWord = _freeSpacedRandPhrases[lingoIndex]; 
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
        private void Randomizer(Array lingo)
        {   // randomly stuff 24 lingo words into a List
            if (PhraseCount < 24)
            {
                Exception NotEnoughWordsException = new Exception("Must enter at least 24 terms to make a valid board", innerException: null);
                throw NotEnoughWordsException;
            }
            if (_randomizedPhrases.Count > 0)
            {
                _randomizedPhrases.Clear();
            }
            Random rand = new Random(); // see: https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netframework-4.8
            int[] order = new int[PhraseCount];
            for (int counter = 0; counter < PhraseCount; counter++)
            {
                order[counter] = rand.Next();
            }
            Array.Sort(order, lingo);
            foreach (string word in lingo)
            {
                _randomizedPhrases.Add(word);
            }
        }
    }
}
