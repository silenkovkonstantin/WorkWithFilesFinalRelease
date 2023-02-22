using System;
using System.IO;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к папке");
            string dir = Console.ReadLine();
            if (!string.IsNullOrEmpty(dir))
            {
                Console.WriteLine(CalcFolderSize(dir)); // Вызов метода вычисления размера папки
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
