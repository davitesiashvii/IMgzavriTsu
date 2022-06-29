using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Infrastructure.Service
{
    public interface IFileProcessor
    {
        bool SaveToFileSystemAsync(byte[] bytes, string path);

        byte[] ReadFileAsync(string path);

        MemoryStream DownloadAsync(string path);

        void DeleteFilesIfExist(params string[] paths);
    }
}
