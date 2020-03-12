using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileManagementHelper;
using LingoBingoGenerator;

namespace ConsoleUI
{
    class LingoBingoConsole
    {
        //  run this exe to display a bingo board using ASCII characters
        //  must:
        //  take input from a user
        //  use an existing source of words
        //  use LingoGenerator to set words, GetRandomizedList, get LongestWordLen
        //  have an ability to generate multiple, randomized boards
        //  properly set the FREE space at index 11
        //  properly draw the ASCII board with one category (future: multiple categories)
        public static List<string> ListWords26 = new List<string>
            {
            "alpha", "bravo", "charlie", "delta", "echo", "foxtrot", "golf", "hotel", "india",
            "juliet", "kilo", "lima", "mike", "november", "oscar", "papa", "quebec", "roger",
            "sierra", "tango", "uniform", "victor", "whiskey", "xray", "yankee", "zulu"
            };
        private static char quote = (char)('"');

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
            StringBuilder thisBoard = BingoBoardGeneratorHelper.GenerateBoard(RandWords);

            Console.WriteLine(thisBoard.ToString());
        }

        static void DrawMenuSystem()
        {
            Console.WriteLine($"Placeholder: Draws a menu system for user input.");
            bool keepGoing = true;
            while (keepGoing)
            {
                Console.WriteLine("\nChoose from the following options:\n");
                Console.WriteLine("\t1. Generate a board using default words.");
                Console.WriteLine("\t2. Generate a board using words in existing category.");
                Console.WriteLine("\t3. Add words to an existing category.");
                Console.WriteLine("\t4. Add a new category of words.");
                Console.WriteLine("\t8. ");
                Console.WriteLine("\t9. ");
                Console.WriteLine("\tQ. Quit.");
                Console.Write("\nYour choice: ");

                string userOption = Console.ReadLine();
                userOption = userOption.ToUpper();

                switch (userOption)
                {
                    case "1":
                        Console.Clear();
                        DrawBoard(ListWords26);
                        Console.Write("\n\nPress <Enter> to Return to Menu. . .");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case "2":
                        List<string> allCategories = new List<string>(XmlFileHelper.GetCategories());
                        List<string> categoryList = new List<string>(allCategories.Distinct());
                        Console.WriteLine("\nCategories to pick from:");
                        foreach (string category in categoryList)
                        {
                            Console.WriteLine($"\t{ category }");
                        }
                        Console.WriteLine();
                        Console.Write("\nEnter the category name to generate a new board: ");
                        userOption = Console.ReadLine();

                        List<string> query = (from c in allCategories
                                    where c == userOption
                                    select c).ToList();

                        List<string> wordsToUse = null;
                        if (query.Count < 1 || string.IsNullOrEmpty(userOption))
                        {
                            Console.WriteLine($"\nError! Category { quote }{ userOption }{ quote } not found.\n");
                        }
                        else
                        {
                            wordsToUse = XmlFileHelper.GetWordsInCategory(userOption);
                            if (wordsToUse == null)
                            {
                                Console.WriteLine($"\nError! Category { quote }{ userOption }{ quote } " +
                                                  $"did not have any words to select.\n");
                            }
                            else if (wordsToUse.Count < 24)
                            {
                                Console.WriteLine($"\nError! Category { quote }{ userOption }{ quote } " +
                                                  $"did not have at least 24 words.\n");
                            }
                            else
                            {
                                Console.Clear();
                                DrawBoard(wordsToUse);
                                Console.Write("\n\nPress <Enter> to Return to Menu. . .");
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        break;

                    case "7":
                        Console.WriteLine("\nPick a category to add words to:\n");
                        Console.WriteLine("\t 1. AlphaPhonetics");
                        Console.WriteLine("\t X. Return");
                        Console.Write("\nYour choice: ");
                        userOption = Console.ReadLine();
                        break;


                    case "8":
                        break;

                    case "9":
                        Console.WriteLine("\nPick a category of words for your board:\n");
                        Console.WriteLine("\tAlphaPhonetics");
                        Console.Clear();
                        DrawBoard(ListWords26);
                        Console.Write("\n\nPress <Enter> to Return to Menu. . .");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case "Q":
                        keepGoing = false;
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

    }
}
