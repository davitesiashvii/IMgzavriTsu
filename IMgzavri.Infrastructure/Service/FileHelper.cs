

namespace IMgzavri.Infrastructure.Service
{
    public static class FileHelper
    {
        public static bool Exists(string path) => System.IO.File.Exists(path);

        public static string BuildPath(IMgzavri.Domain.Models.File file, string basePath = "D:\\IMgzavri", string mainFolder = "Files")
        {
            string path = Path.Combine(basePath + $"\\{mainFolder}\\");

            path = EnsureDirectoryCreation(path, file.CreateDate);

            return Path.Combine(path, Path.GetFileNameWithoutExtension(file.Name) + file.Extension);
        }

        public static string BuildPathForFileServer(IMgzavri.Domain.Models.File file, string requestPath = "/Files", string apiUrl = "https://localhost:5011")
        {
            var day = file.CreateDate.Day.ToString();
            var month = file.CreateDate.Month.ToString();
            var year = file.CreateDate.Year.ToString();

            string path = string.Join('/', apiUrl + requestPath, year, month, day, file.Name);

            return path;
        }

        private static string EnsureDirectoryCreation(string path, DateTimeOffset creationDate)
        {
            var day = creationDate.Day.ToString();
            var month = creationDate.Month.ToString();
            var year = creationDate.Year.ToString();

            var combined = Path.Combine(path, $"{year}\\", $"{month}\\", $"{day}\\");

            if (!Directory.Exists(combined))
                Directory.CreateDirectory(combined);

            return combined;
        }
    }
}
