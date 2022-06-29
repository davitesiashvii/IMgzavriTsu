using IMgzavri.Domain.FileStorage;
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Queries.Extension;
using IMgzavri.Queries.Queries.Profile;
using IMgzavri.Queries.ViewModels;
using IMgzavri.Queries.ViewModels.Profile;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace IMgzavri.Queries.Handlers.Profile
{
    public class GetUserInfoQueryHandler : QueryHandler<GetUserInfoQuery>
    {
        public GetUserInfoQueryHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth,fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(GetUserInfoQuery query, CancellationToken ct)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == query.UserId);

            if (user == null)
                return Result.Error("მომხმარებელი ვერ მოიძებნა");
            FileStoreLinkResult fmRes = null;
            try
            {
                fmRes =  FileStorage.GetFilePhysicalPath(user.PhotoId.Value);
            }
            catch { }
           

            var result = new Result();

            result.Response = new UserInfoVm
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MobileNumber = user.MobileNumber,
                IdNumber = user.IdNumber,
                NumberLicense = user.NumberLicense,
                VerifyUser = user.VerifyUser,
                FileLink = fmRes == null ? null : fmRes.Link
            };

            return result;
            
        }
    }
}
