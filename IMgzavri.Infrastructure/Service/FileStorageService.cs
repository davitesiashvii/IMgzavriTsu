using IMgzavri.Domain.FileStorage;
using IMgzavri.Infrastructure.Db;
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

        public FileStoreLinkResult GetFilePhysicalPath(Guid fileId)
        {
            var file = context.Files.FirstOrDefault(x=>x.Id == fileId);
            var checkResult = FileExists(file);
            if (!checkResult.Exists)
                throw new Exception("File not found");

            var fileStoreLinkResult = new FileStoreLinkResult(file.CorrelationId, FileHelper.BuildPathForFileServer(file));
            return fileStoreLinkResult;
        }

        public List<FileStoreLinkResult> GetFilesPhysicalPath(List<Guid> fileIds)
        {
            var files = context.Files.Where(x => fileIds.Contains(x.Id)).ToList();
            var fileStoreLinkResults = files.Select(x => new FileStoreLinkResult(
                x.CorrelationId,
                FileHelper.BuildPathForFileServer(x))).ToList();

            return fileStoreLinkResults;
        }

        public FileSavingResult UploadFile(FileSavingModel file)
        {

            var savedFilePaths = new List<string>();
            var savedFileIds = new List<Guid>();

            var fileToSave = IMgzavri.Domain.Models.File.Create(file.Name, file.Extension, file.ContentType, file.Size, file.CreatorId, file.CorrelationId);

            var savingPath = FileHelper.BuildPath(fileToSave);

            if (!FileHelper.Exists(savingPath))
            {
                var isSuccess =  FileProcessor.SaveToFileSystemAsync(file.File, savingPath);

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

        public List<FileSavingResult> UploadFiles(List<FileSavingModel> files)
        {
            var savedFilePaths = new List<string>();
            var savingResults = new List<FileSavingResult>();

            foreach (var file in files)
            {
                var fileToSave = IMgzavri.Domain.Models.File.Create(file.Name, file.Extension, file.ContentType, file.Size, file.CreatorId, file.CorrelationId);

                var savingPath = FileHelper.BuildPath(fileToSave);

                if (!FileHelper.Exists(savingPath))
                {
                    var isSuccess = FileProcessor.SaveToFileSystemAsync(file.File, savingPath);

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

            var path = FileHelper.BuildPath(file, "D:\\IRecommend", "Files");

            if (!FileHelper.Exists(path))
            {
                result.Exists = false;
            }

            result.Path = path;

            return result;
        }



    }
}
