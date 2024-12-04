using Fora.Challenge.Application.Models;

namespace Fora.Challenge.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
