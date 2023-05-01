using BBMS.Models;
using System.Threading.Tasks;

namespace BBMS.Service
{
    public interface IEmailService
    {
        Task SendEmailForEmailConfimation(UserEmailOptions userEmailOptions);
        Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailForAccountDeletion(UserEmailOptions userEmailOptions);
    }
}