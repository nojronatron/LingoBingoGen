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
        //  properly set the FREE space at index 12
        //  properly draw the ASCII board using one of least one category if not more to choose from
        //  enable user to add a category and add words to it, saving it for use now or later
        //  enable user to add words to an existing category for use now or later
        //  generate an initial XML file that stores a default set of LingoWords so user can run-and-play immediately

        public static List<string> ListWords26 = new List<string>
            {
            "alpha", "bravo", "charlie", "delta", "echo", "foxtrot", "golf", "hotel", "india",
            "juliet", "kilo", "lima", "mike", "november", "oscar", "papa", "quebec", "romeo",
            "sierra", "tango", "uniform", "victor", "whiskey", "xray", "yankee", "zulu"
            };
        private static char quote = (char)('"');

        static void Main(string[] args)
        {
            DrawMenuSystem();
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
            int startingWindowHeight = Console.WindowHeight;
            int startingWindowWidth = Console.WindowWidth;
            Console.WindowHeight=30;
            Console.WindowWidth=130;
            Console.WriteLine($"Placeholder: Draws a menu system for user input.");
            bool keepGoing = true;
            while (keepGoing)
            {
                //Console.WriteLine(Console.WindowHeight.ToString());
                //Console.WriteLine(Console.WindowWidth.ToString());
                Console.WriteLine("\nChoose from the following options:\n");
                Console.WriteLine("\t1. Generate a board using default words.");
                Console.WriteLine("\t2. Generate a board using words in existing category.");
                Console.WriteLine("\t3. Add words to an existing category.");
                Console.WriteLine("\t4. Add a new category of words.");
                //  Console.WriteLine("\t8. ");
                Console.WriteLine("\t9. Load a custom category and word list (LingoWords.xml).");
                Console.WriteLine("\tQ. Quit.");
                Console.Write("\nYour choice: ");

                string userOption = Console.ReadLine();
                userOption = userOption.ToUpper();

                switch (userOption)
                {
                    case "1":
                        {
                            //  User Selected: Generate a board using default words
                            Console.Clear();
                            DrawBoard(ListWords26);
                            Console.Write("\n\nPress <Enter> to Return to Menu. . .");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }

                    case "2":
                        {
                            //  User Selected: Generate a board using words in an existing Category
                            IEnumerable<string> categoryList = ShowExistingCategories();

                            Console.Write("\nEnter the category name to generate a new board: ");
                            userOption = Console.ReadLine();
                            
                            if (UserSelectedValidCategory(userOption, categoryList, out string mCategory))
                            {
                                DrawBoardOrReturnError(mCategory, categoryList);
                            }
                            else
                            {
                                Console.WriteLine($"\nCategory { quote }{ mCategory }{ quote } not found.");
                                Console.Write("\n\nPress <Enter> to Return to Menu. . .");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            break;
                        }

                    case "3":
                        {
                            //  User Selected: Add words to existing category
                            IEnumerable<string> categoryList = ShowExistingCategories();

                            Console.WriteLine("\nPick a category to add word(s) to:\n");

                            //  get user input on which category to add to
                            Console.Write("\nYour choice: ");
                            string userSelectedCategory = Console.ReadLine();

                            //  validate category exists
                            if (UserSelectedValidCategory(userSelectedCategory, categoryList, out string mCategory))
                            {

                                //  get user input of list of words to add
                                bool working = true;
                                List<string> wordsToAdd = new List<string>();

                                while (working)
                                {
                                    string inputWord = string.Empty;
                                    Console.Write("\nWord to add (q to quit): ");
                                    inputWord = Console.ReadLine();
                                    if (string.IsNullOrEmpty(inputWord))
                                    {
                                        //  skip it and tell the user then prompt for next word
                                        Console.WriteLine();
                                    }
                                    else if (inputWord.Length > 1 && inputWord.Length < 45)
                                    {
                                        //  Add the word to the category list
                                        wordsToAdd.Add(inputWord);
                                        Console.WriteLine($"\nAdded { inputWord } to category { mCategory }.");
                                    }
                                    else if (inputWord.ToLower() == "q")
                                    {
                                        Console.WriteLine($"\nDone adding words to category.");
                                        working = false;
                                    }
                                }

                                //  call helper method to add words to the category including XML-file update/write
                                if (
                                    FileManagementHelper.FileManagementHelper.AddWordsToCategoryList(wordsToAdd, mCategory)
                                    )
                                {
                                    Console.WriteLine($"Lingo Words in category { mCategory } added successfully!");
                                }
                                else
                                {
                                    Console.WriteLine($"Unable to update words in new category { mCategory }.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"\nCategory { quote }{ mCategory }{ quote } not found.");
                            }

                            Console.Write("\n\nPress <Enter> to Return to Menu. . .");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }

                    case "4":
                        {
                            //  display existing categories
                            List<string> existingCategories = new List<string>(ShowExistingCategories());

                            string userNewCategory = string.Empty;

                            while (string.IsNullOrEmpty(userNewCategory))
                            {
                                //  ask user for new category name and do not accept a blank entry
                                Console.Write("\nEnter the title for a new category: ");
                                userNewCategory = Console.ReadLine();
                            }

                            //  call helper method to ensure topic is unique before adding it and writing to a file
                            if (UserSelectedValidCategory(userNewCategory, existingCategories, out string mCategory))
                            {
                                Console.WriteLine($"Category { quote }{ mCategory }{ quote } exists.");
                            }
                            else
                            {
                                List<string> mergedCategories = new List<string>(existingCategories);
                                mergedCategories.Add(userNewCategory);
                                mergedCategories.Distinct();

                                string firstWord = string.Empty;
                                while (string.IsNullOrEmpty(firstWord))
                                {
                                    //  ask user for new category name and do not accept a blank entry
                                    Console.Write($"Enter the first word for new category { userNewCategory }: ");
                                    firstWord = Console.ReadLine();
                                }

                                if (FileManagementHelper.FileManagementHelper.AddNewCategory(userNewCategory, firstWord))
                                {
                                    Console.WriteLine("Operation completed.");
                                }
                                else
                                {
                                    Console.WriteLine("Unable to update word list with new category and word.");
                                }
                            }                            
                            Console.Write("\n\nPress <Enter> to Return to Menu. . .");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }

                    case "9":
                        {
                            Console.WriteLine("\nLoading category and word list. . .");
                            if (FileManagementHelper.FileManagementHelper.DeployDefaultWordlistFile())
                            {
                                Console.WriteLine("File loaded successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Unable to load the file LingoWords.xml.");
                            }
                            Console.Write("\n\nPress <Enter> to Return to Menu. . .");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }

                    case "Q":

                    case "X":
                        keepGoing = false;
                        Console.WindowHeight = startingWindowHeight;
                        Console.WindowWidth = startingWindowWidth;
                        break;

                    default:
                        Console.WriteLine($"\nYour option { userOption } is not valid. Try again!\n");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
        }

        private static bool UserSelectedValidCategory(string userInput, IEnumerable<string> categorylist, out string matchedCategory)
        {
            //  fix case sensitivity bug
            var catFound = from c in categorylist
                           where (c.ToUpper() == userInput.ToUpper())
                           select c;
            if (catFound.Count() > 0)
            {
                matchedCategory = catFound.First();
                return true;
            }
            else
            {
                matchedCategory = userInput;
                return false;
            }
        }

        private static void DrawBoardOrReturnError(string userOption, IEnumerable<string> categoryList)
        {
            //IEnumerable<string> query = from cat in categoryList
            //                            where cat == userOption
            //                            select cat;

            //List<string> wordsToUse = null;
            //if (query.Count() < 1 || string.IsNullOrEmpty(userOption))
            //{
            //    Console.WriteLine($"\nError! Category { quote }{ userOption }{ quote } not found.\n");
            //}
            //else
            //{
            List<string> wordsToUse = FileManagementHelper.FileManagementHelper.GetWordsInCategory(userOption);
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
            //}
        }

        private static IEnumerable<string> ShowExistingCategories()
        {
            List<string> allCategories = new List<string>(FileManagementHelper.FileManagementHelper.GetCategories());
            IEnumerable<string> categoryList = allCategories.Distinct();

            Console.WriteLine("\nCategories to pick from:");
            foreach (string category in categoryList)
            {
                Console.WriteLine($"\t{ category }");
            }
            Console.WriteLine();
            return categoryList;
        }
    }
}
