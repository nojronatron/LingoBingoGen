using LingoBingoLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace LingoBingoLibrary.Helpers.UnitTests
{
    [TestClass()]
    public class FileManagerXMLTests
    {
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
            var expected = 
                @"testLingoWords.xml";
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
                new LingoWord("alpha", "Category1"),
                new LingoWord("bravo", "Category1"),
                new LingoWord("charlie", "Category2"),
                new LingoWord("delta", "Category1"),
                new LingoWord("echo", "Category2")
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
                new LingoWord("Maybe the file could not be found?", "Error")
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
                new LingoWord("Maybe the file could not be found?", "Error")
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
            Assert.Fail();
        }

        [TestMethod()]
        public void SaveToFileTest()
        {
            //  Complete this test after fully testing Collections/Lingowords.cs
            //  var thingy = FileManagerXML.SaveToFile();
            Assert.Fail();
        }

        [TestMethod()]
        public void SaveToFileTest1()
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