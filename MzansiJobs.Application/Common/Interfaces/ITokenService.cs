using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Application.Common.Interfaces
{
    public interface ITokenService
    {

        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}
