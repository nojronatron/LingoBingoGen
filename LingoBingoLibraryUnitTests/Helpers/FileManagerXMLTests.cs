using LingoBingoLibrary.Collections;
using LingoBingoLibrary.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace LingoBingoLibrary.Helpers.UnitTests
{
    [TestClass()]
    public class FileManagerXMLTests
    {
        internal List<LingoWord> TestSmallListLingoWords { get; private set; }
        internal XElement TestSmallXeLingoWords { get; private set; }

        [TestInitialize()]
        public void Setup()
        {
            TestSmallListLingoWords = new List<LingoWord>()
            {
                new LingoWord
                {
                    Word = "foxtrot",
                    LingoCategory = new LingoCategory{
                        Category ="Category1"
                    }
                },
                new LingoWord
                {
                    Word = "golf",
                    LingoCategory = new LingoCategory
                    {
                        Category ="Category1"
                    }
                },
                new LingoWord
                {
                    Word = "hotel",
                    LingoCategory = new LingoCategory
                    {
                        Category ="Category2"
                    }
                },
                new LingoWord
                {
                    Word = "india",
                    LingoCategory = new LingoCategory
                    {
                        Category = "Category1"
                    }
                },
                new LingoWord
                {
                    Word = "juliet",
                    LingoCategory = new LingoCategory
                    {
                        Category = "Category2"
                    }
                }
            };

            TestSmallXeLingoWords = new XElement(
                new XElement("Words",
                    new XElement("Item",
                        new XElement("Category", "Category1"),
                        new XElement("Word", "foxtrot")
                        ),
                    new XElement("Item",
                        new XElement("Category", "Category1"),
                        new XElement("Word", "golf")
                        ),
                    new XElement("Item",
                        new XElement("Category", "Category2"),
                        new XElement("Word", "hotel")
                        ),
                    new XElement("Item",
                        new XElement("Category", "Category1"),
                        new XElement("Word", "india")
                        ),
                    new XElement("Item",
                        new XElement("Category", "Category2"),
                        new XElement("Word", "juliet")
                        )
                    ));
        }

        [TestMethod()]
        public void DefaultCtorTest()
        {
            var expectedStorageFileName = "LingoWords.xml";
            var expectedBackupFileName = "LingoWords.xml.bak";

            var fmx = new FileManagerXML();

            var actualStorageFileName = FileManagerXML.XmlStorageFile.Name;
            var actualBackupFileName = FileManagerXML.XmlBackupFile.Name;

            Assert.AreEqual(expectedStorageFileName, actualStorageFileName);
            Assert.AreEqual(expectedBackupFileName, actualBackupFileName);
        }

        [TestMethod()]
        public void CtorParamsTest()
        {
            var expectedStorageFileName = "MyLingoWords.xml";
            var expectedBackupFileName = "MyXmlBackupFile.xml.bak";

            var fmx = new FileManagerXML(expectedStorageFileName, expectedBackupFileName);

            var actualStorageFileName = FileManagerXML.XmlStorageFile.Name;
            var actualBackupFileName = FileManagerXML.XmlBackupFile.Name;

            Assert.AreEqual(expectedStorageFileName, actualStorageFileName);
            Assert.AreEqual(expectedBackupFileName, actualBackupFileName);
        }

        [TestMethod()]
        public void FindFilenameTest()
        {
            var expected = @"testLingoWords.xml";
            var result = FileManagerXML.FindFilename("testLingoWords");
            var longFilename = new FileInfo(result);

            var actual = longFilename.Name;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void FindDefaultFilenameTest()
        {
            var fmx = new FileManagerXML();
            var expected ="LingoWords.xml";
            var longFilename = FileManagerXML.FindDefaultFilename();
            var filename = new FileInfo(longFilename);

            var actual = filename.Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LoadLingoWordsTest()
        {
            string currDirectory = Directory.GetCurrentDirectory();
            var filepath = Path.Join(currDirectory, "testLingoWords.xml");

            var expectedResult = new List<LingoWord>()
            {
                new LingoWord
                {
                    Word = "alpha",
                    LingoCategory = new LingoCategory
                    {
                        Category = "Category1"
                    }
                },                new LingoWord
                {
                    Word = "bravo",
                    LingoCategory = new LingoCategory
                    {
                        Category = "Category1"
                    }
                },                new LingoWord
                {
                    Word = "charlie",
                    LingoCategory = new LingoCategory
                    {
                        Category = "Category2"
                    }
                },                new LingoWord
                {
                    Word = "delta",
                    LingoCategory = new LingoCategory
                    {
                        Category = "Category1"
                    }
                },                new LingoWord
                {
                    Word = "echo",
                    LingoCategory = new LingoCategory
                    {
                        Category = "Category2"
                    }
                }
            };

            IEnumerable<LingoWord> actualResult = FileManagerXML.LoadLingoWords(filepath);
            var actualResultList = new List<LingoWord>(actualResult);

            for (int idx=0; idx < actualResultList.Count; idx++)
            {
                Assert.IsTrue(expectedResult[idx].Equals(actualResultList[idx]));
            }

            Assert.IsTrue(actualResultList.Count == expectedResult.Count);
        }

        [TestMethod()]
        public void LoadLingoWordsEmptyTest()
        {
            var filepath = " ";
            var expectedErrorResult = new List<LingoWord>()
            {
                new LingoWord
                {
                    Word = "Maybe the file could not be found?",
                    LingoCategory = new LingoCategory{
                        Category = "Error"
                    }
                }
            };

            IEnumerable<LingoWord> actualResult = FileManagerXML.LoadLingoWords(filepath);
            var actualResultList = new List<LingoWord>(actualResult);

            for (int idx=0; idx < actualResultList.Count; idx++)
            {
                Assert.IsTrue(expectedErrorResult[idx].Equals(actualResultList[idx]));
            }

            Assert.IsTrue(actualResultList.Count == expectedErrorResult.Count);
        }

        [TestMethod()]
        public void LoadLingoWordsNotfoundTest()
        {
            string currDirectory = Directory.GetCurrentDirectory();
            var filepath = Path.Join(currDirectory, "NoSuchWords.xml");

            var expectedErrorResult = new List<LingoWord>()
            {
                new LingoWord
                {
                    Word = "Maybe the file could not be found?",
                    LingoCategory = new LingoCategory
                    {
                        Category = "Error"
                    }
                }
            };

            IEnumerable<LingoWord> actualResult = FileManagerXML.LoadLingoWords(filepath);

            var actualResultList = new List<LingoWord>(actualResult);

            for (int idx = 0; idx < actualResultList.Count; idx++)
            {
                Assert.IsTrue(expectedErrorResult[idx].Equals(actualResultList[idx]));
            }

            Assert.IsTrue(actualResultList.Count == expectedErrorResult.Count);
        }

        [TestMethod()]
        public void ConvertToXMLTest()
        {
            //  Complete this test after fully testing Collections/LingoWords.cs
            //  XElement actualResult = FileManagerXML.ConvertToXML();
            var testCollection = new LingoWordsCollection(TestSmallListLingoWords);
            var fmx = new FileManagerXML();
            XElement actualResult = FileManagerXML.ConvertToXML(testCollection);

            System.Console.WriteLine(actualResult.ToString());
            System.Console.WriteLine(TestSmallXeLingoWords.ToString());

            Assert.AreEqual(TestSmallXeLingoWords, actualResult);
        }

        [TestMethod()]
        public void SaveToFileCollectionTest()
        {
            string[] files = null;
            string currSearchDirectory = Directory.GetCurrentDirectory();
            currSearchDirectory = Directory.GetParent(currSearchDirectory).FullName;
            string targetDirectory = Directory.GetCurrentDirectory();
            string prevDirectory = string.Empty;
            bool found = false;

            do
            {
                files = Directory.GetFiles(currSearchDirectory, "LingoWords.xml");
                
                if (files.Length > 0)
                {
                    found = true;
                }
                
                prevDirectory = currSearchDirectory;
                currSearchDirectory = Directory.GetParent(prevDirectory).FullName;
                
                if (currSearchDirectory == @"C:\")
                {
                    Assert.Fail();
                }

            } while (!found);

            var sourceFilePath = files[0];
            var targetFilePath = Path.Join(targetDirectory, "LingoWords.xml");
            var fmx = new FileManagerXML("LingoWords.xml");
            var testCollection = new LingoWordsCollection(TestSmallListLingoWords);
            var actualResult = FileManagerXML.SaveToFile(testCollection);

            if (actualResult)
            {
                var overwrite = true;
                File.Copy(sourceFilePath, targetFilePath, overwrite);
            }

            Assert.IsTrue(actualResult);
        }

        [TestMethod()]
        public void SaveToFileXMLTest()
        {
            string[] files = null;
            string currSearchDirectory = Directory.GetCurrentDirectory();
            currSearchDirectory = Directory.GetParent(currSearchDirectory).FullName;
            string targetDirectory = Directory.GetCurrentDirectory();
            string prevDirectory = string.Empty;
            bool found = false;

            do
            {
                files = Directory.GetFiles(currSearchDirectory, "LingoWords.xml");
                
                if (files.Length > 0)
                {
                    found = true;
                }
                
                prevDirectory = currSearchDirectory;
                currSearchDirectory = Directory.GetParent(prevDirectory).FullName;
                
                if (currSearchDirectory == @"C:\")
                {
                    Assert.Fail();
                }

            } while (!found);

            var sourceFilePath = files[0];
            var targetFilePath = Path.Join(targetDirectory, "LingoWords.xml");

            XElement xeInput = new XElement(
            new XElement("Words",
                new XElement("Item",
                    new XElement("Category", "Test"),
                    new XElement("Word", "SaveToFileTestOutput")
            )));

            var fmx = new FileManagerXML("LingoWords.xml");
            bool actualResult = FileManagerXML.SaveToFile(xeInput);

            if (actualResult)
            {
                var overwrite = true;
                File.Copy(sourceFilePath, targetFilePath, overwrite);
            }

            Assert.IsTrue(actualResult);
        }
    }
}