using System.Threading.Tasks;
using BloggingServer.ResponseModels;

namespace BloggingServer.Services.Interfaces;

public interface IEmailService
{
    /// <summary>
    /// Send an email.
    /// </summary>
    /// <param name="emailMessage">Message object to be sent</param>
    Task Send(EmailMessageModel emailMessage);
}