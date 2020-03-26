using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileManagementHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using LingoBingoGenerator;
using System.Text;
using System.IO;
using System.Threading;

namespace FileManagementHelper.Tests
{
    [TestClass()]
    public class FileManagementHelperTests
    {
        [TestMethod()]
        public void Test_GetWordsInCategory()
        {
            List<string> words = FileManagementHelper.GetWordsInCategory("CSharp");
            string keyWord = "method";
            bool expectedResult = true;
            bool actualResult = words.Contains(keyWord);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void Test_GetWordsAndCategories()
        {
            bool pass = false;
            List<ValueTuple<string, string>> wordsAndCategories = FileManagementHelper.GetWordsAndCategories();
            List<string> words = new List<string>();
            List<string> categories = new List<string>();

            foreach (ValueTuple<string, string> vtwc in wordsAndCategories)
            {
                words.Add(vtwc.Item2);
                categories.Add(vtwc.Item1);
            }

            List<string> distinctWords = new List<string>(words.Distinct());
            List<string> distictCategories = new List<string>(categories.Distinct());

            if (distinctWords.Count > 1 && distictCategories.Count > 0)
            {
                if (distinctWords.Count == words.Count)
                {
                    if (distictCategories.Count == 1)
                    {
                        pass = true;
                    }
                }
                DisplayGetWordsAndCategoriesResults(words, distinctWords, distictCategories);
            }
            Assert.AreEqual(true, pass);
        }

        [TestMethod()]
        public void Test_GetWordsAndCategories_EmptyReturnsBlank()
        {
            List<ValueTuple<string, string>> wordsAndCategories = FileManagementHelper.GetWordsAndCategories("emptyLingoWords.xml");
            foreach (ValueTuple<string, string> vtwc in wordsAndCategories)
            {
                Assert.AreEqual(string.IsNullOrEmpty(vtwc.Item1), string.IsNullOrEmpty(vtwc.Item2));
            }
        }

        [TestMethod()]
        public void Test_GetWordsAndCategories_BadFilenameReturnsErrorCategory()
        {
            List<ValueTuple<string, string>> wordsAndCategories = FileManagementHelper.GetWordsAndCategories("nonExistingFile.xml");
            foreach (ValueTuple<string, string> vtwc in wordsAndCategories)
            {
                Assert.IsTrue(vtwc.Item1.ToString().ToUpper() == "error".ToUpper());
            }
        }

        [TestMethod()]
        public void Test_GetCategories()
        {
            List<string> categories = FileManagementHelper.GetCategories();
            List<string> distinctCategories = null;

            string expectedCategory = "CSharp";

            if (categories != null)
            {
                distinctCategories = new List<string>(categories.Distinct());
                Assert.AreEqual(distinctCategories.Count, 1);
                Assert.AreEqual(expectedCategory, distinctCategories[0]);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void Test_GetWordsInCategory1()
        {
            string category = "CSharp";
            string filename = "LingoWords.xml";
            string cwd = Directory.GetCurrentDirectory();
            string fullFilePath = Path.Combine(cwd, filename);

            XElement xElements = null;
            List<string> words = FileManagementHelper.GetWordsInCategory(category);
            using (StreamReader reader = File.OpenText(fullFilePath))
            {
                xElements = XElement.Load(reader);
            }

            IEnumerable<XElement> itemsXml = xElements.Descendants("Item");
            List<string> baseWords = new List<string>();
            foreach (XElement item in itemsXml)
            {
                if (item.Element("Category").Value == "CSharp") { 
                    baseWords.Add(item.Element("Word").Value); 
                }
            }
            var firstNotSecond = words.Except(baseWords).ToList();
            var secondNotFirst = baseWords.Except(words).ToList();

            Assert.IsTrue(firstNotSecond.Count == 0 && secondNotFirst.Count == 0);
        }

        [TestMethod()]
        public void Test_GetWordsInCategory_InvalidIsZero()
        {
            List<string> words = null;
            words = FileManagementHelper.GetWordsInCategory("alskdh");
            int expectedResult = 0;
            int actualResult = words.Count;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void Test_MergeDocuments()
        {
            XElement expectedResult = new XElement("Words",
                new XElement("Item",
                    new XElement("Category", "Merge"),
                    new XElement("Word", "one")),
                new XElement("Item",
                    new XElement("Category", "Merge"),
                    new XElement("Word", "other"))
                );

            XElement inputOne = new XElement(
                new XElement("Words",
                    new XElement("Item",
                        new XElement("Category", "Merge"),
                        new XElement("Word", "one")
                    )));

            XElement inputTwo = new XElement(
                new XElement("Words",
                    new XElement("Item",
                        new XElement("Category", "Merge"),
                        new XElement("Word", "other")
                    )));

            XElement actualResult = FileManagementHelper.MergeDocuments(inputOne, inputTwo);

            if (actualResult.IsEmpty || null == actualResult)
            {
                Assert.Fail($"actualResult was null or empty. Merge failed.");
            }
                //  only one input argument element was returned so fail the test
                //    Assert.Fail($"First Node == Last Node. Merge failed.");
                //}
            Assert.AreEqual(true, XNode.DeepEquals(expectedResult, actualResult));
                //else if (expectedResult.FirstNode.ToString() == actualResult.FirstNode.ToString())
                //{
                //    if (expectedResult.LastNode.ToString() != actualResult.LastNode.ToString())
                //    {
                //        if (expectedResult.LastNode.PreviousNode.ToString() == actualResult.LastNode.PreviousNode.ToString())
                //        {
                //            mergeSucceeded = true;
                //        }
                //    }
                //}
        }

        [TestMethod()]
        public void Test_MergeObjectLists()
        {
            LingoWordModel lwm = null;
            List<LingoWordModel> objList1 = new List<LingoWordModel>();
            List<LingoWordModel> objList2 = new List<LingoWordModel>();

            //  Example:
            //  Category 1, Word 1
            //  Category 2, Word 2
            //  Category 3, Word 3
            //  Category 4, Word 4

            for (int count = 1; count <= 4; count++)
            {
                lwm = new LingoWordModel();
                lwm.Category = $"Category { count }";
                lwm.Word = $"Word { count }";
                if (count % 2 != 0)
                {
                    objList1.Add(lwm);
                }
                else
                {
                    objList2.Add(lwm);
                }
            }

            StringBuilder expectedResult = new StringBuilder();
            expectedResult.Append("Category 1, ");
            expectedResult.AppendLine("Word 1");
            expectedResult.Append("Category 2, ");
            expectedResult.AppendLine("Word 2");
            expectedResult.Append("Category 3, ");
            expectedResult.AppendLine("Word 3");
            expectedResult.Append("Category 4, ");
            expectedResult.AppendLine("Word 4");

            List<LingoWordModel> mergedLists = FileManagementHelper.MergeObjectLists(objList1, objList2);

            StringBuilder actualResult = new StringBuilder();
            foreach(LingoWordModel lwms in mergedLists)
            {
                actualResult.Append($"{ lwms.Category }, ");
                actualResult.AppendLine($"{ lwms.Word }");
            }
            Console.WriteLine($"Expected\n" +
                              $"Output: { expectedResult.ToString() }\n" +
                              $"Length: { expectedResult.ToString().Length }\n" +
                              $"Actual:\n" +
                              $"Output: { actualResult.ToString() }\n" +
                              $"Length: { actualResult.ToString().Length }\n");

            Assert.IsTrue(expectedResult.ToString().Length == actualResult.ToString().Length);
        }

        [TestMethod()]
        public void Test_GetFileData()
        {
            //  TODO: Write a better test to actually discover if correct file, data were loaded
            string cwd = Directory.GetCurrentDirectory();
            string filename = "LingoWords.xml";
            string fullFilePath = Path.Combine(cwd, filename);

            XElement originalFile = FileManagementHelper.GetFileData(filename);
            XElement xDocFile = FileManagementHelper.GetFileData();

            IEnumerable<XElement> errorMessage = from el in originalFile.Elements()
                                                 where el.Attribute("Category").Value == "Error"
                                                 select el;

            Console.WriteLine($"errorMessage output:\n{ errorMessage.ToString() }\n" +
                              $"originalFile Output:\n{ originalFile.ToString() }\n" +
                              $"xDocFile Output:\n{ xDocFile.ToString() }");

            Assert.AreNotEqual(errorMessage.ToString(), originalFile.ToString());

            //  If the file couldn't be loaded then the returned XElement will contain the following
            /*
            new XElement("Words",             
                new XElement("Item",
                        new XElement("Category", "Error"),
                        new XElement("Word", $"Maybe the file could not be found?")
                        ));
             */
        }

        [TestMethod()]
        public void Test_UpdateFileData()
        {
            bool fileCreated = false;
            XElement document = new XElement(
                new XElement("Root",
                    new XElement("Category", "Merge"),
                    new XElement("Word", "test")
                    ));

            string dstFilename = "Test_UpdateFileData_LingoWords.xml";
            bool operationOutput = FileManagementHelper.UpdateFileData(document, dstFilename);
            //                       document, filename: "..\\..\\..\\UnitTester\\UpdateFileDataOutput.xml");

            string cwd = Directory.GetCurrentDirectory();
            string dstFullFilePath = Path.Combine(cwd, dstFilename);

            if (File.Exists(dstFullFilePath))
            {
                fileCreated = true;
                Thread.Sleep(5000);
                File.Delete(dstFullFilePath);
            }
            Assert.IsTrue(fileCreated);
        }

        [TestMethod()]
        public void Test_ConvertToXElement()
        {
            XElement expectedResult = new XElement(
                new XElement("Words",
                    new XElement("Item",
                        new XElement("Category", "ConvertToXEl 1"),
                        new XElement("Word", "Word 1")
                        ),
                    new XElement("Item",
                        new XElement("Category", "ConvertToXEl 2"),
                        new XElement("Word", "Word 2")
                        ),
                    new XElement("Item",
                        new XElement("Category", "ConvertToXEl 3"),
                        new XElement("Word", "Word 3")
                        ),
                    new XElement("Item",
                        new XElement("Category", "ConvertToXEl 4"),
                        new XElement("Word", "Word 4")
            )));

            List<LingoWordModel> objectList = null;
            LingoWordModel lwm = null;

            objectList = new List<LingoWordModel>();
            for(int index=1; index <= 4; index++)
            {
                lwm = new LingoWordModel();
                lwm.Category = $"ConvertToXEl { index }";
                lwm.Word = $"Word { index }";
                objectList.Add(lwm);
            }

            XElement actualResult = FileManagementHelper.ConvertToXElement(objectList);

            Console.WriteLine(
                $"Expected\n" +
                $"Output: \n{ expectedResult.ToString() }\n" +
                $"Length: \n{ expectedResult.ToString().Length }\n" +
                $"\nActual:\n" +
                $"Output: \n{ actualResult.ToString() }\n" +
                $"Length: \n{ actualResult.ToString().Length }\n"
                );

            Assert.IsTrue(expectedResult.ToString().Length == actualResult.ToString().Length);
        }

        [TestMethod()]
        public void Test_ConvertToObjectList()
        {
            List<LingoWordModel> expectedResult = new List<LingoWordModel>(2);
            LingoWordModel lwm = null;
            for(int index=1; index<3; index++)
            {
                lwm = new LingoWordModel();
                lwm.Category = $"Convert { index }";
                lwm.Word = $"Alpha { index }";
                expectedResult.Add(lwm);
            }

            XElement xEl =
                new XElement(
                    new XElement("Root",
                        new XElement("Item",
                            new XElement("Category", "Convert 1"),
                            new XElement("Word", "Alpha 1")
                            ),
                        new XElement("Item",
                            new XElement("Category", "Convert 2"),
                            new XElement("Word", "Alpha 2")
            )));

            int matchCount = 0;
            int totalCount = 0;

            List<LingoWordModel> actualResult = FileManagementHelper.ConvertToObjectList(xEl);
            if (actualResult.Count > 0)
            {
                for (int jindex = 0; jindex < actualResult.Count; jindex++)
                {
                    if (expectedResult[jindex].Category == actualResult[jindex].Category)
                    {
                        matchCount++;
                    }
                    totalCount++;
                    if (expectedResult[jindex].Word == actualResult[jindex].Word)
                    {
                        matchCount++;
                    }
                    totalCount++;
                }
            }

            Assert.AreEqual(expectedResult.Count, actualResult.Count);  //  if equal than both lists have same number of objects
            Assert.AreEqual(totalCount, matchCount);    //  if equal than all if conditionals returned true
        }

        [TestMethod()]
        public void Test_ConvertToObjectList_EmptyReturnsEmpty()
        {
            XElement xe = null;
            List<LingoWordModel> actualResult = FileManagementHelper.ConvertToObjectList(xe);
            List<LingoWordModel> expectedResult = new List<LingoWordModel>();
            Assert.AreEqual(expectedResult.Count, actualResult.Count);
            Assert.AreEqual(0, actualResult.Count);
        }

        [TestMethod]
        public void Test_DeployDefaultWordlistFile()
        {
            StringBuilder sb = new StringBuilder();
            string sourceFilename = @"..\..\..\FileManagementHelper\StaticDefaultWords.xml";

            string destCWD = Directory.GetCurrentDirectory();
            string destFile = "LingoWords.xml";
            string destFilename = Path.Combine(destCWD, destFile);
            string backupFile = $"{ destFile }.bak";

            sb.AppendLine(CheckFileExists("Source file", sourceFilename, "", "Test will fail."));
            sb.AppendLine(CheckFileExists("Destination file", destFile, "It will be overwritten", "It will be created"));
            sb.AppendLine(CheckFileExists("Backup file", backupFile, "It will be overwritten", "It could get created"));

            bool result = FileManagementHelper.DeployDefaultWordlistFile();

            sb.AppendLine(CheckFileExists("Source file", sourceFilename, "", "Test will fail."));
            sb.AppendLine(CheckFileExists("Destination file", destFile, "It could have been overwritten", "It was created"));
            sb.AppendLine(CheckFileExists("Backup file", backupFile, "", ""));

            Console.WriteLine(sb.ToString());

            Assert.IsTrue(result);
        }

        private string CheckFileExists(string filetype, string filename, string passMsg, string failMsg)
        {
            string result = string.Empty;
            if (File.Exists(filename))
            {
                result = $"Pass Msg: { filetype } { filename } exists. { passMsg }.";
            }
            else
            {
                result = $"Fail Msg: { filetype } { filename } not found! { failMsg }.";
            }
            return result;
        }

        private static void DisplayGetWordsAndCategoriesResults(List<string> words, List<string> distinctWords, List<string> distictCategories)
        {
            Console.WriteLine("W in Words:");
            foreach (string w in words)
            {
                Console.Write($"{ w }, ");
            }
            Console.WriteLine("\nDW in DistinctWords:");
            foreach (string dw in distinctWords)
            {
                Console.Write($"{ dw }, ");
            }
            Console.WriteLine("\nDC in DistinctCategories");
            foreach (string dc in distictCategories)
            {
                Console.Write($"{ dc }, ");
            }
            Console.WriteLine();
        }
    }
}