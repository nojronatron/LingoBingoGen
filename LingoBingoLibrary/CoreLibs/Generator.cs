using LingoBingoLibrary.Collections;
using System.Collections.Generic;

namespace LingoBingoLibrary.CoreLibs
{
    /// <summary>
    /// Primary goal is to interface Collections, Helpers, and Model.
    /// Other calling Libraries use this class to get, set, update, and remove items accordinly.
    /// </summary>
    public class Generator
    {
        /// <summary>
        /// Loads existing words and categories from a file into a collection.
        /// </summary>
        /// <returns></returns>
        public bool LoadWordPools()
        {

            return false;
        }

        /// <summary>
        /// Save existing lingo words and categories to a file.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public bool SaveWordPools(LingoWords collection)
        {

            return false;
        }

        /// <summary>
        /// Gets a randomly generated list of squares that a caller can use.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<string> GetNewBingoBoard(string category)
        {

            return new List<string>();
        }
    }
}
