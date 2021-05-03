using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace LingoBingoLibrary.Helpers.UnitTests
{
    [TestClass()]
    public class FileManagerXMLTests
    {
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
            var expected ="LingoWords.xml";
            var longFilename = FileManagerXML.FindDefaultFilename();
            var filename = new FileInfo(longFilename);
            var actual = filename.Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LoadLingoWordsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ConvertToXMLTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SaveToFileTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SaveToFileTest1()
        {
            Assert.Fail();
        }
    }
}