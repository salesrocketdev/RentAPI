using Rent.Domain.DTO.Request;
using Swashbuckle.AspNetCore.Filters;

namespace Rent.API.SwaggerExamples
{
    public class LoginRequestExample : IExamplesProvider<AuthenticateDTO>
    {
        public AuthenticateDTO GetExamples()
        {
            return new AuthenticateDTO 
            {
                Email = "admin@email.com", 
                Password = "admin" 
            };
        }
    }
}
