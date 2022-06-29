using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Shared.Domain.Models
{
    public class IRecommendSettings
    {
        public IMgzavriConectionStrings ConnectionStrings { get; set; }
        public IRecommendGlobalSettings GlobalSettings { get; set; }
    }

    public class IRecommendGlobalSettings
    {
        public string Origin { get; set; }
        public string ApiUrl { get; set; }
        public string FileStorageUrl { get; set; }
        public string FileStorageClientName { get; set; }
        public string DinkToPdfLibraryPath { get; set; }
    }

    public class IMgzavriConectionStrings
    {
        public string IMgzavriDbContext { get; set; }
    }
}
