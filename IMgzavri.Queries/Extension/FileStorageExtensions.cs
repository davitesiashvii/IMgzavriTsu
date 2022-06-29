
using IMgzavri.Queries.ViewModels;
using IMgzavri.Shared.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Extension
{
    //public static class FileStorageExtensions
    //{
    //    public static async Task<string> GetFileAsString(this IFileStorageClient client, Guid id)
    //    {
    //        var result = ((await client.LoadFileBytesAsync(id)).Parameters[FileStorageConstants.GetFileResultName]).ToString();

    //        return JsonConvert.DeserializeObject<FileViewModel>(result).File;
    //    }

    //    public static async Task<FileStoreLinkResult> GetFilePhysicalPath(this IFileStorageClient client, Guid id)
    //    {
    //        var resultAsString = ((await client.LoadFilePhysicalPathAsync(id)).Parameters[FileStorageConstants.GetFilePhysicalPathResultName]).ToString();

    //        return !string.IsNullOrWhiteSpace(resultAsString) ? JsonConvert.DeserializeObject<FileStoreLinkResult>(resultAsString) : null;
    //    }

    //    public static async Task<List<FileStoreLinkResult>> GetFilesPhysicalPaths(this IFileStorageClient client, List<Guid> ids)
    //    {
    //        var resultAsString = ((await client.LoadFilesPhysicalPathsAsync(ids)).Parameters[FileStorageConstants.GetFilesPhysicalPathsResultName]).ToString();

    //        return !string.IsNullOrWhiteSpace(resultAsString)
    //            ? JsonConvert.DeserializeObject<List<FileStoreLinkResult>>(resultAsString)
    //            : null;
    //    }
    //}
}
