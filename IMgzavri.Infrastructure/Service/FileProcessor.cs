using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Infrastructure.Service
{
    public class FileProcessor : IFileProcessor
    {
        public bool SaveToFileSystemAsync(byte[] bytes, string path)
        {
            using (Stream file = new MemoryStream(bytes))
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                }
            }

            return FileHelper.Exists(path);
        }

        public byte[] ReadFileAsync(string path)
        {
            return  File.ReadAllBytes(path);
        }

        public MemoryStream DownloadAsync(string path)
        {
            var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }

            memory.Position = 0;

            return memory;
        }

        public void DeleteFilesIfExist(params string[] paths)
        {
            foreach (var path in paths)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}
