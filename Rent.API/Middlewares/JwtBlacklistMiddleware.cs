using Rent.Domain.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Rent.API.Middlewares
{
    public class JwtBlacklistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public JwtBlacklistMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                // Extrair o token do header de autorização
                string? authorizationHeader = context.Request.Headers[
                    "Authorization"
                ].FirstOrDefault();

                if (
                    !string.IsNullOrEmpty(authorizationHeader)
                    && authorizationHeader.StartsWith("Bearer ")
                )
                {
                    string token = authorizationHeader.Substring("Bearer ".Length);

                    try
                    {
                        // Decodificar o token
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                        if (jwtToken != null)
                        {
                            var guidToken = jwtToken.Claims
                                .FirstOrDefault(x => x.Type == "jti")
                                ?.Value;

                            if (!string.IsNullOrEmpty(guidToken))
                            {
                                var authenticationService =
                                    scope.ServiceProvider.GetRequiredService<IAuthenticationService>();

                                // Verificar se o token está na blacklist
                                bool isTokenRevoked = await authenticationService.IsTokenRevoked(
                                    guidToken
                                );

                                if (isTokenRevoked)
                                {
                                    context.Response.StatusCode = 401;
                                    await context.Response.WriteAsync(
                                        "Token de autenticação revogado."
                                    );
                                    return;
                                }
                            }
                        }
                    }
                    catch
                    {
                        // Lidar com exceções de decodificação de token, se necessário
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync(
                            "Erro ao decodificar o token de autenticação."
                        );
                        return;
                    }
                }

                // Passar a requisição para o próximo middleware
                await _next(context);
            }
        }
    }
}
