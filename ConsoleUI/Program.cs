using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LingoBingoGenerator;

namespace ConsoleUI
{
    class Program
    {
        //  run this exe to display a bingo board using ASCII characters
        //  must:
        //  take input from a user
        //  use an existing source of words
        //  use LingoGenerator to set words, GetRandomizedList, get LongestWordLen
        //  have an ability to generate multiple, randomized boards
        //  properly set the FREE space at index 11
        //  properly draw the ASCII board with one word 
        public static List<string> ListWords26 = new List<string>
            {
            "alpha", "bravo", "charlie", "delta", "echo", "foxtrot", "golf", "hotel", "india",
            "juliet", "kilo", "lima", "mike", "november", "oscar", "papa", "quebec", "roger",
            "sierra", "tango", "uniform", "victor", "whiskey", "xray", "yankee", "zulu"
            };

        static void Main(string[] args)
        {
            DrawMenuSystem();

            //Console.Write("\n\nPress <Enter> to Quit. . .");
            //Console.ReadLine();
        }
        
        static void DrawBoard(List<string> listWords)
        {
            LingoGenerator Words = new LingoGenerator();
            List<string> randomWords = null;
            Words.SetLingoWords(listWords);
            if (Words.RandomizeList())
            {
                randomWords = Words.GetRandomList();
            }
            LingoGenerator RandWords = new LingoGenerator();
            RandWords.SetLingoWords(randomWords);
            StringBuilder thisBoard = GenerateBoard(RandWords);

            Console.WriteLine(thisBoard.ToString());
        }

        static void DrawMenuSystem()
        {
            Console.WriteLine($"Placeholder: Draws a menu system for user input.");
            bool keepGoing = true;
            while (keepGoing)
            {
                Console.WriteLine("\nChoose from the following options:\n");
                Console.WriteLine("\t1. Add words to an existing category.");
                Console.WriteLine("\t2. Add a new category.");
                Console.WriteLine("\t9. Generate a board using default words.");
                Console.WriteLine("\tX. Exit.");
                Console.Write("\nYour choice: ");

                string userOption = Console.ReadLine();
                userOption = userOption.ToUpper();

                switch (userOption)
                {
                    case "1":
                        Console.WriteLine("\nPick a category to add words to:\n");
                        Console.WriteLine("\t 1. AlphaPhonetics");
                        Console.WriteLine("\t X. Return");
                        Console.Write("\nYour choice: ");
                        userOption = Console.ReadLine();
                        break;

                    case "2":
                        Console.Write("\nEnter a new category name: ");
                        userOption = Console.ReadLine();
                        break;

                    case "9":
                        Console.WriteLine("\nPick a category of words for your board:\n");
                        Console.WriteLine("\tAlphaPhonetics");
                        //  Console.Write("\nYour choice: ");
                        Console.Clear();
                        DrawBoard(ListWords26);
                        Console.Write("\n\nPress <Enter> to Return to Menu. . .");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case "X":
                        keepGoing = false;
                        break;

                    default:
                        Console.WriteLine
                            ("\nYour option {0} is not valid. Try again!\n",
                            userOption);
                        break;
                }
            }
        }

        public static int GetTileWidth(LingoGenerator words)
        {
            return words.GetLongestWordLen() + 2; // longest word length plus a 1-space buffer on either side of each word
        }

        public static int GetHorizontalFrameLength(LingoGenerator words)
        {
            return (GetTileWidth(words) * 5) + 6; // five word boxes in each row plus 6 vertical seperators between word boxes
        }

        public static List<string> SetFreeSpace(LingoGenerator words)
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
            List<string> wordsForBoard = SetFreeSpace(words); // insert the FREE space in the middle of the board
            StringBuilder board = new StringBuilder();
            string lingoWord; // NOTE: Unassigned until rowsWithWordsAndBars containment comparison is done
            int lingoIndex = 0; // manage indexing through the array of lingo terms
            int[] rowsWithAllDashes = { 1, 5, 9, 13, 17, 21 }; // rows with all horizontal dashes
            int[] rowsWithOnlyBars = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 }; // rows with vertical bars
            int[] rowsWithWordsAndBars = { 3, 7, 11, 15, 19 }; // rows with words but not dashes
            int maxWordLenBuf = (1 + GetTileWidth(words));
            int[] barPositions = { 1, (maxWordLenBuf +1), ((maxWordLenBuf * 2)+1),
                ((maxWordLenBuf * 3)+1), ((maxWordLenBuf * 4)+1), ((maxWordLenBuf * 5)+1)};
            int numCols = GetHorizontalFrameLength(words); // total cols to cycle thru for each row
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
