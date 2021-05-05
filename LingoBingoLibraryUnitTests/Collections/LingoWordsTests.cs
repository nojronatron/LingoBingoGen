using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using LingoBingoLibrary.Models;

namespace LingoBingoLibrary.Collections.UnitTests
{
    [TestClass()]
    public class LingoWordsTests
    {
        internal List<LingoWord> TestSmallListLingoWords { get; private set; }
        internal List<LingoWord> TestLargeListLingoWords { get; private set; }

        [TestInitialize()]
        public void Setup()
        {
            TestSmallListLingoWords = new List<LingoWord>()
            {
                new LingoWord("alpha", "Category1"),
                new LingoWord("bravo", "Category1"),
                new LingoWord("charlie", "Category2"),
                new LingoWord("delta", "Category1"),
                new LingoWord("echo", "Category2")
            };
            TestLargeListLingoWords = new List<LingoWord>()
            {
                new LingoWord("alpha", "Category1"),
                new LingoWord("bravo", "Category1"),
                new LingoWord("charlie", "Category1"),
                new LingoWord("delta", "Category1"),
                new LingoWord("echo", "Category1"),
                new LingoWord("foxtrot", "Category1"),
                new LingoWord("golf", "Category1"),
                new LingoWord("hotel", "Category1"),
                new LingoWord("india", "Category1"),
                new LingoWord("juliet", "Category1"),
                new LingoWord("kilo", "Category1"),
                new LingoWord("lima", "Category1"),
                new LingoWord("mike", "Category1"),
                new LingoWord("november", "Category1"),
                new LingoWord("oscar", "Category1"),
                new LingoWord("papa", "Category1"),
                new LingoWord("quebec", "Category1"),
                new LingoWord("romeo", "Category1"),
                new LingoWord("sierra", "Category1"),
                new LingoWord("tango", "Category1"),
                new LingoWord("uniform", "Category1"),
                new LingoWord("victor", "Category1"),
                new LingoWord("whiskey", "Category1"),
                new LingoWord("xray", "Category1")
            };
        }

