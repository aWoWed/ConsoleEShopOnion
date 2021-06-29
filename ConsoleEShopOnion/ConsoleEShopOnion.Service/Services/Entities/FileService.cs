using System;
using System.IO;
using ConsoleEShopOnion.Service.Services.Abstract;

namespace ConsoleEShopOnion.Service.Services.Entities
{
    public class FileService : IFileService
    {
        /// <summary>
        /// Creates file if it does not exist. Opens file and reads its data
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>File and its data</returns>
        public static string Read(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException($"{nameof(fileName)} cannot be null");

            using var fileStream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Read);
            using var reader = new StreamReader(fileStream);
            return reader.ReadToEnd();
        }
        /// <summary>
        /// Writes text to file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="text"></param>
        public static void Write(string fileName, string text)
        {
            using var writer = new StreamWriter(fileName);
            writer.Write(text);
        }

        string IFileService.Read(string fileName) => Read(fileName);
        void IFileService.Write(string fileName, string text) => Write(fileName, text);
    }
}
