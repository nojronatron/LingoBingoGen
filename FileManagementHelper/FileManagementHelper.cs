using LingoBingoGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FileManagementHelper
{
    public static class FileManagementHelper
    {
        public static List<string> GetWordsInCategory(string category)
        {
            //  returns a list of existing categories in teh default XML file
            XElement xElements = new XElement(GetFileData());
            //  TODO: check for an empty XML if so return empty List<string>

            List<LingoWordModel> everything = new List<LingoWordModel>(ConvertToObjectList(xElements));
            var query = from words in everything
                        where words.Category == category
                        select words.Word;
            List<string> categoricalWords = query.ToList();
            return categoricalWords;
        }

        public static List<ValueTuple<string, string>> GetWordsAndCategories()
        {
            //  returns a tuple list containing values of category and word properties
            List<ValueTuple<string, string>> result = new List<ValueTuple<string, string>>();
            //XDocument xDoc = new XDocument(GetFileData());
            XElement xElements = new XElement(GetFileData());
            //  TODO: check for empty XML if so return List<ValueTuple<string, string>> containing errors

            List<LingoWordModel> LingoWordFileList = ConvertToObjectList(xElements);
            foreach (LingoWordModel lwm in LingoWordFileList)
            {
                result.Add(new ValueTuple<string, string>(lwm.Category, lwm.Word));
            }
            return result;
        }

        public static List<string> GetCategories()
        {
            //  returns a list of categories stored in the XML file
            List<string> LingoCategories = new List<string>();
            XElement xElements = new XElement(GetFileData());
            //  TODO: check for empty XML if so return List<string> containing error
            if (xElements == null)
            {
                LingoCategories.Add("Error: File empty or not found.");
            }
            else
            {
                List<LingoWordModel> LingoWordFileList = ConvertToObjectList(xElements);
                foreach (LingoWordModel lwm in LingoWordFileList)
                {
                    LingoCategories.Add(lwm.Category);
                }
            }
            return LingoCategories;
        }


        public static XElement MergeDocuments(XElement other)
        {
            XElement xElements = new XElement(GetFileData());
            xElements.Add(other);
            return xElements;
        }

        public static List<LingoWordModel> MergeObjectLists(List<LingoWordModel> objList1, List<LingoWordModel> objList2)
        {
            //  TODO: set back to private after unittesting
            //  merge existing List of LingoWordModel instance data with another 
            //  List of LingoWordModels returning a new List of LingoWordModels
            if (objList1.Count < 1 && objList2.Count < 1)
            {
                return new List<LingoWordModel>();
            }
            if (objList1.Count < 1) { return objList2; }
            if (objList2.Count < 1) { return objList1; }

            LingoWordModel newItem = null;
            foreach (LingoWordModel lwm in objList2)
            {
                newItem = new LingoWordModel();
                newItem.Word = lwm.Word;
                newItem.Category = lwm.Category;
                objList1.Add(newItem);
            }
            return objList1;
        }

        public static XElement ConvertToXElement(List<LingoWordModel> objectList)
        {
            //  TODO: set back to private after unittesting
            //  Takes a list of LingoWordmodel objects (with data) and converts to an XDocument (ready to write to a file)
            XElement xElement = new XElement("Root");
            for (int index = 0; index < objectList.Count; index++)
            {
                xElement.Add(new XElement("Item",
                    new XElement("Category", objectList[index].Category.ToString()),
                    new XElement("Word", objectList[index].Word.ToString())
                    ));
            }

            return xElement;
        }

        public static List<LingoWordModel> ConvertToObjectList(XElement xElement)
        {
            //  TODO: set back to private after unittesting
            //  takes XElement object and returns a List of LingoWordModel objects with data from input XDocument
            List<LingoWordModel> lingoWordObjects = new List<LingoWordModel>();
            LingoWordModel temp = null;

            IEnumerable<XElement> itemsXml = xElement.Descendants("Item");

            foreach (XElement xItem in itemsXml)
            {
                temp = new LingoWordModel();
                temp.Category = xItem.Element("Category").Value;
                temp.Word = xItem.Element("Word").Value;
                lingoWordObjects.Add(temp);
            }
            return lingoWordObjects;
        }

        public static bool AddWordsToCategoryList(List<string> words, string category)
        {
            //  take args and add to an existing Lingo Word List
            if (words.Count < 1 || string.IsNullOrEmpty(category))
            { 
                return false; 
            }
            LingoWordModel temp = null;
            List<LingoWordModel> wordList = new List<LingoWordModel>();

            foreach(string word in words)
            {
                temp = new LingoWordModel();
                temp.Category = category;
                temp.Word = word;
                wordList.Add(temp);
            }
            //  convert object list to XElement type
            XElement objWordsToAdd = ConvertToXElement(wordList);
            //  merge XElement LingoWords with existing LingoWords
            XElement mergedXElements = MergeDocuments(objWordsToAdd);
            if (UpdateFileData(mergedXElements))
            {
                //  xml file was updated with new list of words in new category
                //  caller can then use the updated xml file to select a category and create a new LingoBingo board
                return true;
            }
            return false;
        }

        public static bool AddNewCategory(string newCategory, string firstWord)
        {
            List<string> newWord = new List<string>();
            newWord.Add(firstWord);
            if (FileManagementHelper.AddWordsToCategoryList(newWord, newCategory))
            {
                return true;
            }
            return false;
        }

        public static XElement GetFileData(string filename = "")
        {
            //  TODO: set to private after unittesting
            string targetCWD = Directory.GetCurrentDirectory();

            if (filename == "")
            {
                filename = "LingoWords.xml";
            }
            string destFilename = Path.Combine(targetCWD, filename);

            XElement xElements = null;
            try
            {
                // Load data from an XML file
                using (StreamReader reader = File.OpenText(destFilename))
                {
                    xElements = XElement.Load(reader);
                }
            }
            catch (Exception ex)
            {
                xElements = new XElement(
                    new XElement("Words",
                        new XElement("Item",
                            new XElement("Category", "Error"),
                            new XElement("Word", $"Maybe the file could not be found?")
                    )));
            }
            return xElements;
        }

        public static bool UpdateFileData(XElement xElement, string filename = "")
        {
            //  TODO: set back to private after unittesting
            //  takes an XElement type and writes its contents to a file as XML, default suggested
            bool succeeded = false;
            string destCWD = Directory.GetCurrentDirectory();

            if (filename == "")
            {
                filename = "LingoWords.xml";
            }
            string backupFilename = Path.Combine(destCWD, $"{ filename }.bak");
            string destFilename = Path.Combine(destCWD, filename);

            try
            {
                if (File.Exists(backupFilename))
                {
                    File.Delete(backupFilename);
                }
                if (File.Exists(destFilename))
                {
                    File.Move(destFilename, backupFilename);
                }
                xElement.Save(destFilename);
                succeeded = true;
            }
            catch
            {
                succeeded = false;
            }
            return succeeded;
        }

        //public static bool DeployDefaultWordlistFile(string filename="")
        public static bool DeployDefaultWordlistFile()
        {
            //  reads built-in (default) XML file for starter words 
            //  if a LingoWords file does not already exist

            bool succeeded = false;
            string filename = "";
            string sourceCWD = "";
            string sourceFile = "";

            //  load the actual default xml file
            //sourceCWD = @"..\..\..\FileManagementHelper";
            sourceCWD = Directory.GetCurrentDirectory();
            sourceFile = "StaticDefaultWords.xml";
            filename = Path.Combine(sourceCWD, sourceFile);

            //  setup destination filename and path if not set in method arguments (e.g. unittesting)
            string destCWD = Directory.GetCurrentDirectory();
            string destFile = "LingoWords.xml";
            string destFilename = Path.Combine(destCWD, destFile);

            try
            {
                string backupFilename = Path.Combine(destCWD, $"{ destFile }.bak");
                XElement xEl = XElement.Load(filename);
                if (File.Exists(destFilename))
                {
                    if (File.Exists(backupFilename))
                    {
                        File.Delete(backupFilename);
                    }
                    File.Move(destFilename, backupFilename);
                }
                xEl.Save(destFilename);
                succeeded = true;
            }
            catch
            {
                succeeded = false;
            }
            return succeeded;
        }
    }
}
