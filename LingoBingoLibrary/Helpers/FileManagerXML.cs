using LingoBingoLibrary.Collections;
using LingoBingoLibrary.DataAccess;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("LingoBingoLibraryUnitTests")]

namespace LingoBingoLibrary.Helpers
{
    public class FileManagerXML
    {
        internal static FileInfo XmlStorageFile { get; private set; } 
        internal static FileInfo XmlBackupFile { get; private set; }

        public FileManagerXML()
        {
            XmlStorageFile = new FileInfo("LingoWords.xml");
            XmlBackupFile = new FileInfo("LingoWords.xml.bak");
        }

        public FileManagerXML(string filename)
        {
            XmlStorageFile = new FileInfo(filename);
            XmlBackupFile = new FileInfo($"{ XmlStorageFile.FullName }.bak");
        }

        public FileManagerXML(string filename, string backupFilename) : this (filename)
        {
            XmlBackupFile = new FileInfo(backupFilename);
        }

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
            
            if (files.Length < 1)
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
            XmlStorageFile = new FileInfo("LingoWords");
            return FindFilename(XmlStorageFile.Name);
        }

        /// <summary>
        /// Loads a specified XML file into a Collection for other modules to use.
        /// Always returns an IEnumerable of type LingoWord but output should be checked for errors!
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<LingoWord> LoadLingoWords(string filepath)
        {
            XElement xe = new XElement(
                        new XElement("Words",
                            new XElement("Item",
                                new XElement("Category", "Error"),
                                new XElement("Word", "Maybe the file could not be found?")
                        )));
            IEnumerable<LingoWord> result = null;

            if (string.IsNullOrEmpty(filepath) || string.IsNullOrWhiteSpace(filepath))
            {
                //  xe is already set to an error output
                ;
            }
            else
            {
                FileInfo sourceFilepath = null;

                sourceFilepath = new FileInfo(filepath);

                try
                {
                    using StreamReader sr = File.OpenText(sourceFilepath.FullName);
                    xe = XElement.Load(sr);
                }
                catch
                {
                    //  xe is already set to an error output
                    ;
                }
            }
            
            IEnumerable<XElement> itemsXml = xe.Descendants("Item");

            result = (from ix in itemsXml
                      select new LingoWord()
                      {
                          LingoCategory = new LingoCategory
                          {
                              Category = ix.Element("Category").Value
                          },
                          Word = ix.Element("Word").Value
                      }
                      ).ToList();

            return result;
        }

        /// <summary>
        /// Extract all items for a given LingoWords collection and set them into XML elements.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        internal static XElement ConvertToXML(LingoWordsCollection collection)
        {
            XElement xe = new XElement("Words");

            for (int idx=0; idx < collection.Count; idx++)
            {
                xe.Add(new XElement("Item",
                    new XElement("Category", collection[idx].LingoCategory.Category),
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
            if (FileManagerXML.XmlStorageFile == null)
            {
                return false;
            }

            bool succeeded = false;
            string saveFileFullname = FileManagerXML.XmlStorageFile.FullName;
            var sffn = new FileInfo(saveFileFullname);
            string backupFileFullname = Path.Combine(sffn.DirectoryName, XmlBackupFile.Name);
                                    
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
        internal static bool SaveToFile(LingoWordsCollection collection)
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
