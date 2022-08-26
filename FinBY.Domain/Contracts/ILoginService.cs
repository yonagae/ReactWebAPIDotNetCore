using FinBY.Domain.Data.DTO;
using FinBY.Domain.Entities;

namespace FinBY.Domain.Contracts
{
    public interface ILoginService
    {
        TokenDTO ValidateCredentials(UserLoginDTO user);

        TokenDTO ValidateCredentials(TokenDTO token);

        bool RevokeToken(string userName);
    }
}
