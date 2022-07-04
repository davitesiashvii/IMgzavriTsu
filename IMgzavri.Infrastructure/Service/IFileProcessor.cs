using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Infrastructure.Service
{
    public interface IFileProcessor
    {
        Task<bool> SaveToFileSystemAsync(byte[] bytes, string path);

        Task<byte[]> ReadFileAsync(string path);

        Task<MemoryStream> DownloadAsync(string path);

        void DeleteFilesIfExist(params string[] paths);
    }
}
