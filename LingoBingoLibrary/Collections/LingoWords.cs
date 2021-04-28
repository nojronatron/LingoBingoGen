using LingoBingoLibrary.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LingoBingoLibrary.Collections
{
    class LingoWords : IEnumerable<LingoWord>
    {
        private List<LingoWord> _lingoList;
        public LingoWords() 
        {
            _lingoList = new List<LingoWord>();
        }
        public LingoWords(IEnumerable<LingoWord> lingoWords)
        {
            _lingoList = new List<LingoWord>(lingoWords);
        }

        public int Count => _lingoList.Count;
        public int CategoryCount => (from lw in _lingoList select lw.Category).Distinct().Count();
        public IEnumerable<string> Categories => (from lw in _lingoList select lw.Category.ToLowerInvariant()).Distinct().ToList();
        public LingoWord this[int idx]
        {
            get
            {
                LingoWord result = default;
                if (idx < _lingoList.Count)
                {
                    result = _lingoList[idx];
                }
                return result;
            }
            set
            {
                _lingoList.Add(value);
            }
        }

        IEnumerator<LingoWord> IEnumerable<LingoWord>.GetEnumerator()
        {
            foreach (LingoWord result in _lingoList)
            {
                yield return result;
            }
        }

        /// <summary>
        /// Casting the object as an IEnumerable T is a guess on my part... short of implementing IEnumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<LingoWord>)_lingoList).GetEnumerator();
        }

        /// <summary>
        /// Returns a list that contains words from a single category.
        /// If category does not exist, returns an empty list.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<string> GetWordsInCategory(string category)
        {
            var categoryWords = new List<string>();

            foreach (var item in this)
            {
                if (item.Category == category.ToLowerInvariant())
                {
                    categoryWords.Add(item.Word);
                }
            }

            categoryWords.Sort();
            return categoryWords;
        }

        /// <summary>
        /// A word is required. Adds a Category that Lingo Words can be added to and returns true.
        /// If parameter category already exists or cannot be added the method will return false.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool AddCategory(string category, string word)
        {
            category = category.Trim();
            word = word.Trim();

            if (string.IsNullOrEmpty(category) || string.IsNullOrWhiteSpace(category) ||
                string.IsNullOrEmpty(word) || string.IsNullOrWhiteSpace(word))
            {
                return false;
            }

            if (this.Categories.Contains(category.ToLowerInvariant()))
            {
                return false;
            }

            _lingoList.Add(new LingoWord(word, category));
            return false;
        }

        /// <summary>
        /// Adds a word in an existing category to this collection. Returns true if process completes, otherwise returns false.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool AddLingoWord(string category, string word)
        {
            if (string.IsNullOrEmpty(category) || string.IsNullOrWhiteSpace(category) ||
                string.IsNullOrEmpty(word) || string.IsNullOrWhiteSpace(word))
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Returns a randomized list of words from a given category with 24 or more words.
        /// Returns an empty list if no such category is in this collection or the category has less than 25 words.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<string> GetRandomWords(string category)
        {
            var categoryWords = GetWordsInCategory(category);

            if (categoryWords.Count < 24)
            {
                return new List<string>();
            }

            string[] wordlist = categoryWords.ToArray();
            double[] order = new double[wordlist.Length];
            var rand = new Random();

            for (int counter=0; counter < wordlist.Length; counter++)
            {
                order[counter] = rand.NextDouble();
            }

            Array.Sort(order, wordlist);
            return new List<string>(wordlist);
        }

        /// <summary>
        /// Edits a 24-string list by inserting the word "FREE" at position 13 and returns true.
        /// If list of strings is longer or shorter than 24 the returns false and arg is not edited.
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public bool GetListWithFreeSpace(ref List<string> words)
        {
            bool result = false;

            if (words.Count != 24)
            {
                return result;
            }

            try
            {
                words.Insert(12, "FREE");
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

    }
}
