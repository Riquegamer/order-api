using System;
using System.Collections.Generic;
using System.Text;

namespace domain.Ports
{
    public interface IServicoSeguranca
    {
        string HashSenha(string senha);
        bool VerificarSenha(string senha, string hash);
        string GerarToken(Guid negocioId, string email);
    }
}
