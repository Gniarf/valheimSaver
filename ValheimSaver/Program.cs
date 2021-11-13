using System;
using System.IO;

namespace ValheimSaver
{
    class Program
    {
        private const string Source = @"C:\Users\garf\AppData\LocalLow\IronGate\Valheim";
        private const string MainSaveFolder = @"C:\Users\garf\OneDrive\Documents\Valheim\Sauvegardes";
        private const string DateFormat = "yyyyMMdd_hhmmss";
        static void Main(string[] args)
        {
            string saveFolder = Path.Combine(MainSaveFolder, DateTime.Now.ToString(DateFormat));
            EnsureTargetDirExists(saveFolder);
            SaveValheim(Source, saveFolder);
        }

        private static void EnsureTargetDirExists(string saveFolder)
        {
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }
        }

        private static void SaveValheim(string source, string saveFolder)
        {
            foreach (string file in Directory.GetFiles(source))
            {
                string sourceDirectory = Path.GetDirectoryName(file);
                string reduced = sourceDirectory.Substring(Source.Length).Trim(Path.DirectorySeparatorChar);
                string targetFolder = Path.Combine(saveFolder, reduced);
                EnsureTargetDirExists(targetFolder);
                File.Copy(file, Path.Combine(targetFolder, Path.GetFileName(file)));
            }
            foreach (string folder in Directory.GetDirectories(source))
            {
                SaveValheim(folder, saveFolder);
            }
        }
    }
}
