using System.IO;

namespace NotepadCsharp
{
    class FileManipulator
    {
        public static string Open(string path)
        {
            return File.ReadAllText(path);
        }
        public static void Save(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
