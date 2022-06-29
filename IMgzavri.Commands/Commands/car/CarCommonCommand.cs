using IMgzavri.Commands.Models.ResponceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Commands.car
{
    public record CreateCarCommand(
        int MarckId,
        int ModelId,
        SaveFileModel MainImage,
        List<SaveFileModel> Images
        ) : Command;

    public record UpdateCarCommand
        (
        Guid CarId,
        int MarckId,
        int ModelId,
        SaveFileModel MainImage,
        List<SaveFileModel> Images
        ):Command;

    public record DeleteCarCommand(
        Guid CarId
        ) : Command;

}
