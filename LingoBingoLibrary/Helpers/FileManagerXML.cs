using LingoBingoLibrary.Collections;
using LingoBingoLibrary.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("LingoBingoLibraryUnitTests")]

namespace LingoBingoLibrary.Helpers
{
    public static class FileManagerXML
    {
        internal static FileInfo xmlStorageFile { get; private set; } //=> new FileInfo("LingoWords.xml");
        internal static FileInfo xmlBackupFile => new FileInfo("LingoWords.xml.bak");

        /// <summary>
        /// Returns the full path to an XML file with the filename argument in the current path.
        /// If not found, returns an empty string. An XML file extension is assumed.
        /// </summary>
        /// <returns></returns>
        internal static string FindFilename(string filename)
        {
            DirectoryInfo rootDirectory;

            try
            {
                rootDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            }
            catch
            {
                return string.Empty;
            }

            string result = string.Empty;
            var xmlFilename = new FileInfo($"{ filename }.xml");
            FileInfo[] files = rootDirectory.GetFiles("*.xml");  //    xmlStorageFile.Extension);

            if (files.Count() < 1)
            {
                return result;
            }

            foreach (FileInfo file in files)
            {
                if (file.Name == xmlFilename.Name)
                {
                    result = file.FullName;
                    break;
                }
            }
            
            return result;
        }

        /// <summary>
        /// Returns the full path to a default XML filename in the current path.
        /// If not found, returns an empty string.
        /// </summary>
        /// <returns></returns>
        internal static string FindDefaultFilename()
        {
            xmlStorageFile = new FileInfo("LingoWords");
            return FindFilename(xmlStorageFile.Name);
        }

        /// <summary>
        /// Loads a specified XML file into a Collection for other modules to use.
        /// Always returns an IEnumerable of type LingoWord but output should be checked for errors!
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<LingoWord> LoadLingoWords(string filepath)
        {
            FileInfo sourceFilepath = new FileInfo(filepath);
            XElement xe = null;

            try
            {
                using (StreamReader sr = File.OpenText(sourceFilepath.FullName))
                {
                    xe = XElement.Load(sr);
                }
            }
            catch
            {
                xe = new XElement(
                    new XElement("Words",
                        new XElement("Item",
                            new XElement("Category", "Error"),
                            new XElement("Word", $"Maybe the file could not be found?")
                    )));
            }

            IEnumerable<XElement> itemsXml = xe.Descendants("Item");

            IEnumerable<LingoWord> result = (from ix in itemsXml
                                             select new LingoWord()
                                             {
                                                 Category = ix.Element("Category").Value,
                                                 Word = ix.Element("Word").Value
                                             }).ToList();

            return result;
        }

        /// <summary>
        /// Extract all items for a given LingoWords collection and set them into XML elements.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        internal static XElement ConvertToXML(LingoWords collection)
        {
            XElement xe = new XElement("Words");

            for (int idx=0; idx < collection.Count; idx++)
            {
                xe.Add(new XElement("Item",
                    new XElement("Category", collection[idx].Category),
                    new XElement("Word", collection[idx].Word)
                    ));
            }

            return xe;
        }

        /// <summary>
        /// Take an existing list of Lingo Words and write them out to a local XML file, overwriting if already exists.
        /// Return true if completes without error, otherwise returns false.
        /// </summary>
        /// <param name="listOfLingoWords"></param>
        /// <returns></returns>
        internal static bool SaveToFile(XElement xElement)
        {
            bool succeeded = false;
            string saveFileFullname = FileManagerXML.xmlStorageFile.FullName;
            var sffn = new FileInfo(saveFileFullname);
            string backupFileFullname = Path.Combine(sffn.DirectoryName, xmlBackupFile.Name);
                                    
            try
            {
                if (File.Exists(backupFileFullname))
                {
                    File.Delete(backupFileFullname);
                }
                
                if (File.Exists(saveFileFullname))
                {
                    File.Move(saveFileFullname, backupFileFullname);
                }

                xElement.Save(saveFileFullname);
                succeeded = true;
            }
            catch
            {
                succeeded = false;
            }

            return succeeded;
        }

        /// <summary>
        /// Wrapper method takes an existing LingoWords collection and saves it to an XML file.
        /// Returns true if successful otherwise returns false.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        internal static bool SaveToFile(LingoWords collection)
        {
            if (collection.Count < 1)
            {
                return false;
            }

            XElement convertedCollection = ConvertToXML(collection);
            return SaveToFile(convertedCollection);
        }
    }
}
