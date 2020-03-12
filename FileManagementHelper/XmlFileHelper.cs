using LingoBingoGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FileManagementHelper
{
    public static class XmlFileHelper
    {
        public static List<string> GetWordsInCategory(string category)
        {
            List<LingoWordModel> everything = new List<LingoWordModel>(GetDataFromXDocument());
            var query = from words in everything
                        where words.Category == category
                        select words.Word;
            List<string> categoricalWords = query.ToList();
            return categoricalWords;
        }

        public static List<ValueTuple<string, string>> GetWordsAndCategories()
        {
            List<ValueTuple<string, string>> result = null;
            List<LingoWordModel> LingoWordFileList = GetDataFromXDocument();
            foreach (LingoWordModel lwm in LingoWordFileList)
            {
                result.Add(new ValueTuple<string, string>(lwm.Category, lwm.Word));
            }
            return result;
        }

        public static List<string> GetCategories()
        {
            List<string> LingoCategories = new List<string>();
            List<LingoWordModel> LingoWordFileList = GetDataFromXDocument();
            foreach (LingoWordModel lwm in LingoWordFileList)
            {
                LingoCategories.Add(lwm.Category);
            }
            return LingoCategories;
        }

        public static List<string> GetWords()
        {
            List<string> LingoWords = new List<string>();
            List<LingoWordModel> LingoWordFileList = GetDataFromXDocument();
            foreach (LingoWordModel lwm in LingoWordFileList)
            {
                LingoWords.Add(lwm.Word);
            }
            return LingoWords;
        }

        private static List<LingoWordModel> GetDataFromXDocument()
        {
            //  string defaultFilename = "initialLingoWords.xml";
            string defaultFilename = "..\\..\\..\\FileManagementHelper\\DefaultLingoWords.xml";
            List<LingoWordModel> listWord = null;
            LingoWordModel item = null;
            try
            {
                // Load data from an XML file
                XDocument xWords = XDocument.Load(defaultFilename);

                // gather 'Item' elements from xDocument into an iterable collection
                IEnumerable<XElement> itemsXml = xWords.Descendants("Item");

                // Create a generic list of Words from XML elements in the iterable itemsXml
                listWord = new List<LingoWordModel>(itemsXml.Count());
                foreach (XElement xItem in itemsXml)
                {
                    item = new LingoWordModel()
                    {
                        Category = xItem.Element("Category").Value,
                        Word = xItem.Element("Word").Value
                    };
                    listWord.Add(item);
                }
            }
            catch (Exception ex)
            {
                string errorText = $"An exception occurred: { ex.Message } " +
                                   $"while reading XML from the file { defaultFilename }. " +
                                   $"Maybe the file could not be found?";
                item = new LingoWordModel();
                item.Category = errorText;
                item.Word = errorText;
                listWord = new List<LingoWordModel>();
                listWord.Add(item);
            }
            return listWord;
        }
    }
}
