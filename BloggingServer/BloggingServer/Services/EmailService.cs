using System.Threading.Tasks;
using BloggingServer.ResponseModels;
using BloggingServer.Services.Interfaces;
using FluentEmail.Core;
using Microsoft.Extensions.Logging;

namespace BloggingServer.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly IFluentEmailFactory _fluentEmailFactory;

    public EmailService(ILogger<EmailService> logger, IFluentEmailFactory fluentEmailFactory)
    {
        _logger = logger;
        _fluentEmailFactory = fluentEmailFactory;
    }

    public async Task Send(EmailMessageModel emailMessageModel)
    {
        _logger.LogInformation("Sending email");
        await _fluentEmailFactory.Create().To(emailMessageModel.ToAddress)
            .Subject(emailMessageModel.Subject)
            .Body(emailMessageModel.Body, false) // true means this is an HTML format message
            .SendAsync();
    }
}