using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    class Program
    {
        private static Student[] students; // Массив с данными студентов

        static void Main(string[] args)
        {
            Console.WriteLine("Укажите путь к файлу Students.dat");
            string StudentsData = Console.ReadLine();
            ReadData(StudentsData);
            CreateFilesByGroups();
            Console.ReadKey();
        }

        /// <summary>
        /// Читает данные студентов и возвращает массив
        /// </summary>
        /// <param name="studentsdata"></param>
        static void ReadData(string studentsdata)
        {
            try
            {
                if ((File.Exists(studentsdata)) || (studentsdata.IndexOfAny(Path.GetInvalidFileNameChars()) != -1))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    // десериализация
                    using (var fs = new FileStream(studentsdata, FileMode.OpenOrCreate))
                    {
                        students = (Student[])formatter.Deserialize(fs);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void CreateFilesByGroups()
        {
            try
            {
                DirectoryInfo studentsDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Students"); // Надеюсь для всех ОС будет работать

                if (!studentsDirectory.Exists)
                    studentsDirectory.Create();

                foreach (var st in students)
                {
                    FileInfo fileInfo = new FileInfo(studentsDirectory.FullName + "/" + st.Group + ".txt");

                    if (!fileInfo.Exists) // Проверим, существует ли файл по данному пути
                    {
                        //   Если не существует - создаём и записываем в строку
                        using (StreamWriter sw = fileInfo.CreateText())
                        {
                            sw.WriteLine(st.Name + ", " + st.DateOfBirth);
                        }
                    }
                    else // Если существует, то просто добавляем новую строку
                    {
                        using (StreamWriter sw = fileInfo.AppendText())
                        {
                            sw.WriteLine(st.Name + ", " + st.DateOfBirth);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
