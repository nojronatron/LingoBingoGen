using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace LingoBingoGen
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!CallInitializerRoutine())
            {
                Console.Write("\n\nUnable to load initializer file, an error occurred.");
                Console.ReadLine();
                return;     // force program execution exit
            }

            // set the Console Window size
            Console.SetWindowSize(150, 40);

            // Query the DB to get the terms and shove them into a List
            using (LingoContext context = new LingoContext())
            {
                List<LingoWord> lingoList = (from w in context.LingoWords
                                             select w).ToList();

                // get the list of Categories and present the to the user to pick one

                // try it using a few Linq queries and a helper method (seems difficult)
                string[] allCategories = (from x in context.LingoWords
                                          select x.Category).ToArray();
                string userCategory = GetCategorySelection(allCategories);
                //string[] selectedCategoryWords = (from y in context.LingoWords
                                                  //where IsCategory(y, userCategory)
                                                  //select y.Word).ToArray();

                // try it using String.Contains() built-in method then manually iterate to select each Word
                string[] selectedLingoWords = (from z in context.LingoWords
                                where z.Category.Contains(userCategory)
                                select z.Word).ToArray();

                // Instantiate BingoBoard using input object of expected Type
                BingoBoard alpha = new BingoBoard(selectedLingoWords);

                // send the array to a static function that will "draw" the board
                Console.WriteLine(alpha.GetBoard());
            }


            Console.Write("\n\nPress <Enter> to Exit. . .");
            Console.ReadLine();
        }
        private static bool CallInitializerRoutine()
        {
            bool result = false;
            try
            {   // query the database
                using (LingoContext dbContext = new LingoContext())
                {
                    List<LingoWord> lingoWords = (from lw
                                                  in dbContext.LingoWords
                                                  select lw).ToList();
                    if (dbContext.LingoWords.Count() > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        Console.WriteLine("No Linq results returned. Initializer probably didn't run.");
                        result = false; // just being explicit here
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;         // just being explicit here
            }
            return result;
        }
        private static string GetCategorySelection(string[] categories)
        {   // TODO: Write the menu system code that returns an appropriate (strong)selection
            Console.WriteLine("This is the menu system.");
            return categories[0];
        }
        public static bool IsCategory(LingoWord w, string c)
        {
            if (w.Category == c)
            {
                return true;
            }
            return false;
        }
    }
}
