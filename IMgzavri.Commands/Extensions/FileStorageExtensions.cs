using IMgzavri.Commands.Models.ResponceModels;
using IMgzavri.Shared.Constants;
using IMgzavri.Shared.Domain.Models;
using Newtonsoft.Json;


namespace IMgzavri.Commands.Extensions
{
    //public static class FileStorageExtensions
    //{
    //    public static async Task<List<FileUploadResult>> UploadFiles(this IFileStorageClient client, List<FileSavingModel> fileSavingModels)
    //    {
    //        if (!fileSavingModels.Any())
    //            throw new DomainException("File model array is empty", ExceptionLevel.Fatal);

    //        var containsOne = fileSavingModels.Count == 1;

    //        var uploadResults = new List<FileUploadResult>();

    //        var parameterName = containsOne
    //            ? FileStorageConstants.UploadedFileResultName
    //            : FileStorageConstants.UploadedFilesResultName;

    //        var result = containsOne
    //            ? await client.UploadFileBytesAsync(fileSavingModels[0])
    //            : await client.UploadFilesBytesAsync(fileSavingModels);

    //        var uploadResult = result.Parameters[parameterName].ToString();

    //        if (string.IsNullOrWhiteSpace(uploadResult))
    //            throw new DomainException("Error occured during uploading files", ExceptionLevel.Fatal);

    //        if (containsOne)
    //        {
    //            uploadResults.Add(JsonConvert.DeserializeObject<FileUploadResult>(uploadResult));
    //        }
    //        else
    //        {
    //            var results = JsonConvert.DeserializeObject<List<FileUploadResult>>(uploadResult);

    //            uploadResults.AddRange(results);
    //        }

    //        return uploadResults;
    //    }

    //    public static async Task<FileUploadResult> UploadFile(this IFileStorageClient client, FileSavingModel fileSavingModel)
    //    {
    //        var fileUploadResult = await client.UploadFileBytesAsync(fileSavingModel);

    //        if (fileUploadResult.Status != ResultStatus.Success)
    //            throw new FileStorageException("File is not correctly uploaded", ExceptionLevel.Fatal);

    //        var uploadResult = JsonConvert.DeserializeObject<FileUploadResult>(fileUploadResult
    //            .Parameters[FileStorageConstants.UploadedFileResultName].ToString());

    //        return uploadResult;
    //    }
    //}
}
