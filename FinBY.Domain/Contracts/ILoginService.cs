using FinBY.Domain.Entities;

namespace FinBY.Domain.Contracts
{
    public interface ILoginService
    {
        Token ValidateCredentials(string userName, string password);

        Token ValidateCredentials(Token token);

        bool RevokeToken(string userName);
    }
}
