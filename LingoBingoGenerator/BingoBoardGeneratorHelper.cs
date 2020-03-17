using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LingoBingoGenerator
{
    public static class BingoBoardGeneratorHelper
    {
        private static int GetTileWidth(LingoGenerator words)
        {
            return words.GetLongestWordLen() + 2; // longest word length plus a 1-space buffer on either side of each word
        }

        private static int GetHorizontalFrameLength(LingoGenerator words)
        {
            return (GetTileWidth(words) * 5) + 6; // five word boxes in each row plus 6 vertical seperators between word boxes
        }

        private static List<string> SetFreeSpace(LingoGenerator words)
        {
            List<string> lingoWords = words.GetLingoWords();
            List<string> result = new List<string>(25);
            int wordNum = 1;
            foreach (string word in lingoWords)
            {
                if (wordNum == 13)
                {
                    result.Add("Free");
                    result.Add(word);
                }
                else { result.Add(word); }
                wordNum++;
            }
            return result;
        }

        public static StringBuilder GenerateBoard(LingoGenerator words)
        {
            if (words == null)
            {
                return new StringBuilder();
            }
            List<string> randomizedWords = new List<string>(words.GetLingoWords());
            List<string> shortendWordList = new List<string>(24);
            LingoGenerator words24 = new LingoGenerator();
            if (randomizedWords.Count > 24)    //  truncate if more than 24 words
            {
                foreach(string word in randomizedWords)
                {
                    shortendWordList.Add(word);
                }
                words24.SetLingoWords(shortendWordList);
            }
            List<string> wordsForBoard = SetFreeSpace(words24); // insert the FREE space in the middle of the board
            StringBuilder board = new StringBuilder();
            string lingoWord; // NOTE: Unassigned until rowsWithWordsAndBars containment comparison is done
            int lingoIndex = 0; // manage indexing through the array of lingo terms
            int[] rowsWithAllDashes = { 1, 5, 9, 13, 17, 21 }; // rows with all horizontal dashes
            int[] rowsWithOnlyBars = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 }; // rows with vertical bars
            int[] rowsWithWordsAndBars = { 3, 7, 11, 15, 19 }; // rows with words but not dashes
            int maxWordLenBuf = (1 + GetTileWidth(words24));
            int[] barPositions = { 1, (maxWordLenBuf +1), ((maxWordLenBuf * 2)+1),
                ((maxWordLenBuf * 3)+1), ((maxWordLenBuf * 4)+1), ((maxWordLenBuf * 5)+1)};
            int numCols = GetHorizontalFrameLength(words24); // total cols to cycle thru for each row
            int boardHeight = 21;
            char bar = '|';
            char dash = '-';
            char space = ' ';
            // rows
            for (int row = 1; row <= boardHeight; row++)
            {
                board.Append("\t");
                // cols
                for (int col = 1; col <= numCols; col++)
                {
                    if ((col) == numCols)
                    {
                        board.Append(bar); // draw a bar on the last col and force next row to start
                        col = numCols + 1;
                    }
                    else if (rowsWithAllDashes.Contains(row))
                    { // if the row is all dashes just draw them and move to the next row
                        for (int i = 1; i <= numCols; i++)
                        {
                            board.Append(dash);
                        }
                        col += numCols;
                    }
                    else if (rowsWithOnlyBars.Contains(row))
                    {
                        if (barPositions.Contains(col))
                        {
                            board.Append(bar);
                        }
                        else
                        {
                            board.Append(space);
                        }
                    }
                    else if (rowsWithWordsAndBars.Contains(row))
                    { // maxWordLenWithBuffer added... MINUS the current word length to fill tile space
                        if (barPositions.Contains(col) && lingoIndex < 25) // only 0-24 allowed on the board
                        {
                            lingoWord = wordsForBoard[lingoIndex];
                            board.Append($"{ bar} { lingoWord } ");
                            col += (lingoWord.Length + 2);
                            lingoIndex++;
                        }
                        else { board.Append(space); }
                    }
                }
                board.AppendLine();
            }
            return board;
        }
    }
}
