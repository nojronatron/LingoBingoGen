using System;
using System.IO;

namespace LingoBingoLibrary.Helpers
{
    /// <summary>
    /// Stand-in for future move to plain-text JSON data formatted files.
    /// </summary>
    public static class FileManagerJSON
    {
        internal static FileInfo xmlStorageFile => new FileInfo("LingoWords.json");
    }
}
