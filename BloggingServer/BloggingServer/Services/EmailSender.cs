using System.Threading.Tasks;
using BloggingServer.ResponseModels;
using BloggingServer.Services.Interfaces;
using DataBaseLayout.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

public class EmailSender : IEmailSender, IEmailSender<User>
{
    private readonly IEmailService _emailService;

    public EmailSender(IEmailService emailService)
    {
        _emailService = emailService;
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        EmailMessageModel emailMessage = new(email,
            subject,
            htmlMessage);

        await _emailService.Send(emailMessage);
    }

    public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        EmailMessageModel emailMessage = new(email,
            "Email confirmation",
            confirmationLink);

        await _emailService.Send(emailMessage);
    }

    public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        EmailMessageModel emailMessage = new(email,
            "Reset password",
            resetLink);

        await _emailService.Send(emailMessage);
    }

    public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
        EmailMessageModel emailMessage = new(email,
            "Password reset code",
            resetCode);

        await _emailService.Send(emailMessage);
    }
}