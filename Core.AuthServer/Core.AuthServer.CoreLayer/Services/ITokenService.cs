using Core.AuthServer.CoreLayer.Configuration;
using Core.AuthServer.CoreLayer.DTOs;
using Core.AuthServer.CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AuthServer.CoreLayer.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp userApp);
        ClientTokenDto CreateTokenByClient(Client client);
    }
}
