using Rent.Domain.Entities;
using Rent.Infrastructure.Data;
using Rent.Domain.Interfaces.Repositories;

namespace Rent.Infrastructure.Repositories
{
    public class AuthenticationRepository : BaseRepository, IAuthenticationRepository
    {
        public AuthenticationRepository(DataContext context)
            : base(context) { }

        public async Task<RevokedToken> RevokeToken(RevokedToken revokedToken)
        {
            var newRevokedToken = new RevokedToken
            {
                Id = revokedToken.Id,
                ExpirationDate = revokedToken.ExpirationDate
            };

            await _context.RevokedTokens.AddAsync(newRevokedToken);
            await _context.SaveChangesAsync();

            return newRevokedToken;
        }
    }
}
