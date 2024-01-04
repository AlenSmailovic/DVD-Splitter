using System.Collections.Generic;
using System.IO;

namespace CD_DVD_Maker
{
    class Program
    {
        const long DVL = 4089446400;    // 3.9 Gb
        const long DVD = 4508876800;    // 4.3 Gb
        const long CD = 681574400;      // 650 Mb
        static void Main(string[] args)
        {
            //string folderName = @"D:\Poze\2021";
            string folderName = args[1];

            long SIZE = DVD;
            if (args[2] == "CD") SIZE = CD;
            if (args[2] == "DVD") SIZE = DVD;
            if (args[2] == "DVL") SIZE = DVL;

            DirectoryInfo d = new DirectoryInfo(folderName);
            FileInfo[] Files = d.GetFiles("*.*");
            List<List<FileInfo>> Directories = new List<List<FileInfo>>();

            int file_index = 0;
            while (file_index < Files.Length)
            {
                long dir_size = 0;
                List<FileInfo> Dir = new List<FileInfo>();
                do
                {
                    dir_size += Files[file_index].Length;
                    if (dir_size < SIZE)
                    {
                        Dir.Add(Files[file_index]);
                        file_index++;
                    }
                    else
                    {
                        break;
                    }
                } while (file_index < Files.Length);

                Directories.Add(Dir);
            }

            for(int index = 0; index < Directories.Count; index++)
            {
                string pathString = Path.Combine(folderName, (index + 1).ToString());
                Directory.CreateDirectory(pathString);

                foreach(FileInfo file in Directories[index])
                {
                    string sourceFile = file.FullName;
                    string destinationFile = Path.Combine(pathString, file.Name);

                    File.Move(sourceFile, destinationFile);
                }
            }
        }
    }
}
