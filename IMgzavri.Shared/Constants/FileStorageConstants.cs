namespace IMgzavri.Shared.Constants
{
    public static class FileStorageConstants
    {
        public static string UploadedFilesResultName => "fileIds";
        public static string UploadedFileResultName => "fileId";
        public static string GetFileResultName => "file";
        public static string GetFilePhysicalPathResultName => "filePath";
        public static string GetFilesPhysicalPathsResultName => "filePaths";

        public static string DownloadFileStreamParameterName => "stream";
        public static string DownloadFileTypeParameterName => "type";
        public static string DownloadFileNameParameterName => "name";
    }
}