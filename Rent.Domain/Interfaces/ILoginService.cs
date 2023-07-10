using Rent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Domain.Interfaces
{
    public interface ILoginService
    {
        //Task<Login> Authenticate(string email, string password);
        Task<TokenResponse> Authenticate(string email, string password);
    }
}
