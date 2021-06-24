﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using LingoBingoLibrary.Models;
using LingoBingoLibrary.DataAccess;

namespace LingoBingoLibrary.Collections.UnitTests
{
    [TestClass()]
    public class LingoWordsTests
    {
        internal List<LingoWord> TestSmallListLingoWords { get; private set; }
        internal List<LingoWord> TestLargeListLingoWords { get; private set; }
        internal int TestBoardSize => 25;

        [TestInitialize()]
        public void Setup()
        {
            TestSmallListLingoWords = new List<LingoWord>()
            {
                new LingoWord
                {
                    Word = "alpha",
                    LingoCategory = new LingoCategory
                    {
                        Category = "Category1"
                    }
                },
                new LingoWord{
                    Word = "bravo",
                    LingoCategory = new LingoCategory
                    {
                        Category ="Category1"
                    }
                },
                new LingoWord
                { Word= "charlie",
                    LingoCategory = new LingoCategory
                    {
                        Category ="Category2"
                    }
                },                
                new LingoWord{ Word = "delta", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "echo", LingoCategory = new LingoCategory { Category = "Category2" } }
            };
            TestLargeListLingoWords = new List<LingoWord>()
            {
                new LingoWord{ Word = "alpha", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "bravo", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "charlie", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "delta", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "echo", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "foxtrot", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "golf", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "hotel", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "india", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "juliet", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "kilo", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "lima", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "mike", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "november", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "oscar", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "papa", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "quebec", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "romeo", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "sierra", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "tango", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "uniform", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "victor", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "whiskey", LingoCategory = new LingoCategory { Category = "Category1" } },
                new LingoWord{ Word = "xray", LingoCategory = new LingoCategory { Category = "Category1" } }
            };
        }

        [TestMethod()]
        public void LingoWordsBlankCTORTest()
        {
            var expectedResult = "LingoBingoLibrary.Collections.LingoWords";
            
            var lwCollection = new LingoWordsCollection();
            var actualResult = lwCollection.GetType().FullName;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void LingoWordsCtorArgsTest()
        {
            var expectedCount = 1;
            var expectedLingoWord = new LingoWord{ Word = "args", LingoCategory = new LingoCategory { Category = "ctor" } };

            var lwCollection = new LingoWordsCollection(new List<LingoWord> { expectedLingoWord });
            var actualCount = lwCollection.Count<LingoWord>();

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
            var lwCollection = new LingoWordsCollection(TestSmallListLingoWords);
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

            var lwCollection = new LingoWordsCollection(TestSmallListLingoWords);
            var actualResult = (from actualLw in lwCollection
                                where actualLw.LingoCategory.Category.ToLowerInvariant() == category.ToLowerInvariant()
                                select actualLw)
                                .ToList();

            Assert.IsTrue(actualResult.Count == expectedCount);
        }

        //[TestMethod()]
        //public void AddCategoryTest()
        //{
        //    if (TestSmallListLingoWords.Count < 1)
        //    {
        //        Assert.Fail();
        //    }

        //    var newCategory = "Category3";
        //    var newWord = "addCategoryTest";

        //    var lwCollection = new LingoWordsCollection(TestSmallListLingoWords);
        //    var startingCategories = lwCollection.Categories;
        //    var startingCategoriesCount = lwCollection.CategoryCount;

        //    var addCatActualResult = lwCollection.AddCategory(newCategory, newWord);

        //    Assert.IsTrue(addCatActualResult);

        //    var endingCategories = lwCollection.Categories;
        //    var endingCategoriesCount = lwCollection.CategoryCount;

        //    bool newCategoryAdded = false;
        //    var actualCategoryDiff = new List<string>();

        //    if (startingCategoriesCount > 0 && startingCategoriesCount + 1 == endingCategoriesCount)
        //    {
        //        newCategoryAdded = true;
        //        foreach(var item in endingCategories)
        //        {
        //            if (startingCategories.Contains(item))
        //            {
        //                ;   //  pass
        //            }
        //            else
        //            {
        //                actualCategoryDiff.Add(item);
        //                Console.WriteLine($"Lonely ending category item: { item }");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        newCategoryAdded = false;
        //    }

        //    Assert.IsTrue(newCategoryAdded);
        //    Assert.IsTrue(actualCategoryDiff.Count == 1);
        //}

        [TestMethod()]
        public void AddLingoWordTest()
        {
            if (TestSmallListLingoWords.Count < 1)
            {
                Assert.Fail();
            }

            var existingCategory = "Category1";
            var newWord = "addWordTest";

            var lwCollection = new LingoWordsCollection(TestSmallListLingoWords);
            var startingWords = (from lwc in lwCollection
                                 where lwc.LingoCategory.Category == existingCategory
                                 select lwc.Word)
                                 .ToList();

            var startingWordsCount = startingWords.Count;
            var addWordActualResult = lwCollection.AddLingoWord(existingCategory, newWord);

            Assert.IsTrue(addWordActualResult);

            var endingWords = (from lwc in lwCollection
                               where lwc.LingoCategory.Category == existingCategory
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

            var lwCollection = new LingoWordsCollection(TestLargeListLingoWords);
            var startCount = lwCollection.Count<LingoWord>();

            if (lwCollection.CategoryCount != 1 && startCount < 24)
            {
                Assert.Fail();
            }

            var actualResults = lwCollection.GetRandomWords("Category1", TestBoardSize - 1);
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

            var lwCollection = new LingoWordsCollection(TestLargeListLingoWords);
            var category1words = (from lw in lwCollection
                                  where lw.LingoCategory.Category == "Category1"
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