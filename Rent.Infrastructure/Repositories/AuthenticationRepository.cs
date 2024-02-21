using Microsoft.EntityFrameworkCore;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Infrastructure.Data;

namespace Rent.Infrastructure.Repositories
{
    public class AuthenticationRepository : BaseRepository<RevokedToken>, IAuthenticationRepository
    {
        public AuthenticationRepository(DataContext context)
            : base(context) { }

        public async Task<bool> IsTokenRevoked(string token)
        {
            RevokedToken? revokedToken = await _context
                .RevokedTokens.Where(x => x.TokenId == token)
                .FirstOrDefaultAsync();

            if (revokedToken == null)
                return false;

            return true;
        }

        public async Task<RevokedToken> RevokeToken(RevokedToken revokedToken)
        {
            var newRevokedToken = new RevokedToken
            {
                TokenId = revokedToken.TokenId,
                ExpirationDate = revokedToken.ExpirationDate
            };

            await _context.RevokedTokens.AddAsync(newRevokedToken);
            await _context.SaveChangesAsync();

            return newRevokedToken;
        }
    }
}
