using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к папке для очистки");
            string dir = Console.ReadLine();
            if (!string.IsNullOrEmpty(dir))
            {
                ClearFolder(dir); //   Вызов метода очистки папки
            }
        }

        static void ClearFolder(string dir)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);

                if ((dirInfo.Exists) || (dir.IndexOfAny(Path.GetInvalidPathChars()) != -1))
                {
                    DirectoryInfo[] dirsArray = dirInfo.GetDirectories(); // Получаем массив вложенных папок

                    foreach (var d in dirsArray) // Чистим от папок, к которым не обращались более 30 минут рекурсивно
                    {
                        if (DateTime.Now - d.LastAccessTime > TimeSpan.FromMinutes(30))
                        {
                            d.Delete(true);
                        }
                    }

                    FileInfo[] filesArray = dirInfo.GetFiles(); // Получаем массив файлов

                    foreach (var f in filesArray) // Чистит от файлов, к которым не обращались более 30 минут
                    {
                        if (DateTime.Now - f.LastAccessTime > TimeSpan.FromMinutes(30))
                        {
                            f.Delete();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Папка очищена");
            Console.ReadKey();
        }
    }
}