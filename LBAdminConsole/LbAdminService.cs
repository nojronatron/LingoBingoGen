using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using LingoBingoLibrary.DataAccess;
using System.Linq;
using LingoBingoLibrary.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LBAdminConsole
{
    public class LbAdminService : IMyService
    {
        private readonly ILogger<LbAdminService> _log;
        private readonly IConfiguration _config;
        private readonly LingoWordsContext _context;
        private LingoWordsCollection LWCollection { get; set; }

        public LbAdminService(ILogger<LbAdminService> log, IConfiguration config, LingoWordsContext context)
        {
            this._log = log;
            this._config = config;
            this._context = context;
        }

        public void Run()
        {
            string message = "Running LbAdminService";
            _log.LogInformation("LbAdminConsole.Run(): {message}", message);

            //  use EFContext to check for existing records in DB Tables before loading
            try
            {
                if (_context.LingoWords.Any())
                {
                    var dbLingoWords = _context.LingoWords
                                        .Include(i => i.LingoCategory)
                                        .ToList();

                    LWCollection = new LingoWordsCollection(dbLingoWords);
                }
            }
            catch (Exception)
            {
                message = "Something went wrong, check relational database configurations and target server/service.";
                _log.LogInformation(message);
            }

            Menu();
        }

        public bool LoadIntoDB()
        {
            //  test for full Tables before proceeding (Admin must delete tables before loading with bulk data)
            var count = _context.LingoWords.Any();

            if (count)
            {
                _log.LogInformation($"LbAdminConsole.LoadIntoDB() DB Tables have data: { count.ToString() }.");
                return false;
            }

            //  use EFContext to load XML data into the DB Tables
            _log.LogInformation("LbAdminConsole.LoadIntoDB(): Attempting to load data from filesystem.");
            var generator = new LingoBingoLibrary.CoreLibs.Generator();
            LWCollection = generator.LoadWordPools();
            List<string> categories = LWCollection.Categories.ToList();

            foreach (var category in categories)
            {
                LingoCategory lingoCategory = new LingoCategory
                {
                    Category = category
                };

                _context.Add(lingoCategory);
                var words = LWCollection.GetWordsInCategory(category);

                foreach (var word in words)
                {
                    LingoWord lingoWord = new LingoWord
                    {
                        Word = word,
                        LingoCategory = lingoCategory
                    };

                    _context.Add(lingoWord);
                }
            }

            var itemsAffected = _context.SaveChanges();
            var msgArgs = $"Stored { itemsAffected } items to DB.";
            _log.LogInformation(System.Reflection.MethodBase.GetCurrentMethod().Name, msgArgs);

            if (itemsAffected < 1)
            {
                return false;
            }

            return true;
        }

        private bool CheckCollection()
        {
            if (LWCollection == null && LWCollection.Count<LingoWord>() < 1)
            {
                string msg = "No records in database. Load new records first.";
                _log.LogInformation(msg);
                Console.WriteLine(msg);
                return false;
            }

            return true;
        }

        private bool ListAllCategories()
        {
            if (!CheckCollection())
            {
                return false;
            }

            var message = string.Empty;
            var categories = LWCollection.Categories;
            message = $"Found { categories.Count() } categories.";
            _log.LogInformation(message);
            Console.WriteLine($"{ message }");
            Console.WriteLine("***** CATEGORIES IN DATABSE *****");

            foreach (var category in categories)
            {
                Console.WriteLine(category);
            }

            Console.WriteLine("**********\n");
            return true;
        }

        private bool ListAllWords()
        {
            if (!CheckCollection())
            {
                return false;
            }

            var message = string.Empty;
            Console.Write("Enter the category: ");
            var userResponse = Console.ReadLine();

            if (string.IsNullOrEmpty(userResponse) || string.IsNullOrWhiteSpace(userResponse))
            {
                message = "Nothing entered, returning to menu.";
                Console.WriteLine(message);
                _log.LogInformation(message);
                return false;
            }

            var wordsInCategory = LWCollection.GetWordsInCategory(userResponse);

            if (string.IsNullOrEmpty(wordsInCategory[0]) || string.IsNullOrWhiteSpace(wordsInCategory[0]))
            {
                message = "No words found or category not found.";
                Console.WriteLine(message);
                _log.LogInformation(message);
            }

            message = $"Found { wordsInCategory.Count() } categories.";
            _log.LogInformation(message);
            Console.WriteLine($"{ message }");
            Console.WriteLine($"***** WORDS IN CATEGORY { userResponse } *****");

            foreach (var word in wordsInCategory)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine("**********\n");
            return true;
        }

        public void Menu()
        {
            var keepMenuOpen = true;

            do
            {
                Console.WriteLine("LingoBingo Admin Menu");
                Console.WriteLine("1)\tLoad Default Words and Categories into an Empty DB");
                Console.WriteLine("2)\tAdd a new Category");
                Console.WriteLine("3)\tAdd a new Word to an existing Category");
                Console.WriteLine("4)\tList all Categories");
                Console.WriteLine("5)\tList Words in a specific Category");
                Console.WriteLine("6)\tList ALL Words and ALL Categories");
                Console.WriteLine("0)\tExit\n");

                Console.Write("Enter your selection: ");
                var userResponse = Console.ReadLine();

                Console.WriteLine($"\nYou selected { userResponse }");
                var succeeded = false;

                switch (userResponse)
                {
                    case "1":
                        {
                            //  load default words and categories into an empty db
                            succeeded = LoadIntoDB();
                            break;
                        }
                    case "2":
                        {
                            //  add a new category
                            break;
                        }
                    case "3":
                        {
                            //  add a new word to an existing category
                            break;
                        }
                    case "4":
                        {
                            //  list all categories
                            succeeded = ListAllCategories();
                            break;
                        }
                    case "5":
                        {
                            //  list words in a specific category
                            succeeded = ListAllWords();
                            break;
                        }
                    case "6":
                        {
                            //  list all words and categories
                            succeeded = ListAllCategories();
                            succeeded = ListAllWords();
                            break;
                        }
                    default:
                        {
                            //  0 or invalid: exit program
                            keepMenuOpen = false;
                            break;
                        }
                }
            } while (keepMenuOpen);

            Console.WriteLine("Exiting program. . .");
        }
    }
}
