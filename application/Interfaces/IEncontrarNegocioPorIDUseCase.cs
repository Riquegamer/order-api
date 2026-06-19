using application.DTOs.Entrada;
using application.DTOs.Saida;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.Interfaces
{
    public interface IEncontrarNegocioPorIDUseCase
    {
        Task<NegocioResponse?> ExecuteAsync(EncontrarNegocioRequest request);
    }
}
