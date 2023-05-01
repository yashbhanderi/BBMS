using BBMS.Data;
using BBMS.Models;
using BBMS.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly BloodBankDbContext _context;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            IUserService userService, IEmailService emailService, IConfiguration configuration, BloodBankDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
            _context = context;
        }
        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel model)
        {
            var user = new ApplicationUser()
            {
                Fname = model.Fname,
                Lname = model.Lname,
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.RoleName);
                await GenerateEmailConfirmationTokenAsync(user);
            }
            return result;
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
            return result;
        }

        public async Task GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfimationEmail(user, token);
            }
        }

        public async Task GenerateForgotPasswordTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgotPasswordEmail(user, token);
            }
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }


        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            return await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(model.UserId), model.Token, model.NewPassword);
        }

        private async Task SendEmailConfimationEmail(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string emailConfirmation = _configuration.GetSection("Application:EmailConfimation").Value;
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmail = new List<string> { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}",user.Fname),
                    new KeyValuePair<string, string>("{{Link}}",string.Format(appDomain+emailConfirmation,user.Id,token))
                }
            };
            await _emailService.SendEmailForEmailConfimation(options);

        }

        private async Task SendForgotPasswordEmail(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:ForgotPassword").Value;
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmail = new List<string> { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}",user.Fname),
                    new KeyValuePair<string, string>("{{Link}}",string.Format(appDomain+confirmationLink,user.Id,token))
                }
            };
            await _emailService.SendEmailForForgotPassword(options);

        }

        public List<ApplicationUser> GetPetientUserDetails()
        {
            List<string> userids =  _context.UserRoles.Where(a => a.RoleId == "1").Select(b => b.UserId).Distinct().ToList();

            List<ApplicationUser> listUsers = _context.Users.Where(a => userids.Any(c => c == a.Id)).ToList();

            return listUsers;
        }
        public List<ApplicationUser> GetDonorUserDetails()
        {
            List<string> userids = _context.UserRoles.Where(a => a.RoleId == "2").Select(b => b.UserId).Distinct().ToList();

            List<ApplicationUser> listUsers = _context.Users.Where(a => userids.Any(c => c == a.Id)).ToList();

            return listUsers;
        }

        public async Task SendEmailforUserDeletion(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
 
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmail = new List<string> { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}",user.Fname)
                }
            };
            await _emailService.SendEmailForAccountDeletion(options);
        }

    }
}
