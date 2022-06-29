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

        FileStoreLinkResult GetFilePhysicalPath(Guid fileId);
        List<FileStoreLinkResult> GetFilesPhysicalPath(List<Guid> fileIds);
        FileSavingResult UploadFile(FileSavingModel file);
        List<FileSavingResult> UploadFiles(List<FileSavingModel> files);

    }
}
