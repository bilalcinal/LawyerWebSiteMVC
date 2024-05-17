using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Utilities;

namespace LawyerWebSiteMVC.Interface;

public interface IAppUserService
{
    Task<IDataResult<AppUser>> SignInAsync(AppUser appUser);
}
