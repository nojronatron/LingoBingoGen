using LingoBingoLibrary.Collections;
using LingoBingoLibrary.Helpers;
using System.Collections.Generic;
using System.IO;

namespace LingoBingoLibrary.CoreLibs
{
    /// <summary>
    /// Primary goal is to interface Collections, Helpers, and Model.
    /// Other calling Libraries use this class to get, set, update, and remove items accordinly.
    /// </summary>
    public class Generator
    {
        internal int DefaultBoardSize { get; private set; }
        internal LingoWordsCollection lingoWords { get; private set; }
        internal FileManagerXML fileManagerXml { get; private set; }
        public Generator()
        {
            lingoWords = new LingoWordsCollection();
            fileManagerXml = new FileManagerXML();
            DefaultBoardSize = 25;
        }

        /// <summary>
        /// Loads existing words and categories from a file into a collection.
        /// </summary>
        /// <returns></returns>
        public bool LoadWordPools()
        {
            FileInfo filename = FileManagerXML.XmlStorageFile;
            var collection = FileManagerXML.LoadLingoWords(filename.FullName);
            lingoWords = new LingoWordsCollection(collection);

            if (lingoWords.Count > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Save existing lingo words and categories to a file.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public bool SaveWordPools(LingoWordsCollection collection)
        {
            FileInfo filename = FileManagerXML.XmlStorageFile;
            return FileManagerXML.SaveToFile(collection);
        }

        /// <summary>
        /// Gets a randomly generated list of squares that a caller can use. Includes FREE space in middle index.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<string> GetNewBingoBoard(string category)
        {
            var randomizedList = lingoWords.GetRandomWords(category, DefaultBoardSize - 1);

            if (lingoWords.GetListWithFreeSpace(ref randomizedList))
            {
                return randomizedList;
            }

            return new List<string>();
        }
    }
}
