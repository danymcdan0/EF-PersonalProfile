using Microsoft.AspNetCore.Identity;
using PersonalProfileAPI.Models.Domains;

namespace NZWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(Owner user, List<string> roles);
    }
}