        [TestMethod()]
        public void LingoWordsBlankCTORTest()
        {
            var expectedResult = "LingoBingoLibrary.Collections.LingoWords";
            
            var lwCollection = new LingoWords();
            var actualResult = lwCollection.GetType().FullName;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void LingoWordsCtorArgsTest()
        {
            var expectedCount = 1;
            var expectedLingoWord = new LingoWord("args", "ctor");

            var lwCollection = new LingoWords(new List<LingoWord> { expectedLingoWord });
            var actualCount = lwCollection.Count;

            Assert.IsTrue(expectedCount == actualCount);

            foreach(var word in lwCollection)
            {
                Assert.IsTrue(word.Equals(expectedLingoWord));
            }
        }

        [TestMethod()]
        public void GetWordsInCategoryTest()
        {
            if (TestSmallListLingoWords.Count < 1)
            {
                Assert.Fail();
            }

            var expectedResult = new List<string>()
            {
                "alpha",
                "bravo",
                "delta"
            };

            var expectedCount = 3;
            var lwCollection = new LingoWords(TestSmallListLingoWords);
            var actualResult = lwCollection.GetWordsInCategory("Category1");

            Assert.IsTrue(actualResult.Count == expectedCount);

            for (int idx = 0; idx < actualResult.Count; idx++)
            {
                Assert.IsTrue(actualResult[idx] == expectedResult[idx]);
            }
        }

        [TestMethod()]
        public void GetWordsInCategoryLINQTest()
        {
            if (TestSmallListLingoWords.Count < 1)
            {
                Assert.Fail();
            }

            var category = "Category1";

            var expectedCount = 3;
            var expectedResult = new List<string>()
            {
                "Category1"
            };

            var lwCollection = new LingoWords(TestSmallListLingoWords);
            var actualResult = (from actualLw in lwCollection
                                where actualLw.Category.ToLowerInvariant() == category.ToLowerInvariant()
                                select actualLw)
                                .ToList();

            Assert.IsTrue(actualResult.Count == expectedCount);
        }

        [TestMethod()]
        public void AddCategoryTest()
        {
            if (TestSmallListLingoWords.Count < 1)
            {
                Assert.Fail();
            }

            var newCategory = "Category3";
            var newWord = "addCategoryTest";

            var lwCollection = new LingoWords(TestSmallListLingoWords);
            var startingCategories = lwCollection.Categories;
            var startingCategoriesCount = lwCollection.CategoryCount;

            var addCatActualResult = lwCollection.AddCategory(newCategory, newWord);

            Assert.IsTrue(addCatActualResult);

            var endingCategories = lwCollection.Categories;
            var endingCategoriesCount = lwCollection.CategoryCount;

            bool newCategoryAdded = false;
            var actualCategoryDiff = new List<string>();

            if (startingCategoriesCount > 0 && startingCategoriesCount + 1 == endingCategoriesCount)
            {
                newCategoryAdded = true;
                foreach(var item in endingCategories)
                {
                    if (startingCategories.Contains(item))
                    {
                        ;   //  pass
                    }
                    else
                    {
                        actualCategoryDiff.Add(item);
                        Console.WriteLine($"Lonely ending category item: { item }");
                    }
                }
            }
            else
            {
                newCategoryAdded = false;
            }

            Assert.IsTrue(newCategoryAdded);
            Assert.IsTrue(actualCategoryDiff.Count == 1);
        }

        [TestMethod()]
        public void AddLingoWordTest()
        {
            if (TestSmallListLingoWords.Count < 1)
            {
                Assert.Fail();
            }

            var existingCategory = "Category1";
            var newWord = "addWordTest";

            var lwCollection = new LingoWords(TestSmallListLingoWords);
            var startingWords = (from lwc in lwCollection
                                 where lwc.Category == existingCategory
                                 select lwc.Word)
                                 .ToList();

            var startingWordsCount = startingWords.Count;
            var addWordActualResult = lwCollection.AddLingoWord(existingCategory, newWord);

            Assert.IsTrue(addWordActualResult);

            var endingWords = (from lwc in lwCollection
                               where lwc.Category == existingCategory
                               select lwc.Word)
                               .ToList();

            var endingWordsCount =endingWords.Count;
            bool newWordAdded = false;
            var actualWordsDiff = new List<string>();

            if (startingWordsCount > 0 && startingWordsCount + 1 == endingWordsCount)
            {
                newWordAdded = true;
                foreach (var item in endingWords)
                {
                    if (startingWords.Contains(item))
                    {
                        ;   //  pass
                    }
                    else
                    {
                        actualWordsDiff.Add(item);
                        Console.WriteLine($"Lonely ending category item: { item }");
                    }
                }
            }
            else
            {
                newWordAdded = false;
            }

            Assert.IsTrue(newWordAdded);
            Assert.IsTrue(actualWordsDiff.Count == 1);
        }

        [TestMethod()]
        public void GetRandomWordsTest()
        {
            //  The collection enforces the rule that there must be at least 24 words in the selected category

            var diffCount = 0;  //  maintain a count of all words that do not match

            var lwCollection = new LingoWords(TestLargeListLingoWords);
            var startCount = lwCollection.Count;

            if (lwCollection.CategoryCount != 1 && startCount < 24)
            {
                Assert.Fail();
            }

            var actualResults = lwCollection.GetRandomWords("Category1");
            var actualResultsCount = actualResults.Count;

            Assert.IsTrue(actualResultsCount > 0);
            Assert.IsTrue(startCount == actualResultsCount);

            for (int idx=0; idx < startCount; idx++)
            {
                diffCount += (lwCollection[idx].Word != actualResults[idx]) ? 1 : 0;
            }

            Console.WriteLine($"Randomized positions: { diffCount }/{ actualResultsCount }");
            Assert.IsTrue(diffCount > 0);
        }

        [TestMethod()]
        public void GetListWithFreeSpaceTest()
        {
            var expectedText = "FREE";
            var freeSpaceIdx = 12;
            var expectedListCount = 25;

            var lwCollection = new LingoWords(TestLargeListLingoWords);
            var category1words = (from lw in lwCollection
                                  where lw.Category == "Category1"
                                  select lw.Word)
                                  .ToList();

            var succeeded = lwCollection.GetListWithFreeSpace(ref category1words);
            Assert.IsTrue(succeeded);

            var actualListCount = category1words.Count;
            Assert.AreEqual(expectedListCount, actualListCount);

            var actualFreeSpaceText = category1words[freeSpaceIdx];
            Assert.AreEqual(expectedText, actualFreeSpaceText);
        }
    }
}