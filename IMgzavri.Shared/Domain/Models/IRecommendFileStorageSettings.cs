using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Shared.Domain.Models
{
    public class IRecommendFileStorageSettings
    {
        public IMgzavriFileStorageSettingsConectionStrings ConnectionStrings { get; set; }
        public IRecommendFileStorageSettingsGlobalSettings GlobalSettings { get; set; }
    }

    public class IRecommendFileStorageSettingsGlobalSettings
    {
        public string Origin { get; set; }
        public string ApiUrl { get; set; }
        public string FileSystemBasePath { get; set; }
        public string MainFolderName { get; set; }
        public string FileServerRequestPath { get; set; }
        public string SupportedFormats { get; set; }
    }

    public class IMgzavriFileStorageSettingsConectionStrings
    {
        public string IMgzavriFileStorageDbContext { get; set; }
    }
}
