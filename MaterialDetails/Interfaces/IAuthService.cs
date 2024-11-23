using MaterialDetails.Models;
using MaterialDetails.ViewModels;

namespace MaterialDetails.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(UserSignupModel userViewModel);
        Task<string> LoginAsync(string username, string password);
    }
}
