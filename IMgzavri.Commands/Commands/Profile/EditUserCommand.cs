using IMgzavri.Commands.Models.ResponceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Commands.Profile
{
    public record EditUserCommand(
        string Email,
        string FirstName,
        string LastName,
        string MobileNumber,
        string IdNumber,
        string NumberLicense,
        SaveFileModel Photo,
        List<SaveFileModel> IdNumberImages,
        List<SaveFileModel> DrivingLicenseImages
        ) : Command;
    
}
