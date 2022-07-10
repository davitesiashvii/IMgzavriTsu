using IMgzavri.Domain.FileStorage;
using IMgzavri.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Infrastructure.Service
{
    public class FileStorageService : IFileStorageService
    {
        protected readonly IMgzavriDbContext context;
        private readonly IFileProcessor FileProcessor;

        public FileStorageService(IMgzavriDbContext context, IFileProcessor fileProcessor)
        {
            this.context = context;
            this.FileProcessor = fileProcessor;
        }

        public async Task<FileStoreLinkResult> GetFilePhysicalPath(Guid fileId)
        {
            var file = await context.Files.FirstOrDefaultAsync(x=>x.Id == fileId);
            var checkResult = FileExists(file);
            if (!checkResult.Exists)
                throw new Exception("File not found");

            var fileStoreLinkResult = new FileStoreLinkResult(file.CorrelationId, FileHelper.BuildPathForFileServer(file));
            if (fileStoreLinkResult != null)
                fileStoreLinkResult.Link += file.Extension;
            return fileStoreLinkResult;
        }

        public async Task<List<FileStoreLinkResult>> GetFilesPhysicalPath(List<Guid> fileIds)
        {
            var files = context.Files.Where(x => fileIds.Contains(x.Id)).ToList();
            var fileStoreLinkResults = files.Select(
                x => {
                    var z = new FileStoreLinkResult(
                   x.CorrelationId,
                   FileHelper.BuildPathForFileServer(x));
                    if (z != null)
                        z.Link += x.Extension;
                    return z;
                    }
                    ).ToList();

            return fileStoreLinkResults;
        }

        public async Task<FileSavingResult> UploadFile(FileSavingModel file)
        {

            var savedFilePaths = new List<string>();
            var savedFileIds = new List<Guid>();

            var fileToSave = IMgzavri.Domain.Models.File.Create(file.Name, file.Extension, file.ContentType, file.Size, file.CreatorId, file.CorrelationId);

            var savingPath = FileHelper.BuildPath(fileToSave);

            if (!FileHelper.Exists(savingPath))
            {
                var isSuccess = await FileProcessor.SaveToFileSystemAsync(file.File, savingPath);

                if (!isSuccess)
                {
                    if (savedFilePaths.Count > 0)
                        FileProcessor.DeleteFilesIfExist(savedFilePaths.ToArray());

                    throw new Exception("sfd");
                }

                savedFilePaths.Add(savingPath);
                savedFileIds.Add(fileToSave.Id);

                context.Files.AddRange(fileToSave);
            }

            var result = context.SaveChanges();

            if (result == 0)
            {
                FileProcessor.DeleteFilesIfExist(savedFilePaths.ToArray());

                throw new Exception();
            }


            var fileSavingResult = new FileSavingResult(savedFileIds[0], fileToSave.CorrelationId);
            return fileSavingResult;
        }

        public async Task<List<FileSavingResult>> UploadFiles(List<FileSavingModel> files)
        {
            var savedFilePaths = new List<string>();
            var savingResults = new List<FileSavingResult>();

            foreach (var file in files)
            {
                var fileToSave = IMgzavri.Domain.Models.File.Create(file.Name, file.Extension, file.ContentType, file.Size, file.CreatorId, file.CorrelationId);

                var savingPath = FileHelper.BuildPath(fileToSave);

                if (!FileHelper.Exists(savingPath))
                {
                    var isSuccess = await FileProcessor.SaveToFileSystemAsync(file.File, savingPath);

                    if (!isSuccess)
                    {
                        if (savedFilePaths.Count > 0)
                             FileProcessor.DeleteFilesIfExist(savedFilePaths.ToArray());

                        throw new Exception("sfd");
                    }

                    savedFilePaths.Add(savingPath);
                    savingResults.Add(new FileSavingResult(fileToSave.Id, fileToSave.CorrelationId));

                    context.Files.AddRange(fileToSave);
                }
            }

            var result = context.SaveChanges();

            if (result == 0)
            {
                FileProcessor.DeleteFilesIfExist(savedFilePaths.ToArray());

                throw new Exception();
            }

            return savingResults;
        }


        protected CheckResult FileExists(IMgzavri.Domain.Models.File file)
        {
            var result = new CheckResult
            {
                Exists = true
            };

            result.File = file;

            if (file == null)
            {
                result.Exists = false;
            }

            var path = FileHelper.BuildPath(file, "D:\\IMgzavri", "Files");

            if (!FileHelper.Exists(path))
            {
                result.Exists = false;
            }

            result.Path = path;

            return result;
        }

        public string GetImagelinkToMainImageId(Guid mainImageId)
        {
            FileStoreLinkResult fmRes = null;
            try
            {
                fmRes = this.GetFilePhysicalPath(mainImageId).GetAwaiter().GetResult();
            }
            catch { return null; }

            return fmRes.Link;
        }

        public List<string> GetImagelinksToCarId(Guid carId)
        {
            var fmRes = new List<FileStoreLinkResult>();
            try
            {
                fmRes = this.GetFilesPhysicalPath(context.CarImages.Where(x => x.CarId == carId).Select(z => z.ImageId).ToList()).GetAwaiter().GetResult();
            }
            catch { return null; }

            return fmRes.Select(x => x.Link).ToList();
        }

        public string GetImagelinkToCarId(Guid carId)
        {
            FileStoreLinkResult fmRes = null;
            try
            {
                fmRes = this.GetFilePhysicalPath(context.Cars.FirstOrDefault(x => x.Id == carId).MainImageId.Value).GetAwaiter().GetResult();
            }
            catch { return ""; }

            return fmRes.Link;
        }

        public List<string> GetProfileImagelinksToCarId(Guid userId, int type)
        {
            var fmRes = new List<FileStoreLinkResult>();
            try
            {
                fmRes = this.GetFilesPhysicalPath(context.ProfileImages.Where(x => x.UserId == userId && x.Type == type).Select(z => z.ImageId).ToList()).GetAwaiter().GetResult();
            }
            catch { return null; }

            return fmRes.Select(x => x.Link).ToList();
        }







    }
}
