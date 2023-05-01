using BBMS.Data;
using BBMS.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBMS.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel model);
        Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(SignInModel signInModel);
        Task GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task GenerateForgotPasswordTokenAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task SignOutAsync();
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model);

        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);

        Task SendEmailforUserDeletion(string id);
        List<ApplicationUser> GetPetientUserDetails();
        List<ApplicationUser> GetDonorUserDetails();


    }
}