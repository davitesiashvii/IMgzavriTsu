using IMgzavri.Domain.FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Infrastructure.Service
{
    public interface IFileStorageService
    {

        Task<FileStoreLinkResult> GetFilePhysicalPath(Guid fileId);
        Task<List<FileStoreLinkResult>> GetFilesPhysicalPath(List<Guid> fileIds);
        Task<FileSavingResult> UploadFile(FileSavingModel file);
        Task<List<FileSavingResult>> UploadFiles(List<FileSavingModel> files);

    }
}
