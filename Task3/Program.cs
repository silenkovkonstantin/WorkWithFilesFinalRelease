using System;
using System.IO;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к папка для очистки");
            string dir = Console.ReadLine();
            double sizeBefore;
            double sizeAfter;

            if (!string.IsNullOrEmpty(dir))
            {
                sizeBefore = CalcFolderSize(dir); // Вызов метода вычисления размера папки до очистки
                Console.WriteLine($"Исходный размер папки: {sizeBefore} байт");
                ClearFolder(dir); // Вызов метода очистки папки
                sizeAfter = CalcFolderSize(dir); // Вызов метода вычисления размера папки после очистки
                Console.WriteLine($"Освобождено: {sizeBefore - sizeAfter} байт");
                Console.WriteLine($"Текущий размер папки: {sizeAfter} байт");
            }
        }

        /// Метод рекурсивной очистки папки
        static void ClearFolder(string dir)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);

                if ((dirInfo.Exists) || (dir.IndexOfAny(Path.GetInvalidPathChars()) != -1))
                {
                    DirectoryInfo[] dirsArray = dirInfo.GetDirectories(); // Получаем массив вложенных папок

                    foreach (var d in dirsArray) // Чистим рекурсивно (параметр true) от папок, к которым не обращались более 30 минут
                    {
                        if (DateTime.Now - d.LastAccessTime > TimeSpan.FromMinutes(30))
                        {
                            d.Delete(true);
                        }
                    }

                    FileInfo[] filesArray = dirInfo.GetFiles(); // Получаем массив файлов

                    foreach (var f in filesArray)
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
        }

        /// Вычисляет размер папки
        static double CalcFolderSize(string dir)
        {
            double folderSize = 0;

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);

                if ((dirInfo.Exists) || (dir.IndexOfAny(Path.GetInvalidPathChars()) != -1))
                {
                    FileInfo[] filesArray = dirInfo.GetFiles(); // Получаем массив файлов

                    foreach (var f in filesArray)
                    {
                        folderSize += f.Length;
                    }

                    DirectoryInfo[] dirsArray = dirInfo.GetDirectories(); // Получаем массив вложенных папок

                    if (dirsArray.Length != 0)
                    {
                        foreach (var d in dirsArray) // Вычисляем размер каждой вложенной папки рекурсивно
                        {
                            folderSize += CalcFolderSize(d.FullName);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return folderSize;
        }
    }
}
